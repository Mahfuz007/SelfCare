using Application.Common;
using Application.Constants;
using Application.Features.Investments.AddProfits;
using Application.Features.Investments.AddPurchaseInfo;
using Application.Features.Investments.Approval;
using Application.Features.Investments.Complete;
using Application.Features.Investments.GetInvestments;
using Application.Features.Investments.GetInvestmentById;
using Application.Features.Investments.GetPortfolioMetrics;
using Application.Features.Investments.Initiate;
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
            investment.Status = InvestmentConstant.Status.INPROGRESS.ToString();
            investment.StartDate = request.StartDate.ToUniversalTime();
            investment.LastModifiedDate = DateTime.UtcNow;

            await _repository.UpdateOneAsync(investment);
            return new CommonResponse(HttpStatusCode.OK, investment);
        }

        public Task<CommonResponse> UpdateAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<CommonResponse> AddPurchaseInfo(AddPurchaseInfoRequest request)
        {
            var investment = await _repository.FindByIdAsync(request.InvestmentId);
            if (investment is null)
            {
                return new CommonResponse(HttpStatusCode.NotFound, "Provide Valid Information");
            }

            var paymentInfo = new PurchaseInfo
            {
                Amount = request.Amount,
                When = DateTime.UtcNow,
                UnitCount = request.UnitCount,
                UnitPrice = Math.Round(request.UnitPrice, 4),
                Charge = Math.Round(request.Charge, 4),
                InvoiceNo = request.InvoiceNo,
                Remarks = request.Remarks,
            };

            investment.Amount = Math.Round(investment.Amount + request.Amount + request.Charge, 4);
            investment.UnitCount = investment.UnitCount + request.UnitCount;
            investment.PurchaseInfos.Add(paymentInfo);
            investment.IsPaymentCompleted = true;

            if(investment.Status == InvestmentConstant.Status.INITIATED.ToString())
            {
                investment.Status = InvestmentConstant.Status.PAID.ToString();
            }
            investment.LastModifiedDate = DateTime.UtcNow;

            await _repository.UpdateOneAsync(investment);

            return new CommonResponse(HttpStatusCode.OK, investment);

        }

        public async Task<CommonResponse> AddReturnInfo(AddReturnRequest request)
        {
            var returnDetails = new ReturnDetails()
            {
                When = DateTime.UtcNow,
                Amount = Math.Round(request.Amount, 4),
                Remarks = request.Remarks,
                FiscalYear = !string.IsNullOrEmpty(request.year) ? request.year : DateTime.UtcNow.Year.ToString(),
            };

            var investment = await _repository.FindByIdAsync(request.InvestmentId);
            if (investment.ReturnInstallmentDetails is null)
            {
                investment.ReturnInstallmentDetails = new List<ReturnDetails> { returnDetails };
            }
            else investment.ReturnInstallmentDetails.Add(returnDetails);
            investment.LastModifiedDate = DateTime.UtcNow;

            await _repository.UpdateOneAsync(investment);
            return new CommonResponse(HttpStatusCode.OK, investment);
        }

        public async Task<CommonResponse> GetPortfolioMetrics(GetPortfolioMetricsRequest request)
        {
            var filter = Builders<Investment>.Filter.Empty;

            if (request.StartDate != default)
            {
                filter &= Builders<Investment>.Filter.Gte(x => x.CreatedDate, UtilityService.GetStartOfDayUtc(request.StartDate));
            }

            if (request.EndDate != default)
            {
                filter &= Builders<Investment>.Filter.Lte(x => x.CreatedDate, UtilityService.GetEndOfDayUtc(request.EndDate));
            }

            if (!string.IsNullOrEmpty(request.InvestmentType))
            {
                filter &= Builders<Investment>.Filter.Eq(x => x.SourceName, request.InvestmentType);
            }

            if (!request.IncludeInactive)
            {
                filter &= Builders<Investment>.Filter.Ne(x => x.Status, InvestmentConstant.Status.CLOSED.ToString());
            }

            var investments = await _repository.GetItemsAsync(filter);

            

            // Calculate portfolio summary
            var totalInvested = investments.Sum(x => x.Amount);
            var totalItem = investments.Count(x => x.Status != InvestmentConstant.Status.CLOSED.ToString());
            var currentItem = investments.Count(x =>
                                                    x.Status == InvestmentConstant.Status.CONFIMED.ToString()
                                                    || x.Status == InvestmentConstant.Status.INPROGRESS.ToString());
            var currentValue = investments.Where(x =>
                                                    x.Status == InvestmentConstant.Status.CONFIMED.ToString()
                                                    || x.Status == InvestmentConstant.Status.INPROGRESS.ToString()).Sum(x => x.Amount);

            var earnedAmount = investments.Sum( x => x?.ReturnInstallmentDetails?.Sum(y => y.Amount) ?? 0);

            var portfolioMetrics = new GetPortfolioMetricsResponse()
            {
                CurrentInvestedAmount = currentValue,
                CurrentInvestedItem = currentItem,
                TotalInvestedAmount = totalInvested,
                TotalInvestedItem = totalItem,
                EarnedAmount = earnedAmount,
            };

            return new CommonResponse(portfolioMetrics);
        }

        public async Task<GetInvestmentByIdResponse?> GetInvestmentById(string investmentId)
        {
            var investment = await _repository.FindByIdAsync(investmentId);
            
            if (investment == null)
            {
                return null;
            }

            return new GetInvestmentByIdResponse
            {
                InvestmentId = investment.ItemId,
                Name = investment.Name,
                Description = investment.Description,
                SourceName = investment.SourceName,
                Status = investment.Status,
                Amount = investment.Amount,
                UnitCount = investment.UnitCount,
                UnitPrice = investment.PurchaseInfos.Any() ? investment.PurchaseInfos.First().UnitPrice : 0,
                DurationInMonths = investment.DurationInMonths,
                MaximumRoiDeclaredInPercentage = investment.MaximumRoiDeclaredInPercentage,
                ReturnInstallmentCount = investment.ReturnInstallmentCount,
                CreatedDate = investment.CreatedDate,
                LastModifiedDate = investment.LastModifiedDate,
                StartDate = investment.StartDate != DateTime.MinValue ? investment.StartDate : null,
                ExpectedMatureDate = investment.ExpectedMatureDate != DateTime.MinValue ? investment.ExpectedMatureDate : null,
                IsPaymentCompleted = investment.IsPaymentCompleted,
                CreatedBy = investment.CreatedBy,
                ConfirmationDetails = investment.ConfirmationDetails,
                PurchaseInfos = investment.PurchaseInfos,
                ReturnInstallmentDetails = investment.ReturnInstallmentDetails,
                FinalProfitAmount = investment.FinalProfitAmount,
                FinalProfitPercentage = investment.FinalProfitPercentage,
                FinalMatureDate = investment.FinalMatureDate,
                FinalReturnAmount = investment.FinalReturnAmount
            };
        }

        public async Task<CommonResponse> CompleteAsync(CompleteInvestmentRequest request)
        {
            var investment = await _repository.FindByIdAsync(request.InvestmentId);

            investment.FinalReturnAmount = Math.Round(investment.ReturnInstallmentDetails.Sum(x => x.Amount), 4);
            investment.FinalProfitAmount = Math.Round(investment.FinalReturnAmount-investment.Amount, 4);
            investment.FinalProfitPercentage = Math.Round(investment.FinalProfitAmount / investment.Amount * 100, 4);
            investment.FinalMatureDate = DateTime.UtcNow;
            investment.Status = InvestmentConstant.Status.COMPLETED.ToString();
            investment.LastModifiedDate = DateTime.UtcNow;

            await _repository.UpdateOneAsync(investment);
            return new CommonResponse(HttpStatusCode.OK, investment);
        }
    }
}
