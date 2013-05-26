namespace Sleepycat.DbXml
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    internal sealed class NativeInputStream : InputStream
    {
        private Stream stream_;

        internal NativeInputStream(Stream s)
        {
            this.stream_ = s;
        }

        public override int ReadBytes(IntPtr toFill, int maxToRead)
        {
            int num2;
            int ofs = 0;
            while (((num2 = this.stream_.ReadByte()) != -1) && (ofs < maxToRead))
            {
                Marshal.WriteByte(toFill, ofs, (byte) num2);
                ofs++;
            }
            return ofs;
        }

        public override int CurrentPosition
        {
            get
            {
                return (int) this.stream_.Position;
            }
        }
    }
}

