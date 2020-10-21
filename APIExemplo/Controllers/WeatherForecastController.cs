using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APIExemplo.Controllers
{
    [Authorize("ValidaUsuario")]
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //usar verbos (entrada)
        //usar HTTP Codes (saída)

        [HttpGet("[action]")]
        public IActionResult ObterHoras()
        {
            return Ok(DateTime.Now.ToString());
        }

        [HttpGet("[action]")]
        public IActionResult ObterHorasTexto()
        {
            return Ok(DateTime.Now.ToLongDateString());
        }


        [HttpPost]
        public IActionResult Rebecer()
        {
            return Ok("dados recebidos");
        }

    }
}
