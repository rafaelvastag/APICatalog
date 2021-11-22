﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalog.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{v:apiVersion}/version")]
    [ApiController]
    public class VersioningV1Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("<html><body><h2> VersioningV1Controller - V 1.0 </h2></body></html>", "text/html");
        }
    }
}
