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
            result.Data = new List<Album>();

            result.Data.Add(new Album 
                {
                    AlbumId = 1,
                    Title = "Test album",
                    Description = "Description",
                    UserName = "alexcurran839",
                });


            result.Data.Add(new Album
                {
                    AlbumId = 2,
                    Title = "Test album2",
                    Description = "Description2",
                    UserName = "alexcurran839",
                }); 
            result.Success = true;
            return result;

            //try
            //{
            //    IQueryable<Album> albums = _db.Set<Album>();
            //    result.Data = albums.ToList();
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine(e.Message);
            //    result.Success = false;
            //    result.InternalError = true;
            //    result.ErrorMessage = "Error retrieving from the database";
            //    return result;
            //}

            //result.Success = true;
            //return result;
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
                result.ErrorMessage = "Error writing to the database " + e.InnerException;
                return result;
            }

            result.Success = true;
            return result;
        }
        public Result<Album> GetById(int id)
        {
            Result <Album> result = new Result<Album>();
            result.Data = new Album();

            result.Data = new Album 
                {
                    AlbumId = 2,
                    Title = "Test album2",
                    Description = "Description2",
                    UserName = "alexcurran839",
                };

            result.Success = true;
            return result;
        //try
        //{
        //    result.Data = _db.Albums.Find(id);

        //}
        //catch (Exception e)
        //{
        //    Debug.WriteLine(e.Message);
        //    result.Success = false;
        //    result.InternalError = true;
        //    result.ErrorMessage = "Error retrieving from the database";
        //}

        //return result
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
        public Result<List<Album>> GetAllAlbumsForUser(string UserName)
        {
            Result<List<Album>> result = new Result<List<Album>>();
            try
            {
                var albums = from a in _db.Albums
                             where a.UserName == UserName
                             select a;
                result.Data = albums.ToList();

            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                result.Success = false;
                result.InternalError = true;
                result.ErrorMessage = "Error Accessing the database";
            }

            return result; 

        }
        public Result Delete(int id)
        {
            Result result = new Result();
            try
            {
                //Instead of querying the DB, create a temp Album with same id
                Album album = new Album(id);
                _db.Albums.Remove(album);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                result.Success = false;
                result.ErrorMessage = "Error in the database";
                result.InternalError = true;

                return result;
            }

            result.Success = true;
            return result;
        }

        public Result<string> GetAlbumPath(int AlbumId)
        {
            string AlbumPath = "";
            Result<string> result = new Result<string>();

            try
            {
                var album = _db.Albums.SingleOrDefault(a => a.AlbumId == AlbumId);
                if (album != null)
                {
                    AlbumPath = album.DirectoryPath;
                    result.Data = AlbumPath;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "Album does not exist!";
                }
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                result.Success = false;
                result.InternalError = true;
                result.ErrorMessage = "Error Accessing the database";
                result.Data = "";

                return result; 
            }

            result.Success = false;
            return result;
        }

        public Result Delete(Album album)
        {
            Result result = new Result();
            try
            {
                _db.Albums.Remove(album);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                result.Success = false;
                result.ErrorMessage = "Error in the database";
                result.InternalError = true;

                return result;
            }

            result.Success = true;
            return result;
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
        

    }
}
