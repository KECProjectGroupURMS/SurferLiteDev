using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using System.IO;
using System.IO.Compression;

namespace WCFServiceSurferlite
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceSurferlite" in both code and config file together.
    [ServiceContract]
    public interface IServiceSurferlite
    {
        [OperationContract]
        List<string> GetHrefLinks(string url);

        [OperationContract]
        List<string> GetData(string url);

        [OperationContract]
        void ServiceDownloadTest();

        [OperationContract]
        //GZipStream GetHtml(Uri URL);<-- not supported
        Stream GetHtml(Uri URL);
        //int GetHtml(Uri URL);

        [OperationContract]
        void getdatafromPack();

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
