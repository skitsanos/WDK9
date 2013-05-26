/*
 * Berkeley DB XML .NET API
 *
 * (c) 2005 Parthenon Computing Ltd
 *
 * Written by John Snelson
 *
 * $Id: queryForMetaData.cs,v 1.2 2005/04/06 10:46:23 jpcs Exp $
 *
 */

using Sleepycat.Db;
using Sleepycat.DbXml;

public class QueryForMetaData
{

	private static string theContainer = "namespaceExampleData.dbxml";

	// Shows a timestamp for each record that matches the given XPath query.
	// The timestamp is stored as metadata on each document. This metadata was 
	// added to the document when the example data was loaded into the container 
	// using exampleLoadContainer.java. The timestamp represents the time
	// when the document was loaded into the container.
	private static void showTimeStamp(Manager mgr, string query,
		QueryContext context)
	{
		// Perform a single query against the referenced container using
		// the referenced context. The timestamp metadata attribute is then
		// displayed.
		System.Console.WriteLine("Exercising query: '" + query + "'.");
		System.Console.WriteLine("Return to continue: ");
		System.Console.ReadLine();

		//Perform the query
		Results results = mgr.Query(null, query, context, new DocumentConfig());
	
		while(results.MoveNext())
		{
			using(Document document = results.Current.ToDocument()) 
			{
				// We return the metadata as a MetaData object
				using(MetaData md = document.GetMetaData("http://dbxmlExamples/timestamp", "timeStamp"))
				{
					System.Console.WriteLine("Document " + document.Name +
						" stored on " + md.Value.ToDateTime());
				}
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

						// Show all the vegetables
						showTimeStamp(mgr, "collection(\"" + theContainer +
							"\")/vegetables:item", context);
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
		System.Console.WriteLine("This program retrieves DBXML documents and then retrieves the date and time");
		System.Console.WriteLine("that the document was stored in the container. You should run exampleLoadContainer");
		System.Console.WriteLine("before running this example. You are only required to pass this command the path");
		System.Console.WriteLine("location of the database environment that you specified when you loaded the examples");
		System.Console.WriteLine("data:");
		System.Console.WriteLine();	
		System.Console.WriteLine("\t-h <dbenv directory>");
		System.Console.WriteLine("For example:");
		System.Console.WriteLine("\tqueryForMetaData.exe -h examplesEnvironment");

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
