/*
 * Berkeley DB XML .NET API
 *
 * (c) 2005 Parthenon Computing Ltd
 *
 * Written by John Snelson
 *
 * $Id: simpleQuery.cs,v 1.3 2005/04/06 10:46:23 jpcs Exp $
 *
 */

using Sleepycat.Db;
using Sleepycat.DbXml;

public class SimpleQuery
{
	private static string theContainer = "simpleExampleData.dbxml";

	private static void doQuery(Manager mgr, string query)
	{
		// Perform a single query against the referenced container.
		System.Console.WriteLine("Exercising query: '" + query + "'.");
		System.Console.WriteLine("Return to continue: ");
		System.Console.ReadLine();

		// Perform the query
		using(QueryContext context = mgr.CreateQueryContext())
		{
			Results results = mgr.Query(null, query, context, new DocumentConfig());
			// Iterate over the results
			while(results.MoveNext())
			{
				System.Console.WriteLine(results.Current);
			}
			System.Console.WriteLine(results.Size + " results returned for query '" 
				+ query + "'.");
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
				// Open a non-transactional container
				using(Container container = mgr.OpenContainer(null, theContainer))
				{
					//Find all the Vendor documents in the database
					doQuery(mgr, "collection(\"" + theContainer +
						"\")/vendor");
	    
					//Find all the vendors that are wholesale shops
					doQuery(mgr, "collection(\"" + theContainer +
						"\")/vendor[@type=\"wholesale\"]");
	    
					//Find the product document for "Lemon Grass"
					doQuery(mgr, "collection(\"" + theContainer +
						"\")/product/item[.=\"Lemon Grass\"]");
	    
					//Find all the products where the price is less than or equal to 0.11
					doQuery(mgr, "collection(\"" + theContainer +
						"\")/product/inventory[number(price)<=0.11]");
	    
					//Find all the vegetables where the price is less than or equal to 0.11
					doQuery(mgr, "collection(\"" + theContainer +
						"\")/product[number(inventory/price)<=0.11 and category=\"vegetables\"]");
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
		System.Console.WriteLine("This program performs various queries against a DBXML container.");
		System.Console.WriteLine("You should run exampleLoadContainer before running this example.");
		System.Console.WriteLine("You are only required to pass this command the path location of the database");
		System.Console.WriteLine("environment that you specified when you loaded the examples data:");
		System.Console.WriteLine();			
		System.Console.WriteLine("\t-h <dbenv directory>");
		System.Console.WriteLine("For example:");
		System.Console.WriteLine("\tsimpleQuery.exe -h examplesEnvironment");

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
