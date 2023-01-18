using LP.Bank.Application.DTOs.DebitOperations;
using LP.Bank.Application.Responses;
using MediatR;

namespace LP.Bank.Application.Features.BankOperations.Requests.Commands
{
    public class CreateDebitOperationCommand : IRequest<BaseCommandResponse>
    {
        public DebitOperationDto DebitOperationDto { get; set; }
    }
}
