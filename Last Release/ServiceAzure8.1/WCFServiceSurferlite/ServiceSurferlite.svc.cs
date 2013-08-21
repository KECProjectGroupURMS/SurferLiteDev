using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// For Streams
using System.IO;

namespace WCFServiceSurferlite
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceSurferLite : IServiceSurferlite
    {
        ClientContactDepartment clientDep;
        UserDataStoreDepartment userData;
        InternetContactDepartment internetContact;
        CompressorDepartment comDep;

        public Stream GetData(Uri url)
        //public string GetData(Uri url)
        {
            
            
            // TEST: DELETE LATER
            LogDepartment.Log("Request Got for: " + url.ToString());
            
            internetContact = new InternetContactDepartment();
            internetContact.SendReceiveRequest(url);

            comDep = new CompressorDepartment();
            comDep.CompressBytes(internetContact.NewReceivedByteArray);

            //return comDep.CompressedStream.Length.ToString();
            return comDep.CompressedStream;
        }

        //public string SaveDataToCloud(Object data)
        public string SaveDataToCloud(string filename="Log")
        {
            userData = new UserDataStoreDepartment();
            userData.SaveInfo("username", "password", filename);

            LogDepartment.Log("Save data cloud to success: " + filename);
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
