using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

/*
 * http://www.c-sharpcorner.com/blogs/download-file-from-ftp-with-subdirectories-using-c-sharp
 */
namespace Ftp
{
    class FtpTransfer
    { 

        public void DownloadFile()
        {
            string dirpath = "";
            string ftpadress = "";
            string username = "";
            string password = "";

            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create("ftp://xxx.xxx.xx.xx/"); // FTP Address  
                ftpRequest.Credentials = new NetworkCredential(username, password); // Credentials  
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                List<string> directories = new List<string>(); // create list to store directories.   
                string line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    directories.Add(line); // Add Each Directory to the List.  
                    line = streamReader.ReadLine();
                }
                using (WebClient ftpClient = new WebClient())
                {
                    ftpClient.Credentials = new NetworkCredential(username, password);
                    string[] filename = ftpadress.Split('/');
                    //Filename = filename[filename.Length - 1];
                    for (int k = 0; k <= filename.Length - 1; k++)
                    {
                        dirpath = dirpath + "\\" + filename[k];
                        createdir(dirpath);
                    }
                    try
                    {
                        for (int i = 0; i <= directories.Count - 1; i++)
                        {
                            if (directories[i].Contains("."))
                            {
                                string path = "ftp://xxx.xxx.xx.xx//" + ftpadress + "/" + directories[i].ToString();
                                ftpClient.DownloadFile(path, dirpath.ToString() + "\\" + directories[i].ToString());
                            }
                            else
                            {
                                string path = "ftp://xxx.xxx.xx.xx//" + ftpadress + "/" + directories[i].ToString();
                                string subdirpath = dirpath + "\\" + directories[i].ToString();
                                createdir(subdirpath);
                                string[] subdirectory = Return(path, username, password);
                                for (int j = 0; j <= subdirectory.Length - 1; j++)
                                {
                                    string subpath = directories[i] + "/" + subdirectory[j];
                                    directories.Add(subpath); // Add the Sub-directories with the path to directories.  
                                }
                            }
                        }
                    }
                    catch (WebException e)
                    {
                        String status = ((FtpWebResponse)e.Response).StatusDescription;
                        Console.WriteLine(status.ToString());
                    }
                }
                streamReader.Close();
            }
            catch (WebException l)
            {
                String status = ((FtpWebResponse)l.Response).StatusDescription;
                Console.WriteLine(status.ToString());
            }
        }
        // Here i get the list of Sub-directories and the files.   
        public string[] Return(string filepath, string username, string password)
        {
            List<string> directories = new List<string>();
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(filepath);
                ftpRequest.Credentials = new NetworkCredential(username, password);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    directories.Add(line);
                    line = streamReader.ReadLine();
                }
            }
            catch (WebException e)
            {
                String status = ((FtpWebResponse)e.Response).StatusDescription;
                Console.WriteLine(status.ToString());
            }
            return directories.ToArray();
        }
        // In this part i create the sub-directories.   
        public void createdir(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

    }
}
