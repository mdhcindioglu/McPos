using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McPos.Shared.Models
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess => Errors?.Any() != true;
        public List<Error>? Errors { get; set; } = new();
        public Error? Error => Errors?.FirstOrDefault();
        public void AddError(Error error) => Errors?.Add(error);
        public void AddError(ResponseCodes code) => Errors?.Add(new Error { Code = code, });
        public void AddError(ResponseCodes code, string message) => Errors?.Add(new Error { Code = code, Message = message, });
        public void AddError(Exception exception) => Errors?.Add(new Error { Code = ResponseCodes.Failed, Message = $"{exception.Message}{(exception.InnerException != null ? $"\r\nInnerException:\r\n{exception.InnerException.Message}" : null)}" });
        public bool HasError(ResponseCodes code) => Errors?.Any(error => error.Code == code) == true;
    }

    public class Response : Response<object> { }

    public class Error
    {
        public ResponseCodes? Code { get; set; }
        public string? CodeName => Code?.ToString();
        public string? Message { get; set; } = string.Empty;
    }

    public enum ResponseCodes
    {
        Failed = -1,
        NotFound = -2,
        AddedBefore = -3,
    }
}