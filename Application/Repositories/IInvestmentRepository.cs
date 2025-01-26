using Application.Common;
using Application.Features.Investments.Initiate;

namespace Application.Repositories
{
    public interface IInvestmentRepository
    {
        Task<CommonResponse> InitiateAsync(InitiateInvestmentRequest request);
        Task<CommonResponse> UpdateAsync();
        Task<CommonResponse> DeleteAsync();
        Task<CommonResponse> TerminateAsync();
        Task<CommonResponse> UpdatePaymentAsync();
        Task<CommonResponse> UpdateApprovalAsync();
        Task<bool> IsApprovedAsync(string id);
        Task<bool> IsPaymentPendingAsync(string id);
        Task<bool> IsExistsAsync(string id);
    }
}
