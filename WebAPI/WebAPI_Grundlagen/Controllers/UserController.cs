using ArticleLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Grundlagen.Models.DB;

namespace WebAPI_Grundlagen.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DbManager _dbManager;

        public UserController(DbManager dbManager)
        {
            this._dbManager = dbManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            user.ApiKey = Guid.NewGuid();
            await _dbManager.AddAsync(user);
            await _dbManager.SaveChangesAsync();
            return new JsonResult(user.ApiKey);
        }
    }
}
