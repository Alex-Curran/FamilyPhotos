using FamilyPhotos.Models;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace FamilyPhotos.DataAccess
{
    public class AlbumsDataAccess
    {
        private readonly FamilyPhotosDB _db = new FamilyPhotosDB();

        public Result<List<Album>> GetAllAlbums()
        {
            Result<List<Album>> result = new Result<List<Album>>();

            try
            {
                IQueryable<Album> albums = _db.Set<Album>();
                result.Data = albums.ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                result.Success = false;
                result.InternalError = true;
                result.ErrorMessage = "Error retrieving from the database";
                return result;
            }

            result.Success = true;
            return result;
        }

        public Album GetByTitle(string Title)
        {
            Album album = new Album();
            try
            {
                album = _db.Albums.SingleOrDefault(a => a.Title == Title);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return album;
            }

            return album;
        }
        public Album GetById(int id)
        {
            Album album = new Album();
            try
            {
                album = _db.Albums.Find(id);
                return album;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        
            return album;
        }

        
        
        //TODO: NOT DONE YET
        public List<Album> GetAllAlbumnsForUser(string UserName, out bool errorFlag)
        {
            errorFlag = false;
            List<Album> albums = new List<Album>();
            return albums;
            
        }

        public bool Delete(int id)
        {
            try
            {
                //Instead of querying the DB, create a temp Album with same id
                Album album = new Album(id);
                _db.Albums.Remove(album);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                return false;
            }

            return true;
        }
        public bool Delete(Album album)
        {
            try
            {
                _db.Albums.Remove(album);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public Result Update(Album updatedAlbum, Album originalAlbum)
        {
            Result result = new Result();

            try
            {
                _db.Entry(originalAlbum).CurrentValues.SetValues(updatedAlbum);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

                result.Success = false;
                result.ErrorMessage = e.Message;
                return result;
            }

            result.Success = true;
            return result;
        }

        public Result Update(Album updatedAlbum, int originalAlbumId)
        {
            Result result = new Result();
            try
            {
                Album originalAlbum = GetById(originalAlbumId);
                _db.Entry(originalAlbum).CurrentValues.SetValues(updatedAlbum);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

                result.Success = false;
                result.ErrorMessage = e.Message;
                return result;
            }

            return result;
        }

        public bool AlbumExists(int id)
        {
            Album album = new Album();
            try
            {
                album = _db.Albums.Find(id);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            if (album == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Result Add(Album album)
        {
            Result result = new Result();
            try
            {
                _db.Albums.Add(album);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);

                result.Success = false;
                result.InternalError = true;
                result.ErrorMessage = "Error writing the database " + e.Message; 
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
