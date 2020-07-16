using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RVT_A_BusinessLayer;
using RVT_A_BusinessLayer.Interfaces;
using RVT_Block_lib.Models;
using RVT_A_BusinessLayer.Responses;

namespace RVT_Administrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAuthController : ControllerBase
    {
        private readonly IAuth auth;

        public AdminAuthController()
        {
            var bl = new BusinessManager();
            auth = bl.GetAuth();
        }

        [HttpPost]
        public async Task<ActionResult<AdminAuthResp>> AdminAuth([FromBody] AdminAuthMessage message)
        {
            if (ModelState.IsValid)
            {
                var resp = await auth.AdminAuth(message);
                return resp;
            }
            else
                return BadRequest();
        }
    }
}
