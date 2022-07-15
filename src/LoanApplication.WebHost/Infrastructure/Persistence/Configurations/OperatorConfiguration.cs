using LoanApplication.WebHost.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoanApplication.WebHost.Infrastructure.Persistence.Configurations;

public class OperatorMapping : IEntityTypeConfiguration<Operator>
{
    public void Configure(EntityTypeBuilder<Operator> builder)
    {
        builder.ToTable("Operators");
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasConversion(x => x.Value, x => new OperatorId(x));

        builder
            .Property(x => x.Login)
            .HasConversion(x => x.Value, x => Login.Of(x, null).Value)
            .HasMaxLength(50);

        builder
            .Property(x => x.Password)
            .HasConversion(x => x.Value, x => Password.Of(x, null).Value)
            .HasMaxLength(50);

        builder
            .OwnsOne(x => x.Name, options =>
            {
                options
                    .Property(x => x.First)
                    .HasColumnName("FirstName")
                    .HasMaxLength(50);

                options
                    .Property(x => x.Last)
                    .HasColumnName("LastName")
                    .HasMaxLength(75);
            })
            .Navigation(x => x.Name)
            .IsRequired();

        builder.Property(x => x.CompetenceLevel)
            .HasConversion(x => x.Value, x => new MonetaryAmount(x))
            .HasColumnName("CompetenceLevel_Amount");
    }
}
