/*
 * Berkeley DB XML .NET API
 *
 * (c) 2005 Parthenon Computing Ltd
 *
 * Written by John Snelson
 *
 * $Id: queryForDocumentValue.cs,v 1.3 2005/04/06 10:46:21 jpcs Exp $
 *
 */

using Sleepycat.Db;
using Sleepycat.DbXml;

public class QueryForDocumentValue
{

	private static string theContainer = "namespaceExampleData.dbxml";

	private static string getValue(Manager mgr, Document document, 
		string query, QueryContext context)
	{
		///////////////////////////////////////////////////////////////////////////
		////////    Return specific information from a document. //////////////////
		////////          Assumes a result set of size 1         //////////////////
		///////////////////////////////////////////////////////////////////////////

		using(Value docValue = new Value(document))
		{
			// Perform the query
			// The document provides the context for the query, so neither
			// collection() nor doc() needs to be part of the query.
			using(QueryExpression queryExpr = mgr.Prepare(null, query, context))
			{
				using(Results result = queryExpr.Execute(null, docValue, context, new DocumentConfig()))
				{
					// We require a result set size of 1.
					if(!result.MoveNext())
					{
						System.Console.WriteLine("Error!  query '" + query + 
							"' returned a result size size != 1");
						throw new System.Exception( "getValue found result set not equal to 1.");
					}
	
					// Get the value. If we allowed the result set to be larger than size 1,
					// we would have to loop through the results, processing each as is
					// required by our application.
					return result.Current.ToString();
				}
			}
		}
	}
    
	private static void getDetails(Manager mgr, string query, QueryContext context)
	{
		// Perform a single query against the referenced container using
		// the referenced context.
		System.Console.WriteLine("Exercising query: '" + query + "'.");
		System.Console.WriteLine("Return to continue:");
		System.Console.ReadLine();
	
		// Perform the query
		Results results = mgr.Query(null, query, context, new DocumentConfig());
	
		System.Console.WriteLine("\tProduct : Price : Inventory Level");	

		while(results.MoveNext())
		{
			/// Retrieve the value as a document
			using(Document theDocument = results.Current.ToDocument())
			{
				// Obtain information of interest from the document. Note that the 
				// wildcard in the query expression allows us to not worry about what
				// namespace this document uses.
				string item = getValue(mgr, theDocument,
					"string(/*/product)", context);
				string price = getValue(mgr, theDocument, 
					"string(/*/inventory/price)", context);
				string inventory = getValue(mgr, theDocument, 
					"string(/*/inventory/inventory)", context);
	    
				System.Console.WriteLine("\t" + item + " : " + price + " : " +
					inventory);
			}
		}

		System.Console.WriteLine(results.Size + " results returned for query '" + 
			query + "'.");
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
	    
						// Get details on Zulu Nuts
						getDetails(mgr, "collection(\"" + theContainer +
							"\")/fruits:item/product[text() = 'Zulu Nut']", context);
	    
						// Get details on all vegetables that start with 'A'
						getDetails(mgr, "collection(\"" + theContainer +
							"\")/vegetables:item/product[starts-with(text(),'A')]", 
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
		System.Console.WriteLine("This program retrieves DBXML documents and then retrieves information of");
		System.Console.WriteLine("interest from the retrieved document(s). You should run exampleLoadContainer");
		System.Console.WriteLine("before running this example. You are only required to pass this command the path");
		System.Console.WriteLine("location of the database environment that you specified when you loaded the");
		System.Console.WriteLine("examples data:");
		System.Console.WriteLine();	
		System.Console.WriteLine("\t-h <dbenv directory>");
		System.Console.WriteLine("For example:");
		System.Console.WriteLine("\tqueryForDocumentValue.exe -h examplesEnvironment");

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
