using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            var bl = new BusinessManager();
            _terminal = bl.GetTerminal();
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationResponse>> Auth([FromBody] AuthenticationMessage message)
        {
            var user = await _userManager.FindByNameAsync(message.IDNP);

                if (user!=null)
            {
                var signInResult= await _signInManager.PasswordSignInAsync(message.IDNP, message.VnPassword, false, false);
                if (signInResult.Succeeded)
                    RedirectToAction("Index", "Home");
            }
            else
            {
                BadRequest();
            }

            var resp = await _terminal.Auth(message);
            return resp;

        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}