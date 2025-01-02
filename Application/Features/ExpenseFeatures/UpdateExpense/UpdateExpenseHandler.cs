using Application.Common;
using Application.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExpenseFeatures.UpdateExpense
{
    public class UpdateExpenseHandler : IRequestHandler<UpdateExpenseRequest, CommonResponse>
    {
        private IValidator<UpdateExpenseRequest> _validator;
        private IExpenseRepository _expenseRepository;

        public UpdateExpenseHandler(IValidator<UpdateExpenseRequest> validator, IExpenseRepository expenseRepository)
        {
            _validator = validator;
            _expenseRepository = expenseRepository;
        }

        public async Task<CommonResponse> Handle(UpdateExpenseRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
            {
                return new CommonResponse(validationResult);
            }

            return await _expenseRepository.UpdateExpense(request);
        }
    }
}
