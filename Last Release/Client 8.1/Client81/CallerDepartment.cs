using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Stream
using System.IO;

namespace Client81
{
    // This class contacts to server with request got from customer
    class CallerDepartment
    {
        //internal ServiceReferenceAzure.ServiceSurferliteClient client;
        internal ServiceReferenceAzureLocal.ServiceSurferliteClient client;
        internal byte[] receivedData;
        public byte[] ReceivedData
        {
            get { return receivedData; }
            set { receivedData = value; }
        }

        internal string ans;
        internal string length;

        public CallerDepartment()
        {
            client = new ServiceReferenceAzureLocal.ServiceSurferliteClient();
            //client = new ServiceReferenceAzure.ServiceSurferliteClient();
            ans = "true";
        }

        public async Task SendRequest(string stringURL)
        {
            Uri url = new Uri(stringURL);
            receivedData = await client.GetDataAsync(url);
            length=receivedData.Length.ToString();

            this.ReceivedData = this.receivedData;
            //receivedData = await clientStaff.GetDataAsync(new Uri(stringURL));
        }

        public async void SaveDataToCloud()
        {
            //await client.SaveDataToCloudAsync("Log");
            //string ans = await client.SaveDataToCloudAsync("Log");
        }
    }
}
