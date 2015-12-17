using FamilyPhotos.Models;
using FamilyPhotos.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyPhotos.Logic
{
    public class AlbumLogic
    {
        private readonly AlbumsDataAccess dataAccess = new AlbumsDataAccess();


        public Album GetAlbumById(int id)
        {
            // empty album means error
            Album album = new Album();
            album = dataAccess.GetById(id);
            return album;

        }
        /// <summary>
        /// Returns all the albums in the database. 
        /// </summary>
        /// <returns></returns>
        public List<Album> GetAllAlbumns()
        {
            List<Album> albums = new List<Album>();
            albums = dataAccess.GetAllAlbumns();
            return albums;
        }

        //TODO: dont return the error code, instead write it to a log file!
        /// <summary>
        ///     Adds an album to the database and filesystem. 
        /// </summary>
        /// <param name="album"></param> Album to be added 
        /// <param name="ErrorCode"></param> Error status if a error occurs
        /// <returns>
        ///     True: error, check the ErrorCode
        ///     False: no error 
        /// </returns>
        public bool Add(Album album, out string ErrorCode)
        {
            // Verify required properites are set 
            if(album.Title == null && album.UserId == 0)
            {
                ErrorCode = "missingInformation";
                return true;
            }

            string path = "C:/FamilyPhotos.Data/" + album.Title;

            if (!Directory.Exists(path))
            {
                // Save to Database
                if (dataAccess.Add(album))
                {
                    ErrorCode = "success";
                    Directory.CreateDirectory(path);
                    return false;
                }
                else
                {
                    ErrorCode = "databaseError";
                    return true;
                }
            }
            else
            {
                ErrorCode = "alreadyExists";
                return false; 

            } 
        }
    }
}
