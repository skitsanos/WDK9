/*
 * Berkeley DB XML .NET API
 *
 * (c) 2005 Parthenon Computing Ltd
 *
 * Written by John Snelson
 *
 * $Id: simpleAdd.cs,v 1.5 2005/04/04 10:01:12 jpcs Exp $
 *
 */

using Sleepycat.Db;
using Sleepycat.DbXml;

public class SimpleAdd 
{

	private static string theContainer = "simpleExampleData.dbxml";

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
							// Create string contents for documents.
							string document1 = "<aDoc><title>doc1</title><color>green</color></aDoc>";
							string document2 = "<aDoc><title>doc2</title><color>yellow</color></aDoc>";

							// Put the document, asking DB XML to generate a name
							DocumentConfig docconfig = new DocumentConfig();
							docconfig.GenerateName = true;
							container.PutDocument(txn, "", document1, uc, docconfig);
	    
							// Do it again for the second document
							container.PutDocument(txn, "", document2, uc, docconfig);
	    
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
			System.Console.WriteLine("Error performing document add against " + theContainer);
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
		System.Console.WriteLine("This program adds a few simple XML documents to a specified container.");
		System.Console.WriteLine("You should run exampleLoadContainer before running this example.");
		System.Console.WriteLine("You are only required to pass this command the path location of the database");
		System.Console.WriteLine("environment that you specified when you loaded the examples data:");
		System.Console.WriteLine();
		System.Console.WriteLine("\t-h <dbenv directory>");
		System.Console.WriteLine("For example:");
		System.Console.WriteLine("\tsimpleAdd.exe -h examplesEnvironment");

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
