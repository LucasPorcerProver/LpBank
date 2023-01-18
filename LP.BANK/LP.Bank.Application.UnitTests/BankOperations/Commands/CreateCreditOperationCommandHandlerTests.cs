using LP.Bank.Application.Contracts.Persistence;
using LP.Bank.Application.DTOs.CreditOperations;
using LP.Bank.Application.Features.BankOperations.Handlers.Commands;
using LP.Bank.Application.Features.BankOperations.Requests.Commands;
using LP.Bank.Application.Responses;
using LP.Bank.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LP.Bank.Application.UnitTests.BankOperations.Commands
{
    public class CreateCreditOperationCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly CreateCreditOperationCommandHandler _handler;

        public CreateCreditOperationCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            _handler = new CreateCreditOperationCommandHandler(_mockUow.Object);
        }

        [Fact]
        public async Task ShouldNotAddCreditWhenAccountNotExists()
        {
            var dto = new CreditOperationDto() {  AccountId = Guid.NewGuid(), Value = 999 };
            var result = await _handler.Handle(new CreateCreditOperationCommand() { CreditOperationDto = dto }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Errors.ShouldNotBeNull();
            result.Success.ShouldBeFalse();
        }

        [Fact]
        public async Task ShouldAddCreditWithSuccess()
        {
            var mockRepo = new Mock<IBankAccountRepository>();
            var mockUow = new Mock<IUnitOfWork>();

            mockRepo.Setup(r => r.AccountExists(It.IsAny<Guid>())).ReturnsAsync(true);
            mockUow.Setup(r => r.BankAccountRepository).Returns(mockRepo.Object);

            var handler = new CreateCreditOperationCommandHandler(mockUow.Object);

            var dto = new CreditOperationDto() { AccountId = It.IsAny<Guid>(), Value = 999 };
            var result = await handler.Handle(new CreateCreditOperationCommand() { CreditOperationDto = dto }, CancellationToken.None);


            result.ShouldBeOfType<BaseCommandResponse>();
            result.Errors.ShouldBeNull();
            result.Success.ShouldBeTrue();
        }
    }
}
