using LP.Bank.Application.Features.BankOperations.Requests.Commands;
using LP.Bank.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LP.Bank.Application.Responses;
using System.Linq;
using LP.Bank.Domain;
using LP.Bank.Application.DTOs.CreateBankAccount.Validators;

namespace LP.Bank.Application.Features.BankOperations.Handlers.Commands
{
    public class CreateBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBankAccountCommandHandler(
           IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseCommandResponse> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBankAccountDtoValidator(_unitOfWork.BankAccountRepository);
            var validationResult = await validator.ValidateAsync(request.Dto);

            if (validationResult.IsValid == false)
            {
                return BaseCommandResponse.ThrowNewErrorResponse("Create Account Error", validationResult.Errors.Select(q => q.ErrorMessage).ToList());
            }

            var account = new BankAccount()
            {
                Ammount = request.Dto.Ammount,
                Number = request.Dto.Number
            };

            await _unitOfWork.BankAccountRepository.Add(account);
            await _unitOfWork.Save();

            return BaseCommandResponse.ThrowNewSuccessResponse("Operation Successful");
        }
    }
}
