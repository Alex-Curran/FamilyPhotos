using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyPhotos.Models
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string Description { get; set;}
        public string UserName { get; set; }
        public string DirectoryPath { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime DateCreated { get; set;}

        [Column(TypeName = "DateTime2")]
        public DateTime DateUpdated { get; set; }

        public List<Photo> Photos { get; set; }


        public Album()
        {

        }

        public Album(int AlbumId)
        {
            this.AlbumId = AlbumId;
        }
    }
}
