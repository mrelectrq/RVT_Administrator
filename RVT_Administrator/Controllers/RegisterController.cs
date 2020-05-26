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
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RegisterController : ControllerBase
    {
        ITerminal terminal;

        public RegisterController()
        {
           var bl = new BusinessManager();
            terminal = bl.GetTerminal();
        }


        [HttpPost]
        public async Task<ActionResult<RegistrationResponse>> RegAct([FromBody]RegistrationMessage message)
        {
            //string test = message;
            if (ModelState.IsValid)
            {
               // var request = RegistrationMessage.Deserialize(message);

                var result = await terminal.Registration(message);

                return result;
            }
            else
                return BadRequest();


        }

    }
}