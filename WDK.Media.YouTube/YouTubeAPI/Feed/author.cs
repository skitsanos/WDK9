using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeAPI.Feed
{
    /// <summary>
    /// 
    /// </summary>
    public class author
    {
        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlElement()]
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlElement()]
        public string uri { get; set; }
    }
}
