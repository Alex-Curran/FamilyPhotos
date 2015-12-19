using System;
using System.ComponentModel.DataAnnotations;

namespace FamilyPhotos.Models.ViewModels
{
    public class AlbumViewModel
    {
        [Editable(false)]
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Editable(false)]
        public DateTime DateCreated { get; set; }
        [Editable(false)]
        public DateTime DateUpdated { get; set; }
        public Photo CoverPhoto { get; set; }

    }
}
