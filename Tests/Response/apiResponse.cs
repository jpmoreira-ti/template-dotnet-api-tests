using System.Net;

namespace TestsAPI.Response
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        
        public HttpStatusCode StatusCode { get; set; }
    }
}