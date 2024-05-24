using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SozonovBackend.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ILogger _logger;
        public BaseApiController(ILogger logger)
        {
            _logger = logger;
        }

        protected async Task<IActionResult> Invoke<T>(Func<Task<T>> func)
        {
            try
            {
                var result = await func().ConfigureAwait(false);

                if (result is IActionResult actionResult)
                    return actionResult;

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }

        protected async Task<IActionResult> Invoke(Func<Task> func)
        {
            try
            {
                await func().ConfigureAwait(false);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }
    }
}
