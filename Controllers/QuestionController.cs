using CapitalTest.DTOs;
using CapitalTest.IServices;
using CapitalTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapitalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly ILogger<QuestionController> _logger;
        public QuestionController(IQuestionService questionService, ILogger<QuestionController> logger)
        {
            this._questionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Post : api/Question/add
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddQuestion([FromBody] CreateQuestionDto question)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _questionService.AddQuestion(question);
                return CreatedAtAction(nameof(GetQuestionById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding question.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // DELETE: api/Question/:id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            try
            {
                var result = await _questionService.DeleteQuestion(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting question.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/Question/getbytype/:type
        [HttpGet("getbytype/{type}")]
        [Authorize]
        public async Task<IActionResult> GetQuestionsByType(QuestionType type)
        {
            try
            {
                var result = await _questionService.GetQuestionsByType(type);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching questions by type.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // GET: api/Question/:id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetQuestionById(Guid id)
        {
            try
            {
                var result = await _questionService.GetQuestionById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching question by id.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // PUT: api/Question/:id
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateQuestion(UpdateQuestionDto question, Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _questionService.UpdateQuestion(question, id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating question.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }   
    }
}
