using FamilyPhotos.Logic;
using FamilyPhotos.Models;
using System.Net;
using System.Web.Http;

namespace FamilyPhotos.API.Controllers
{
    [Authorize]
    public class PhotoController : ApiController
    {
        private readonly PhotoLogic logic = new PhotoLogic();

        // GET: api/photo/id
        // returns photo with specfied id
        public IHttpActionResult GET(int id)
        {
            Result<PhotoViewModel> result = new Result<PhotoViewModel>();
            result = logic.GetById(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else if (result.InternalError)
            {
                return new HttpActionResult(HttpStatusCode.InternalServerError, result.ErrorMessage);
            }
            else
            {
                return new HttpActionResult(HttpStatusCode.BadRequest, result.ErrorMessage);
            }
           
        }


        // POST: api/photo
        // Saves Photo
        //public IHttpActionResult POST(PhotoViewModel photoViewModel)
        //{

           
        //}
    }
}
