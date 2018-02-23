using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;

/*
 * http://www.c-sharpcorner.com/UploadFile/0d5b44/ftp-using-C-Sharp-net/
 */

/*
static void Main(string[] args)  
        {  
            Program.GetallContents();  
            Program.PostDatatoFTP(0);  
            Program.GetDataFromFTP();  
        }  
 */

namespace Ftp
{
    class FTPOperations
    {
        void ops()
        {

            // Step 1: Connection Establishment

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://Hostname.com/");
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            // This example assumes the FTP site uses anonymous logon.  
            request.Credentials = new NetworkCredential("maruthi", "******");
            request.KeepAlive = false;
            request.UseBinary = true;
            request.UsePassive = true;


            // Step 2: Connection Behavior

            request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);



            // Step 3: Read the Contents - It will show all the contents and directories of your FTP Drive

            try
            {
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;


                request.Credentials = new NetworkCredential("maruthi", "******");
                request.KeepAlive = false;
                request.UseBinary = true;
                request.UsePassive = true;


                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                Console.WriteLine(reader.ReadToEnd());

                Console.WriteLine("Directory List Complete, status {0}", response.StatusDescription);

                reader.Close();
                response.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            // Get and open the following content only,

            try
            {
                //FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://Hostname.com/6.txt");
                //request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);

                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = new NetworkCredential("maruthi", "******");

                request.KeepAlive = false;
                request.UseBinary = true;
                request.UsePassive = true;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(responseStream);

                Console.WriteLine(reader.ReadToEnd());

                Console.WriteLine("Download Complete", response.StatusDescription);

                reader.Close();

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
            // Step 4: Post a file

        public static void PostDatatoFTP(int i)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://Hostname.com" + @"\" + "TestFile0.txt");
                request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential("maruthi", "******");
                // Copy the contents of the file to the request stream.  
                StreamReader sourceStream = new StreamReader(@"E:\yourlocation\SampleFile.txt");
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

        /*
         *     FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://Hostname.com/TestFile.txt");   
         * To read all file content.
         */

    }
}
