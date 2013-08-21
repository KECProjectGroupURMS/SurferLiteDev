using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceSurferlite
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceSurferLite : IServiceSurferlite
    {
        ClientContactDepartment clientDep;

        public string GetData(Uri url)
        {
            // TEST: DELETE LATER
            LogDepartment.Log("Request Got for: "+url.ToString());
            clientDep = new ClientContactDepartment();

            
            clientDep.InternetContactDepartment.SendReceiveRequest(url);
            return string.Format("You entered: {0}", url.ToString());
        }

        public string SaveDataToCloud(Object data)
        {
            clientDep.UserDataStoreDepartment.SaveInfo(data);
            return "Success";
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
}
