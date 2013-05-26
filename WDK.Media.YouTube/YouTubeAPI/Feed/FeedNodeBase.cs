using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeAPI.Feed
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class FeedNodeBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlText(typeof(String))]
        public string Text { get; set; }
    }
}
