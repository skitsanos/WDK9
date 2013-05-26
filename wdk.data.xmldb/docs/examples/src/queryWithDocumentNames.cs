/*
 * Berkeley DB XML .NET API
 *
 * (c) 2005 Parthenon Computing Ltd
 *
 * Written by John Snelson
 *
 * $Id: queryWithDocumentNames.cs,v 1.2 2005/04/06 10:46:23 jpcs Exp $
 *
 */

using Sleepycat.Db;
using Sleepycat.DbXml;

public class QueryWithDocumentNames
{
	private static string theContainer = "namespaceExampleData.dbxml";

	// Performs a query against a document using an XmlQueryContext.
	private static void doContextQuery(Manager mgr, string query, 
		QueryContext context)
	{
		// Perform a single query against the referenced container using
		// the referenced context.
		System.Console.WriteLine("Exercising query: '" + query + "'.");
		System.Console.WriteLine("Return to continue: ");
		System.Console.ReadLine();
	
		// Perform the query
		Results results = mgr.Query(null, query, context, new DocumentConfig());
		// Iterate over the results
		while(results.MoveNext())
		{
			using(Document document = results.Current.ToDocument())
			{
				System.Console.WriteLine("Document name: " + document.Name);
				System.Console.WriteLine(results.Current);
			}
		}
		System.Console.WriteLine(results.Size + " results returned for query '" 
			+ query + "'.");
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
					// Create a context and declare the namespaces
					using(QueryContext context = mgr.CreateQueryContext()) 
					{
						context.SetNamespace("fruits", "http://groceryItem.dbxml/fruits");
						context.SetNamespace("vegetables", "http://groceryItem.dbxml/vegetables");
						context.SetNamespace("desserts", "http://groceryItem.dbxml/desserts");

						// Query for documents using their document names.
						// The name is stored as meta data on the document, using the
						// namespace "http://www.sleepycat.com/2002/dbxml" and the name
						// "name".

						// Notice that you do NOT have to declare the dbxml namespace in
						// the QueryContext used for this query. Also, each document name 
						// was set by exampleLoadContainer when the document was loaded into
						// the Container.
						doContextQuery(mgr, "collection(\"" + theContainer +
							"\")[dbxml:metadata('dbxml:name')='ZuluNut.xml']",
							context);
						doContextQuery(mgr, "collection(\"" + theContainer +
							"\")[dbxml:metadata('dbxml:name')='TrifleOrange.xml']",
							context);
						doContextQuery(mgr, "collection(\"" + theContainer +
							"\")[dbxml:metadata('dbxml:name')='TriCountyProduce.xml']",
							context);
						doContextQuery(mgr, "collection(\"" + theContainer +
							"\")[/fruits:item/product=\"Zulu Nut\"]",
							context);
					}
				}
			}
		}
		catch(DbXmlException e) 
		{
			System.Console.WriteLine("Error performing query against " + theContainer);
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
		System.Console.WriteLine("This program retrieves DB XML documents using their document names.");
		System.Console.WriteLine("You should run exampleLoadContainer before running this example.");
		System.Console.WriteLine("You are only required to pass this command the path location of the database");
		System.Console.WriteLine("environment that you specified when you loaded the examples data:");
		System.Console.WriteLine();			
		System.Console.WriteLine("\t-h <dbenv directory>");
		System.Console.WriteLine("For example:");
		System.Console.WriteLine("\tqueryWithDocumentNames.exe -h examplesEnvironment");

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
