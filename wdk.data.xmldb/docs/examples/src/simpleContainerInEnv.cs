/*
 * Berkeley DB XML .NET API
 *
 * (c) 2005 Parthenon Computing Ltd
 *
 * Written by John Snelson
 *
 * $Id: simpleContainerInEnv.cs,v 1.1 2005/04/05 17:29:29 jpcs Exp $
 *
 */

using Sleepycat.Db;
using Sleepycat.DbXml;

public class SimpleContainerInEnv
{
	public static void Main(string[] args) 
	{
		// The path the directory where you want to place the environment
		// must exist!!
		string environmentPath = "/path/to/environment/directory";

		// Environment configuration is minimal:
		// create + 50MB cache
		// no transactions, logging, or locking
		EnvironmentConfig config = new EnvironmentConfig();
		config.CacheSize = 50 * 1024 * 1024;
		config.Create = true;
		config.InitializeCache = true;
		Environment env = new Environment(environmentPath, config);

		// Create Manager using that environment, no DBXML flags
		Manager mgr = new Manager(env, new ManagerConfig());

		// Multiple containers can be opened in the same database environment
		using(Container container1 = mgr.CreateContainer(null, "myContainer1"))
		{
			using(Container container2 = mgr.CreateContainer(null, "myContainer2"))
			{
				using(Container container3 = mgr.CreateContainer(null, "myContainer3"))
				{
					// do work here //
				}
			}
		}
	}
}
