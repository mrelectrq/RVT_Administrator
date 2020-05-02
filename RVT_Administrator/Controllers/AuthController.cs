using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public AuthController()
        {
            var bl = new BusinessManager();
            _terminal = bl.GetTerminal();
        }



        [HttpPost]
        public async Task<ActionResult<AuthenticationResponse>> Auth([FromBody] AuthenticationMessage message)
        {

            if(string.IsNullOrEmpty(message.IDNP))
            {
                return BadRequest();
            }
            if(string.IsNullOrEmpty(message.VnPassword))
            {
                return BadRequest();
            }

                var resp = await _terminal.Authentication(message);
                return resp;
              
        }
    }
}