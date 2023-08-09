using Application.Features.CategoryFeatures.CreateCategory;
using Application.Features.CategoryFeatures.GetCategory;
using Application.Features.UpdateCategory;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly IBaseRepository<Category> _baseRepository;
        private readonly IMapper _mapper;
        public CategoryRepository(IBaseRepository<Category> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<bool> BeAnExistingCategory(string id)
        {
            var result = await _baseRepository.FindByIdAsync(id);
            return result is not null;
        }

        public async Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest createCategoryRequest)
        {
            var category = _mapper.Map<Category>(createCategoryRequest);
            category.CreatedDate = DateTime.UtcNow;
            category.LastModifiedDate = DateTime.UtcNow;
            await _baseRepository.InsertOneAsync(category);

            return _mapper.Map<CreateCategoryResponse>(category);
        }

        public async Task<GetCategoryResponse> GetCategory(string categoryId)
        {
            var category = await _baseRepository.FindByIdAsync(categoryId);
            return _mapper.Map<GetCategoryResponse>(category);
        }

        public async Task<UpdateCategoryResponse> UpdateCategory(UpdateCategoryRequest updateCategoryRequest)
        {
            var category = _mapper.Map<Category>(updateCategoryRequest);
            category.LastModifiedDate = DateTime.UtcNow;
            await _baseRepository.ReplaceOneAsync(category);
            return _mapper.Map<UpdateCategoryResponse>(category);
        }
    }
}
