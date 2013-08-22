using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client81
{
    // This class gets and queue request from users and also give data in response to each request.
    class CustomerDepartment
    {
        private string status;
        private string url;
        private string data;
        private string decompressedData;

        public string DecompressedData
        {
            get { return decompressedData; }
            set { decompressedData = value; }
        }

        public string browseStatus
        {
            get{return status;}
            set { status = value; }
        }

        public string stringURL
        {
            get
            {
                return url;
            }
            set
            {
                if (value.Contains("http://") | value.Contains("https://"))
                {
                    url = value;
                }
                else
                {
                    url = "http://" + value;
                }
            }
        }

        public string dataReceived
        {
            get { return data; }
            set { data = value; }
        }

        private CallerDepartment CallerDepartment;

        internal CompressionDepartment CompressionDepartment;
        
        public async Task GetUri()
        {
            CallerDepartment = new CallerDepartment();
            
            await CallerDepartment.SendRequest(stringURL);

            CompressionDepartment = new Client81.CompressionDepartment();
            CompressionDepartment.DecompressBytes(CallerDepartment.ReceivedData);

            DecompressedData = CompressionDepartment.DataDecompressed;
            //Gets page from server here and store to data
            //data = CallerDepartment.SendRequest(url);
            
        }
    }
}
