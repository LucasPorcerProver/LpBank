using LP.Bank.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LP.Bank.Infra.Data.Bank.Configurations.Entities
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("bank_account");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnType("uuid")
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
                .Property(x => x.Ammount)
                .HasColumnName("ammount")
                .HasColumnType("numeric");

            builder
                .Property(x => x.Number)
                .HasColumnName("number")
                .HasColumnType("numeric");

            builder.HasData(
                 new BankAccount
                 {
                     Id = Guid.NewGuid(),
                     Number = 8445865,
                     Ammount = 999,
                 },
                 new BankAccount
                 {
                     Id = Guid.NewGuid(),
                     Number = 9224968,
                     Ammount = 50
                 }
            );
        }
    }
}
