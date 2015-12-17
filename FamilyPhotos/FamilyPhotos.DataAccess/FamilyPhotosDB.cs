using FamilyPhotos.Models;
using System.Data.Entity;

namespace FamilyPhotos.DataAccess
{
    public class FamilyPhotosDB : DbContext
    {
        public DbSet<Photo> Photos { get; set;}
        public DbSet<Album> Albums { get; set;}
        public DbSet<User> Users { get; set; }
      
        public FamilyPhotosDB() : base("FamilyPhotosDb")
        {

        }
    }
}
