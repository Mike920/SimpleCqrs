using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MessageApp.Application.Models
{
    public class Result<TResult>
    {
        public Result(HttpStatusCode Status, TResult Content)
        {
            this.Status = Status;
            this.Content = Content;
        }
        public HttpStatusCode Status { get; set; }
        public TResult Content { get; set; }

    }

    public static class Result
    {

        public static Result<TResult> NoContent<TResult>(TResult data) => new Result<TResult>(HttpStatusCode.NoContent, data);
        public static Result<TResult> Ok<TResult>(TResult data) => new Result<TResult>(HttpStatusCode.OK, data);
        public static Result<TResult> NotFound<TResult>(TResult data) => new Result<TResult>(HttpStatusCode.NotFound, data);
    }
}
