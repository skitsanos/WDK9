using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeAPI.Feed
{
    public class feedLink : link
    {
        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlAttribute()]
        public int countHint { get; set; }
    }
}
