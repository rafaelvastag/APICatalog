using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalog.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{v:apiVersion}/version")]
    [ApiController]
    public class VersioningV1Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("<html><body><h2> VersioningV1Controller - GET V 1.0 </h2></body></html>", "text/html");
        }

        [HttpGet, MapToApiVersion("2.0")]
        public IActionResult GetV2()
        {
            return Content("<html><body><h2> VersioningV1Controller - GET V 2.0 </h2></body></html>", "text/html");
        }
    }
}
