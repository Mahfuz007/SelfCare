using AutoMapper;
using Domain.Entities;

namespace Application.Features.UpdateCategory
{
    public sealed class UpdateCategoryMapper: Profile
    {
        public UpdateCategoryMapper()
        {
            CreateMap<UpdateCategoryRequest, Category>();
            CreateMap<Category, UpdateCategoryResponse>();
        }
    }
}
