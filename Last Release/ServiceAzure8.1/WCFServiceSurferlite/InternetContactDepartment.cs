using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// for network support for example HttpWebRquest
using System.Net;

// for streams
using System.IO;

namespace WCFServiceSurferlite
{
    public class InternetContactDepartment
    {
        private byte[] newReceivedByteArray;
        public byte[] NewReceivedByteArray
        {
            get { return newReceivedByteArray; }
            set { newReceivedByteArray = value; }
        }

        public void SendReceiveRequest(Uri url)
        {
            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(url);
            //HttpWReq.MaximumAutomaticRedirections = 1;
            //HttpWReq.AllowAutoRedirect = true;
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
    }
}