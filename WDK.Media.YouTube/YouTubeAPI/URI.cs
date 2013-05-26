using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YouTubeAPI
{
    static class URI
    {
        public static string ClientLogin
        {
            get { return "https://www.google.com/youtube/accounts/ClientLogin"; }
        }
        public static string StandardFeeds
        {
            get { return "http://gdata.youtube.com/feeds/api/standardfeeds/"; }
        }
        public static string Upload
        {
            get { return "http://uploads.gdata.youtube.com"; }
        }
    }
}
