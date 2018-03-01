using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GM.Shared.Utilities
{
    public static class Directories
    {
        /* Example:
            string sourceDirectory = @"c:\sourceDirectory";
            string targetDirectory = @"c:\targetDirectory";
            Copy(sourceDirectory, targetDirectory);
        */
        public static void Copy(string sourceDirectory, string targetDirectory)
            {
                DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
                DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

                CopyAll(diSource, diTarget);
            }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Directories.cs - Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        public static bool Create(string folder, bool deleteOldFolder = false)
        {
            if (deleteOldFolder)
            {
                Delete(folder);
            }
            if (Directory.Exists(folder))
            {
                return false;
            }
            else
            {
                Directory.CreateDirectory(folder);
                return true;
            }
        }

        public static bool Delete(string folder)
        {
            if (Directory.Exists(folder))
            {
                DirectoryInfo di = new DirectoryInfo(folder);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                di.Delete();
                return true;
            }
            else
            {
                return false;
            }

        }
        public static void DeleteAndCreateDirectory(string folder)
        {
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }
            Directory.CreateDirectory(folder);
        }


    }
}
