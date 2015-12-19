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

        // Returns all the albums in the database as an albumViewModel
        public Result<List<AlbumViewModel>> GetAllAlbums()
        {
            Result<List<Album>> result = new Result<List<Album>>();
            Result<List<AlbumViewModel>> viewModelResult = new Result<List<AlbumViewModel>>();
            viewModelResult.Data = new List<AlbumViewModel>();

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

        //Returns the album with the given id as AlbumViewModel
        public Result<AlbumViewModel> GetAlbumById(int id)
        {
            Result<Album> result = new Result<Album>();
            Result<AlbumViewModel> resultViewModel = new Result<AlbumViewModel>();
            result.Data = new Album();
            resultViewModel.Data = new AlbumViewModel();

            //TODO seperate internal and bad request error. Check if exists? or throw specific exception down under?
  
            result = dataAccess.GetById(id);

            resultViewModel.Data = AlbumService.ConvertAlbumToAlbumViewModel(result.Data);
            resultViewModel.Success = true;
            return resultViewModel;
        }

        public Result<List<Album>> GetAlbumsForUser(string UserName)
        {
            Result<List<Album>> result = new Result<List<Album>>();
            List<Album> albums = new List<Album>();

            result = dataAccess.GetAllAlbumsForUser(UserName);
            return result;
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

        public Result Delete(Album album)
        {
            Result result = new Result();

            // Check if the album exists already
            if (dataAccess.AlbumExists(album.AlbumId))
            {
                result = dataAccess.Delete(album);

                if (result.Success)
                {
                    //Delete the directory
                    FileService fileService = new FileService();
                    fileService.DeleteDirectory(album.DirectoryPath);
                    result.Success = true;

                    return result; 
                }
                else
                {
                    return result; 
                }
            }
            else
            {
                result.Success = false;
                result.ErrorMessage = "Album does not exist";
                return result;
            }
        }

        //TODO: NOT DONE
        public Result Update(Album updatedAlbum)
        {
            //Get the original album 
            Album originalAlbum = dataAccess.GetByTitle(updatedAlbum.Title);
            FileService fileService = new FileService();
            Result result = new Result();

            if (originalAlbum == null)
            {
                result.Success = false;
                result.ErrorMessage = "Album not found ";
                return result;
            }

            if (updatedAlbum.Title != originalAlbum.Title)
            {
                updatedAlbum.DirectoryPath = fileService.UpdateDirectory(updatedAlbum.Title);
            }
            updatedAlbum.DateUpdated = DateTime.Now;


            //Update the album in the database 
            result = dataAccess.Update(updatedAlbum, originalAlbum);
            //if(result.Success == true && updatedAlbum.)         
            return result;
        }
    }
}
