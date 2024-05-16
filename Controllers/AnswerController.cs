using CapitalTest.IServices;
using CapitalTest.Models;
using CapitalTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        private readonly ILogger<AnswerController> _logger;
        public AnswerController(IAnswerService answerService, ILogger<AnswerController> logger)
        {
            this._answerService = answerService ?? throw new ArgumentNullException(nameof(answerService));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/Answer/:id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAnswerById(Guid id)
        {
            try
            {
                var answer = await _answerService.GetAnswerById(id);
                if (answer == null)
                {
                    return NotFound();
                }
                return Ok(answer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching answers by id.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/Answer/user/:id
        [HttpGet("user/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAnswerByUser(Guid id)
        {
            try
            {
                var answer = await _answerService.GetAnswerByUser(id);
                if (answer == null)
                {
                    return NotFound();
                }
                return Ok(answer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching answers by user id.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // POST: api/Answer
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SubmitAnswer(Answers answer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var submittedAnswer = await _answerService.SubmitAnswer(answer);
                return CreatedAtAction(nameof(GetAnswerById), new { id = submittedAnswer.Id }, submittedAnswer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while submitting answers.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
