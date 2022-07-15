using LoanApplication.WebHost.Domain;
using LoanApplication.WebHost.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoanApplication.WebHost.Infrastructure.Persistence.Configurations;

public class LoanApplicationConfiguration : IEntityTypeConfiguration<Domain.LoanApplication>
{
    public void Configure(EntityTypeBuilder<Domain.LoanApplication> builder)
    {
        builder.ToTable("LoanApplications");
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasConversion(x => x.Value, x => new LoanApplicationId(x));

        builder
            .Property(x => x.Number)
            .HasConversion(x => x.Value, x => LoanApplicationNumber.Of(x, null).Value)
            .HasMaxLength(50);

        builder
            .Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.OwnsOne(x => x.Score, options =>
        {
            options
                .Property(x => x.Explanation)
                .HasMaxLength(250);

            options
                .Property(x => x.Score)
                .HasConversion<string>()
                .HasMaxLength(50);
        });

        builder.OwnsOne(x => x.Customer, options =>
            {
                options
                    .Property(x => x.NationalIdentifier)
                    .HasConversion(x => x.Value, x => NationalIdentifier.Of(x, null).Value)
                    .HasColumnName("Customer_NationalIdentifier_Value")
                    .IsRequired()
                    .HasMaxLength(11);

                options.OwnsOne(x => x.Name, options =>
                    {
                        options
                            .Property(x => x.First)
                            .IsRequired()
                            .HasMaxLength(50);

                        options
                            .Property(x => x.Last)
                            .IsRequired()
                            .HasMaxLength(75);
                    })
                    .Navigation(x => x.Name)
                    .IsRequired();

                options
                    .Property(x => x.Birthdate)
                    .HasConversion<DateOnlyConverter, DateOnlyComparer>()
                    .IsRequired();

                options
                    .Property(x => x.MonthlyIncome)
                    .HasConversion(x => x.Value, x => new MonetaryAmount(x))
                    .HasColumnName("Customer_MonthlyIncome_Amount");

                options.OwnsOne(x => x.Address, options =>
                    {
                        options
                            .Property(x => x.Country)
                            .IsRequired()
                            .HasMaxLength(50);

                        options
                            .Property(x => x.ZipCode)
                            .IsRequired()
                            .HasMaxLength(6);

                        options
                            .Property(x => x.City)
                            .IsRequired()
                            .HasMaxLength(50);

                        options
                            .Property(x => x.Street)
                            .IsRequired()
                            .HasMaxLength(50);
                    })
                    .Navigation(x => x.Address)
                    .IsRequired();
            })
            .Navigation(x => x.Customer)
            .IsRequired();

        builder.OwnsOne(x => x.Property, options =>
            {
                options.Property(x => x.Value)
                    .HasConversion(x => x.Value, x => PropertyValue.Of(x, null).Value)
                    .HasColumnName("Property_Value_Amount");

                options.OwnsOne(x => x.Address, options =>
                    {
                        options
                            .Property(x => x.Country)
                            .IsRequired()
                            .HasMaxLength(50);

                        options
                            .Property(x => x.ZipCode)
                            .IsRequired()
                            .HasMaxLength(6);

                        options
                            .Property(x => x.City)
                            .IsRequired()
                            .HasMaxLength(50);

                        options
                            .Property(x => x.Street)
                            .IsRequired()
                            .HasMaxLength(50);
                    })
                    .Navigation(x => x.Address)
                    .IsRequired();
            })
            .Navigation(x => x.Property)
            .IsRequired();

        builder.OwnsOne(x => x.Loan, options =>
            {
                options.Property(x => x.InterestRate)
                    .HasConversion(x => x.Value, x => Percent.Of(x, null).Value)
                    .HasColumnName("Loan_InterestRate_Value")
                    .IsRequired();

                options.Property(x => x.LoanAmount)
                    .HasConversion(x => x.Value, x => LoanAmount.Of(x, null).Value)
                    .HasColumnName("Loan_LoanAmount_Amount")
                    .IsRequired();

                options.Property(x => x.LoanNumberOfYears)
                    .HasConversion(x => x.Value, x => LoanNumberOfYears.Of(x, null).Value)
                    .IsRequired();
            })
            .Navigation(x => x.Loan)
            .IsRequired();

        builder.OwnsOne(x => x.Decision, options =>
        {
            options
                .Property(x => x.DecisionDate)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            options.Property(x => x.DecisionBy)
                .HasConversion(x => x.Value, x => new OperatorId(x))
                .HasColumnName("Decision_DecisionBy_Value");
        });

        builder.OwnsOne(x => x.Registration, opts =>
        {
            opts.Property(x => x.RegistrationDate)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            opts.Property(x => x.RegisteredBy)
                .HasConversion(x => x.Value, x => new OperatorId(x))
                .HasColumnName("Registration_RegisteredBy_Value");
        });
    }
}
