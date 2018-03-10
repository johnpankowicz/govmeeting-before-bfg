using GM.OnlineAccess;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace GM.Ftp
{

    /* This will be a utility for tranfering file to/from a host via FTP.
     * It is just test code at this point.
     */
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello From FTP app!");

            // Get the FTP login info from the Development appsettings.
            string currentDir = Directory.GetCurrentDirectory();
            string secretsDir = Path.GetFullPath(Path.Combine(currentDir, "../../../_SECRETS")); 

            var builder = new ConfigurationBuilder()
                .SetBasePath(secretsDir)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            Console.WriteLine(configuration["Host:FtpAddress"]);

            string username = configuration["Host:FtpUsername"];
            string password = configuration["Host:FtpPassword"];
            string ftpAddress = configuration["Host:FtpAddress"];

            // Test it.
            string name = "file01.txt";
            string path = @"C:\TMP\";
            string contents = "aaaaa\nbbbb\ncccc\n";
            File.WriteAllText(path + name, contents);

            FTPOperations ops = new FTPOperations(username, password, ftpAddress);
            string listing = ops.ListFtpDrive();
            Console.Write(listing);
            ops.WriteFileToFtp(path + name, name);
            ops.ReadFileFromFtp(name, path + "newfile01.txt");
        }
    }
}
