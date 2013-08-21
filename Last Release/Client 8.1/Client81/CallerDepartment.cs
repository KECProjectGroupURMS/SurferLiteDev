using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client81
{
    // This class contacts to server with request got from customer
    class CallerDepartment
    {
        internal ServiceReferenceAzure.ServiceSurferliteClient clientStaff;
        internal string receivedData;

        public CallerDepartment()
        {
            clientStaff = new ServiceReferenceAzure.ServiceSurferliteClient();
        }

        internal string SendRequest(string stringURL)
        {
            try{
                receivedData = "New";
                return receivedData;
            }
            catch { return "NoNetwork"; }
        }
    }
}
