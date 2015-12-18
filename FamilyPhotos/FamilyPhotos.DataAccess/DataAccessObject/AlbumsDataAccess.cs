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

        public Album GetByTitle(string Title)
        {
            try
            {
                Album Album = _db.Albums.SingleOrDefault(album => album.Title == Title);
            }
            catch(Exception e)
            {
                
            }
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

        public List<Album> GetAllAlbumns(out bool errorFlag)
        {
            errorFlag = false;
            try
            {
                IQueryable<Album> albums = _db.Set<Album>();
                return albums.ToList();
            }
            catch (Exception e)
            {
                errorFlag = true;
                Debug.WriteLine(e.Message);
                return new List<Album>();
            }
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

        public bool Update(Album updatedAlbum, Album originalAlbum)
        {
            try
            {
                _db.Entry(originalAlbum).CurrentValues.SetValues(updatedAlbum);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public bool Update(Album updatedAlbum, int originalAlbumId)
        {
            try
            {
                Album originalAlbum = GetById(originalAlbumId);
                _db.Entry(originalAlbum).CurrentValues.SetValues(updatedAlbum);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
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

        public bool Add(Album album)
        {
            try
            {
                _db.Albums.Add(album);
                _db.SaveChanges();
            }
            catch (Exception e)
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
