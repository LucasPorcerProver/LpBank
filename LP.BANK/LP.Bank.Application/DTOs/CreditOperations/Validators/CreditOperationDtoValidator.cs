using FluentValidation;
using LP.Bank.Application.Contracts.Persistence;

namespace LP.Bank.Application.DTOs.CreditOperations.Validators
{
    public class CreditOperationDtoValidator : AbstractValidator<CreditOperationDto>
    {
        public CreditOperationDtoValidator(IBankAccountRepository bankAccountRepository)
        {
            RuleFor(p => p.AccountId)
               .NotNull()
               .MustAsync(async (id, token) =>
               {
                   var accountExists = await bankAccountRepository.AccountExists(id);
                   return accountExists;
               })
               .WithMessage("{PropertyName} does not exist");

            RuleFor(p => p.Value)
                .GreaterThan(0)               
                .WithMessage("{PropertyName} needs to be more than 0.");
        }
    }
}
