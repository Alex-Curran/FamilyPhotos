using FamilyPhotos.Logic;
using FamilyPhotos.Models;
using System.Net;
using System.Web;
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
        public IHttpActionResult POST(PhotoViewModel photoViewModel, HttpPostedFileBase postedFile)
        {
            Result result = logic.Add(photoViewModel);

            if (result.Success)
            {
                return Ok();
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

        //DELETE: api/Photo
        public IHttpActionResult DELETE(PhotoViewModel photoViewModel)
        {
            Result result = logic.Delete(photoViewModel);

            if (result.Success)
            {
                return Ok();
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


        //PUT: api/Photo
        public IHttpActionResult PUT(PhotoViewModel photoViewModel)
        {
            Result result = logic.Update(photoViewModel);

            if (result.Success)
            {
                return Ok();
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
    }
}
