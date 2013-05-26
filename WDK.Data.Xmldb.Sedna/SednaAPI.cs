using System;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization;
using System.IO;
using System.Text;

namespace WDK.Data.Xmldb.Sedna
{
	/// <summary>
	/// Contains an enumeration of all the Sedna instructions.
	/// </summary>
	public enum InstructionCode : int
	{
		BulkLoadPortion = 410, BulkLoadEnd = 420, BulkLoadSucceeded = 440, UpdateSucceeded = 340, UpdateFailed = 350, BulkLoadFailed = 450, GeneralError = 100, BulkLoadError = 400, ItemEnd = 370, ResultEnd = 375, ItemPart = 360, StartUp = 110, Login = 120, AuthenticationResponse = 130, SendSessionParameters = 140, SendAuthenticationParameters = 150, AuthenticationOK = 160, AuthenticationFailed = 170, BeginTransaction = 210, CommitTransaction = 220, RollbackTransaction = 225, BeginTransactionOk = 230, BeginTransactionFailed = 240, CloseConnection = 500, TransactionRollback = 520, ConnectionClosed = 510, CommitSucceeded = 250, CommitFailed = 260, Execute = 300, QuerySucceeded = 320, QueryFailed = 320, BulkLoadFileRequest = 430, BulkLoadStreamRequest = 431, GetNextItem = 310
	}

	/// <summary>
	/// The base exception for all Sedna errors.
	/// </summary>
	public class SednaException : System.Exception
	{
		public InstructionCode ReturnCode;

		public SednaException(InstructionCode retCode, string message) : base(message)
		{
			ReturnCode = retCode;
		}

		public SednaException(string message) : base(message) { }
	}
		
	/// <summary>
	/// Encapsulates all network access and protocol specifics.
	/// </summary>
	internal class NetworkOperations
	{
		public const int NetworkTimeout = 7; // timeout in seconds
		public const int SocketMessageBufferSize = 10240;
		public const int BulkLoadPortionSize = 5120;
		public const int IntSize = 4;
		public const int StringTypeSize = 1;
		public const int ProtocolVersion = 1;
		static readonly byte[] int_array = new byte[IntSize];

		internal class Message
		{
			public InstructionCode Instruction;
			public byte[] Body;
			
			public Message()
			{
				Body = new byte[SocketMessageBufferSize];	
			}
		}	

		internal class StringItem
		{
			public string Item;
			public bool HasNextItem;
		}

		/// <summary>
		/// Performs a synchronous network read with a timeout. 
		/// </summary>
		static void ReadWithTimeout(NetworkStream stream, byte[] body, int pos, int len)
		{
			int wait = 100; // wait this number of ms between tries
			int timeoutStart = (1000/wait) * NetworkTimeout; // timeout after n seconds waiting.
			
			int timeout = timeoutStart;
			while (pos < len && timeout > 0)
			{
				int count = 0;
				if (stream.DataAvailable)
				{
					count += stream.Read(body, pos, len - pos);
					timeout = timeoutStart;
				}
				else
				{
					System.Threading.Thread.Sleep(wait);
					timeout--;
				}
				pos += count;
			}
			if (timeout==0) throw new SednaException("Timeout waiting for reply from server");
		}
		
		public static void ReadMessage(Message msg, NetworkStream bufInputStream)
		{
			msg.Instruction = (InstructionCode)NetworkOperations.ReadInt(bufInputStream);
			int len = NetworkOperations.ReadInt(bufInputStream);
			msg.Body = new byte[len];

			if (len!=0)
				ReadWithTimeout(bufInputStream, msg.Body, 0, len);
		}

		public static void WriteMessage(InstructionCode instruction, NetworkStream outputStream, NetworkStream inputStream, params InstructionCode[] okResponses)
		{
			WriteMessage(instruction, outputStream, inputStream, new byte[0], okResponses);
		}
	
		public static void WriteMessage(InstructionCode instruction, NetworkStream outputStream, NetworkStream inputStream, string[] textItems, params InstructionCode[] okResponses)
		{
			MemoryStream buffer = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(buffer);
			WriteString(writer, textItems);
			writer.Close();
			WriteMessage(instruction, outputStream, inputStream, buffer.ToArray(), okResponses);
		}

		public static void WriteMessage(InstructionCode instruction, NetworkStream outputStream, NetworkStream inputStream, byte[] body, params InstructionCode[] okResponses)
		{
			Message msg = new Message();
			msg.Instruction = instruction;
			msg.Body = body;
			WriteMessage(msg, outputStream);
			ReadMessage(msg, inputStream);
			
			bool ok = false;
			foreach (InstructionCode response in okResponses)
				if (msg.Instruction==response) ok = true;

			if (!ok)
				throw GetErrorInfo(msg.Instruction, msg.Body);
		}

		public static void WriteMessage(Message msg, NetworkStream outputStream)
		{
			if (msg.Body.Length > SocketMessageBufferSize) throw new IndexOutOfRangeException("Size of packet exceeds maximum buffer size");
			NetworkOperations.WriteInt((int)msg.Instruction, outputStream);
			NetworkOperations.WriteInt((int)msg.Body.Length, outputStream);
			outputStream.Write(msg.Body, 0, msg.Body.Length);
			outputStream.Flush();
		}

		public static bool BulkLoad(Stream input, NetworkStream bufInputStream, NetworkStream outputStream) 
		{
			Message msg = new Message();
			int bytes_read = -1;
        
			try
			{
				while(bytes_read != 0)
				{
					byte[] buffer = new byte[BulkLoadPortionSize];
					bytes_read = input.Read(buffer, 0, BulkLoadPortionSize);
					if (bytes_read != 0)   
					{ 	
						// crop the array to the correct size
						msg.Body = new byte[bytes_read + IntSize + StringTypeSize];
						Array.Copy(buffer, 0, msg.Body, IntSize + StringTypeSize, bytes_read);

						msg.Instruction = InstructionCode.BulkLoadPortion;
						msg.Body[0] = 0;

						NetworkOperations.WriteInt(bytes_read, msg.Body, 0 + StringTypeSize);
						NetworkOperations.WriteMessage(msg, outputStream);
					}
           
				} 

				NetworkOperations.WriteMessage(InstructionCode.BulkLoadEnd, outputStream, bufInputStream, InstructionCode.BulkLoadSucceeded, InstructionCode.UpdateSucceeded);

				return true;
			}
			catch(IOException e)
			{
				msg.Instruction = InstructionCode.BulkLoadError;
				try
				{
					NetworkOperations.WriteMessage(msg, outputStream);
					NetworkOperations.ReadMessage(msg, bufInputStream);
				}
				catch(IOException ex)
				{
					throw new SednaException(InstructionCode.BulkLoadFailed, "Unable to bulk load: IO error: " + ex.Message);
				}
   	       
				throw new SednaException(InstructionCode.BulkLoadFailed, NetworkOperations.GetErrorInfo(msg.Instruction, msg.Body) + " : " + e.Message);
			}

		}

		/// <summary>
		/// Writes out a string (or series of strings) in sedna format.
		/// </summary>
		public static void WriteString(BinaryWriter writer, params string[] textItems)
		{
			foreach (string text in textItems)
			{
				writer.Write((byte)0);
				NetworkOperations.WriteInt(text.Length, int_array, 0);
				writer.Write(int_array);
				writer.Write(Encoding.ASCII.GetBytes(text));
			}
		}
		
		public static string ReadString(Message msg, int offset)
		{
			int messageOffset = /* buffer length */ offset  + StringTypeSize + /* string size */ IntSize;
			return System.Text.Encoding.ASCII.GetString(msg.Body, messageOffset, msg.Body.Length-messageOffset);
		}
		
		public static StringItem ReadStringItem(NetworkStream inputStream) 
		{
			Message msg = new Message();
			StringItem sitem = new StringItem();
			StringBuilder strBuf = new StringBuilder();
   	
			NetworkOperations.ReadMessage(msg, inputStream);
			if ((msg.Instruction == InstructionCode.ItemEnd) || (msg.Instruction == InstructionCode.ResultEnd)) 
			{   	 
				sitem.HasNextItem = false;
				sitem.Item = null;
				return sitem;
			}
			
			while((msg.Instruction != InstructionCode.ItemEnd) && (msg.Instruction != InstructionCode.ResultEnd))
			{
				if (msg.Instruction == InstructionCode.GeneralError) 
					throw NetworkOperations.GetErrorInfo(msg.Instruction, msg.Body);
   	
				if (msg.Instruction == InstructionCode.ItemPart) 
					strBuf.Append(Encoding.ASCII.GetString(msg.Body, (IntSize + StringTypeSize), msg.Body.Length-(IntSize + StringTypeSize)));

				NetworkOperations.ReadMessage(msg, inputStream);
			}
			
			if (msg.Instruction == InstructionCode.ResultEnd) sitem.HasNextItem = false;
				else if (msg.Instruction == InstructionCode.ItemEnd) sitem.HasNextItem = true;
			
			sitem.Item = strBuf.ToString();
			
			return sitem;
		}

		public static Exception GetErrorInfo(InstructionCode response, byte [] body)
		{
			if (body.Length < IntSize) return new SednaException(response, "General error");

			Array.Copy(int_array, 0, body, IntSize + 1, IntSize);
			int length = ReadInt(int_array, 0);
    	
			return new SednaException(response, System.Text.Encoding.ASCII.GetString(body, IntSize + IntSize + StringTypeSize, length - (IntSize + IntSize + StringTypeSize)).Replace("\n", Environment.NewLine));
		}

		public static void WriteInt(int i, NetworkStream bufOutputStream) 
		{
      		byte[] buffer = new byte[IntSize];
			WriteInt(i, buffer, 0);
			bufOutputStream.Write(buffer, 0, buffer.Length);
		}
	    
		public static void WriteInt(int i, byte[] byte_array, int pos)
		{
			// we could use BinaryWriter here but this is faster.
			byte_array[pos] =(byte)( 0xff & (i >> 24)); byte_array[pos+1] = (byte)(0xff & (i >> 16)); byte_array[pos+2] = (byte)(0xff & (i >> 8)); byte_array[pos+3] = (byte)(0xff & i);
		}

		public static int ReadInt(byte[] int_array, int index)
		{
			// we could use BinaryReader here but this is faster.
			return  (((int_array[index + 0] & 0xff) << 24) | ((int_array[index + 1] & 0xff) << 16) | ((int_array[index + 2] & 0xff) << 8) | (int_array[index + 3] & 0xff));
		}
		
		public static int ReadInt(NetworkStream bufInputStream) 
		{
			ReadWithTimeout(bufInputStream, int_array, 0, IntSize);
			return ReadInt(int_array, 0);
		}
	}
	
	/// <summary>
	/// Represents a connection to the server.
	/// </summary>
	public class SednaSession
	{
		internal Socket socket;
		internal NetworkStream outputStream;
		internal NetworkStream inputStream;
		private bool isClosed;
		private bool inTransaction = false;
		private bool haveUpdated = false;
		public const int DefaultPort = 5050;
		private string currentDatabase = null;
		
		private SednaSession(string dbname, string host, int port, string userName, string password)
		{
			currentDatabase = dbname;
			try
			{
				socket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
				socket.Connect(new IPEndPoint( Dns.GetHostEntry(host).AddressList[0], port ) );
			}
			catch (IOException ex)
			{
				throw new SednaException("No connection could be made to that host: " + ex.Message);
			}
			
			outputStream = new NetworkStream(socket, FileAccess.Write);
			inputStream = new NetworkStream(socket, FileAccess.Read);
			// send startup message.
			NetworkOperations.WriteMessage(InstructionCode.StartUp, outputStream, inputStream, InstructionCode.SendSessionParameters);
			// send parameters.
			NetworkOperations.Message msg = CreateParametersMessage(dbname, userName, password);
			NetworkOperations.WriteMessage(msg, outputStream);
			
			// get the response...
			NetworkOperations.ReadMessage(msg, inputStream);
			// need to send the password
			if (msg.Instruction == InstructionCode.SendAuthenticationParameters)
				NetworkOperations.WriteMessage(InstructionCode.AuthenticationResponse, outputStream, inputStream, new string[] { password }, InstructionCode.AuthenticationOK);
			else if (msg.Instruction == InstructionCode.AuthenticationFailed || msg.Instruction == InstructionCode.GeneralError)
				throw new SednaException("Authentication failed");
		}

		private NetworkOperations.Message CreateParametersMessage(string dbname, string userName, string password)
		{
			NetworkOperations.Message msg = new NetworkOperations.Message();
			MemoryStream stream = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(stream);
			msg.Instruction = InstructionCode.Login;
			writer.Write((byte)1); // protocol version... 1.0
			writer.Write((byte)0);
			NetworkOperations.WriteString(writer, userName, dbname);
			writer.Close();
			msg.Body = stream.ToArray();
			return msg;			
		}
		
		public static SednaSession CreateSession(string host, int port, string dbName, string userName, string password)
		{
			return new SednaSession(dbName, host, port, userName, password);
		}

		public static SednaSession CreateSession(string host, string dbName, string userName, string password)
		{
			return CreateSession(host, DefaultPort, dbName, userName, password);
		}

		public bool IsClosed
		{
			get
			{
				return isClosed;
			}
		}

		/// <summary>
		/// Begins a new transaction manually
		/// </summary>
		public void BeginTransaction()
		{
			NetworkOperations.WriteMessage(InstructionCode.BeginTransaction, outputStream, inputStream, InstructionCode.BeginTransactionOk);
			inTransaction = true;
			haveUpdated = false;
		}

		/// <summary>
		/// Terminates a session, rolling back any transactions that were not committed.
		/// </summary>
		public void Close()
		{
			// for reason the server rejects this message.
			NetworkOperations.WriteMessage(InstructionCode.CloseConnection, outputStream, inputStream, InstructionCode.ConnectionClosed, InstructionCode.TransactionRollback);
			inputStream.Close();
			outputStream.Close();
			isClosed = true;
		}

		public void RollbackTransaction()
		{
			inTransaction = false;
			haveUpdated = false;
			NetworkOperations.WriteMessage(InstructionCode.RollbackTransaction, outputStream, inputStream, InstructionCode.TransactionRollback);
		}
		
		/// <summary>
		/// Commits the current transaction
		/// </summary>
		public void CommitTransaction()
		{
			inTransaction = false;
			haveUpdated = false;
			NetworkOperations.WriteMessage(InstructionCode.CommitTransaction, outputStream, inputStream, InstructionCode.CommitSucceeded);
		}

		
		/// <summary>
		/// The CreateDocument statement creates a new standalone document
		/// </summary>
		/// <param name="name"></param>
		public void CreateDocument(string name)
		{
			Execute("CREATE DOCUMENT " + "\"" + name + "\"");			
		}

		public void CreateDocument(string name, string collection)
		{
			Execute("CREATE DOCUMENT " + "\"" + name + "\" IN COLLECTION " + "\"" + collection + "\"");
		}

		/// <summary>
		/// The DropDocument statement drops standalone document
		/// </summary>
		/// <param name="name"></param>
		public void DropDocument(string name)
		{
			Execute("DROP DOCUMENT " + "\"" + name + "\"");						
		}

		public void DropDocument(string name, string collection)
		{
			Execute("DROP DOCUMENT " + "\"" + name + "\" IN COLLECTION " + "\"" + collection + "\"");
		}

		/// <summary>
		/// The CREATE COLLECTION statement creates a new collection with the name that is the result of <i>name</i>
		/// </summary>
		/// <param name="name"></param>
		/// <remarks>
		/// To access a single document from collection in an XQuery or XML update query, 
		/// the document function accepts a collection name as its second optional argument<br/>
		/// document(doc-name-expr, coll-name-expr)<p/>
		/// The function collection can be called from any place within an XQuery or 
		/// XML update query where the function call is allowed. The collection function 
		/// returns the sequence of all documents that belong to the collection named coll-name-expr<p/>
		/// collection(coll-name-expr)
		/// </remarks>
		public void CreateCollection(string name)
		{
			Execute("CREATE COLLECTION " + "\"" + name + "\"");
		}
		/// <summary>
		/// The RenameCollection statement renames collection with the name 
		/// that is result of the old-name-expr. 
		/// The new name is assigned which is result of the new-name-expr.
		/// </summary>
		/// <param name="oldName"></param>
		/// <param name="newName"></param>
		public void RenameCollection(string oldName, string newName)
		{
			Execute("RENAME COLLECTION " + "\"" + oldName + "\" INTO " + "\"" + newName + "\"");
		}

		/// <summary>
		/// Loads a document into a database.
		/// </summary>
		public void Load(string filename, string documentName)
		{
			Execute("LOAD \"" + filename + "\" " + "\"" + documentName + "\"");
		}

		public void Load(string filename, string documentName, string collectionName)
		{
			Execute("LOAD \"" + filename + "\" " + "\"" + documentName + "\" "+ "\"" + collectionName + "\"");
		}

		/**
		 * Loads document source into database
		 */ 
		public void LoadXml(string source, string documentName)
		{
			Execute("");
		}
		
		/// <summary>
		/// Indicates whether a transaction is in progress but has not yet been committed.
		/// </summary>
		public bool InTransaction
		{
			get
			{
				// inTransaction is only interesting if we've committed updates.
				// so we check this against haveUpdated too.
				return inTransaction && haveUpdated;
			}
		}
		
		/// <summary>
		/// Executes a query. If there are no results, null is returned.
		/// </summary>
		public QueryResult Execute(string query)
		{
			StringReader reader = new StringReader(query);
			return Execute(reader);
		}	

		/// <summary>
		/// Executes a query that is read from a stream.
		/// </summary>
		public QueryResult Execute(TextReader queryReader)
		{
			try
			{
				if (!inTransaction) BeginTransaction();
			
				string query = queryReader.ReadToEnd();
				NetworkOperations.Message msg = new NetworkOperations.Message();
				msg.Instruction = InstructionCode.Execute;
			
				MemoryStream stream = new MemoryStream();
				BinaryWriter writer = new BinaryWriter(stream);
				writer.Write((byte)0); // xml is 0, scheme xml is 1
				NetworkOperations.WriteString(writer, query);
				writer.Close();

				msg.Body = stream.ToArray();
			
				NetworkOperations.WriteMessage(msg, outputStream);

				NetworkOperations.ReadMessage(msg, inputStream);

				if (msg.Instruction==InstructionCode.QuerySucceeded)
					return new QueryResult(NetworkOperations.ReadStringItem(inputStream), inputStream, outputStream);

				else if (msg.Instruction==InstructionCode.QueryFailed || msg.Instruction==InstructionCode.UpdateFailed || msg.Instruction==InstructionCode.GeneralError)
					throw NetworkOperations.GetErrorInfo(msg.Instruction, msg.Body);

				else if (msg.Instruction==InstructionCode.UpdateSucceeded)
				{
					haveUpdated = true;
					return null;
				}

				else if (msg.Instruction==InstructionCode.BulkLoadFileRequest)
				{
					try
					{
						haveUpdated = true;
						string filename = NetworkOperations.ReadString(msg, 0);
						Stream fileStream = File.OpenRead(filename);
						if (!NetworkOperations.BulkLoad(fileStream, inputStream, outputStream))
							throw new SednaException(InstructionCode.BulkLoadFailed, "Bulk load failed");
						return null;
					}
					finally
					{
						stream.Close();
					}
				}
				else if (msg.Instruction==InstructionCode.BulkLoadStreamRequest)
				{
					if (!NetworkOperations.BulkLoad(Console.OpenStandardInput(), inputStream, outputStream))
						throw new SednaException("Bulk load failed");
					return null;
				}
				else
					throw new SednaException("Unexpected error");
			}
			catch (Exception ex)
			{
				inTransaction = false;
				haveUpdated = false;
				throw ex;
			}
		}
	
	}

	/// <summary>
	/// Represents the result of a query. May only contain the partial result at any one time.
	/// </summary>
	public class QueryResult
	{
		string text;
		bool hasNextItem;
		NetworkStream inputStream;
		NetworkStream outputStream;

		internal QueryResult(NetworkOperations.StringItem item, NetworkStream inputStream, NetworkStream outputStream)
		{
			this.inputStream = inputStream;
			this.outputStream = outputStream;
			text = item.Item;
			hasNextItem = item.HasNextItem;
		}

		public string Value { get { return text==null ? "" : text.Replace("\n", "\r\n"); } }

		/// <summary>
		/// Indicates whether there are more batches in the result waiting to download.
		/// </summary>
		public bool HasNext { get { return hasNextItem; } }

		/// <summary>
		/// Retrieves the next part of the result.
		/// </summary>
		/// <returns></returns>
		public string Next()
		{
			if (!hasNextItem) return null;

			NetworkOperations.Message msg = new NetworkOperations.Message();
			msg.Instruction = InstructionCode.GetNextItem;
			NetworkOperations.WriteMessage(msg, outputStream);
			 
			NetworkOperations.StringItem item = NetworkOperations.ReadStringItem(inputStream);
			if (item.Item==null)
			{
				hasNextItem = false;
				return null;
			}
			else
			{
				text += item.Item;
				return item.Item;
			}
		}

		public void Next(TextWriter writer)
		{
			writer.Write(Next());
		}

		/// <summary>
		/// Synchronously retrieves the entire result.
		/// </summary>
		/// <returns></returns>
		public string GetCompleteResult()
		{
			while (HasNext) Next();
			return Value;
		}

	}
}