using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SozonovBackend.Controllers.UsersControllers;
using SozonovBackend.Models.Admin;

namespace SozonovBackend.Controllers.AdminController
{
    [Route("api/admin/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginJson login)
        {
            _logger.LogInformation($"Api метод Login, контроллера {nameof(AdminController)} начал свою работу," +
                $" входные данные: Login: {login.Login}, Password: {login.Password}");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Api метод получил невалидные данные: {ModelState}");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Password([FromBody] LoginJson login)
        {
            _logger.LogInformation($"Api метод Password, контроллера {nameof(AdminController)} начал свою работу," +
                $" входные данные: Login: {login.Login}, Password: {login.Password}");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Api метод получил невалидные данные: {ModelState}");
                return BadRequest(ModelState);
            }

            return Ok();

        }
    }
}
