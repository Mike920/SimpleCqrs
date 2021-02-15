using MessageApp.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageApp.Controllers
{
    public class BaseApiController : ControllerBase
    {
        public ObjectResult ApiContent<TResult>(Result<TResult> result)
        {
            return StatusCode((int)result.Status, result.Content);
        }
    }
}
