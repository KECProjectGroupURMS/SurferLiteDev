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
                if (value.Contains("http://"))
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

        internal CallerDepartment CallerDepartment
        {
            get
            {
                return new CallerDepartment();
            }
            set
            {
            }
        }

        internal CompressionDepartment CompressionDepartment
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        
        internal void GetUri()
        {
            //Gets page from server here and store to data
            data = CallerDepartment.SendRequest(url);
        }
    }
}
