using Application.Common;
using Application.Features.Investments.Approval;
using Application.Features.Investments.GetInvestments;
using Application.Features.Investments.Initiate;
using Application.Features.Investments.UpdatePayment;

namespace Application.Repositories
{
    public interface IInvestmentRepository
    {
        Task<CommonResponse> InitiateAsync(InitiateInvestmentRequest request);
        Task<CommonResponse> UpdateAsync();
        Task<CommonResponse> DeleteAsync();
        Task<CommonResponse> TerminateAsync();
        Task<CommonResponse> UpdatePaymentAsync(UpdatePaymentRequest request);
        Task<CommonResponse> UpdateApprovalAsync(ApprovalRequest request);
        Task<bool> IsApprovedAsync(string id);
        Task<bool> IsPaymentPendingAsync(string id);
        Task<bool> IsExistsAsync(string id);
        Task<string> GetInvestmentStatusAsync(string id);
        Task<CommonResponse> GetInvenstments(GetInvestmentRequest request);
    }
}
