using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.Net;
using MHTMLBuilder;
using System.IO;

//Added for remote webpage manipulation
using HtmlAgilityPack;

namespace WcfServiceLibraryServerForQuickTest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ServiceOnAzure : IService1
    {

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
                        output.Add("Name="+tag.Attributes["name"].Value);
                        output.Add("Content="+tag.Attributes["content"].Value);
                    }
                
                }
            }

            
            
            // return answer
            return output;
        }

        /// <summary>
        /// Added method to download file from server to store app
        /// </summary>
        public Stream GetHtml()
        {
            
            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create("http://www.contoso.com");
            HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();

            Stream objStream;
            objStream = HttpWResp.GetResponseStream();
  
            return objStream;
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
            testing.SavePageTest("http://www.google.com", @"C:\Google_SavePage.html");
            //testing.createMHTMLFile2("http://www.google.com", @"C:\FinalTest.mht");
            //testing.createHTMLFile("http://www.google.com", @"C:\RezaSavePageComplete.html");

            //testing.createMHTMLFile2("http://www.google.com", @"C:\NewTests.mht");
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
