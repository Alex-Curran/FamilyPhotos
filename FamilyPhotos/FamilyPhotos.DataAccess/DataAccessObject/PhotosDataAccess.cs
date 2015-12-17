using FamilyPhotos.Models;
using System;
using System.Diagnostics;

namespace FamilyPhotos.DataAccess
{
    public class PhotosDataAccess
    {
        private readonly FamilyPhotosDB _db = new FamilyPhotosDB();

        public Photo GetById(int id)
        {
            Photo photo = new Photo();

            try
            {
                photo = _db.Photos.Find(id);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return photo;           
        } 

        public bool Delete(Photo photo)
        {
            try
            {
                _db.Photos.Remove(photo);
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public bool Update(Photo updatedPhoto, Photo originalPhoto)
        {
            try
            {
                _db.Entry(originalPhoto).CurrentValues.SetValues(updatedPhoto);
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public bool Add(Photo photo)
        {
            try
            {
                _db.Photos.Add(photo);
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                Debug.Write(e.Message);
                return false;
            }

            return true;
        }

        public bool SaveDB()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }
    }
}
