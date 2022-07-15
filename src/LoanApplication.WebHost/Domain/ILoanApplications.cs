namespace LoanApplication.WebHost.Domain;

public interface ILoanApplications
{
    Task<LoanApplication?> WithNumber(LoanApplicationNumber loanApplicationNumber);

    Task Add(LoanApplication loanApplication);
}
