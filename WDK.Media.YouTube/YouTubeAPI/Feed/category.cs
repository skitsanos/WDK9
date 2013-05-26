using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeAPI.Feed
{
    /// <summary>
    /// 
    /// </summary>
    public class category : FeedNodeBase
    {
        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlAttribute()]
        public string scheme { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlAttribute()]
        public string term { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlAttribute()]
        public string label { get; set; }
    }
}
