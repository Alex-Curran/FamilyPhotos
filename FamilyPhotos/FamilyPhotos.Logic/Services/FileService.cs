using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyPhotos.Logic.Services
{
    public class FileService
    {
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
    }
}
