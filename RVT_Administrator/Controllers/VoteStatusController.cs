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
using RVT_Block_lib.Requests;

namespace RVT_Administrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteStatusController : ControllerBase
    {
        private readonly ITerminal _terminal;

        public VoteStatusController()
        {
            var bl = new BusinessManager();
            _terminal = bl.GetTerminal();
        }


        [HttpPost]
        public async Task<ActionResult<VoteStatusResponse>> VoteStatus([FromBody] VoteStatusMessage message)
        {
            if (ModelState.IsValid)
            {
                var response = await _terminal.VoteStatus(message);
                return response;
            }
            else 
                return BadRequest();
        }
    }
}
