using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog;
using RVT_A_BusinessLayer;
using RVT_A_BusinessLayer.Interfaces;
using RVT_Block_lib.Models;
using RVT_Block_lib.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RVT_Administrator.Controllers
{
    [Route("api/[controller]")]
    public class VoteController : Controller
    {
        private readonly ITerminal _terminal;
        private static Logger _nLog = LogManager.GetLogger("UserLog");
        public VoteController()
        {
            var bl = new BusinessManager();
            _terminal = bl.GetTerminal();
        }


        [HttpPost]
        public async Task<ActionResult<VoteAdminResponse>> Vote([FromBody] VoteAdminMessage message)
        {
            var response = _terminal.Vote(message);
            if (response.VoteStatus = true)
                _nLog.Info(response.Message);
            else _nLog.Error(response.Message);
            return response;
        }
    }
}
