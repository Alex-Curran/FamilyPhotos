using FamilyPhotos.Models;
using FamilyPhotos.DataAccess;

namespace FamilyPhotos.Logic
{
    public class PhotoLogic
    {
        private readonly PhotosDataAccess dataAccess = new PhotosDataAccess();

        public Photo GetById(int id)
        {
            Photo photo = new Photo();
            if (id > 1)
            {
                photo = dataAccess.GetById(id);
                return photo;
            }
            else
            {
                return photo;
            }
        }
        public bool Add(Photo photo)
        {
            // Verfify the properties of Photo
            //PhotoServices ps = new PhotoServices();
            // Create Thumbnail
            //Photo thumbnail = ps.CreateThumbnail(photo);

            // Save thumb and original to the disk
            //ps.SavePhoto(photo, thumbnail);

            // Try saving
            if (dataAccess.Add(photo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(Photo photo)
        {
            if (dataAccess.Delete(photo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int PhotoId)
        {
            Photo photo = new Photo(PhotoId);

            if (dataAccess.Delete(photo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Photo updatedPhoto, Photo originalPhoto)
        {
            if (dataAccess.Update(updatedPhoto, originalPhoto))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Photo updatedPhoto, int originalPhotoId)
        {
            Photo originalPhoto = new Photo(originalPhotoId);

            if (dataAccess.Update(updatedPhoto, originalPhoto))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}