using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeAPI.Feed
{
    /// <summary>
    /// 
    /// </summary>
    public class control
    {
        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlElement(Namespace = "http://purl.org/atom/app#")]
        public string draft { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlElement(Namespace = "http://gdata.youtube.com/schemas/2007")]
        public state state { get; set; }
    }
}
