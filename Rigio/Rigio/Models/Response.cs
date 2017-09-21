using System;
namespace Rigio.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }
		public string Content { get; set; }

        public Response(int statusCode, string content) {
            StatusCode = statusCode;
            Content = content;
            IsSuccessStatusCode = StatusCode >= 200 && StatusCode < 300;
        }
    }
}
