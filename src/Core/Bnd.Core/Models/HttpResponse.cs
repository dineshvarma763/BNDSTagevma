namespace Bnd.Core.Models
{
    public class HttpResponse<T>
    {
        public T? Result { get; set; }
        public string? ErrorMessage { get; set; }
        public bool Success { get; set; }
    }
}
