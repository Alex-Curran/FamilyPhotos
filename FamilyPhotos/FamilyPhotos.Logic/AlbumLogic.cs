using FamilyPhotos.Models;
using FamilyPhotos.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyPhotos.Logic.Services;
using FamilyPhotos.Models.ViewModels;

namespace FamilyPhotos.Logic
{
    public class AlbumLogic
    {
        private readonly AlbumsDataAccess dataAccess = new AlbumsDataAccess();

        public Album GetAlbumById(int id)
        {
            Album album = new Album();
            album = dataAccess.GetById(id);
            return album;
        }

        public Result<List<AlbumViewModel>> GetAllAlbums()
        {
            Result<List<Album>> result = new Result<List<Album>>();
            Result<List<AlbumViewModel>> viewModelResult = new Result<List<AlbumViewModel>>();

            result = dataAccess.GetAllAlbums();

            //Convert from album to albumViewModel
            if (result.Success)
            {
                foreach (Album album in result.Data)
                {
                    viewModelResult.Data.Add(AlbumService.ConvertAlbumToAlbumViewModel(album));
                }
            }

            viewModelResult.Success = result.Success;
            viewModelResult.InternalError = result.InternalError;
            viewModelResult.ErrorMessage = result.ErrorMessage;

            return viewModelResult;
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

        public Result Add(Album album)
        {
            Result result = new Result();
            FileService fileService = new FileService();

            // Verify required properites are set 
            if (album == null)
            {
                result.Success = false;
                result.ErrorMessage = "Album is null";
                return result;
            }
            else if (album.Title == null)
            {
                result.Success = false;
                result.ErrorMessage = "Album Title is null";
                return result;
            }

            // Create the directory in the file system
            string directoryPath = fileService.createDirectory(album.Title);
            if(directoryPath != null)
            {
                album.DirectoryPath = directoryPath;
            }
            else
            {
                result.Success = false;
                result.InternalError = true;
                result.ErrorMessage = "Error Creating Directory";
                return result;
            }

            //Update the date created and date updated properties
            album.DateCreated = DateTime.Now;
            album.DateUpdated = album.DateCreated;

            // Save to Database
            result = dataAccess.Add(album);
            return result;
        }

        public Result Update(Album updatedAlbum)
        {
            //Get the original album 
            Album originalAlbum = dataAccess.GetByTitle(updatedAlbum.Title);
            FileService fileService = new FileService();
            Result result = new Result();

            if(originalAlbum == null)
            {
                result.Success = false;
                result.ErrorMessage = "Album not found ";
                return result;  
            }

            if(updatedAlbum.Title != originalAlbum.Title)
            {
                updatedAlbum.DirectoryPath = fileService.UpdateDirectory(updatedAlbum.Title);
            }
            updatedAlbum.DateUpdated = DateTime.Now;


            //Update the album in the database 
            result = dataAccess.Update(updatedAlbum, originalAlbum);   
            //if(result.Success == true && updatedAlbum.)         
            return result; 
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
