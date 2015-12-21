using System;
using System.IO;
using System.Web;

namespace FamilyPhotos.Models
{
    public class PhotoViewModel
    {
        public int PhotoId { get; set; }
        public int UserId { get; set; }
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateUploaded { get; set;}
        public HttpPostedFileBase httpPostedFileBase { get; set; }
        public Stream File { get; set; }
        
    }
}
