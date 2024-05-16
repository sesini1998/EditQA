using CapitalTest.DTOs;
using CapitalTest.IServices;
using CapitalTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CapitalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramsService _programService;
        private readonly ILogger<ProgramController> _logger;
        public ProgramController(IProgramsService programService, ILogger<ProgramController> logger)
        {
            this._programService = programService ?? throw new ArgumentNullException(nameof(programService));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/Program/:id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetProgramById(Guid id)
        {
            try
            {
                var result = await _programService.GetProgramById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching program by id.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // Post : api/Program/add
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProgram([FromBody] CreateProgramDto program)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _programService.AddProgram(program);
                return CreatedAtAction(nameof(GetProgramById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding program.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // PUT: api/Program/:id
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProgram(UpdateProgramDto program, Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _programService.UpdateProgram(program, id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating program.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        // DELETE: api/Program/:id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProgram(Guid id)
        {
            try
            {
                var result = await _programService.DeleteProgram(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting program.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
