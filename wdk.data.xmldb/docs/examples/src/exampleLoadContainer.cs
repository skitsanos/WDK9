/*
 * Berkeley DB XML .NET API
 *
 * (c) 2005 Parthenon Computing Ltd
 *
 * Written by John Snelson
 *
 * $Id: exampleLoadContainer.cs,v 1.3 2005/04/04 10:01:12 jpcs Exp $
 *
 */

using Sleepycat.Db;
using Sleepycat.DbXml;

using System.Collections;
using System.IO;

public class AddIndex 
{

	public static void Main(string[] args) 
	{
		IDictionary options = parseArguments(args, "h:p:");

		string envdir = (string)options['h'];
		string filePath = (string)options['p'];
		if(envdir == null || filePath == null) Usage();

		DirectoryInfo fileDir = new DirectoryInfo(filePath);
		if(!fileDir.Exists) 
		{
			System.Console.WriteLine("File path directory does not exist.");
			Usage();
		}

		DirectoryInfo nsDataDir = GetSubDirectory(fileDir, "nsData");
		DirectoryInfo simpleDataDir = GetSubDirectory(fileDir, "simpleData");

		try 
		{
			// Open an environment and manager
			using(Manager mgr = CreateManager(envdir)) 
			{

				LoadFiles(mgr, "namespaceExampleData.dbxml",
					nsDataDir.GetFiles("*.xml"));
				LoadFiles(mgr, "simpleExampleData.dbxml",
					simpleDataDir.GetFiles("*.xml"));
			}
		}
		catch(DbXmlException e) 
		{
			System.Console.WriteLine("Error in exampleLoadContainer");
			System.Console.WriteLine(e.ToString());
		}
	}

	public static void LoadFiles(Manager mgr, string containerName, FileInfo[] files) 
	{
		// Open a transactional container
		ContainerConfig containerconfig = new ContainerConfig();
		containerconfig.Create = true;
		containerconfig.Transactional = true;
		using(Container container = mgr.OpenContainer(null, containerName,
				  containerconfig)) 
		{

			// Create an update context
			using(UpdateContext uc = mgr.CreateUpdateContext()) 
			{

				// Start a transaction
				using(Transaction txn = mgr.CreateTransaction()) 
				{

					DocumentConfig docconfig = new DocumentConfig();

					foreach(FileInfo file in files) 
					{
						using(FileStream stream = file.OpenRead()) 
						{
            
							// Create a document
							using(Document doc = mgr.CreateDocument()) 
							{

								doc.Name = file.Name;
								doc.StreamContent = mgr.CreateInputStream(stream);
								doc.SetMetaData(new MetaData("http://dbxmlExamples/timestamp",
									"timeStamp",
									new Value(System.DateTime.Now)));
								container.PutDocument(txn, doc, uc, docconfig);
								System.Console.WriteLine("Added " + file.Name + " to container " +
									containerName);
							}
						}
					}

					// Commit the index adds
					txn.Commit();
				}
			}
		}
	}

	public static DirectoryInfo GetSubDirectory(DirectoryInfo fileDir, string subdir) 
	{
		DirectoryInfo[] dirs = fileDir.GetDirectories(subdir);
		if(dirs.Length < 1) 
		{
			System.Console.WriteLine(subdir + " subdirectory does not exist.");
			Usage();      
		}
		return dirs[0];
	}

	public static Manager CreateManager(string envdir) 
	{
		EnvironmentConfig envconf = new EnvironmentConfig();
		envconf.CacheSize = 50 * 1024 * 1024;
		envconf.Create = true;
		envconf.InitializeCache = true;
		envconf.Transactional = true;
		envconf.InitializeLocking = true;
		envconf.InitializeLogging = true;
		envconf.Recover = true;

		ManagerConfig mgrconfig = new ManagerConfig();
		mgrconfig.AdoptEnvironment = true;

		Environment env = new Environment(envdir, envconf);
		try 
		{
			return new Manager(env, mgrconfig);
		}
		catch(System.Exception e) 
		{
			env.Dispose();
			throw e;
		}
	}

	private static void Usage() 
	{
		System.Console.WriteLine("This program loads the examples XML data into the examples container.");
		System.Console.WriteLine("Provide the directory where you want to place your database environment, ");
		System.Console.WriteLine("and the path to the xmlData directory (this exists in your DB XML examples");
		System.Console.WriteLine("directory).");
		System.Console.WriteLine();
		System.Console.WriteLine("\t-h <dbenv directory> -p <filepath>");	
		System.Console.WriteLine("For example:");
		System.Console.WriteLine("\texampleLoadContainer.exe -h examplesEnvironment -p /home/user1/dbxml-1.1.0/examples/xmlData");

		System.Environment.Exit(-1);
	}

	private static IDictionary parseArguments(string[] args, string options) 
	{
		IDictionary result = new Hashtable();
		for(int i = 0; i < args.Length; ++i) 
		{
			string arg = args[i];
			if((arg.StartsWith("-")
#if WIN32
				|| arg.StartsWith("/")
#endif
				) && arg.Length > 1) 
			{

				int index = options.IndexOf(arg[1]);
				if(index == -1) 
				{
					System.Console.WriteLine("Unknown option: " + arg);
					Usage();
					break;
				}
				else 
				{
					++index;
					if(index < options.Length && options[index] == ':') 
					{
						if(arg.Length > 2 && (arg[2] == '='
#if WIN32
							|| arg[2] == ':'
#endif
							)) 
						{
							result[arg[1]] = arg.Substring(3);
						}
						else if(arg.Length > 2) 
						{
							System.Console.WriteLine("Unknown option: " + arg);
							Usage();
						}
						else 
						{
							++i;
							if(i >= args.Length) 
							{
								System.Console.WriteLine("Invalid option: " + arg);
								Usage();
							}
							result[arg[1]] = args[i];
						}
					}
					else 
					{
						result[arg[1]] = "true";
					}
				}
			}
			else 
			{
				System.Console.WriteLine("Too many arguments: " + arg);
				Usage();
			}
		}
		return result;
	}

}
