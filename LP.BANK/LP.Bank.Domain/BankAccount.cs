using System;
using System.ComponentModel.DataAnnotations;

namespace LP.Bank.Domain
{
    public class BankAccount        
    {
        [Key]
        public Guid Id { get; set; }
        public decimal? Ammount { get; set; }
        public int Number { get; set; }
    }
}
