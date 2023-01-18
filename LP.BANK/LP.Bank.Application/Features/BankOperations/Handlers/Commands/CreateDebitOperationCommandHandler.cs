using LP.Bank.Application.DTOs.DebitOperations.Validators;
using LP.Bank.Application.Features.BankOperations.Requests.Commands;
using LP.Bank.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LP.Bank.Application.Responses;
using System.Linq;

namespace LP.Bank.Application.Features.BankOperations.Handlers.Commands
{
    public class CreateDebitOperationCommandHandler : IRequestHandler<CreateDebitOperationCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDebitOperationCommandHandler(
           IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseCommandResponse> Handle(CreateDebitOperationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new DebitOperationDtoValidator(_unitOfWork.BankAccountRepository);
            var validationResult = await validator.ValidateAsync(request.DebitOperationDto);

            if (validationResult.IsValid == false)
            {
                return BaseCommandResponse.ThrowNewErrorResponse("Debit Operation Failed", validationResult.Errors.Select(q => q.ErrorMessage).ToList());
            }

            var account = await _unitOfWork.BankAccountRepository.Get(request.DebitOperationDto.AccountId);

            if (account.Ammount < request.DebitOperationDto.Value)
            {
                return BaseCommandResponse.ThrowNewErrorResponse("Not Ammount for finish this operation");
            }

            await _unitOfWork.BankAccountRepository.Update(account);
            await _unitOfWork.Save();

            return BaseCommandResponse.ThrowNewSuccessResponse("Operation Successful");
        }
    }
}
