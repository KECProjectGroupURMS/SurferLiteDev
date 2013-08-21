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

        internal CallerDepartment CallerDepartment
        {
            get
            {
                throw new System.NotImplementedException();
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

        private Uri UrlModifier(string URLString)
        {
            return new Uri("https://www.google.com/");
        }


        internal void GetUri()
        {
            status = "Completed";
        }
    }
}
