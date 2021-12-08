﻿using Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Spex.Stock.Api.Controllers
{
    [Produces("application/json")]
    [ProducesResponseType(typeof(ClientErrors), 400)]
    [ProducesResponseType(typeof(ServerError), 500)]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {

    }
}
