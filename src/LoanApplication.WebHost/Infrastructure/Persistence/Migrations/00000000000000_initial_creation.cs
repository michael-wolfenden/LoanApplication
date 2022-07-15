using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanApplication.WebHost.Infrastructure.Persistence.Migrations
{
    public partial class initial_creation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Decision_DecisionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Decision_DecisionBy_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Customer_NationalIdentifier_Value = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Customer_Name_First = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Customer_Name_Last = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Customer_Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Customer_MonthlyIncome_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Customer_Address_Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Customer_Address_ZipCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Customer_Address_City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Customer_Address_Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Property_Value_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Property_Address_Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Property_Address_ZipCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Property_Address_City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Property_Address_Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Loan_LoanAmount_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Loan_LoanNumberOfYears = table.Column<int>(type: "int", nullable: false),
                    Loan_InterestRate_Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Registration_RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Registration_RegisteredBy_Value = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score_Score = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Score_Explanation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    CompetenceLevel_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanApplications");

            migrationBuilder.DropTable(
                name: "Operators");
        }
    }
}
