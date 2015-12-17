using FamilyPhotos.Logic;
using FamilyPhotos.Models;
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
            Photo photo = logic.GetById(id);
            if(photo == null)
            {
                return BadRequest("Photo not found");
            }
            else
            {
                return Ok(photo);
            }
           
        }

        // POST: api/photo
        // Saves Photo
        public IHttpActionResult POST(PhotoViewModel photoViewModel)
        {

            return Ok();
        }
    }
}
