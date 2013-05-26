using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace YouTubeAPI
{
    public class ClientLogin
    {
        /// <summary>
        /// 
        /// </summary>
        public string Auth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string YouTubeUser { get; set; }
        public string DeveloperKey { get; set; }
        /// <summary>
        /// 
        /// </summary>        
        public ClientLogin()
        {            
            this.Auth = string.Empty;
            this.YouTubeUser = string.Empty;
            this.DeveloperKey = string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseStrem"></param>
        /// <returns></returns>
        public static ClientLogin Parse(Stream Response)
        {
            using (StreamReader streamRd = new StreamReader(Response))
            {   
                string strResponse = streamRd.ReadToEnd();                
                ClientLogin newClientLogin = new ClientLogin();
                newClientLogin.Auth = strResponse.Substring(5, strResponse.IndexOf("\n") - 5);
                newClientLogin.YouTubeUser = strResponse.Substring(strResponse.IndexOf("YouTubeUser=") + 12, strResponse.Length - strResponse.IndexOf("YouTubeUser=") - 12);
                return newClientLogin;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Response"></param>
        /// <returns></returns>
        public static void Parse(ClientLogin AuthInfo,string Response)
        {
            AuthInfo.Auth = Response.Substring(5, Response.IndexOf("\n") - 5);
            AuthInfo.YouTubeUser = Response.Substring(Response.IndexOf("YouTubeUser=") + 12, Response.Length - Response.IndexOf("YouTubeUser=") - 12);          
        }
    }
}
