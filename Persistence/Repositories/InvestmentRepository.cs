using Application.Common;
using Application.Constants;
using Application.Features.Investments.Initiate;
using Application.Repositories;
using Domain.Entities;
using System.Net;

namespace Persistence.Repositories
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private readonly IBaseRepository<Investment> _repository;

        public InvestmentRepository(IBaseRepository<Investment> repository)
        {
            _repository = repository;
        }

        public Task<CommonResponse> DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CommonResponse> InitiateAsync(InitiateInvestmentRequest request)
        {
            var investment = new Investment()
            {
                ItemId = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description,
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow,
                SourceName = request.SourceName,
                Amount = request.Amount,
                ExpectedMatureDate = DateTime.UtcNow.AddMonths(request.DurationInMonths),
                DurationInMonths = request.DurationInMonths,
                MaximumRoiDeclaredInPercentage = request.MaximumRoiDeclaredInPercentage,
                Status = InvestmentConstant.Status.INITIATED.ToString(),
                ReturnInstallmentCount = request.ReturnInstallmentCount,
            };
            await _repository.InsertOneAsync(investment);
            return new CommonResponse(HttpStatusCode.Created, new {ItemId = investment.ItemId});
        }

        public Task<bool> IsApprovedAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistsAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsPaymentPendingAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<CommonResponse> TerminateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CommonResponse> UpdateApprovalAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CommonResponse> UpdateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CommonResponse> UpdatePaymentAsync()
        {
            throw new NotImplementedException();
        }
    }
}
