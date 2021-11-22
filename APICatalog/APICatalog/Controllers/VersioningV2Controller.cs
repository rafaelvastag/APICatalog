using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalog.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/version")]
    [ApiController]
    public class VersioningV2Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("<html><body><h2> VersioningV2Controller - V 2.0 </h2></body></html>", "text/html");
        }
    }
}
