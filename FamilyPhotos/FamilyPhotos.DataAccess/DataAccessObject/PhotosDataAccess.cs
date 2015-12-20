using FamilyPhotos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace FamilyPhotos.DataAccess
{
    public class PhotosDataAccess
    {
        private readonly FamilyPhotosDB _db = new FamilyPhotosDB();

        public Result<Photo> GetById(int id)
        {
            Result<Photo> result = new Result<Photo>();
            Photo photo = new Photo()
            {
                PhotoId = 1,
                AlbumId = 2,
                Title = "Test photo",
                UserId = 1
            };
            result.Data = photo;
            result.Success = true;
            return result;
        
            //try
            //{
            //    photo = _db.Photos.Find(id);
            //}
            //catch(Exception e)
            //{
            //    Debug.WriteLine(e.Message);
            //    result.Success = false;
            //    result.ErrorMessage = "Error in the database " + e.InnerException;
            //    return result;
            //}

            //result.Success = true;
            //return result;            
        } 

        public Result<List<Photo>> GetForAlbum(int albumId)
        {
            Result<List<Photo>> result = new Result<List<Photo>>();

            try
            {

                var photos = from p in _db.Photos
                             where p.AlbumId == albumId
                             select p;

                result.Data = photos.ToList();

            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                result.Success = false;
                result.InternalError = true;
                result.ErrorMessage = "Database Erorr";

                return result;
            }

            result.Success = true;
            return result;
        }

        public Result Delete(Photo photo)
        {
            Result result = new Result();
            try
            {
                _db.Photos.Remove(photo);
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);

                result.ErrorMessage = "Database error";
                result.Success = false;
                result.InternalError = true;
                return result;
            }
            result.Success = true;
            return result;
        }

        public Result Update(Photo updatedPhoto, Photo originalPhoto)
        {
            Result result = new Result();
            try
            {
                _db.Entry(originalPhoto).CurrentValues.SetValues(updatedPhoto);
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);

                result.Success = false;
                result.ErrorMessage = "Database error";
                result.InternalError = true;
                return result;
            }

            result.Success = true;
            return result;
        }

        public Result Add(Photo photo)
        {
            Result result = new Result();
            try
            {
                _db.Photos.Add(photo);
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                Debug.Write(e.Message);
                result.Success = false;
                result.ErrorMessage = "Database error";
                result.InternalError = true;

                return result;
            }

            result.Success = true;
            return result;
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
