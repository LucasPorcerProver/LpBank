using LP.Bank.Application.Contracts.Persistence;
using LP.Bank.Application.Features.BankOperations.Requests.Commands;
using LP.Bank.Application.Responses;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using LP.Bank.Application.DTOs.CreditOperations.Validators;

namespace LP.Bank.Application.Features.BankOperations.Handlers.Commands
{
    public class CreateCreditOperationCommandHandler : IRequestHandler<CreateCreditOperationCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCreditOperationCommandHandler(
           IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseCommandResponse> Handle(CreateCreditOperationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreditOperationDtoValidator(_unitOfWork.BankAccountRepository);
            var validationResult = await validator.ValidateAsync(request.CreditOperationDto);

            if (validationResult.IsValid == false)
            {
                return BaseCommandResponse.ThrowNewErrorResponse("Credit Operation Failed", validationResult.Errors.Select(q => q.ErrorMessage).ToList());
            }

            var account = await _unitOfWork.BankAccountRepository.Get(request.CreditOperationDto.AccountId);

            await _unitOfWork.BankAccountRepository.Update(account);
            await _unitOfWork.Save();

            return BaseCommandResponse.ThrowNewSuccessResponse("Operation Successful");
        }
    }
}
