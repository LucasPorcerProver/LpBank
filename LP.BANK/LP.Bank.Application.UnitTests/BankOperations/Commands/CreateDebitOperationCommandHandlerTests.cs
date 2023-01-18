using LP.Bank.Application.Contracts.Persistence;
using LP.Bank.Application.DTOs.CreditOperations;
using LP.Bank.Application.DTOs.DebitOperations;
using LP.Bank.Application.Features.BankOperations.Handlers.Commands;
using LP.Bank.Application.Features.BankOperations.Requests.Commands;
using LP.Bank.Application.Responses;
using LP.Bank.Application.UnitTests.Mocks;
using LP.Bank.Domain;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LP.Bank.Application.UnitTests.BankOperations.Commands
{
    public class CreateDebitOperationCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly CreateDebitOperationCommandHandler _handler;

        public CreateDebitOperationCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            _handler = new CreateDebitOperationCommandHandler(_mockUow.Object);
        }

        [Fact]
        public async Task ShouldNotAddDebitWhenAccountNotExists()
        {
            var dto = new DebitOperationDto() { AccountId = Guid.NewGuid(), Value = 999 };
            var result = await _handler.Handle(new CreateDebitOperationCommand() { DebitOperationDto = dto }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Errors.ShouldNotBeNull();
            result.Success.ShouldBeFalse();
        }

        [Fact]
        public async Task ShouldAddDebitWithSuccess()
        {
            var mockRepo = new Mock<IBankAccountRepository>();
            var mockUow = new Mock<IUnitOfWork>();

            mockRepo.Setup(r => r.AccountExists(It.IsAny<Guid>())).ReturnsAsync(true);
            mockRepo.Setup(r => r.Get(It.IsAny<Guid>())).ReturnsAsync(new BankAccount()
            {
                Ammount = 999,
                Id = It.IsAny<Guid>(),
                Number = It.IsAny<int>()
            });

            mockUow.Setup(r => r.BankAccountRepository).Returns(mockRepo.Object);

            var handler = new CreateDebitOperationCommandHandler(mockUow.Object);

            var dto = new DebitOperationDto() { AccountId = It.IsAny<Guid>(), Value = 90 };
            var result = await handler.Handle(new CreateDebitOperationCommand() { DebitOperationDto = dto }, CancellationToken.None);


            result.ShouldBeOfType<BaseCommandResponse>();
            result.Errors.ShouldBeNull();
            result.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task ShouldNotAddDebitWhenAmmountIsInsuficient()
        {
            var mockRepo = new Mock<IBankAccountRepository>();
            var mockUow = new Mock<IUnitOfWork>();

            mockRepo.Setup(r => r.AccountExists(It.IsAny<Guid>())).ReturnsAsync(true);
            mockRepo.Setup(r => r.Get(It.IsAny<Guid>())).ReturnsAsync(new BankAccount()
            {
                Ammount = 999,
                Id = It.IsAny<Guid>(),
                Number = It.IsAny<int>()
            });

            mockUow.Setup(r => r.BankAccountRepository).Returns(mockRepo.Object);

            var handler = new CreateDebitOperationCommandHandler(mockUow.Object);

            var dto = new DebitOperationDto() { AccountId = It.IsAny<Guid>(), Value = 1000 };
            var result = await handler.Handle(new CreateDebitOperationCommand() { DebitOperationDto = dto }, CancellationToken.None);


            result.ShouldBeOfType<BaseCommandResponse>();

            result.Message.ShouldBe("Not Ammount for finish this operation");
            result.Success.ShouldBeFalse();
        }
    }
}
