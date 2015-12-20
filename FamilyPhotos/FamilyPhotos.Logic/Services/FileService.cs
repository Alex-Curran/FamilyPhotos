using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyPhotos.Models;

namespace FamilyPhotos.Logic.Services
{
    public class FileService
    {
        string rootDirectory = @"C:\FamilyPhotos.Data\";
        public bool DeleteDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

        }

        internal string UpdateDirectory(string title)
        {
            return rootDirectory + title;
        }

        internal string createDirectory(string title)
        {
            string directoryPath = rootDirectory + title;
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                return directoryPath;
            }
            else
            {
                directoryPath = null;
                return directoryPath;   
            }
        }

    }
}
