using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExpenseFeatures.UpdateExpense
{
    public class UpdateExpenseMapper : Profile
    {
        public UpdateExpenseMapper()
        {
            CreateMap<UpdateExpenseRequest, Expense>()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ExpenseId));
            CreateMap<Expense, UpdateExpenseResponse>();
        }
    }
}
