using Application.Features.TaxFeature.GetTaxCalculation;
using Application.Repositories;

namespace Persistence.Repositories
{
    public class TaxRepository : ITaxRepository
    {
        public GetTaxCalculationResponse GetTaxCalculation(GetTaxCalculationRequest request)
        {
            var taxCalculation = new GetTaxCalculationResponse()
            {
                TotalIncome = request.TotalIncome,
                TaxableIncome = GetTaxableIncome(request.TotalIncome),
                MaximumRebateAmount = GetMaximumRebateAmount(request.TotalIncome),
                InvestmentNeededForGainMaximumRebate = GetInvestmentNeededForGainMaximumRebate(request.TotalIncome),
                CurrentRebateGainAmount = GetCurrentRebateGainAmount(request.TotalIncome, request.TotalInvestment),
                AllowableRebateAmountRemaining = GetAllowableRebateAmountRemaining(request.TotalIncome, request.TotalInvestment),
                TaxWithoutRebate = GetApplicableTaxWithoutRebate(request.TotalIncome),
                ApplicableTaxAfterCurrentRebate = GetApplicableTaxAfterCurrentRebate(request.TotalIncome, request.TotalInvestment),
                ApplicableTaxAfterMaxRebate = GetApplicableTaxAfterMaxRebate(request.TotalIncome)
            };

            return taxCalculation;
        }

        private long GetTaxableIncome(long totalIncome)
        {
            long taxExamption = Math.Min(totalIncome / 3, 450000);
            return totalIncome - taxExamption;
        }

        private long GetMaximumRebateAmount(long totalIncome)
        {
            var taxableIncome = GetTaxableIncome(totalIncome);
            return (long)Math.Round(taxableIncome * 0.03);
        }

        private long GetCurrentRebateGainAmount(long totalIncome, long totalInvestment)
        {
            if (totalInvestment == 0) return 0;
            var maximumRebateAmount = GetMaximumRebateAmount(totalIncome);
            var rebateGainFromInvestment = totalInvestment * 0.15;

            return (long)Math.Min(rebateGainFromInvestment, maximumRebateAmount);
        }

        private long GetAllowableRebateAmountRemaining(long totalIncome, long totalInvestment)
        {
            var maximumRebateAmount = GetMaximumRebateAmount(totalIncome);
            var currentRebateGainAmount = GetCurrentRebateGainAmount(totalIncome, totalInvestment);

            return maximumRebateAmount - currentRebateGainAmount;
        }

        private long GetInvestmentNeededForGainMaximumRebate(long totalIncome)
        {
            var maximumRebateAmount = GetMaximumRebateAmount(totalIncome);
            return (maximumRebateAmount * 100) / 15;
        }

        private long GetApplicableTaxWithoutRebate(long totalIncome)
        {
            long taxableIncome = GetTaxableIncome(totalIncome);
            long total = 0;
            long remaining = taxableIncome - 350000;

            total += (long) (Math.Min(remaining, 100000) * 0.05);
            remaining = Math.Max(remaining - 100000, 0);

            total += (long)(Math.Min(remaining, 400000) * 0.10);
            remaining = Math.Max(remaining - 400000, 0);

            total += (long)(Math.Min(remaining, 500000) * 0.15);
            remaining = Math.Max(remaining - 500000, 0);

            total += (long)(Math.Min(remaining, 500000) * 0.20);
            remaining = Math.Max(remaining - 500000, 0);

            total += (long) (remaining * 0.25);

            return total;
        }

        private long GetApplicableTaxAfterCurrentRebate(long totalIncome, long totalInvestment)
        {
            long totalTax = GetApplicableTaxWithoutRebate(totalIncome);
            long currentRebate = GetCurrentRebateGainAmount(totalIncome, totalInvestment);

            return totalTax - currentRebate;
        }

        private long GetApplicableTaxAfterMaxRebate(long totalIncome)
        {
            long totalTax = GetApplicableTaxWithoutRebate(totalIncome);
            long maxRebate = GetMaximumRebateAmount(totalIncome);

            return totalTax - maxRebate;
        }
    }
}
