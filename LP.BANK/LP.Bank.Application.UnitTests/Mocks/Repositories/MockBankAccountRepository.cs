using LP.Bank.Application.Contracts.Persistence;
using LP.Bank.Domain;
using Moq;
using System;

namespace LP.Bank.Application.UnitTests.Mocks.Repositories
{
    public static class MockBankAccountRepository
    {
        public static Mock<IBankAccountRepository> GetBankAccountRepository()
        {
            var mockRepo = new Mock<IBankAccountRepository>();

            mockRepo.Setup(r => r.Add(It.IsAny<BankAccount>())).ReturnsAsync(It.IsAny<BankAccount>());
            mockRepo.Setup(r => r.Update(It.IsAny<BankAccount>()));

            mockRepo.Setup(r => r.Get(It.IsAny<Guid>())).ReturnsAsync(It.IsAny<BankAccount>());

            mockRepo.Setup(r => r.AccountExists(It.IsAny<Guid>())).ReturnsAsync(It.IsAny<bool>());
            mockRepo.Setup(r => r.AccountNumberExists(It.IsAny<int>())).ReturnsAsync(It.IsAny<bool>());

            return mockRepo;
        }

       
    }
}
