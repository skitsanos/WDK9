using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YouTubeAPI
{
    static class Requests
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static string ClientLoginRequest(string UserName, string Password, string Source)
        {
            return String.Format("Email={0}&Passwd={1}&service=youtube&source={2}",
                                UserName, Password, Source);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string RetrieveVideo(SearchOptions type, string time)
        {
            return URI.StandardFeeds + type.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static string UploadVideo(string UserName)
        {
            return String.Format(URI.Upload + "/feeds/api/users/{0}/uploads", UserName);
        }
        
    }
}
