﻿using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExpenseFeatures.GetAllExpense
{
    public class GetAllExpenseRequest : QueryRequestBase,IRequest<List<GetAllExpenseResponse>>
    {
        
    }
}
