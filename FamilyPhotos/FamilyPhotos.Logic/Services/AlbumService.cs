using FamilyPhotos.Models;
using FamilyPhotos.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyPhotos.Logic
{
    public class AlbumService
    {
        public static AlbumViewModel ConvertAlbumToAlbumViewModel(Album album)
        {
            AlbumViewModel albumViewModel = new AlbumViewModel();

            albumViewModel.Title = album.Title;
            albumViewModel.AlbumId = album.AlbumId;
            albumViewModel.Description = album.Description;
            albumViewModel.DateCreated = album.DateCreated;
            albumViewModel.DateUpdated = album.DateUpdated;
            albumViewModel.CoverPhoto = GetCoverPhoto(album.AlbumId);

            return albumViewModel;
        }

        public static Photo GetCoverPhoto(int albumId)
        {
            //TODO: TEMP
            Photo photo = new Photo();
            photo.PhotoId = 1;
            photo.Title = "Cover Photo";
            photo.Description = "This is the description of the photo";
            photo.AlbumId = albumId;
            return photo;
        }
    }
}

