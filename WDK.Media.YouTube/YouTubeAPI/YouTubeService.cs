using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using XMLSerialization;
using YouTubeAPI.Utils;
using System.Threading;

namespace YouTubeAPI
{
    public enum YouTubeCategories
    {
        /// <summary>
        /// 
        /// </summary>
        [EnumAttribute("Autos")]
        AutosAndVehicles,
        /// <summary>
        /// 
        /// </summary>
        Comedy,
        /// <summary>
        /// 
        /// </summary>
        Education,
        /// <summary>
        /// 
        /// </summary>
        Entertainment,
        /// <summary>
        /// 
        /// </summary>
        [EnumAttribute("Film")]
        FilmAndAnimation,        
        /// <summary>
        /// 
        /// </summary>
        [EnumAttribute("Howto")]
        HowtoAndStyle,
        /// <summary>
        /// 
        /// </summary>
        Music,
        /// <summary>
        /// 
        /// </summary>
        [EnumAttribute("News")]
        NewsAndPolitics,
        /// <summary>
        /// 
        /// </summary>
        [EnumAttribute("Nonprofit")]
        NonprofitsAndActivism,
        /// <summary>
        /// 
        /// </summary>
        [EnumAttribute("People")]
        PeopleAndBlogs,
        /// <summary>
        /// 
        /// </summary>
        [EnumAttribute("Animals")]
        PetsAndAnimals,
        /// <summary>
        /// 
        /// </summary>
        [EnumAttribute("Tech")]
        ScienceAndTechnology,
        /// <summary>
        /// 
        /// </summary>
        Sports,
        /// <summary>
        /// 
        /// </summary>
        [EnumAttribute("Travel")]
        TravelAndEvents
    }
    
    public enum HTTPMethods
    {
        GET,
        POST
    }
    public enum SearchOptions
    {
        top_rated,
        top_favorites,
        most_viewed,
        most_recent,
        most_discussed,
        most_linked,
        most_responded,
        recently_featured,
        watch_on_mobile

    }
   
    public class YouTubeService
    {
        private static string Alphabet = "abcdefghijklmnopqrstuvwxyz1234567890";
        public ClientLogin AuthInfo {get; private set; }
        private bool bIsAuthorized;
        private YouTubeWebRequest Request;
        public event EventHandler<YouTubeEventArgs> OnTranferingProgress;
        public event EventHandler<YouTubeEventArgs> OnExceptionCatched;
        private AutoResetEvent eventWaitResponse = new AutoResetEvent(false);
        private volatile string strResponse = "";

        public string ServerResponse
        {
            get 
            {
                return strResponse;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public YouTubeService(string DeveloperKey)
        {
            this.AuthInfo = new ClientLogin() { DeveloperKey = DeveloperKey};
            this.bIsAuthorized = false;
            Request = new YouTubeWebRequest();
            Request.OnTranfering += new EventHandler<YouTubeEventArgs>(Request_OnTranfering);
            Request.OnReceveResponseComplete += new EventHandler<YouTubeEventArgs>(Request_OnReceveResponseComplete);
            Request.OnExceptionCatched += new EventHandler<YouTubeEventArgs>(Request_OnExceptionCatched);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Request_OnExceptionCatched(object sender, YouTubeEventArgs args)
        {
            if (this.OnExceptionCatched != null)
            {
                strResponse = args.Message;
                this.OnExceptionCatched(sender, args);
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void Request_OnReceveResponseComplete(object sender, YouTubeEventArgs args)
        {
            strResponse = args.Message;
            eventWaitResponse.Set();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>        
        void Request_OnTranfering(object sender, YouTubeEventArgs args)
        {
            if (this.OnTranferingProgress != null)
            {
                this.OnTranferingProgress(sender, args);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="Source"></param>
        public void Login(string UserName, string Password, string Source)
        {
            //Console.WriteLine("Login....");

            string strPOSTData = Requests.ClientLoginRequest(UserName, Password, Source);
            MemoryStream ms = new MemoryStream();
            ms.Write(Encoding.UTF8.GetBytes(strPOSTData), 0, Encoding.UTF8.GetByteCount(strPOSTData));
            Request.InitURI(URI.ClientLogin);
            Request.YouTubeRequest.ContentType = YouTubeWebRequest.strContentType;
            Request.YouTubeRequest.KeepAlive = false;
            //ClientLogin.Parse(this.AuthInfo, request.DoPOST(this.AuthInfo, ms));
            Request.DoPOST(this.AuthInfo, ms);            
            eventWaitResponse.WaitOne();
            ClientLogin.Parse(this.AuthInfo, this.strResponse);

            //Console.WriteLine(this.AuthInfo.YouTubeUser);
            //Console.WriteLine(this.AuthInfo.Auth);

            this.bIsAuthorized = true;
        }
        /// <summary>
        /// 
        /// </summary>
        public void RetriveVideo()
        {
            if (!bIsAuthorized)
                throw new Exception("Not authorized!");
            //YouTubeWebRequest request = new YouTubeWebRequest(Requests.RetrieveVideo(SearchOptions.most_discussed,""));            
            Request.InitURI(Requests.RetrieveVideo(SearchOptions.most_discussed, ""));
            //Console.WriteLine(Request.DoGET(this.AuthInfo));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="YouTubeFileInfo"></param>
        /// <returns></returns>
        public void UploadVideo(YouTubeVideoFileInfo YouTubeFileInfo)
        {
            //Console.WriteLine("Uploading...");

            FileInfo fileUploadInfo = new FileInfo(YouTubeFileInfo.FilePath);
            string  strBoundaryString =  this.GetBoundaryString();

            Request.InitURI(Requests.UploadVideo(this.AuthInfo.YouTubeUser));
            Request.YouTubeRequest.Headers.Add("Slug", fileUploadInfo.Name);
            Request.YouTubeRequest.ContentType = String.Format("multipart/related; boundary={0}", strBoundaryString);                   
            Request.YouTubeRequest.KeepAlive = false;
            //string strResponse = Request.DoPOST(this.AuthInfo, this.GetPOSTData(strBoundaryString, FileName));
            Request.DoPOST(this.AuthInfo, this.GetPOSTData(strBoundaryString, YouTubeFileInfo));
            eventWaitResponse.WaitOne();

            //>>Test
            //using (StreamWriter streamWriter = new StreamWriter("response.xml"))
            //{
            //    streamWriter.Write(this.strResponse);
            //}
            //Console.WriteLine(this.strResponse);
            //<<Test
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BoundaryString"></param>
        /// <returns></returns>
        private MemoryStream GetPOSTData(string BoundaryString, YouTubeVideoFileInfo YouTubeFileInfo)
        {
            //byte[] buffer;
            string strPostData = string.Empty;
            MemoryStream ms = new MemoryStream();
            StringBuilder strPostDataBuilder = new StringBuilder();
            
            strPostDataBuilder.AppendLine("--" + BoundaryString);
            strPostDataBuilder.AppendLine("Content-Type: application/atom+xml; charset=UTF-8");
            strPostDataBuilder.AppendLine();
                                  
            #region YouTube feed initialization

            YouTubeAPI.Feed.entry entry = new YouTubeAPI.Feed.entry(YouTubeAPI.Feed.feedType.upload);
            entry.group.title.type = "plain";
            entry.group.title.Text = YouTubeFileInfo.Title;//"Bad Wedding Toast";
            entry.group.description.type = "plain";
            entry.group.description.Text = YouTubeFileInfo.Description;//"I gave a bad toast at my friend's wedding.";
            entry.group.category.scheme = "http://gdata.youtube.com/schemas/2007/categories.cat";
            entry.group.category.Text = EnumToString.Get(typeof(YouTubeCategories),YouTubeFileInfo.Category);//"People";
            entry.group.keywords.Text = YouTubeFileInfo.Keywords;
            
            #endregion
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("media", "http://search.yahoo.com/mrss/");
            namespaces.Add("yt", "http://gdata.youtube.com/schemas/2007");
            string strSRZD = XmlSerializationUtility.ObjectToXmlString(entry, namespaces);

            strPostDataBuilder.Append("<?xml version=\"1.0\"?>");
            strPostDataBuilder.AppendLine(strSRZD);
            //Console.WriteLine("************************************");
            //Console.WriteLine(strPostDataBuilder.ToString());
            //Console.WriteLine("************************************");
            strPostDataBuilder.AppendLine("--" + BoundaryString);
            strPostDataBuilder.AppendLine("Content-Type: video/mpeg");
            strPostDataBuilder.AppendLine("Content-Transfer-Encoding: binary");
            strPostDataBuilder.AppendLine();

            //>>Test
            //using (StreamWriter streamWriter = new StreamWriter("Atom.xml"))
            //{
            //    streamWriter.Write(strPostDataBuilder.ToString());
            //}
            //<<Test

            ms.Write(Encoding.UTF8.GetBytes(strPostDataBuilder.ToString()), 0, Encoding.UTF8.GetByteCount(strPostDataBuilder.ToString()));
            byte[] fileToUpload = FileUtils.GetFile(YouTubeFileInfo.FilePath);
            ms.Write(fileToUpload, 0, fileToUpload.Length);            
            ms.Write(Encoding.UTF8.GetBytes("\n--" + BoundaryString + "--"), 0, Encoding.UTF8.GetByteCount("\n--" + BoundaryString + "--"));
            return ms;            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetBoundaryString()
        {
            string strRandomBoundaryString = string.Empty;
            Random randomizer = new Random((int)DateTime.Now.Ticks);
            for(int i = 0; i < 20; i++)
            {
                strRandomBoundaryString += Alphabet[randomizer.Next(Alphabet.Length - 1)];
            }
            return strRandomBoundaryString;
        }

    }
}
