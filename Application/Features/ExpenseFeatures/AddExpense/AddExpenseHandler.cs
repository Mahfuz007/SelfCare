using Application.Common;
using Application.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExpenseFeatures.AddExpense
{
    public sealed class AddExpenseHandler : IRequestHandler<AddExpenseRequest, AddExpenseResponse>
    {
        private readonly IValidator<AddExpenseRequest> _validator;
        private readonly IExpenseRepository _expenseRepository;
        public AddExpenseHandler(IValidator<AddExpenseRequest> validator, IExpenseRepository expenseRepository)
        {
            _validator = validator;
            _expenseRepository = expenseRepository;
        }

        public async Task<AddExpenseResponse> Handle(AddExpenseRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.ToString());
            }

            return await _expenseRepository.AddExpense(request);
        }
    }
}
