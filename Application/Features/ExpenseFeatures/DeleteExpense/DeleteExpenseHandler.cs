using Application.Common;
using Application.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Features.ExpenseFeatures.DeleteExpense
{
    public class DeleteExpenseHandler : IRequestHandler<DeleteExpenseRequest, CommonResponse>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IValidator<DeleteExpenseRequest> _validator;

        public DeleteExpenseHandler(IExpenseRepository expenseRepository, IValidator<DeleteExpenseRequest> validator)
        {
            _expenseRepository = expenseRepository;
            _validator = validator;
        }

        public async Task<CommonResponse> Handle(DeleteExpenseRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) return new CommonResponse(validationResult: validationResult);
            return await _expenseRepository.DeleteExpense(request.ExpenseId);
        }
    }
}
