using System;
using FamilyPhotos.Models;

namespace FamilyPhotos.Logic.Services
{
    internal class PhotoService
    {
        internal static PhotoViewModel ConvertPhotoToPhotoViewModel(Photo photo)
        {
            PhotoViewModel viewModel = new PhotoViewModel()
            {
                PhotoId = photo.PhotoId,
                UserId = photo.UserId,
                AlbumId = photo.AlbumId,
                Title = photo.Title,
                Description = photo.Description,
                DateUploaded = photo.DateUploaded
            };
        
            return viewModel;
        }

        internal static Photo ConvertPhotoViewModelToPhoto(PhotoViewModel ViewModel)
        {
            Photo photo = new Photo()
            {
                PhotoId = ViewModel.PhotoId,
                UserId = ViewModel.UserId,
                AlbumId = ViewModel.AlbumId,
                Title = ViewModel.Title,
                Description = ViewModel.Description,
                DateUploaded = ViewModel.DateUploaded
            };

            return photo;
        }
    }
}
