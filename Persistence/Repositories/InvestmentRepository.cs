using Application.Common;
using Application.Constants;
using Application.Features.Investments.Approval;
using Application.Features.Investments.GetInvestments;
using Application.Features.Investments.Initiate;
using Application.Features.Investments.UpdatePayment;
using Application.Repositories;
using Domain.Entities;
using MongoDB.Driver;
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

        public async Task<CommonResponse> GetInvenstments(GetInvestmentRequest request)
        {
            var filter = Builders<Investment>.Filter.Empty;
            if(!string.IsNullOrEmpty(request.CreatedBy))
            {
                filter &= Builders<Investment>.Filter.Eq(x => x.CreatedBy, request.CreatedBy);
            }

            if(!string.IsNullOrEmpty(request.SearchKey))
            {
                filter &= Builders<Investment>.Filter.Eq(x => x.Name, request.SearchKey);
                filter &= Builders<Investment>.Filter.Eq(x => x.Description, request.SearchKey);
            }

            if(request.StartDate != default)
            {
                filter &= Builders<Investment>.Filter.Gte(x => x.CreatedDate, UtilityService.GetStartOfDayUtc(request.StartDate));
            }

            if(request.EndDate != default)
            {
                filter &= Builders<Investment>.Filter.Lte(x => x.CreatedDate, UtilityService.GetEndOfDayUtc(request.EndDate));
            }

            var (investments, totalCount) = await _repository.GetItemsWithCountAsync(filter, request.PageIndex, request.PageSize);
            return new CommonResponse(investments, totalCount);
        }

        public async Task<string> GetInvestmentStatusAsync(string id)
        {
            var investment = await _repository.FindByIdAsync(id);
            return investment is not null ? investment.Status : "";
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

        public async Task<bool> IsExistsAsync(string id)
        {
            var inventment = await _repository.FindByIdAsync(id);
            return inventment is not null;
        }

        public Task<bool> IsPaymentPendingAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<CommonResponse> TerminateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CommonResponse> UpdateApprovalAsync(ApprovalRequest request)
        {
            var investment = await _repository.FindByIdAsync(request.InvestmentId);
            var confirmationDetails = new ConfirmationDetails()
            {
                When = DateTime.UtcNow,
                InvoiceNo = request.InvoiceNo,
                Remarks = request.Remarks,
            };

            investment.ConfirmationDetails = confirmationDetails;
            investment.ExpectedMatureDate = UtilityService.GetEndOfDayUtc(request.MatureDate);
            investment.Status = InvestmentConstant.Status.CONFIMED.ToString();
            investment.StartDate = request.StartDate.ToUniversalTime();
            investment.LastModifiedDate = DateTime.UtcNow;

            await _repository.UpdateOneAsync(investment);
            return new CommonResponse(HttpStatusCode.OK, investment);
        }

        public Task<CommonResponse> UpdateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CommonResponse> UpdatePaymentAsync(UpdatePaymentRequest request)
        {
            var investment = await _repository.FindByIdAsync(request.InvestmentId);
            if(investment is null) { 
                return new CommonResponse(HttpStatusCode.NotFound, "Provide Valid Information");
            }

            var senderInfo = new PaymentDetails
            {
                Amount = request.Amount,
                When = DateTime.UtcNow,
                Method = request.Method,
                MethodDetails = new PaymentMethodDetails
                {
                    AccountHolderName = request.SenderName,
                    AccountNo = request.SenderAccountNumber,
                    BranchName = request.SenderBranchName
                },
                TransferType = request.Way
            };

            var receiverInfo = new PaymentDetails
            {
                Amount = request.Amount,
                When = DateTime.UtcNow,
                Method = request.Method,
                MethodDetails = new PaymentMethodDetails
                {
                    AccountHolderName = request.ReceiverName,
                    AccountNo = request.ReceiverAccountNumber,
                    BranchName = request.ReceiverBranchName
                },
                TransferType = request.Way
            };

            investment.Amount = request.Amount;
            investment.IsPaymentCompleted = true;
            investment.SenderPaymentDetails = senderInfo;
            investment.ReceiverPaymentDetails = receiverInfo;
            investment.IsPaymentCompleted = true;
            investment.Status = InvestmentConstant.Status.PAID.ToString();
            investment.LastModifiedDate = DateTime.UtcNow;

            await _repository.UpdateOneAsync(investment);

            return new CommonResponse(HttpStatusCode.OK, investment);
        }
    }
}
