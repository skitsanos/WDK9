using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace YouTubeAPI.Utils
{
    class FileUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static byte[] GetFile(string FileName)
        {
            int bytesRead = 0;
            using (FileStream fs = File.OpenRead(FileName))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] buffer = new byte[4096];
                    while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, bytesRead);
                    }
                    return ms.ToArray();
                }
            }
        }

    }
}
