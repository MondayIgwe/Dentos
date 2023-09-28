using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Elite3E.Infrastructure.Helper
{
    public class SystemIOHelper
    {
        public static string PROJECT_NAME = System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location).Replace(".dll", "").ToLower();
        public static string DIR_PROJECT_DEBUG = AppDomain.CurrentDomain.BaseDirectory.ToLower();
        public static string DIR_SOLUTION_HOME = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        public static string DIR_RESOURCES = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Resources\\";

        public static bool CreateTextFile(string path, string filename)
        {//https://www.c-sharpcorner.com/UploadFile/mahesh/create-a-text-file-in-C-Sharp/
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    Console.WriteLine("The directory was created successfully at: " + Directory.GetCreationTime(path));
                }

                //Correcting possible slash issue
                //string lastChar = path.Substring(path.Length - 1);
                //path = (lastChar.Equals("\\") || lastChar.Equals("//")) ? path : path + "\\";

                string filenameWithPath = Path.Combine(path,filename);

                if (File.Exists(filenameWithPath))
                {
                    DeleteFile(filenameWithPath);
                    Console.WriteLine("Previous text file found and Deleted: " + filenameWithPath);
                }

                //Using closes the file after it's been created.
                using (File.Create(filenameWithPath)) { };

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Create Text File - " + e.StackTrace);
                return false;
            }
        }

        public static bool RenameFile(string currentFileNameWithPath, string newFileNameWithPath)
        {
            try
            {
                System.IO.File.Move(currentFileNameWithPath, newFileNameWithPath);
                Pause(400);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to rename file - " + e.StackTrace);
                return false;
            }
        }

        public static bool MoveFile(string currentFilePathWithFileName, string newFilePathWithFileName)
        {
            try
            {
                System.IO.File.Move(currentFilePathWithFileName, newFilePathWithFileName);
                Pause(400);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to move file - " + e.StackTrace);
                return false;
            }
        }

        public static bool CopyFile(string currentFilePathWithFileName, string newFilePathWithFileName)
        {
            try
            {
                System.IO.File.Copy(currentFilePathWithFileName, newFilePathWithFileName);
                Pause(400);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to copy file - " + e.StackTrace);
                return false;
            }
        }

        public static bool DeleteFile(string FilePath, string filename)
        {
            return DeleteFile(Path.Combine(FilePath, filename));
        }
        public static bool DeleteFile(string filenameWithPath)
        {
            try
            {
                System.IO.File.Delete(filenameWithPath);
                Pause(400);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Delete file - " + e.StackTrace);
                return false;
            }
        }

        public static bool DeleteAllFilesInDir(string FilePath)
        {
            try
            {
                GetFiles(FilePath).ForEach(s => System.IO.File.Delete(s));
                Pause(400);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Delete All files in Dir - " + e.StackTrace);
                return false;
            }
        }

        //For when you want to delete as many files of the same name in a folder but leave the rest.
        public static bool DeleteFileInDirContains(string FilePath, string fileToDel)
        {
            try
            {
                //getFiles(path).FindAll(s=> s.ToLower().Contains(fileToDel.ToLower())).ForEach(s => System.IO.File.Delete(s));
                //Separated into two lines for debugging.
                List<string> filesToDelList = GetFiles(FilePath).FindAll(s => s.ToLower().Contains(fileToDel.ToLower()));
                filesToDelList.ForEach(s => System.IO.File.Delete(s));
                Pause(400);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Delete specific files in Dir - " + e.StackTrace);
                return false;
            }
        }

        public static bool WriteToTextFile(string FilePath, string filename, string text)
        {
            return WriteToTextFile(Path.Combine(FilePath, filename), text);
        }

        public static bool WriteToTextFile(string filenameWithPath, string text)
        {
            try
            {
                File.AppendAllText(filenameWithPath, text);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<string> GetFiles(string path)
        {//https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.getfiles?view=netframework-4.7.2#System_IO_Directory_GetFiles_System_string_
            try
            {
                return Directory.GetFiles(path).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<string> GetDirectories(string path)
        {//https://stackoverflow.com/questions/7296956/how-to-list-all-sub-directories-in-a-directory/7296968
         //https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.getdirectories?view=netframework-4.7.2
            try
            {
                List<string> result = new List<string>(Directory.GetDirectories(path, "*"));
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool IsFileInDir(string path, string filename)
        {
            try
            {
                List<string> filesInDir = GetFiles(path);
                return filesInDir.Any(s => s.ToLower().Contains(filename.ToLower()));
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string RetrieveFileNameInDir(string path, string partialFilename)
        {
            try
            {
                List<string> filesInDir = GetFiles(path);
                return filesInDir.Find(s => s.ToLower().Contains(partialFilename.ToLower()));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool WaitForFileInDir(string path, string filename, int timeout)
        {
            bool isFileFound = false;
            int counter = 0;
            try
            {
                while (!isFileFound && counter < timeout)
                {
                    if (IsFileInDir(path, filename))
                    {
                        isFileFound = true;
                    }
                    else
                    {

                        isFileFound = false;
                        counter++;
                    }
                    PauseInSeconds(1);
                }

                return isFileFound;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] - " + ex.Message);
                return false;
            }
        }

        public static string WaitAndRetrieveNewFileFromDir(string path, List<string> originalFileList, int timeout)
        {
            string newFileName = null;
            int counter = 0;
            int originalFileListSize = originalFileList.Count();
            try
            {
                while (newFileName == null && counter < timeout)
                {
                    List<string> currentFileList = GetFiles(path);
                    if (currentFileList.Count() > originalFileListSize)
                    {
                        Pause(500);
                        foreach (string currentFileName in currentFileList)
                        {
                            if (!originalFileList.Any(s => s.Equals(currentFileName)))
                            {
                                newFileName = currentFileName;
                                break;
                            }
                        }
                    }
                    else
                    {
                        counter++;
                    }
                    PauseInSeconds(1);
                }

                return newFileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] - " + ex.Message);
                return newFileName;
            }
        }

        public static bool IsFolderInDir(string path, string foldername)
        {
            try
            {
                List<string> foldersInDir = GetDirectories(path);
                return foldersInDir.Any(s => s.ToLower().Contains(foldername.ToLower()));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string ReadFile(string filenameWithPath)
        {
            try
            {
                string text = File.ReadAllText(filenameWithPath);
                return text;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetResourceFilePath(string fileName)
        {
            return Path.Combine(SystemIOHelper.DIR_RESOURCES, fileName);
        }

        private static void Pause(int mili)
        {
            try
            {
                Thread.Sleep(mili);
            }
            catch (Exception)
            {
            }
        }

        private static void PauseInSeconds(int seconds)
        {
            Pause(seconds * 1000);
        }

    }
}
