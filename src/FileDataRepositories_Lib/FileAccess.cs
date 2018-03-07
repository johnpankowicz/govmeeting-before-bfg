using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GM.FileDataRepositories
{
    public static class FileAccess
    {
        public static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name));
        }
        public static string Readfile(string path)
        {
            try
            {
                string text = System.IO.File.ReadAllText(path);
                return text;
            }
            catch (Exception e)
            {
                Console.WriteLine("Common.cs - The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static void CopyDirectories(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyDirectoryRecursively(diSource, diTarget);
        }

        public static void CopyDirectoryRecursively(DirectoryInfo source, DirectoryInfo target)
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
                CopyDirectoryRecursively(diSourceSubDir, nextTargetSubDir);
            }
        }

        public static bool CreateDirectory(string folder, bool deleteOldFolder = false)
        {
            if (deleteOldFolder)
            {
                DeleteDirectoryRecursively(folder);
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

        public static bool DeleteDirectoryRecursively(string folder)
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
