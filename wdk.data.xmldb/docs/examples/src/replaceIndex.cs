/*
 * Berkeley DB XML .NET API
 *
 * (c) 2005 Parthenon Computing Ltd
 *
 * Written by John Snelson
 *
 * $Id: replaceIndex.cs,v 1.2 2005/04/06 10:46:23 jpcs Exp $
 *
 */

using Sleepycat.Db;
using Sleepycat.DbXml;

public class ReplaceIndex
{
	private static string theContainer = "namespaceExampleData.dbxml";

	// Method used to replace an index with a new index type
	private static void replaceIndex(Manager mgr, Container container,
		string URI, string nodeName, string indexType, Transaction txn)
	{
		System.Console.WriteLine("Replacing index specification '" + indexType +
			"' from node '" + nodeName + "'.");

		// Retrieve the index specification from the container
		using(IndexSpecification idxSpec = container.GetIndexSpecification(txn)) 
		{
	
			// See what indexes exist on the container
			int count = 0;
			System.Console.WriteLine("Before index replacement:");
			// Loop over the indexes and report what's there.
			while(idxSpec.MoveNext()) 
			{
				System.Console.WriteLine("\tFor node '" + idxSpec.Current.Name +
					"', found index: '" + idxSpec.Current.Index + "'.");
				++count;
			}	
			System.Console.WriteLine(count + " indexes found.");
	
			// Replace the container's index specification with a new specification
			idxSpec.ReplaceIndex(
				new IndexSpecification.Entry(URI, nodeName, indexType));
	
			// Get an update context.
			using(UpdateContext updateContext = mgr.CreateUpdateContext()) 
			{
				// Set the specification back to the container
				container.SetIndexSpecification(txn, idxSpec, updateContext);
			}
		}
	
		// Retrieve the index specification from the container
		using(IndexSpecification idxSpec = container.GetIndexSpecification(txn))
		{
			// Look at the indexes again to make sure our deletion took.
			int count = 0;
			System.Console.WriteLine("After index replacement:");
			while(idxSpec.MoveNext())
			{
				System.Console.WriteLine("\tFor node '" + idxSpec.Current.Name +
					"', found index: '" + idxSpec.Current.Index + "'.");
				++count;
			}
			System.Console.WriteLine(count + " indexes found.");
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
						// Replace the index on the "product" node.
						replaceIndex(mgr, container, "", "product",
							"node-attribute-substring-string node-element-equality-string",
							txn);
						// Commit the index replacement
						txn.Commit();
					}
				}
			}
		}
		catch(DbXmlException e) 
		{
			System.Console.WriteLine("Error replacing index for container " + theContainer);
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
		System.Console.WriteLine("This program replaces a DB XML container's index specification with a new");
		System.Console.WriteLine("specification. You should run exampleLoadContainer before running this example.");
		System.Console.WriteLine("You are only required to pass this command the path location of the database");
		System.Console.WriteLine("environment that you specified when you loaded the examples data:");
		System.Console.WriteLine();
		System.Console.WriteLine("\t-h <dbenv directory>");
		System.Console.WriteLine();
		System.Console.WriteLine("For best results, run addIndex before running this program.");
		System.Console.WriteLine();
		System.Console.WriteLine("For example:");
		System.Console.WriteLine("\treplaceIndex.exe -h examplesEnvironment");

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
