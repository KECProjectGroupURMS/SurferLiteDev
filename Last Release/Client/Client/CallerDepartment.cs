using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    // This class contacts to server with request got from customer
    class CallerDepartment
    {
        internal ServiceReferenceAzure.ServiceSurferliteClient clientStaff;

        public CallerDepartment()
        {
            clientStaff = new ServiceReferenceAzure.ServiceSurferliteClient();
            clientStaff.GetDataAsync(0);
        }        
    }
}
