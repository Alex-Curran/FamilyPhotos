using FamilyPhotos.Models;
using FamilyPhotos.DataAccess;
using System;
using System.Collections.Generic;
using FamilyPhotos.Logic.Services;

namespace FamilyPhotos.Logic
{ 
    public class PhotoLogic
    {

        private readonly PhotosDataAccess dataAccess = new PhotosDataAccess();

        public Result<PhotoViewModel> GetById(int id)
        {
            Result<PhotoViewModel> resultViewModel = new Result<PhotoViewModel>();
            Result<Photo> result = new Result<Photo>();

            if (id < 1)
            {
                resultViewModel.Success = false;
                resultViewModel.ErrorMessage = "Invalid Id";
                return resultViewModel;
            }

            result = dataAccess.GetById(id);

            resultViewModel.Data = PhotoService.ConvertPhotoToPhotoViewModel(result.Data);

            return resultViewModel;
        }

        public Result<List<PhotoViewModel>> GetForAlbum(int albumId)
        {
            Result<List<PhotoViewModel>> resultViewModel = new Result<List<PhotoViewModel>>();
            Result<List<Photo>> result = new Result<List<Photo>>();

            result = dataAccess.GetForAlbum(albumId);

           if (result.Success && result.Data != null)
           {
                foreach (var photo in result.Data)
                {
                    resultViewModel.Data.Add(PhotoService.ConvertPhotoToPhotoViewModel(photo));
                }

            return resultViewModel;

            }
            else
            {
                resultViewModel.Success = false;
                resultViewModel.ErrorMessage = "Error";
                return resultViewModel;
            }
        }

        public Result Delete(PhotoViewModel photoViewModel)
        {
            Photo photo = PhotoService.ConvertPhotoViewModelToPhoto(photoViewModel);
            Result result = dataAccess.Delete(photo);

            if (result.Success)
            {
                //TODO: Delete from tthe file system
                return result;
            }
            else
            {
                return result;
            }
        }

        public Result Update(PhotoViewModel photoViewModel)
        {

            Result result = new Result();
            return result;

            //Get the original photo
            //Result<Photo> originalPhoto = dataAccess.GetById(photoViewModel.PhotoId);

            //if (!originalPhoto.Success)
            //{
            //    result.ErrorMessage = originalPhoto.ErrorMessage;
            //    result.InternalError = originalPhoto.InternalError;
            //    return result;
            //}
            //else if(originalPhoto.Data != null)
            //{
            //    result.Success = false;
            //    result.ErrorMessage = "Photo not found";
            //    return result;
            //}
            //else
            //{

         //}
        }

        public Result Add(PhotoViewModel photoViewModel)
    {
        Result result = new Result();
        Photo photo = PhotoService.ConvertPhotoViewModelToPhoto(photoViewModel);
        AlbumLogic albumLogic = new AlbumLogic();
        Result<string> AlbumPathResult = albumLogic.GetAlbumPath(photoViewModel.AlbumId);

        if (!AlbumPathResult.Success)
        {
            result.Success = false;
            result.ErrorMessage = AlbumPathResult.ErrorMessage;
            return result;
        }

        photo.FilePath_Original = PhotoService.SetFilePath(AlbumPathResult.Data, photo.Title);
        result = dataAccess.Add(photo);

        return result;
    }

}
}
