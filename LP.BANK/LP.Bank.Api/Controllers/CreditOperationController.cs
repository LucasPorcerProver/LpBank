using LP.Bank.Application.DTOs.CreditOperations;
using LP.Bank.Application.Features.BankOperations.Requests.Commands;
using LP.Bank.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LP.Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CreditOperationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CreditOperationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreditOperationDto request)
        {
            var command = new CreateCreditOperationCommand { CreditOperationDto = request };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
