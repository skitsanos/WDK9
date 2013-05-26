using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeAPI.Feed
{
    /// <summary>
    /// 
    /// </summary>
    public class keywords : FeedNodeBase
    {
        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public List<string> listKeywords { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlElement()]
        private string strKeywords;

        /// <summary>
        /// 
        /// </summary>
        [System.Xml.Serialization.XmlText(typeof(String))]
        public new string Text
        {
            get
            {
                if (this.listKeywords.Count > 0)
                {
                    StringBuilder strRetVal = new StringBuilder();
                    int i = 0;
                    foreach (string keyword in this.listKeywords)
                    {
                        strRetVal.Append(keyword);
                        if (i++ != this.listKeywords.Count - 1)
                        {
                            strRetVal.Append(",");
                        }
                    }
                    this.strKeywords = strRetVal.ToString();
                }

                return strKeywords;
            }
            set
            {
                strKeywords = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public keywords()
        {
            this.listKeywords = new List<string>();
        }

    }
}
