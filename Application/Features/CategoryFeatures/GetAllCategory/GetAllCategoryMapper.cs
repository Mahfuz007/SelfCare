using Application.Features.CategoryFeatures.CreateCategory;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.GetAllCategory
{
    public class GetAllCategoryMapper : Profile
    {
        public GetAllCategoryMapper()
        {
            CreateMap<Category, GetAllCategoryResponse>();
        }
    }
}
