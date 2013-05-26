namespace Sleepycat.Db
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void ErrorCallback(Sleepycat.Db.Environment env, string errpfx, string msg);
}

