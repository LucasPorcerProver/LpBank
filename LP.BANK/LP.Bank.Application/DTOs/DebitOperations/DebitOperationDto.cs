using System;

namespace LP.Bank.Application.DTOs.DebitOperations
{
    public class DebitOperationDto
    {
        public Guid AccountId { get; set; }
        public decimal Value { get; set; }
    }
}
