using Application.Common;
using Application.Repositories;
using FluentValidation;
using MediatR;


namespace Application.Features.ExpenseFeatures.AddExpense
{
    public sealed class AddExpenseHandler : IRequestHandler<AddExpenseRequest, CommonResponse>
    {
        private readonly IValidator<AddExpenseRequest> _validator;
        private readonly IExpenseRepository _expenseRepository;
        public AddExpenseHandler(IValidator<AddExpenseRequest> validator, IExpenseRepository expenseRepository)
        {
            _validator = validator;
            _expenseRepository = expenseRepository;
        }

        public async Task<CommonResponse> Handle(AddExpenseRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new CommonResponse(validationResult);
            }

            return await _expenseRepository.AddExpense(request);
        }
    }
}
