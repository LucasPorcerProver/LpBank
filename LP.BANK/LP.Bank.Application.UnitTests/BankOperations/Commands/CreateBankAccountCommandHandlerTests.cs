using AutoMapper;
using LP.Bank.Application.Contracts.Persistence;
using LP.Bank.Application.DTOs.CreateBankAccount;
using LP.Bank.Application.Features.BankOperations.Handlers.Commands;
using LP.Bank.Application.Features.BankOperations.Requests.Commands;
using LP.Bank.Application.Responses;
using LP.Bank.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LP.Bank.Application.UnitTests.BankOperations.Commands
{
    public class CreateBankAccountCommandHandlerTests
    {      
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly CreateBankAccountCommandHandler _handler;

        public CreateBankAccountCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            _handler = new CreateBankAccountCommandHandler(_mockUow.Object);
        }

        [Fact]
        public async Task ShouldAddBankAccountWithSuccess()
        {
            var dto = new CreateBankAccountDto() { Ammount = 9999, Number = 200 };
            var result = await _handler.Handle(new CreateBankAccountCommand() { Dto = dto }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Errors.ShouldBeNull();
            result.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task ShouldNotAddBankAccountWhenNumberAlreadyExists()
        {
            var mockRepo = new Mock<IBankAccountRepository>();
            var mockUow = new Mock<IUnitOfWork>();

            mockRepo.Setup(r => r.AccountNumberExists(It.IsAny<int>())).ReturnsAsync(true);
            mockUow.Setup(r => r.BankAccountRepository).Returns(mockRepo.Object);

            var handler = new CreateBankAccountCommandHandler(mockUow.Object);

            var dto = new CreateBankAccountDto() { Ammount = 9999, Number = 200 };

            var commandResultFistAccount = await handler.Handle(new CreateBankAccountCommand() { Dto = dto }, CancellationToken.None);

            commandResultFistAccount.ShouldBeOfType<BaseCommandResponse>();
            commandResultFistAccount.Errors.ShouldNotBeNull();
            commandResultFistAccount.Success.ShouldBeFalse();
        }
    }
}
