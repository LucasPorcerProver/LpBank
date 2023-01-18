using LP.Bank.Application.DTOs.CreditOperations;
using LP.Bank.Application.Responses;
using MediatR;

namespace LP.Bank.Application.Features.BankOperations.Requests.Commands
{
    public class CreateCreditOperationCommand : IRequest<BaseCommandResponse>
    {
        public CreditOperationDto CreditOperationDto { get; set; }
    }
}
