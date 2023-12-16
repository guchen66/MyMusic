using System;
using System.IO;
using System.Linq;

namespace Music.ToolKit.Common
{

    public class DiskManager
    {
        public static string ListAllDrives()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in allDrives)
            {
                if (drive.IsReady)
                {
                    return string.Join(" ", drive.Name);
                }

            }
            return null;
        }

        public void ListItemsInRoot(string driveName)
        {
            if (Directory.Exists(driveName))
            {
                var files = Directory.GetFiles(driveName).Select(Path.GetFileName);
                var directories = Directory.GetDirectories(driveName).Select(Path.GetFileName);

                Console.WriteLine($"Files and directories in {driveName}:");
                foreach (var file in files)
                {
                    Console.WriteLine($"File: {file}");
                }
                foreach (var directory in directories)
                {
                    Console.WriteLine($"Directory: {directory}");
                }
            }
            else
            {
                Console.WriteLine("Drive not found or accessible.");
            }
        }

        public void FindItemsByKeywordInRoot(string driveName, string keyword)
        {
            if (Directory.Exists(driveName))
            {
                var matchingFiles = Directory.GetFiles(driveName)
                                             .Where(file => Path.GetFileName(file).Contains(keyword))
                                             .Select(Path.GetFileName);
                var matchingDirectories = Directory.GetDirectories(driveName)
                                                   .Where(dir => new DirectoryInfo(dir).Name.Contains(keyword))
                                                   .Select(Path.GetFileName);

                Console.WriteLine($"Files and directories in {driveName} containing '{keyword}':");
                foreach (var file in matchingFiles)
                {
                    Console.WriteLine($"File: {file}");
                }
                foreach (var directory in matchingDirectories)
                {
                    Console.WriteLine($"Directory: {directory}");
                }
            }
            else
            {
                Console.WriteLine("Drive not found or accessible.");
            }
        }

        public void ListAllItemsInDrive(string driveName)
        {
            if (Directory.Exists(driveName))
            {
                var allFiles = Directory.GetFiles(driveName, "*", SearchOption.AllDirectories)
                                        .Select(file => Path.GetFileName(file));
                var allDirectories = Directory.GetDirectories(driveName, "*", SearchOption.AllDirectories)
                                              .Select(dir => Path.GetFileName(dir));

                Console.WriteLine($"All files and directories in {driveName}:");
                foreach (var file in allFiles)
                {
                    Console.WriteLine($"File: {file}");
                }
                foreach (var directory in allDirectories)
                {
                    Console.WriteLine($"Directory: {directory}");
                }
            }
            else
            {
                Console.WriteLine("Drive not found or accessible.");
            }
        }

        public void OpenItemInRoot(string driveName, string itemName)
        {
            string fullPath = Path.Combine(driveName, itemName);
            if (File.Exists(fullPath) || Directory.Exists(fullPath))
            {
                System.Diagnostics.Process.Start(fullPath);
            }
            else
            {
                Console.WriteLine($"Item '{itemName}' not found in {driveName}.");
            }
        }

        public void OpenItemInDrive(string driveName, string itemName)
        {
            string fullPath = Directory.GetFiles(driveName, itemName, SearchOption.AllDirectories)
                                       .Union(Directory.GetDirectories(driveName, itemName, SearchOption.AllDirectories))
                                       .FirstOrDefault();
            if (!string.IsNullOrEmpty(fullPath))
            {
                System.Diagnostics.Process.Start(fullPath);
            }
            else
            {
                Console.WriteLine($"Item '{itemName}' not found in {driveName}.");
            }
        }
    }

}
