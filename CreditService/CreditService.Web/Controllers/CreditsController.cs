using CreditService.Application.Credits.Commands;
using CreditService.Application.Credits.Queries;
using CreditService.Web.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CreditsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CreditDTO>>> GetAllCredits([FromQuery] GetAllCreditsQuery query)
        {

            var credits = await _mediator.Send(query);

            if (credits == null || credits.Count == 0)
            {
                return NotFound("No credits found!");
            }

            return Ok(credits);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCredit([FromBody] CreateCreditCommand command)
        {
            try
            {
                var creditId = await _mediator.Send(command);

                if (creditId == Guid.Empty) return BadRequest($"Error GUID {creditId} is empty");

                return Ok(creditId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
        [HttpPut("{number}")]
        public async Task<IActionResult> CreditUpdate(string number, [FromBody] CreditUpdateRequest request)
        {
            var command = new UpdateCreditCommand()
            {
                Number = number,
                Status = request.Status
            };
            await _mediator.Send(command);

            return NoContent();
        }

    }
}
