using System;

namespace LP.Bank.Application.DTOs.CreditOperations
{
    public class CreditOperationDto
    {
        public Guid AccountId { get; set; }
        public decimal Value { get; set; }
    }
}
