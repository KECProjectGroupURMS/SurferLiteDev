using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using System.Net;
using MHTMLBuilder;
using System.IO;

//Added for remote webpage manipulation
using HtmlAgilityPack;
using System.IO.Compression;
using System.Web.Hosting;
using System.Diagnostics;

namespace WCFServiceSurferlite
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IServiceSurferlite
    {
        public List<string> GetHrefLinks(string url)
        {
            //var webDocument = new HtmlDocument();
            //webDocument.Load(GetHtml(url));

            // Get a page from remote server
            var webGet = new HtmlWeb();
            var webDocument = webGet.Load(url);

            var linksOnPage = from lnks in webDocument.DocumentNode.Descendants()
                              where lnks.Name == "a" &&
                                    lnks.Attributes["href"] != null &&
                                    lnks.InnerText.Trim().Length > 0
                              select new
                              {
                                  Url = lnks.Attributes["href"].Value,
                                  Text = lnks.InnerText
                              };

            List<string> newList = new List<string>();
            foreach (var item in linksOnPage)
            {

                //newList.Add(item.Url+" [[[[["+item.Text+"]]]]]");
                //For now let's just pick Url
                newList.Add(item.Url);
            }

            return newList;
        }
        public List<string> GetData(string url)
        {
            if (url == "http://")
            {
                url = "http://www.microsoft.com";
            }
            // Get a page from remote server
            var webGet = new HtmlWeb();
            var document = webGet.Load(url);

            var metaTags = document.DocumentNode.SelectNodes("//meta");

            List<string> output = new List<string>();



            if (metaTags != null)
            {
                foreach (var tag in metaTags)
                {
                    if (tag.Attributes["name"] != null && tag.Attributes["content"] != null)
                    {
                        output.Add("Name=" + tag.Attributes["name"].Value);
                        output.Add("Content=" + tag.Attributes["content"].Value);
                    }

                }
            }



            // return answer
            return output;
        }

        /// <summary>
        /// Added method to download file from server to store app
        /// </summary>
        //XXXXXXXXXXXXpublic GZipStream GetHtml(Uri URL)
        public Stream GetHtml(Uri URL)
        //public int GetHtml(Uri URL)
        {

            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(URL);
            HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();

            Stream gotStream;
            gotStream = HttpWResp.GetResponseStream();

            //destination stream
            MemoryStream compressedStream = new MemoryStream();
            //getting ready for source
            var sourceStream = new MemoryStream();
            gotStream.CopyTo(sourceStream);
            sourceStream.Close();

            //converting to byte[]
            byte[] newByteArray = sourceStream.ToArray();

            //make new compressor "zip"
            GZipStream zip = new GZipStream(compressedStream, CompressionLevel.Optimal, true);
            zip.Write(newByteArray, 0, newByteArray.Length);
            zip.Close();
            ////////////////////////////////////
            //var theMemStream = new MemoryStream();
            //zip.CopyTo(theMemStream);
            //theMemStream.Close();
            //////////////////////////////////////
            //long a = compressedStream.Length;
            /////////////////////////////////////

            //////////////////////////////////////
            compressedStream.Position = 0;
            return compressedStream;
            //return newStream;
            //XXXXreturn zip;
            //return 1;
            // Get the URL specified



        }

        public void getdatafromPack()
        {
            string text = "<html><a href='thisis.jpg' >Test</a></html>";

            var webDocument = new HtmlDocument();
            webDocument.LoadHtml(text);

            // Get <a> tags that have a href attribute and non-whitespace inner text
            var linksOnPage = from lnks in webDocument.DocumentNode.Descendants()
                              where lnks.Name == "a" &&
                                    lnks.Attributes["href"] != null &&
                                    lnks.InnerText.Trim().Length > 0
                              select new
                              {
                                  Url = lnks.Attributes["href"].Value,
                                  Text = lnks.InnerText
                              };

        }

        /// <summary>
        /// Added Service Operation
        /// </summary>

        public void ServiceDownloadTest()
        {
            ConvertionTesting testing = new ConvertionTesting();

            //string mhtml = testing.GetPageArchiveTest("http://www.google.com");

            //Console.WriteLine("MHTML is \n " + mhtml);

            //testing.SavePageTextTest("http://www.google.com", @"C:\Google_SavePageText.txt");

            //testing.SavePageTest("http://www.google.com", @"Temporary\New.htm");
            //testing.createMHTMLFile2("http://www.google.com", @"C:\FinalTest.mht");

            testing.createHTMLFile("http://www.google.com", @"Temporary\Hacktolive.html");

            //testing.createMHTMLFile2("http://hacktolive.org/wiki/Hacktolive.org", @"Temporary\Hacktolive.mht");
            //testing.createMHTMLFile("http://www.google.com", @"C:\NewTests.mht");

            //const string testString = @"----=_NextPart_000_00";

            //String s2 =  testString;

            //Console.WriteLine("new line is" + Environment.NewLine);
            //Console.WriteLine("S2 is:" + s2);

            Console.ReadLine();
        }


        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }

    internal class ConvertionTesting
    {
        private Builder MHTMLBuilder = new Builder();

        public String createMHTMLFile2(String URL, String fileLocation)
        {
            String path = String.Empty;

            try
            {
                MHTMLBuilder.CreateMHTML(URL, fileLocation);

                //Console.ReadLine();
            }
            catch (CustomException.BuilderInvalidFileNameException e)
            {
                Console.WriteLine("BuilderInvalidFileNameException is " + e.CustomMessage);
            }
            catch (CustomException.BuilderDownLoadHTMLException e)
            {
                Console.WriteLine("BuilderDownLoadHTMLException " + e.CustomMessage);
            }

            return path;
        }


        public String createMHTMLFile(String URL, String fileLocation)
        {
            String path = String.Empty;

            try
            {
                path = MHTMLBuilder.SavePageArchive(fileLocation, Builder.FileStorage.DiskPermanent, URL);
                Console.WriteLine(path);
                //Console.ReadLine();
            }
            catch (CustomException.BuilderInvalidFileNameException e)
            {
                Console.WriteLine("BuilderInvalidFileNameException is " + e.CustomMessage);
            }
            catch (CustomException.BuilderDownLoadHTMLException e)
            {
                Console.WriteLine("BuilderDownLoadHTMLException " + e.CustomMessage);
            }

            return path;
        }

        public String createHTMLFile(String URL, String fileLocation)
        {
            String path = String.Empty;

            try
            {
                path = MHTMLBuilder.SavePageComplete(fileLocation, URL);
                Console.WriteLine(path);
                //Console.ReadLine();
            }
            catch (CustomException.BuilderInvalidFileNameException e)
            {
                Console.WriteLine("BuilderInvalidFileNameException is " + e.CustomMessage);
            }
            catch (CustomException.BuilderDownLoadHTMLException e)
            {
                Console.WriteLine("BuilderDownLoadHTMLException " + e.CustomMessage);
            }
            finally
            {
                Debug.WriteLine("OK");
            }

            return path;
        }


        public String SavePageTest(String URL, String fileLocation)
        {
            String path = String.Empty;

            try
            {
                path = MHTMLBuilder.SavePage(fileLocation, URL);
                Console.WriteLine(path);
                //Console.ReadLine();
            }
            catch (CustomException.BuilderInvalidFileNameException e)
            {
                Console.WriteLine("BuilderInvalidFileNameException is " + e.CustomMessage);
            }
            catch (CustomException.BuilderDownLoadHTMLException e)
            {
                Console.WriteLine("BuilderDownLoadHTMLException " + e.CustomMessage);
            }

            return path;
        }

        public String SavePageTextTest(String URL, String fileLocation)
        {
            String path = String.Empty;

            try
            {
                path = MHTMLBuilder.SavePageText(fileLocation, URL);
                Console.WriteLine(path);
            }
            catch (CustomException.BuilderInvalidFileNameException e)
            {
                Console.WriteLine("BuilderInvalidFileNameException is " + e.CustomMessage);
            }
            catch (CustomException.BuilderDownLoadHTMLException e)
            {
                Console.WriteLine("BuilderDownLoadHTMLException " + e.CustomMessage);
            }

            return path;
        }


        public String GetPageArchiveTest(String URL)
        {
            String MHTMLBody = String.Empty;

            try
            {
                MHTMLBody = MHTMLBuilder.GetPageArchive(URL);
                Console.WriteLine(MHTMLBody);
            }
            catch (CustomException.BuilderInvalidFileNameException e)
            {
                Console.WriteLine("BuilderInvalidFileNameException is " + e.CustomMessage);
            }
            catch (CustomException.BuilderDownLoadHTMLException e)
            {
                Console.WriteLine("BuilderDownLoadHTMLException " + e.CustomMessage);
            }

            return MHTMLBody;
        }


    }
}
