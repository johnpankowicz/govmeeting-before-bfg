//using Google.Apis.Auth.OAuth2;
//using Google.Apis.Auth.OAuth2.Flows;
//using Google.Apis.Auth.OAuth2.Responses;
//using Google.Apis.Services;
//using Google.Apis.Storage.v1;
//using Google.Apis.Upload;
//using Google.Apis.Util.Store;
//using Grpc.Core;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using System.Threading;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GM.Backend.ProcessRecordingLib
{
    // http://deepinsightsdotnet.blogspot.com/2014/12/tutorial-using-google-cloud-storage.html

    public class GoogleCloudStorage
    {
        // Setup the basic items you need for making the calls:

        private static ClientSecrets _clientSecrets = new ClientSecrets();
        private static UserCredential _userCredential = null;
        private static StorageService _storageService = new StorageService();

        private const string ClientId = "<client id>";
        private const string ClientSecret = "<client secret>";
        private static string[] Scopes = new[] { @"https://www.googleapis.com/auth/devstorage.full_control" };

        // You need to go to the google console to get the client id and secret. Go to APIs and auth, Credentials and create a new client ID.
        // Mine is a general windows application (not a service) so I selected that option.
        // It will ask you to configure stuff in between, go ahead and do that as well.

       public async System.Threading.Tasks.Task GetToken()
        {
            // Get the token from the API using the below code in your method:

            _clientSecrets.ClientId = ClientId;
            _clientSecrets.ClientSecret = ClientSecret;

            CancellationTokenSource cts = new CancellationTokenSource();
            _userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(_clientSecrets, Scopes, "<your email account>", cts.Token);

            //You need to do this to handle token expiry.
            //await RefreshTokenIfExpired(cts);
        }

        public void GetListOfBuckets()
        {
            // Use the below code to get the list of buckets in your project:

            var bucketsQuery = _storageService.Buckets.List("<project name>");
            bucketsQuery.OauthToken = _userCredential.Token.AccessToken;

            var buckets = bucketsQuery.Execute();
            string bucketName = buckets.Items[0].Name;

            // The project name above is the weird string which Google generates for you when you create your project.
            // It is is not the friendly name you gave the project.
        }


        // To upload the file:
        public async Task UploadAsync(string bucketName, string filename) {

            Google.Apis.Storage.v1.Data.Object blob = null;
            ObjectsResource.InsertMediaUpload uploadRequest = null;

            blob = new Google.Apis.Storage.v1.Data.Object();
            blob.Bucket = bucketName;
            blob.Name = "keyName";

            using (var fileStream = new FileStream(filename, FileMode.Open))
            {
                uploadRequest = new ObjectsResource.InsertMediaUpload(_storageService, blob, bucketName, fileStream, "text/plain");
                uploadRequest.OauthToken = _userCredential.Token.AccessToken;
                await uploadRequest.UploadAsync();
            }

            // This was in comments - to get status of upload
            //    var response = mediaInsertUpload.Upload();
            //    string status = response.Status.ToString();

            // Remember to refresh the tokens if you do lot of operations and before you go to the next batch.
        }


        // To download the file:
        public void Download(string bucketName)
        { 
            ObjectsResource.GetRequest downloadRequest = null;

            downloadRequest = new ObjectsResource.GetRequest(_storageService, bucketName, "keyName");
            MemoryStream stream = new MemoryStream();
            downloadRequest.Download(stream);

            // I am just downloading to a memory stream. You can download to a file stream as well.

        }


        // To delete the file:
        public void Delete(string bucketName)
        {
            ObjectsResource.DeleteRequest deleteRequest = null;

            deleteRequest = new ObjectsResource.DeleteRequest(_storageService, bucketName, "keyName");
            deleteRequest.OauthToken = _userCredential.Token.AccessToken;
            deleteRequest.Execute();

        }


        // To refresh the token use the code below. The token expires in 3600 seconds
        public static async Task RefreshTokenIfExpired(CancellationTokenSource cts)
        {
            if (_userCredential.Token.IsExpired(Google.Apis.Util.SystemClock.Default))
            {
                await _userCredential.RefreshTokenAsync(cts.Token);
            }
        }
    }
}





////https://stackoverflow.com/questions/29820758/google-cloud-storage-insertmediaupload-service-baseuri-argumentnullexception
//public void insertMediaUpload()
//{
//    GoogleAuthorizationCodeFlow flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer()
//    {
//        ClientSecrets = new ClientSecrets { ClientId = _googleSettings.ClientID, ClientSecret = _googleSettings.ClientSecret },
//        DataStore = new FileDataStore(System.Web.HttpContext.Current.Server.MapPath("/App_Data")),
//        Scopes = new string[] { StorageService.Scope.DevstorageReadWrite },
//    });

//    UserCredential userCredentials = new UserCredential(flow, Environment.UserName, new TokenResponse());

//    StorageService service = new StorageService(new BaseClientService.Initializer()
//    {
//        HttpClientInitializer = userCredentials,
//        ApplicationName = "GCS Sample"
//    });

//    Google.Apis.Storage.v1.Data.Object fileobj = new Google.Apis.Storage.v1.Data.Object() { Name = fileDetails.FileName };

//    ObjectsResource.InsertMediaUpload uploadService = service.Objects.Insert(fileobj, _googleSettings.BucketName, fileDetails.Stream, fileDetails.MimeContentType);

//    IUploadProgress progress = uploadService.Upload();

//    if (progress.Exception != null) //The exception always exists.
//    {
//        throw progress.Exception;
//    }
//}


// https://stackoverflow.com/questions/26196515/uploading-objects-to-google-cloud-storage-buckets-in-c-sharp
//public async System.Threading.Tasks.Task UploadItAsync()
//{
//    static string bucketForImage = ConfigurationManager.AppSettings["testStorageName"];
//    static string projectName = ConfigurationManager.AppSettings["GCPProjectName"];

//    string gcpPath = Path.Combine(Server.MapPath("~/Images/Gallery/"), uniqueGcpName + ext);
//    var clientSecrets = new ClientSecrets();
//    clientSecrets.ClientId = ConfigurationManager.AppSettings["GCPClientID"];
//    clientSecrets.ClientSecret = ConfigurationManager.AppSettings["GCPClientSc"];

//    var scopes = new[] { @"https://www.googleapis.com/auth/devstorage.full_control" };
//    var cts = new CancellationTokenSource();
//    var userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(clientSecrets, scopes, ConfigurationManager.AppSettings["GCPAccountEmail"], cts.Token);
//    var service = new Google.Apis.Storage.v1.StorageService();
//    var bucketToUpload = bucketForImage;
//    var newObject = new Google.Apis.Storage.v1.Data.Object()
//    {
//        Bucket = bucketToUpload,
//        Name = bkFileName
//    };

//    FileStream fileStream = null;
//    try
//    {
//        fileStream = new FileStream(gcpPath, FileMode.Open);
//        var uploadRequest = new Google.Apis.Storage.v1.ObjectsResource.InsertMediaUpload(service, newObject,
//        bucketToUpload, fileStream, "image/" + ext);
//        uploadRequest.OauthToken = userCredential.Token.AccessToken;
//        await uploadRequest.UploadAsync();
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine("GoogleCloudStorage.cs - " + ex.Message);
//    }
//    finally
//    {
//        if (fileStream != null)
//        {
//            fileStream.Dispose();
//        }
//    }
//}






//public async System.Threading.Tasks.Task DoItAsync()
//{
//    string clientId = "799460485959 - lat4g5bsltd55jcq0sm101kqbpfjdn86.apps.googleusercontent.com";
//    string clientSecret = "i4u8PCM95_lHk2N5 - Pz25Bms";

//    var clientSecrets = new ClientSecrets();
//    clientSecrets.ClientId = clientId;
//    clientSecrets.ClientSecret = clientSecret;
//    //there are different scopes, which you can find here https://cloud.google.com/storage/docs/authentication
//    var scopes = new[] { @"https://www.googleapis.com/auth/devstorage.full_control" };

//    var cts = new CancellationTokenSource();
//    var userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(clientSecrets, scopes, "yourGoogle@email", cts.Token);

//    // Sometimes you might also want to refresh authorization token via:
//    // await userCredential.RefreshTokenAsync(cts.Token);

//    var service = new Google.Apis.Storage.v1.StorageService();

//    var newBucket = new Google.Apis.Storage.v1.Data.Bucket()
//    {
//        Name = "your-bucket-name-1"
//    };

//    //var newBucketQuery = service.Buckets.Insert(newBucket, projectName);
//    //newBucketQuery.OauthToken = userCredential.Result.Token.AccessToken;
//    ////you probably want to wrap this into try..catch block
//    //newBucketQuery.Execute();

//    var bucketsQuery = service.Buckets.List(projectName);
//    bucketsQuery.OauthToken = userCredential.Result.Token.AccessToken;
//    var buckets = bucketsQuery.Execute();

//    //enter bucket name to which you want to upload file
//    var bucketToUpload = buckets.Items.FirstOrDefault().Name;
//    var newObject = new Object()
//    {
//        Bucket = bucketToUpload,
//        Name = "some-file-" + new Random().Next(1, 666)
//    };

//    FileStream fileStream = null;
//    try
//    {
//        var dir = Directory.GetCurrentDirectory();
//        var path = Path.Combine(dir, "test.png");
//        fileStream = new FileStream(path, FileMode.Open);
//        var uploadRequest = new Google.Apis.Storage.v1.ObjectsResource.InsertMediaUpload(service, newObject,
//        bucketToUpload, fileStream, "image/png");
//        uploadRequest.OauthToken = userCredential.Result.Token.AccessToken;
//        await uploadRequest.UploadAsync();
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine("GoogleCloudStorage.cs - " + ex.Message);
//    }
//    finally
//    {
//        if (fileStream != null)
//        {
//            fileStream.Dispose();
//        }
//    }
//}
