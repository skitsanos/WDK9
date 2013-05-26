using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace YouTubeAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class YouTubeEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public Int64 BytesTotal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int64 BytesTransfered { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }        

        /// <summary>
        /// 
        /// </summary>
        public YouTubeEventArgs()
        {
            this.BytesTotal = 0;
            this.BytesTransfered = 0;
            this.Message = "";            
        }
    }
}
