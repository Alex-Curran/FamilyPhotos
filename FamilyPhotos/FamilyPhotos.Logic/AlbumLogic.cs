using FamilyPhotos.Models;
using FamilyPhotos.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyPhotos.Logic.Services;

namespace FamilyPhotos.Logic
{
    public class AlbumLogic
    {
        private readonly AlbumsDataAccess dataAccess = new AlbumsDataAccess();

        /// <summary>
        ///  
        /// </summary>
        /// <param name="id">id of the album to find</param>
        /// <returns>
        ///     Album with the id 
        /// </returns>
        public Album GetAlbumById(int id)
        {
            Album album = new Album();
            album = dataAccess.GetById(id);
            return album;
        }

        /// <summary>
        /// Gets all the albums
        /// </summary>
        /// <param name="errorFlag">ErrorFlag keeps track of any errors</param>
        /// <returns> List of all the albums </returns>
        public List<Album> GetAllAlbumns(out bool errorFlag)
        {
            List<Album> albums = new List<Album>();
            albums = dataAccess.GetAllAlbumns(out errorFlag);
            return albums;
        }

        /// <summary>
        /// Gets all the albums for a specified user, given the  UserName
        /// </summary>
        /// <param name="UserName">Name of the User that we want</param>
        /// <param name="errorFlag">Keeps track of errors</param>
        /// <returns>List of all the user's albums</returns>
        public List<Album> GetAlbumsForUser(string UserName, out bool errorFlag)
        {
            List<Album> albums = new List<Album>();
            albums = dataAccess.GetAllAlbumnsForUser(UserName, out errorFlag);
            return albums;
        }

        /// <summary>
        /// Adds an album to the database
        /// </summary>
        /// <param name="album">Album to be added</param>
        /// <returns>Bool: True: there was an error
        ///                False: no error 
        ///</returns>
        public bool Add(Album album)
        {
            // Verify required properites are set 
            if (album == null)
            {
                return true;
            }

            if (album.Title == null || album.UserName == null)
            {
                return true;
            }

            // Set the Created date 
            album.DateCreated = DateTime.Now;
            album.DateUpdated = album.DateCreated;

            album.DirectoryPath = "C:/FamilyPhotos.Data/" + album.Title;

            if (!Directory.Exists(album.DirectoryPath))
            {
                // Save to Database
                if (dataAccess.Add(album))
                {
                    Directory.CreateDirectory(album.DirectoryPath);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public bool Update(Album albumUpdated)
        {
            //Get the original album 

        }

        /// <summary>
        /// Deletes an album. 
        /// </summary>
        /// <param name="id">Id of the album to be deleted</param>
        /// <returns>Bool: True: there was an error
        ///                False: no error 
        ///</returns>
        public bool Delete(Album album)
        {
            // Check if the album exists already
            if (dataAccess.AlbumExists(album.AlbumId))
            {
                if (dataAccess.Delete(album))
                {
                    //Delete the directory
                    FileService fileService = new FileService();
                    fileService.DeleteDirectory(album.DirectoryPath);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
