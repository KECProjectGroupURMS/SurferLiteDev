using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// for azure storage
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace WCFServiceSurferlite
{
    public class UserDataStoreDepartment
    {
        //internal void SaveInfo(object data)
        public void SaveInfo(string username, string password, string filename)
        {
            try
            {
                // Variables for the cloud storage objects.
                CloudStorageAccount cloudStorageAccount;
                CloudBlobClient blobClient;
                CloudBlobContainer blobContainer;
                BlobContainerPermissions containerPermissions;
                CloudBlob blob;

                // Use the emulatedstorage account.
                //cloudStorageAccount = CloudStorageAccount.DevelopmentStorageAccount;

                // If you want to use Windows Azure cloud storage account, use the following
                // code (after uncommenting) instead of the code above.
                cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=portalvhdsttynh6yyg3st7;AccountKey=98geuLlPFP8Q9Va1XSGj8PQOLcWgp2dpnwaf0/ci1yEcfd16ILT59xVh4+Inq200+BncQo4vxYKA8n7oQWK2Ig==");

                // Create the blob client, which provides
                // authenticated access to the Blob service.
                blobClient = cloudStorageAccount.CreateCloudBlobClient();

                // Get the container reference.
                // this container name must be small letters only
                blobContainer = blobClient.GetContainerReference("surferlitecontainer");
                // Create the container if it does not exist.
                blobContainer.CreateIfNotExist();

                // Set permissions on the container.
                containerPermissions = new BlobContainerPermissions();
                // This sample sets the container to have public blobs. Your application
                // needs may be different. See the documentation for BlobContainerPermissions
                // for more information about blob container permissions.
                containerPermissions.PublicAccess = BlobContainerPublicAccessType.Blob;
                blobContainer.SetPermissions(containerPermissions);

                // Get a reference to the blob.
                blob = blobContainer.GetBlobReference(filename+".txt");

                // Upload a file from the local system to the blob.
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/"+filename+".txt");

                blob.UploadFile(filePath);  // File from emulated storage.
                string a=blob.DownloadText();
            }
            catch (StorageClientException e)
            {
                //Console.WriteLine("Storage client error encountered: " + e.Message);

                // Exit the application with exit code 1.\
            }
            catch (Exception e)
            {
                //Console.WriteLine("Error encountered: " + e.Message);

                // Exit the application with exit code 1.
            }
            finally
            {
                // Exit the application.
            }
        }
    }
}