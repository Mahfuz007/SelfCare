namespace Application.Features.TaxFeature.GetTaxCalculation
{
    public sealed record GetTaxCalculationResponse
    {
        public long TotalIncome { get; set; }
        public long TaxableIncome { get; set; }
        public long MaximumRebateAmount { get; set; }
        public long InvestmentNeededForGainMaximumRebate { get; set; }
        public long CurrentRebateGainAmount { get; set; }
        public long AllowableRebateAmountRemaining { get; set; }
        public long TaxWithoutRebate { get; set; }
        public long ApplicableTaxAfterCurrentRebate { get; set; }
        public long ApplicableTaxAfterMaxRebate { get; set; }
    }
}
