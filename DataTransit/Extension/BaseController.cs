using DataTransit.Contract.Messaging.Base;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DataTransit.Extension
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public new IActionResult Response<T>(T model, HttpStatusCode statusCode = default(HttpStatusCode)) where T : BaseApiResponse
        {
            var sCode = statusCode == default(HttpStatusCode) ? model.IsSucceed ? HttpStatusCode.OK : model.IsAccessDenied ? HttpStatusCode.Forbidden : HttpStatusCode.BadRequest : statusCode;

            return new ObjectResult(model) { StatusCode = (int)sCode };
        }
    }
}
