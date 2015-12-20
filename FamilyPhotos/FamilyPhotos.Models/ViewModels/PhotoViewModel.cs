using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
