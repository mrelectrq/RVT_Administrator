using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
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
        private static Logger _nLog = LogManager.GetLogger("UserLog");
        public RegisterController()
        {
           var bl = new BusinessManager();
            terminal = bl.GetTerminal();
        }


        [HttpPost]
        public async Task<ActionResult<RegistrationResponse>> RegAct([FromBody]RegistrationMessage message)
        {
            if (ModelState.IsValid)
            {
                var result = await terminal.Registration(message);
                if (result.Status = false)
                    _nLog.Error(result.Status);
                else
                    _nLog.Info(result.Message);
                return result;
            }
            else
                return BadRequest();


        }

    }
}