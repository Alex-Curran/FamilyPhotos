using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyPhotos.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }   
        public int UserId { get; set; }
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
         
        [Column(TypeName="DateTime2")]
        public DateTime DateUploaded { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime DateUpdated { get; set; }
        public string FilePath_Original { get; set; }
        public string FilePath_Thumb { get; set; }

        public Photo(int PhotoId)
        {
            this.PhotoId = PhotoId;
        }

        public Photo()
        {
        }

    }
}
