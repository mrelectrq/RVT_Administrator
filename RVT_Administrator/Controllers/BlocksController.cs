using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RVT_A_BusinessLayer;
using RVT_A_BusinessLayer.Interfaces;
using RVT_A_BusinessLayer.Responses;
using RVT_A_DataLayer.Entities;
using RVT_Block_lib.Models;

namespace RVT_Administrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlocksController : ControllerBase
    {
        private readonly IAdmin _admin;
        public BlocksController()
        {
            var bl = new BusinessManager();
            _admin = bl.GetAdmin();
        }


        [HttpPost]
        public async Task<ActionResult<List<Blocks>>> Block(BlocksMessage blockmess)
        {
            var response = await _admin.Blocks(blockmess);

            return response;
        }
    }
}
