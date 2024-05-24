using AutoMapper;
using Domain.entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SozonovBackend.ConfigurationManager;
using SozonovBackend.Models.Proposal;
using SozonovBackend.Repository;
using SozonovBackend.Services.MailSendService;

namespace SozonovBackend.Controllers.UsersControllers
{
    [ApiController]
    [Route("api/proposal/[action]")]
    [Route("api/proposal")]
    [Authorize]
    public class ProposalController : BaseApiController
    {
        private readonly IRepositoryProposal _repositoryProposal;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        public ProposalController(ILogger<ProposalController> logger,
            IRepositoryProposal repository,
            IMailService mailService,
            IMapper mapper) : base(logger)
        {
            _repositoryProposal = repository;
            _mailService = mailService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProposals()
        {
            _logger.LogInformation($"Api метод GetAllProposals, контроллера {nameof(ProposalController)} начал свою работу");
            return await Invoke<IEnumerable<Proposal>>(() => _repositoryProposal.GetAll());
        }

        [HttpPost]
        [Route("{id:int}")]
        public async Task<IActionResult> SendAnswer([FromBody] ProposalAnswerRequest answer, int id)
        {
            _logger.LogInformation($"Api метод Id, контроллера {nameof(ProposalController)} начал свою работу," +
                $" входные данные: Answer: {answer.Answer}");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Api метод получил невалидные данные: {ModelState}");
                return BadRequest(ModelState);
            }

            return await Invoke<IActionResult>(() => MakeAnswer(id, answer));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] ProposalRequest proposal)
        {
            _logger.LogInformation($"Api метод Create, контроллера {nameof(ProposalController)} начал свою работу," +
                $" входные данные: FirstName: {proposal.Name}," +
                $" Email: {proposal.Text}, " +
                $"Text: {proposal.Text}");

            if(!ModelState.IsValid)
            {
                _logger.LogWarning($"Api метод получил невалидные данные: {ModelState}");
                return BadRequest(ModelState);
            }

            return await Invoke(() =>
            {
                var proposalResponse = _mapper.Map<Proposal>(proposal);
                proposalResponse.DateTime = DateTime.Now.ToUniversalTime();

                _repositoryProposal.Add(proposalResponse);

                return Task.CompletedTask;
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] ProposalDeleteRequest proposalRequest)
        {
            _logger.LogInformation($"Api метод Delete, контроллера {nameof(ProposalController)} начал свою работу," +
                $" входные данные: Id: {proposalRequest.Id.Count()}");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Api метод получил невалидные данные: {ModelState}");
                return BadRequest(ModelState);
            }

            return await Invoke(() => _repositoryProposal.Delete(proposalRequest.Id));
        }

        private async Task<IActionResult> MakeAnswer(int id, ProposalAnswerRequest answer)
        {
            var proposal = await _repositoryProposal.Get(id);
            proposal.Answer = answer.Answer;

            var senderSettings = new SenderMailSettings
            {
                Body = answer.Answer,
                Subject = Configurations.MailSettings.SenderName,
                RecipientMail = proposal.Mail
            };

            var messageSendResult = await _mailService.SendMailSettings(senderSettings,
                Configurations.MailSettings);

            if (!messageSendResult)
            {
                _logger.LogInformation($"при ответе на заявку с id: {id} произошла ошибка");
                proposal.StatusCode = ProposalStatus.Error;
                await _repositoryProposal.Update(proposal);

                return StatusCode(500);
            }

            proposal.StatusCode = ProposalStatus.Done;
            await _repositoryProposal.Update(proposal);
            _logger.LogInformation($"Ответ на заявку с id: {id} отправлен успешно");

            return Ok();
        }
    }
}
