using System;
using FamilyPhotos.Models;
using System.IO;

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

        internal static string SetFilePath(string albumPath, string photoName)
        {
            string path = albumPath + "/" + photoName;
            return path;
        }

        internal static void SavePhoto(string path, Stream stream)
        {
            using (Stream file = File.Create(path))
            {
                CopyStream(stream, file);
            }
        }

        private static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }
    }
}
