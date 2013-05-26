using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YouTubeAPI
{
    public class YouTubeVideoFileInfo
    {
        public string FilePath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public YouTubeCategories Category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private string keywords;
        /// <summary>
        /// 
        /// </summary>
        public string Keywords
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
                    this.keywords = strRetVal.ToString();
                }                
                return this.keywords;
            }
            set
            {
                this.keywords = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<string> listKeywords { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public YouTubeVideoFileInfo()
        {
            this.listKeywords = new List<string>();
        }
        /// <summary>
        /// 
        /// </summary>
        public YouTubeVideoFileInfo(string FilePath)
        {
            this.listKeywords = new List<string>();
            this.FilePath = FilePath;
        }
    }
}
