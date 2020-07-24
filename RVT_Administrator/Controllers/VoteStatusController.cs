﻿using System;
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
        private readonly IAdmin _admin;

        public VoteStatusController()
        {
            var bl = new BusinessManager();
            _admin = bl.GetAdmin();
        }


        [HttpGet]
        public async Task<ActionResult<VoteStatusResponse>> VoteStatus()
        {
            if (ModelState.IsValid)
            {
                var response = await _admin.VoteStatus();
                return response;
            }
            else 
                return BadRequest();
        }
    }
}
