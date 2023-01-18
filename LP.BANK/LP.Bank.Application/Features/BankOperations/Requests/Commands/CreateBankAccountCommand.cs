using LP.Bank.Application.DTOs.CreateBankAccount;
using LP.Bank.Application.Responses;
using MediatR;

namespace LP.Bank.Application.Features.BankOperations.Requests.Commands
{
    public class CreateBankAccountCommand : IRequest<BaseCommandResponse>
    {
        public CreateBankAccountDto Dto { get; set; }
    }
}
