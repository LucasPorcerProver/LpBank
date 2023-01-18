using LP.Bank.Application.DTOs.CreateBankAccount;
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
    public class BankAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BankAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> CreateAccount([FromBody] CreateBankAccountDto request)
        {
            var command = new CreateBankAccountCommand { Dto = request };
            var response = await _mediator.Send(command);
            return Created("", response);
        }
    }
}
