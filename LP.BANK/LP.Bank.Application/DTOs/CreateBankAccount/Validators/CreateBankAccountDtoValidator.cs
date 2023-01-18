using FluentValidation;
using LP.Bank.Application.Contracts.Persistence;

namespace LP.Bank.Application.DTOs.CreateBankAccount.Validators
{
    public class CreateBankAccountDtoValidator : AbstractValidator<CreateBankAccountDto>
    {
        public CreateBankAccountDtoValidator(IBankAccountRepository bankAccountRepository)
        {
            RuleFor(p => p.Number)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var accountExists = await bankAccountRepository.AccountNumberExists(id);
                    return accountExists == false;
                })
                .WithMessage("{PropertyName} already exist");
        }
    }
}
