/*
 * Berkeley DB XML .NET API
 *
 * (c) 2005 Parthenon Computing Ltd
 *
 * Written by John Snelson
 *
 * $Id: queryWithContext.cs,v 1.2 2005/04/06 10:46:23 jpcs Exp $
 *
 */

using Sleepycat.Db;
using Sleepycat.DbXml;

public class QueryWithContext
{
	private static string theContainer = "namespaceExampleData.dbxml";

	// Performs a query against a document using an QueryContext.
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
			System.Console.WriteLine(results.Current);
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

						// Set a variable
						context.SetVariableValue("aDessert",
							new Value("Blueberry Boy Bait"));

						// Perform the queries

						// Find all the Vendor documents in the database. Vendor documents do
						// not use namespaces, so this query returns documents.
						doContextQuery(mgr, "collection(\"" + theContainer +
							"\")/vendor", context);

						// Find the product document for "Lemon Grass". 
						// This query returns no documents
						// because a namespace prefix is not identified for the 'item' node.
						doContextQuery(mgr, "collection(\"" + theContainer +
							"\")/item/product[.=\"Lemon Grass\"]", context);

						// Find the product document for "Lemon Grass" using the namespace 
						// prefix 'fruits'. This query successfully returns a document.
						doContextQuery(mgr, "collection(\"" + theContainer +
							"\")/fruits:item/product[.=\"Lemon Grass\"]", context);

						// Find all the vegetables
						doContextQuery(mgr, "collection(\"" + theContainer +
							"\")/vegetables:item", context);

						// Find the dessert called Blueberry Boy Bait.
						// Note the use of a variable.
						doContextQuery(mgr, "collection(\"" + theContainer +
							"\")/desserts:item/product[.=$aDessert]", context);
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
		System.Console.WriteLine("This program illustrates how to query for documents that require namespace");
		System.Console.WriteLine("usage in the query. You should run exampleLoadContainer before running this example.");
		System.Console.WriteLine("You are only required to pass this command the path location of the database");
		System.Console.WriteLine("environment that you specified when you loaded the examples data:");
		System.Console.WriteLine();		
		System.Console.WriteLine("\t-h <dbenv directory>");
		System.Console.WriteLine("For example:");
		System.Console.WriteLine("\tqueryWithContext.exe -h examplesEnvironment");

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
