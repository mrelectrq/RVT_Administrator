using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using RVT_A_BusinessLayer;
using RVT_A_BusinessLayer.Interfaces;
using RVT_A_BusinessLayer.Responses;
using RVT_Block_lib.Models;

namespace RVT_Administrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITerminal _terminal;

        private static Logger _nLog = LogManager.GetLogger("UserLog");
        public AuthController()
        {
            var bl = new BusinessManager();
            _terminal = bl.GetTerminal();
        }

        [HttpHead]
        public IActionResult Head()
        {
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<AuthenticationResponse>> Auth([FromBody] AuthenticationMessage message)
        {
            if (ModelState.IsValid)
            {
                var resp = await _terminal.Auth(message);
                if (resp.Status = true)
                    _nLog.Info(resp.Message);
                else _nLog.Error(resp.Message);
                return resp;
            }
            else
                return BadRequest();
        }

    }
}