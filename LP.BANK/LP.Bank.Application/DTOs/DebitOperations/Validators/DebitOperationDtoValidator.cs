using FluentValidation;
using LP.Bank.Application.Contracts.Persistence;

namespace LP.Bank.Application.DTOs.DebitOperations.Validators
{
    public class DebitOperationDtoValidator : AbstractValidator<DebitOperationDto>
    {       
        public DebitOperationDtoValidator(IBankAccountRepository bankAccountRepository)
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
