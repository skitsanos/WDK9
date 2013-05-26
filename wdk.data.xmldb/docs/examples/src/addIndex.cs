/*
 * Berkeley DB XML .NET API
 *
 * (c) 2005 Parthenon Computing Ltd
 *
 * Written by John Snelson
 *
 * $Id: addIndex.cs,v 1.6 2005/04/05 17:29:29 jpcs Exp $
 *
 */

using Sleepycat.Db;
using Sleepycat.DbXml;

public class AddIndex
{

	private static string theContainer = "namespaceExampleData.dbxml";

	private static void addIndex(Container container, string uri, string name,
		string index, Transaction txn, UpdateContext uc ) 
	{
		System.Console.WriteLine("Adding index type '" + index +
			"' to node '" + name + "'.");

		// Retrieve the index specification from the container
		using(IndexSpecification idxSpec = container.GetIndexSpecification(txn)) 
		{

			// See what indexes exist on the container
			int count = 0;
			System.Console.WriteLine("Before index add.");
			while(idxSpec.MoveNext()) 
			{
				System.Console.WriteLine("\tFor node '" + idxSpec.Current.Name +
					"', found index: '" + idxSpec.Current.Index +
					"'.");
				++count;
			}

			System.Console.WriteLine(count + " indexes found.");

			// Add the index to the specification.
			// If it already exists, then this does nothing.
			idxSpec.AddIndex(new IndexSpecification.Entry(uri, name, index));

			// Set the specification back to the container
			container.SetIndexSpecification(txn, idxSpec, uc);
		}

		// Retrieve the index specification from the container
		using(IndexSpecification idxSpec = container.GetIndexSpecification(txn)) 
		{

			// Look at the indexes again to make sure our replacement took.
			int count = 0;
			System.Console.WriteLine("After index add.");
			while(idxSpec.MoveNext()) 
			{
				System.Console.WriteLine("\tFor node '" + idxSpec.Current.Name +
					"', found index: '" + idxSpec.Current.Index +
					"'.");
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

						// Create an update context
						using(UpdateContext uc = mgr.CreateUpdateContext()) 
						{
							// Add an string equality index for the "product" element node.
							addIndex(container, "", "product", "node-element-equality-string",
								txn, uc);
							// Add an edge presence index for the product node
							addIndex(container, "", "product", "edge-element-presence", txn, uc);
	    
							// Commit the index adds
							txn.Commit();
						}
					}
				}
			}
		}
		catch(DbXmlException e) 
		{
			System.Console.WriteLine("Error adding index to container " + theContainer);
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
		System.Console.WriteLine("This program adds several indexes to a DBXML container.");
		System.Console.WriteLine("You should run exampleLoadContainer before running this example.");
		System.Console.WriteLine("You are only required to pass this command the path location of the database");
		System.Console.WriteLine("environment that you specified when you loaded the examples data:");
		System.Console.WriteLine();
		System.Console.WriteLine("\t-h <dbenv directory>");
		System.Console.WriteLine("For example:");
		System.Console.WriteLine("\taddIndex.exe -h examplesEnvironment");

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
