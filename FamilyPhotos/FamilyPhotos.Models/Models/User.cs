using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FamilyPhotos.Models
{
    public class User
    {
        [Key]    
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Album> Albums { get; set; }
       
    }
}
