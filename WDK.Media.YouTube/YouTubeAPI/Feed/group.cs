using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YouTubeAPI.Feed
{
    /// <summary>
    /// 
    /// </summary>
    public class group
    {
        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlElement(Namespace = "http://search.yahoo.com/mrss/")]
        public title title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlElement(Namespace = "http://search.yahoo.com/mrss/")]
        public description description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlElement(Namespace = "http://search.yahoo.com/mrss/")]
        public category category { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlElement(Namespace = "http://search.yahoo.com/mrss/")]
        public keywords keywords { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CreateContent"></param>
        public group(bool CreateContent)
        {
            if (CreateContent)
            {
                this.title = new title();
                this.description = new description();
                this.category = new category();
                this.keywords = new keywords();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public group()
        {
        }
    }
}
