using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.Helpers
{
    public class Result<T>
    {
        public bool Succeed { get; set; }
        public string? Error { get; set; }
        public T? Data { get; set; }

        public static Result<T> Success(T data) => new() { Succeed = true, Data = data };
        public static Result<T> Fail(string error) => new() { Succeed = false, Error = error };
    }
}
