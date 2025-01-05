using System.Net;
using Microsoft.AspNetCore.Http;

namespace BackendStore.Helpers
{
    public class TemplateResponse<T>
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public T Data { get; set; }

        public TemplateResponse(string message, HttpStatusCode status, T data) 
        {
            Message = message;
            Status = (int)status; 
            Data = data;
        }
    }
}
