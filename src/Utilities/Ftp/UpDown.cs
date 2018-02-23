using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;

/*
 * https://docs.microsoft.com/en-us/dotnet/framework/network-programming/how-to-download-files-with-ftp
 */

namespace Ftp
{
    public class UpDown
    {
        string username;
        string password;
        string address;


        public void Test()
        {
            UpDown updown = new UpDown("ftp://govmeeting.org", "johnpankowicz", "mypassword");
            updown.PostDatatoFTP("TestFile0.txt", @"E:\yourlocation\SampleFile.txt");
         }

        public UpDown(string _address, string _username, string _password)
        {
            address = _address;
            username = _username;
            password = _password;
        }

        public void PostDatatoFTP(string input, string output)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(address + @"\" + output);
                request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(username, password);
                // Copy the contents of the file to the request stream.  
                StreamReader sourceStream = new StreamReader(input);
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

                response.Close();
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message.ToString());
                String status = ((FtpWebResponse)e.Response).StatusDescription;
                Console.WriteLine(status);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }


        public void GetDatafromFtp()
        {
            // Get the object used to communicate with the server.  
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://www.contoso.com/test.htm");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // This example assumes the FTP site uses anonymous logon.  
            request.Credentials = new NetworkCredential(username, password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            Console.WriteLine(reader.ReadToEnd());

            Console.WriteLine("Download Complete, status {0}", response.StatusDescription);

            reader.Close();
            response.Close();
        }

    }
}
