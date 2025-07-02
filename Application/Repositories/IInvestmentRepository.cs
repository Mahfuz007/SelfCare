using Application.Common;
using Application.Features.Investments.AddProfits;
using Application.Features.Investments.AddPurchaseInfo;
using Application.Features.Investments.Approval;
using Application.Features.Investments.GetInvestments;
using Application.Features.Investments.GetInvestmentById;
using Application.Features.Investments.GetPortfolioMetrics;
using Application.Features.Investments.Initiate;

namespace Application.Repositories
{
    public interface IInvestmentRepository
    {
        Task<CommonResponse> InitiateAsync(InitiateInvestmentRequest request);
        Task<CommonResponse> UpdateAsync();
        Task<CommonResponse> DeleteAsync();
        Task<CommonResponse> TerminateAsync();
        Task<CommonResponse> UpdateApprovalAsync(ApprovalRequest request);
        Task<bool> IsApprovedAsync(string id);
        Task<bool> IsPaymentPendingAsync(string id);
        Task<bool> IsExistsAsync(string id);
        Task<string> GetInvestmentStatusAsync(string id);
        Task<CommonResponse> GetInvenstments(GetInvestmentRequest request);
        Task<CommonResponse> AddPurchaseInfo(AddPurchaseInfoRequest request);
        Task<CommonResponse> AddReturnInfo(AddReturnRequest request);
        Task<CommonResponse> GetPortfolioMetrics(GetPortfolioMetricsRequest request);
        Task<GetInvestmentByIdResponse?> GetInvestmentById(string investmentId);
    }
}
