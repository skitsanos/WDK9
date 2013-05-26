using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeAPI.Feed
{
    /// <summary>
    /// 
    /// </summary>
    public class description : FeedNodeBase
    {
        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlAttribute()]
        public string type
        {
            get { return this.Type; }
            set { this.Type = value; }
        }
    }
}
