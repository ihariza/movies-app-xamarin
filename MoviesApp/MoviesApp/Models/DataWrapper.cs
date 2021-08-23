namespace MoviesApp.Models
{
    public class DataWrapper<T>
    {
        public bool Success { get; set; }
        public T Result { get; set; }
        public string MessageError { get; set; }
    }
}
