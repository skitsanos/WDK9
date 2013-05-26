/*
 * Berkeley DB XML .NET API
 *
 * (c) 2005 Parthenon Computing Ltd
 *
 * Written by John Snelson
 *
 * $Id: modifyDocument.cs,v 1.4 2005/04/06 10:46:21 jpcs Exp $
 *
 */

using Sleepycat.Db;
using Sleepycat.DbXml;

public class ModifyDocument
{

	private static string theContainer = "namespaceExampleData.dbxml";

	// Method to add a "description" element after the query's target nodes
	private static void doModify(Manager mgr, Container container, QueryContext context,
		string query, Transaction txn) 
	{

		using(QueryExpression expression = mgr.Prepare(txn, query, context)) 
		{
       
			System.Console.WriteLine("Updating document for the expression: '" + 
				query + "' ");
			System.Console.WriteLine("Return to continue: ");
			System.Console.ReadLine();
	    
			// Print the document(s) to be updated -- those that describe 
			// Zapote Blanco (a fruit). The document strings are only being 
			// printed to show before/after results.
			// Most modification programs would not perform the additional queries.
			using(Results results = expression.Execute(txn, context, new DocumentConfig())) 
			{
				dumpDocuments(results);
      
				results.Reset();

				System.Console.WriteLine("About to update the document(s) above.");
				System.Console.WriteLine("Look for a new element after the target " +
					"element, named 'description' ...");
				System.Console.WriteLine("Return to continue: ");
				System.Console.ReadLine();
       
				// The modification is a new element in the target node, called 
				// "descripton, which goes immediately after the <product> element.
				// if this program is run more than once, and committed, additional
				// identical elements are added.  It is easy to modify this program
				// to change the modification.
				using(Modify modify = mgr.CreateModify()) 
				{
					using(QueryExpression subexpr = mgr.Prepare(txn, ".", context)) 
					{
						modify.AddInsertAfterStep(subexpr, Modify.XmlObject.Element, "description", "very nice");

						using(UpdateContext uc = mgr.CreateUpdateContext()) 
						{
							long numMod = modify.Execute(txn, results, context, uc);
	    
							System.Console.WriteLine("Performed " + numMod + " modification operations");
							dumpDocuments(results);
						}
					}
				}
			}
		}
	}

	// display documents matching the query
	private static void dumpDocuments(Results results) 
	{
		System.Console.WriteLine("Found " + results.Size + " documents ");
		
		results.Reset();
		while(results.MoveNext()) 
		{
			using(Document doc = results.Current.ToDocument()) 
			{
				System.Console.WriteLine("Document content: ");
				System.Console.WriteLine(doc.StringContent);
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
	    
							// Modify the document that describes "Zapote Blanco" (a fruit)
							string query = "collection(\"" + theContainer + "\")/fruits:item/product[. = 'Zapote Blanco']";

							doModify(mgr, container, context, query, txn);

							System.Console.WriteLine("If committed, this program will add a new element each time it is run.");
							System.Console.WriteLine("Press 'c' to commit this change:");
							int c = System.Console.Read();
							if (c == (int)'c' || c == (int)'C')
								txn.Commit();
							else
								txn.Abort();
						}

					}

				}
			}
		}
		catch(DbXmlException e) 
		{
			System.Console.WriteLine("Error performing document modification against " + theContainer);
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
		System.Console.WriteLine("This program modifies documents found in a DBXML container.");
		System.Console.WriteLine("You should run exampleLoadContainer before running this example.");
		System.Console.WriteLine("You are only required to pass this command the path location of the");
		System.Console.WriteLine("database environment that you specified when you loaded the examples data:");
		System.Console.WriteLine();
		System.Console.WriteLine("\t-h <dbenv directory>");
		System.Console.WriteLine("For example:");
		System.Console.WriteLine("\tmodifyDocument.exe -h examplesEnvironment");

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
