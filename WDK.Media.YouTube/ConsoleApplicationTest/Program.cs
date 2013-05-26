using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using YouTubeAPI;
using iTunesRSS;
using System.Xml;
using System.Xml.Serialization;
using XMLSerialization;
using YouTubeAPI.Utils;

namespace ConsoleApplicationTest
{
    
    class Program
    {
        public static void YouTube()
        {
            YouTubeService youTube = new YouTubeService("AI39si6FVg_zhgRNm_kODUN80kTgzXUgF1qMrlzr9VpMj4GlCo6KkLxNVccugM4sV5b0b6gxIpg6rO0hjp82sDeY0YJpIfZmVw");
            youTube.Login("Jhotest", "jhotest", "simple");
            youTube.OnTranferingProgress += new EventHandler<YouTubeEventArgs>(youTube_OnTranferingProgress);
            //youTube.RetriveVideo();
            YouTubeVideoFileInfo newFileInfo = new YouTubeVideoFileInfo()
            {
              FilePath = @"e:\MYFOLDERS\Visual Studio 2008\Projects\VideoUploader\Bin\test.flv",
              Title = "Test video title",
              Description = "Description for my video",
              Category = YouTubeCategories.PetsAndAnimals,
              Keywords = "test, .net, library, programing"              
            };
            youTube.UploadVideo(newFileInfo);           

            
        }

        static void youTube_OnTranferingProgress(object sender, YouTubeEventArgs e)
        {
            Console.WriteLine("Transered: {0}", e.BytesTransfered);
        }
        public static void TestEntryFeed()
        {
            YouTubeAPI.Feed.entry entry = new YouTubeAPI.Feed.entry(YouTubeAPI.Feed.feedType.upload);
            entry.group.title.type = "plain";
            entry.group.title.Text = "Bad Wedding Toast";
            entry.group.description.type = "plain";
            entry.group.description.Text = "I gave a bad toast at my friend's wedding.";
            entry.group.category.scheme = "http://gdata.youtube.com/schemas/2007/categories.cat";
            entry.group.category.Text = EnumToString.Get(typeof(YouTubeCategories), YouTubeCategories.PeopleAndBlogs);
            entry.group.keywords.listKeywords.Add("toast");
            entry.group.keywords.listKeywords.Add("wedding");
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("media", "http://search.yahoo.com/mrss/");
            namespaces.Add("yt", "http://gdata.youtube.com/schemas/2007/");
            string strSRZD = XmlSerializationUtility.ObjectToXmlString(entry, "http://www.w3.org/2005/Atom", namespaces);
            Console.WriteLine(strSRZD);

        }
        static void Main(string[] args)
        {
            //TestEntryFeed();
            YouTube();
            //Feed feed = new Feed("Mega title");
            //feed.GenerateFeed();
            //iTunesFeed feed = new iTunesFeed();             
            //feed.Title = new System.ServiceModel.Syndication.TextSyndicationContent("Title");
            //feed.Categories.Add(new System.ServiceModel.Syndication.SyndicationCategory("Technology"));
            //iTunesFeedItem item = new iTunesFeedItem();
            //item.Title = new System.ServiceModel.Syndication.TextSyndicationContent("");
            //feed.Items.Add(item);
            //XmlWriter xmlWr = XmlWriter.Create("test.xml");
            //feed.SaveiTunesFeed(xmlWr);
            //xmlWr.Close();

            Console.WriteLine("Ready");
            Console.ReadLine();
        }
    }
}
