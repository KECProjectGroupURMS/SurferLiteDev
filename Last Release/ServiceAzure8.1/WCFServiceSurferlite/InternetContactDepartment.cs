using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// for network support for example HttpWebRquest
using System.Net;

// for streams
using System.IO;
using HtmlAgilityPack;

namespace WCFServiceSurferlite
{
    public class InternetContactDepartment
    {
        internal string currentAbsoluteUri;
        public string CurrentAbsoluteUri
        {
            get { return currentAbsoluteUri; }
            set { currentAbsoluteUri = value; }
        }


        private byte[] newReceivedByteArray;
        public byte[] NewReceivedByteArray
        {
            get { return newReceivedByteArray; }
            set { newReceivedByteArray = value; }
        }

        public void SendReceiveRequest(Uri url)
        {
            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(url);
            HttpWReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.6; Windows NT 6.1; Trident/5.0; InfoPath.2; SLCC1; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET CLR 2.0.50727) 3gpp-gba UNTRUSTED/1.0";
            //HttpWReq.MaximumAutomaticRedirections = 1;
            //HttpWReq.AllowAutoRedirect = true;
            CurrentAbsoluteUri= HttpWReq.Address.AbsoluteUri;
            HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();

            Stream gotStream;
            gotStream = HttpWResp.GetResponseStream();

            //getting ready for source
            var sourceStream = new MemoryStream();
            gotStream.CopyTo(sourceStream);
            sourceStream.Close();

            //converting to byte[]
            NewReceivedByteArray = sourceStream.ToArray();
        }

        public void GetFullPage(string url)
        {
            var document = new HtmlWeb().Load(url);
            var urls = document.DocumentNode.Descendants("img")
                                            .Select(e => e.GetAttributeValue("src", null))
                                            .Where(s => !String.IsNullOrEmpty(s));
        }
    }
}