
namespace FamilyPhotos.Models
{
    public class Result
    {
        public bool Success { get; set; }
        public bool InternalError { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Result<T> : Result
    {
        public T Data; 
    }
}
