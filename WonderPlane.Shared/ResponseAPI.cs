
namespace WonderPlane.Shared
{
    public class ResponseAPI<T>
    {
        public bool EsCorrecto { get; set; }
        public T? Valor { get; set; }
        public string? Mensaje { get; set; }
        public T? Data { get; set; }
    }
}
