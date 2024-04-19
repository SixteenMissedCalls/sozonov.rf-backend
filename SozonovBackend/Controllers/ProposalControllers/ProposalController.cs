using Microsoft.AspNetCore.Mvc;
using SozonovBackend.Models.Proposal;

namespace SozonovBackend.Controllers.UsersControllers
{
    [ApiController]
    [Route("api/proposal/{action}")]
    public class ProposalController : ControllerBase
    {
        private readonly ILogger _logger;
        public ProposalController(ILogger<ProposalController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("api/proposal")]
        public async Task<IActionResult> GetAllProposals()
        {
            _logger.LogInformation($"Api метод GetAllProposals, контроллера {nameof(ProposalController)} начал свою работу");
            return Ok();
        }

        [HttpPost]
        [ActionName("Id")]
        public async Task<IActionResult> SendAnswer([FromBody] AnswerJson answer)
        {
            _logger.LogInformation($"Api метод Id, контроллера {nameof(ProposalController)} начал свою работу," +
                $" входные данные: Answer: {answer.Answer}");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Api метод получил невалидные данные: {ModelState}");
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProposalJson proposal)
        {
            _logger.LogInformation($"Api метод Create, контроллера {nameof(ProposalController)} начал свою работу," +
                $" входные данные: FirstName: {proposal.FirstName}," +
                $" Email: {proposal.Text}, " +
                $"Text: {proposal.Text}");

            if(!ModelState.IsValid)
            {
                _logger.LogWarning($"Api метод получил невалидные данные: {ModelState}");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] ProposalDeleteJson proposal)
        {
            _logger.LogInformation($"Api метод Delete, контроллера {nameof(ProposalController)} начал свою работу," +
                $" входные данные: Id: {proposal.Id.Count()}");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Api метод получил невалидные данные: {ModelState}");
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
