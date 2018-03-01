using Google.Cloud.Storage.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GM.Backend.ProcessRecordingLib
{
    class GoogleBucket
    {
        public void UploadFile(string bucketName, string localPath,
            string objectName = null, string contentType = null)
        {
            var storage = StorageClient.Create();
            using (var file = File.OpenRead(localPath))
            {
                objectName = objectName ?? Path.GetFileName(localPath);
                storage.UploadObject(bucketName, objectName, contentType, file);
                Console.WriteLine($"GoogleBucket.cs - Uploaded {objectName}.");
            }
        }
        public void DownloadObject(string bucketName, string objectName,
            string localPath = null)
        {
            var storage = StorageClient.Create();
            localPath = localPath ?? Path.GetFileName(objectName);
            using (var outputFile = File.OpenWrite(localPath))
            {
                storage.DownloadObject(bucketName, objectName, outputFile);
            }
            Console.WriteLine($"GoogleBucket.cs - downloaded {objectName} to {localPath}.");
        }

        void CreateBucket()
        {
            // Your Google Cloud Platform project ID.
            string projectId = "YOUR-PROJECT-ID";


            // Instantiates a client.
            StorageClient storageClient = StorageClient.Create();

            // The name for the new bucket.
            string bucketName = projectId + "-test-bucket";
            try
            {
                // Creates the new bucket.
                storageClient.CreateBucket(projectId, bucketName);
                Console.WriteLine($"GoogleBucket.cs - Bucket {bucketName} created.");
            }
            catch (Google.GoogleApiException e)
            when (e.Error.Code == 409)
            {
                // The bucket already exists.  That's fine.
                Console.WriteLine("GoogleBucket.cs - " + e.Error.Message);
            }
        }


    }
}
