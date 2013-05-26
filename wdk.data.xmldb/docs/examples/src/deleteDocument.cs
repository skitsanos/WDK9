/*
 * Berkeley DB XML .NET API
 *
 * (c) 2005 Parthenon Computing Ltd
 *
 * Written by John Snelson
 *
 * $Id: deleteDocument.cs,v 1.5 2005/04/06 10:32:19 jpcs Exp $
 *
 */

using Sleepycat.Db;
using Sleepycat.DbXml;

public class DeleteDocument
{
	private static string theContainer = "namespaceExampleData.dbxml";

	// Method that deletes all documents from a DB XML container that match a given
	// XQuery.
	private static void doDeleteDocument(Manager mgr, Container container, 
		string query, QueryContext context, Transaction txn)
	{
		System.Console.WriteLine("Deleting documents for expression: '" + query + "'.");
		System.Console.WriteLine("Return to continue: ");
		System.Console.ReadLine();
	
		// Perform our query. We'll delete any document contained in this result set.
		Results results = mgr.Query(txn, query, context, new DocumentConfig());
		System.Console.WriteLine("Found " + results.Size + 
			" matching the expression '" + query + "'.");
	
		// Get an update context.
		using(UpdateContext updateContext = mgr.CreateUpdateContext()) 
		{
			while(results.MoveNext()) 
			{
				Document document = results.Current.ToDocument();

				string name = document.Name;
				System.Console.WriteLine("Deleting document: " + name + ".");
	    
				// Peform the delete
				container.DeleteDocument(txn, document, updateContext);
				System.Console.WriteLine("Deleted document: " + name + ".");
			}
		}
	}

	// Utility method that we use to make sure the documents that we thought 
	// were deleted from the container are in fact deleted. 
	private static void confirmDelete(Manager mgr, string query, QueryContext context) 
	{
		System.Console.WriteLine("Confirming the delete.");
		System.Console.WriteLine("The query: '" + query + 
			"' should return result set size 0.");
		Results results = mgr.Query(null, query, context, new DocumentConfig());
		if(results.Size == 0) 
		{
			System.Console.WriteLine("Result set size is 0. Deletion confirmed.");
		} 
		else 
		{
			System.Console.WriteLine("Result set size is " + results.Size +
				". Deletion failed.");
		}
	}
    
	public static void Main(string[] args) 
	{

		string envdir = parseArguments(args);

		try 
		{
			// Open an environment and manager
			using(Manager mgr = CreateManager(envdir)) 
			{

				// Open a transactional container
				ContainerConfig containerconfig = new ContainerConfig();
				containerconfig.Transactional = true;
				using(Container container = mgr.OpenContainer(null, theContainer,
						  containerconfig)) 
				{

					// Start a transaction
					using(Transaction txn = mgr.CreateTransaction()) 
					{

						// Create a context and declare the namespaces
						using(QueryContext context = mgr.CreateQueryContext()) 
						{
							context.SetNamespace("fruits", "http://groceryItem.dbxml/fruits");
							context.SetNamespace("vegetables", "http://groceryItem.dbxml/vegetables");
							context.SetNamespace("desserts", "http://groceryItem.dbxml/desserts");
	    
							// Delete the document that describes Mabolo (a fruit)
							string query = "collection(\"" + theContainer + "\")/fruits:item[product = 'Mabolo']";

							// If doDeleteDocument throws an exception then the using block
							// will call Dispose() on the Transaction, which will cause it
							// to abort itself.
							doDeleteDocument(mgr, container, query, context, txn);
							txn.Commit();

							confirmDelete(mgr, query, context);
						}
					}
				}
			}
		}
		catch(DbXmlException e) 
		{
			System.Console.WriteLine("Error performing document delete against " + theContainer);
			System.Console.WriteLine(e.ToString());
		}
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
		System.Console.WriteLine("This program deletes an XML document from a DBXML container.");
		System.Console.WriteLine("You should run exampleLoadContainer before running this example.");
		System.Console.WriteLine("You are only required to pass this command the path location of the");
		System.Console.WriteLine("database environment that you specified when you loaded the examples data:");
		System.Console.WriteLine();
		System.Console.WriteLine("\t-h <dbenv directory>");	
		System.Console.WriteLine("For example:");
		System.Console.WriteLine("\tdeleteDocument.exe -h examplesEnvironment");

		System.Environment.Exit(-1);
	}

	private static string parseArguments(string[] args) 
	{
		string envdir = null;
		for(int i = 0; i < args.Length; ++i) 
		{
			string arg = args[i];
			if((arg.StartsWith("-")
#if WIN32
				|| arg.StartsWith("/")
#endif
				) && arg.Length > 1) 
			{
				switch(arg[1]) 
				{
					case 'h': 
					{
						++i;
						if(i >= args.Length) 
						{
							System.Console.WriteLine("Invalid option: " + arg);
							Usage();
						}
						envdir = args[i];
						break;
					}
					default: 
					{
						System.Console.WriteLine("Unknown option: " + arg);
						Usage();
						break;
					}
				}
			}
			else 
			{
				System.Console.WriteLine("Too many arguments: " + arg);
				Usage();
			}
		}
		if(envdir == null) 
		{
			System.Console.WriteLine("Environment directory not found.");
			Usage();
		}
		return envdir;
	}

}
