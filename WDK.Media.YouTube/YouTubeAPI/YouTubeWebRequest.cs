using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

namespace YouTubeAPI
{
    public class YouTubeWebRequest
    {
        /// <summary>
        /// 
        /// </summary>    
        public event EventHandler<YouTubeEventArgs> OnTranfering;

        /// <summary>
        /// 
        /// </summary>        
        public event EventHandler<YouTubeEventArgs> OnTransferComplete;

        /// <summary>
        /// 
        /// </summary>        
        public event EventHandler<YouTubeEventArgs> OnReceveResponseComplete;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<YouTubeEventArgs> OnExceptionCatched;

        /// <summary>
        /// 
        /// </summary>
        public HttpWebRequest YouTubeRequest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string strContentType 
        {
            get { return "application/x-www-form-urlencoded"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public MemoryStream POSTData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ///private static AutoResetEvent allDone = new AutoResetEvent(false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RequestUri"></param>
        /// <param name="Method"></param>
        public YouTubeWebRequest()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RequestUri"></param>
        public void InitURI(string RequestUri)
        {
            this.YouTubeRequest = (HttpWebRequest)WebRequest.Create(RequestUri);
            this.YouTubeRequest.ContentType = strContentType;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Auth"></param>
        /// <returns></returns>
        public void DoGET(ClientLogin Auth)
        {
            string retVal = string.Empty;
            this.YouTubeRequest.Method = HTTPMethods.GET.ToString();
            this.YouTubeRequest.Headers.Add("Authorization", "GoogleLogin auth=" + Auth.Auth);
            this.YouTubeRequest.Headers.Add("X-GData-Key", "key=" + Auth.DeveloperKey);
            this.YouTubeRequest.BeginGetResponse(ReadCallbackResponse, this);

            //using (HttpWebResponse response = (HttpWebResponse)this.YouTubeRequest.GetResponse())
            //{
            //    using (StreamReader streamRd = new StreamReader(response.GetResponseStream()))
            //    {
            //        retVal = streamRd.ReadToEnd();                    
            //    }                
            //}
            //return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Auth"></param>
        /// <param name="POSTData"></param>
        /// <returns></returns>
        public void DoPOST(ClientLogin Auth, MemoryStream POSTDt)
        {

            string retVal = string.Empty;
            this.YouTubeRequest.Method = HTTPMethods.POST.ToString();
            this.YouTubeRequest.Headers.Add("Authorization", "GoogleLogin auth=" + Auth.Auth);
            this.YouTubeRequest.Headers.Add("X-GData-Key", "key=" + Auth.DeveloperKey);
            this.YouTubeRequest.ContentLength = POSTDt.ToArray().Length;
            this.POSTData = POSTDt;
           
            this.YouTubeRequest.BeginGetRequestStream(ReadCallback, this);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asynchronousResult"></param>
        private void ReadCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                YouTubeWebRequest request = (YouTubeWebRequest)asynchronousResult.AsyncState;
                //Console.WriteLine("Working...");
                Stream ms = request.YouTubeRequest.EndGetRequestStream(asynchronousResult);

                int bytesCount = 0;
                Int64 bytesTotalTransfered = 0;
                Int64 bytesTotal = request.POSTData.Length;
                byte[] bytes = new byte[2048];

                YouTubeEventArgs args = new YouTubeEventArgs();
                args.BytesTotal = bytesTotal;

                request.POSTData.Seek(0, SeekOrigin.Begin);

                while ((bytesCount = request.POSTData.Read(bytes, 0, bytes.Length)) > 0)
                {
                    ms.Write(bytes, 0, bytesCount);
                    bytesTotalTransfered += bytesCount;
                    args.BytesTransfered = bytesTotalTransfered;
                    if (this.OnTranfering != null)
                    {
                        this.OnTranfering(this, args);
                    }
                }


                if (this.OnTransferComplete != null)
                {
                    args.Message = "SUCCESS UPLOAD";
                    this.OnTransferComplete(this, args);
                }

                request.YouTubeRequest.BeginGetResponse(ReadCallbackResponse, this);
                //Console.WriteLine("Done");
            }
            catch (Exception ex)
            {
                YouTubeEventArgs args = new YouTubeEventArgs();
                if (this.OnExceptionCatched != null)
                {
                    args.Message = ex.ToString();
                    this.OnExceptionCatched(this, args);
                }
            }                    
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="asynchronousResult"></param>
        private void ReadCallbackResponse(IAsyncResult asynchronousResult)
        {
            
            YouTubeWebRequest request = (YouTubeWebRequest)asynchronousResult.AsyncState;            
            WebResponse webResp = request.YouTubeRequest.EndGetResponse(asynchronousResult);
            String message = "";            
            try
            {                
                using (StreamReader streamRd = new StreamReader(webResp.GetResponseStream()))
                {                    
                    message = streamRd.ReadToEnd();
                    //Console.WriteLine(message);
                }
                
                YouTubeEventArgs args = new YouTubeEventArgs();
                if (this.OnReceveResponseComplete != null)
                {                    
                    args.Message = message;
                    this.OnReceveResponseComplete(this, args);
                }
            }
            catch (WebException ex)
            {
                using (StreamReader streamRd = new StreamReader(ex.Response.GetResponseStream()))
                {
                    //Console.WriteLine("web exception\n" + streamRd.ReadToEnd());                    
                    
                    if (this.OnExceptionCatched != null)
                    {
                        YouTubeEventArgs args = new YouTubeEventArgs();
                        args.Message = streamRd.ReadToEnd();
                        this.OnExceptionCatched(this, args);
                    }
                }
            }
            
        }
        
        
    }

    
}
