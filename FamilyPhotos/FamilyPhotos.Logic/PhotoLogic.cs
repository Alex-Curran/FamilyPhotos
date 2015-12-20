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

            if(id < 1)
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
                foreach(var photo in result.Data)
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

        public Result Add(PhotoViewModel photoViewModel)
        {
            Photo photo = PhotoService.ConvertPhotoViewModelToPhoto(photoViewModel);
            //TODO: Set the path, and save to the disk

            Result result = dataAccess.Add(photo);

            return result; 
        }

        //public bool Delete(Photo photo)
        //{

        //}

        //public bool Delete(int PhotoId)
        //{

        //}

        //public bool Update(Photo updatedPhoto, Photo originalPhoto)
        //{


        //}

        //public bool Update(Photo updatedPhoto, int originalPhotoId)
        //{

        //}

    }
}