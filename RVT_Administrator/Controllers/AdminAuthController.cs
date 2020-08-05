using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RVT_A_BusinessLayer;
using RVT_Block_lib.Models;
using RVT_A_BusinessLayer.Responses;
using RVT_A_BusinessLayer.Interfaces;
using NLog;

namespace RVT_Administrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAuthController : ControllerBase
    {
        private readonly IAdmin auth;
        private static Logger _nLog=LogManager.GetLogger("AdminLog");

        public AdminAuthController()
        {
            var bl = new BusinessManager();
            auth = bl.GetAdmin();
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
