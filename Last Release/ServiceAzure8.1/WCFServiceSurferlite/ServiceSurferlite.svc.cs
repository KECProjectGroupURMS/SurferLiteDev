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
        UserDataStoreDepartment userData;
        InternetContactDepartment internetContact;
        CompressorDepartment comDep;
        DataAnalyzeModifyFilterDepartment DAMFD;
        string sizeOriginal;
        string sizeResult;

        public Stream GetData(Uri url)
        //public string GetData(Uri url)
        {
            try
            {
                // TEST: DELETE LATER
                try {
                    LogDepartment.Log("Request Got for: " + url.ToString());
                }
                catch
                {
                }
                
                internetContact = new InternetContactDepartment();
                internetContact.SendReceiveRequest(url);
                sizeOriginal = internetContact.NewReceivedByteArray.Length.ToString();

                DAMFD =new DataAnalyzeModifyFilterDepartment();
                DAMFD.RemoveScriptsStyleComment(internetContact.NewReceivedByteArray);

                DAMFD.RemoveHrefs(internetContact.NewReceivedByteArray);

                comDep = new CompressorDepartment();
                //comDep.CompressBytes(internetContact.NewReceivedByteArray);
                comDep.CompressBytes(DAMFD.ModifiedByte);

                sizeResult = comDep.CompressedStream.Length.ToString();

                //return comDep.CompressedStream.Length.ToString();
                return comDep.CompressedStream;
            }
            catch (Exception e)
            {
                string test = "Error: "+e.ToString();
                byte[] byteArray = Encoding.ASCII.GetBytes(test);

                comDep = new CompressorDepartment();
                comDep.CompressBytes(byteArray);

                return comDep.CompressedStream;
            }
        }

        //public Stream GetData(Uri url)
        public string GetDataTest(string uri)
        {
            try
            {
                Uri url = new Uri(uri);
                // TEST: DELETE LATER
                try
                {
                    LogDepartment.Log("Request Got for: " + url.ToString());
                }
                catch
                {
                }

                internetContact = new InternetContactDepartment();
                internetContact.SendReceiveRequest(url);

                comDep = new CompressorDepartment();
                comDep.CompressBytes(internetContact.NewReceivedByteArray);

                //return comDep.CompressedStream.Length.ToString();
                return comDep.CompressedStream.ToString();
            }
            catch(Exception e)
            {
                return e.ToString()+"Error in execution of the server codes.";
            }
            
        }

        //public string SaveDataToCloud(Object data)
        public string SaveDataToCloud(string filename="Log")
        {
            try
            {
                userData = new UserDataStoreDepartment();
                userData.SaveInfo("username", "password", filename);

                try
                {
                    LogDepartment.Log("Save data cloud to success: " + filename);
                }
                catch
                {
                }
                
                return "Success";
            }
            catch(Exception e)
            {
                return e.ToString()+" Unsucess";
            }
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
