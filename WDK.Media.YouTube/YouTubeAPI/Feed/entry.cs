using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YouTubeAPI.Feed
{
    /// <summary>
    /// 
    /// </summary>
    public enum feedType
    {
        upload
    }
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot(Namespace = "http://www.w3.org/2005/Atom")]
    public class entry
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlElement()]
        public string id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement()]
        public DateTime published { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement()]
        public DateTime updated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(Namespace = "http://purl.org/atom/app#")]
        public control control { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement()]
        public List<category> category { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement()]
        public title title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement()]
        public content content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement()]
        public List<link> link { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement()]
        public author author { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(Namespace = "http://search.yahoo.com/mrss/")]
        public group group { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(Namespace = "http://schemas.google.com/g/2005")]
        public comments comments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type"></param>
        public entry(feedType Type)
        {
            if (Type == feedType.upload)
            {
                this.group = new group(true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public entry()
        {
            this.category = new List<category>();
            this.link = new List<link>();
        }
    }
    
    
    
        
        
}
