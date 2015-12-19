using FamilyPhotos.Logic;
using FamilyPhotos.Models;
using FamilyPhotos.Models.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace FamilyPhotos.API.Controllers
{
    [Authorize]
    public class AlbumController : ApiController
    {
        private readonly AlbumLogic logic = new AlbumLogic();

        // GET: api/Album
        // Returns all Albums
        public IHttpActionResult Get()
        {
            Result<List<AlbumViewModel>> result = new Result<List<AlbumViewModel>>();

            result = logic.GetAllAlbums();

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else if(result.InternalError)
            {
                return new HttpActionResult(HttpStatusCode.InternalServerError, result.ErrorMessage);
            }
            else
            {
                return new HttpActionResult(HttpStatusCode.BadRequest, result.ErrorMessage);
            }

        }

        // GET: api/Album/5
        // Returns specific Album
        public IHttpActionResult Get(int id)
        {
            Result<AlbumViewModel> result = new Result<AlbumViewModel>();
            result = logic.GetAlbumById(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else if(result.InternalError)
            {
                return new HttpActionResult(HttpStatusCode.InternalServerError, result.ErrorMessage);
            }
            else
            {
                return new HttpActionResult(HttpStatusCode.BadRequest, result.ErrorMessage);
            }
        }

        //GET: api/GetUsersAlbums/userName
        // Returns all the albums for a user
        public IHttpActionResult GetUsersAlbums(string userName)
        {
            Result<List<Album>> result = new Result<List<Album>>();
            result = logic.GetAlbumsForUser(userName);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else if(result.InternalError)
            {
                return new HttpActionResult(HttpStatusCode.InternalServerError, result.ErrorMessage);
            }
            else
            {
                return new HttpActionResult(HttpStatusCode.BadRequest, result.ErrorMessage);
            }

        }

        // POST: api/Album
        // Creates an Album
        public IHttpActionResult Post([FromBody]Album album)
        {
            Result result = new Result();
            result = logic.Add(album);

            if (result.Success)
            {
                return new HttpActionResult(HttpStatusCode.Created, "Album created");
            }
            else if(result.InternalError)
            {
                return new HttpActionResult(HttpStatusCode.InternalServerError, result.ErrorMessage);
            }
            else
            {
                return new HttpActionResult(HttpStatusCode.BadRequest, result.ErrorMessage);
            }

        }

        // DELETE: api/Album
        // Deletes an album
        public IHttpActionResult Delete(Album album)
        {
            Result result = new Result();

            result = logic.Delete(album);

            if (result.Success)
            {
                return new HttpActionResult(HttpStatusCode.OK, "Successfully Deleted");
            }
            else if (result.InternalError)
            {
                return new HttpActionResult(HttpStatusCode.InternalServerError, result.ErrorMessage);
            }else
            {
                return new HttpActionResult(HttpStatusCode.BadRequest, result.ErrorMessage);
            }
        }

        //PUT: api/Album
        //Updates the album 
        public IHttpActionResult PUT(Album album)
        {
            Result result = new Result();
            result = logic.Update(album);

            if (result.Success)
            {
                return new HttpActionResult(HttpStatusCode.OK, "Album updated");

            }
            else if(result.InternalError)
            {
                return new HttpActionResult(HttpStatusCode.InternalServerError, result.ErrorMessage); 
            }
            else
            {
                return new HttpActionResult(HttpStatusCode.BadRequest, result.ErrorMessage);

            }
        }
    }
}
