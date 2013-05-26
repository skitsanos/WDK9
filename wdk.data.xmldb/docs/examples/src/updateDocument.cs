/*
 * Berkeley DB XML .NET API
 *
 * (c) 2005 Parthenon Computing Ltd
 *
 * Written by John Snelson
 *
 * $Id: updateDocument.cs,v 1.2 2005/04/06 10:46:23 jpcs Exp $
 *
 */

using Sleepycat.Db;
using Sleepycat.DbXml;

using System.Text;

public class UpdateDocument
{
	private static string theContainer = "namespaceExampleData.dbxml";

	// Modifies an XML document that is stored in a DB XML container
	private static void doUpdateDocument(Manager mgr, Container container, 
		string query, QueryContext context, Transaction txn) 
	{
		System.Console.WriteLine("Updating documents for expression: '" + query + "'.");
		System.Console.WriteLine("Return to continue: ");
		System.Console.ReadLine();
	
		// query for all the documents that we want to update
		Results results = mgr.Query(txn, query, context, new DocumentConfig());
		System.Console.WriteLine("Found " + results.Size + 
			" matching the expression '" + query + "'.");
	
		// Get an update context.
		using(UpdateContext updateContext = mgr.CreateUpdateContext()) 
		{
			while(results.MoveNext()) 
			{
				Document document = results.Current.ToDocument();

				// Retrieve the entire document as a single String object
				string docString = document.StringContent;
				System.Console.WriteLine("Updating document: ");
				System.Console.WriteLine(docString);

				// This next method just modifies the document string
				// in a small way.
				string newDocString = getNewDocument(mgr, document, context, 
					docString);
	    
				System.Console.WriteLine("Updating document...");
	    
				//Set the document's content to be the new document string
				document.StringContent = newDocString;
	    
				// Now replace the document in the container
				container.UpdateDocument(txn, document, updateContext);
				System.Console.WriteLine("Document updated.");
			}
		}
	}

	private static string getNewDocument(Manager mgr, Document document, 
		QueryContext context, string docString)
	{
		// Get the substring that we want to replace
		string inventory = getValue(mgr, document, 
			"/*/inventory/inventory/text()", context);

		// Convert the String representation of the inventory level to an 
		// integer, increment by 1, and then convert back to a String for 
		// replacement on the document.
		int newInventory = System.Int32.Parse(inventory) + 1;
		string newVal = newInventory.ToString();

		// Perform the replace
		StringBuilder strbuff = new StringBuilder(docString);
		strbuff.Replace(inventory, newVal);
		System.Console.WriteLine("Inventory was " + inventory +
			", it is now " + newVal + ".");
		return strbuff.ToString();
	}

	private static string getValue(Manager mgr, Document document, string query,
		QueryContext context)
	{
		// Perform the query
		// The document provides the context for the query
		using(QueryExpression queryExpr = mgr.Prepare(null, query, context))
		{
			using(Value docValue = new Value(document))
			{
				using(Results result = queryExpr.Execute(null, docValue, context, new DocumentConfig()))
				{

					// We require a result set size of 1.
					if(!result.MoveNext())
					{
						System.Console.WriteLine("Error! query '" + query + 
							"' returned a result size < 1");
						throw new System.Exception("getValue found result set not equal to 1.");
					}

					// Get the value. If we allowed the result set to be larger than size 1,
					// we would have to loop through the results, processing each as is
					// required by our application.
					return result.Current.ToString();
				}
			}
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
	    
							// update the document that describes Zapote Blanco (a fruit)
							string query = "collection(\"" + theContainer +
								"\")/fruits:item/product[text() = 'Zapote Blanco']";
							doUpdateDocument(mgr, container, query, context, txn);

							// Commit the writes. This causes the container write operations
							// to be saved to the container.
							txn.Commit();
						}
					}
				}
			}
		}
		catch(DbXmlException e) 
		{
			System.Console.WriteLine("Error performing document update against " + theContainer);
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
		System.Console.WriteLine("This program updates a document stored in a DB XML container.");
		System.Console.WriteLine("You should run exampleLoadContainer before running this example.");
		System.Console.WriteLine("You are only required to pass this command the path location of the");
		System.Console.WriteLine("database environment that you specified when you loaded the examples data:");
		System.Console.WriteLine();	
		System.Console.WriteLine("\t-h <dbenv directory>");
		System.Console.WriteLine("For example:");
		System.Console.WriteLine("\tupdateDocument.exe -h examplesEnvironment");

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
