using LP.Bank.Application.Contracts.Persistence;
using LP.Bank.Application.UnitTests.Mocks.Repositories;
using Moq;

namespace LP.Bank.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockBankAccountRepo = MockBankAccountRepository.GetBankAccountRepository();

            mockUow.Setup(r => r.BankAccountRepository).Returns(mockBankAccountRepo.Object);

            return mockUow;
        }
    }
}
