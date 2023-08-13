using Application.Common;
using Application.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExpenseFeatures.DeleteExpense
{
    public class DeleteExpenseHandler : IRequestHandler<DeleteExpenseRequest, bool>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IValidator<DeleteExpenseRequest> _validator;

        public DeleteExpenseHandler(IExpenseRepository expenseRepository, IValidator<DeleteExpenseRequest> validator)
        {
            _expenseRepository = expenseRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(DeleteExpenseRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) throw new BadRequestException(validationResult.ToString());
            return await _expenseRepository.DeleteExpense(request.ExpenseId);
        }
    }
}
