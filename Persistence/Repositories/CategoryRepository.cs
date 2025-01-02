using Application.Features.CategoryFeatures.CreateCategory;
using Application.Features.CategoryFeatures.GetCategory;
using Application.Features.CategoryFeatures.UpdateCategory;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MongoDB.Driver;

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

        public async Task<bool> DeleteCategory(string categoryId)
        {
            await _baseRepository.DeleteByIdAsync(categoryId);
            return true;
        }

        public async Task<List<GetCategoryResponse>> GetCategory(GetCategoryRequest request)
        {
            var filter = Builders<Category>.Filter.Empty;
            if(!string.IsNullOrEmpty(request.CategoryId))
            {
                filter &= Builders<Category>.Filter.Eq(x => x.ItemId, request.CategoryId);
            }

            var categories = await _baseRepository.FindAllAsync(filter, request.PageIndex, request.PageSize);
            return _mapper.Map<List<GetCategoryResponse>>(categories);
        }

        public async Task<UpdateCategoryResponse> UpdateCategory(UpdateCategoryRequest updateCategoryRequest)
        {
            var category = await _baseRepository.FindByIdAsync(updateCategoryRequest.ItemId);
            if (category == null) return new UpdateCategoryResponse() { Name = "No Category Found for this Item Id" };
            category.ItemId = updateCategoryRequest.ItemId;
            category.Name = updateCategoryRequest.Name;
            category.IsDefault = updateCategoryRequest.IsDefault;
            category.IsExpense = updateCategoryRequest.IsExpense;
            category.LastModifiedDate = DateTime.UtcNow;
            await _baseRepository.UpdateOneAsync(category);
            return _mapper.Map<UpdateCategoryResponse>(category);
        }

        public async Task<Category> GetSpecificExpenseCategory(Expense expense)
        {
            if(!string.IsNullOrEmpty(expense.CategoryId))
            {
                return await _baseRepository.FindByIdAsync(expense.CategoryId);
            }

            var filter = Builders<Category>.Filter.Or(
                    Builders<Category>.Filter.Eq(x => x.Name, expense.Name),
                    Builders<Category>.Filter.Text(expense.Name),
                    Builders<Category>.Filter.Eq(x => x.Name, "Others"));
            var category = await _baseRepository.FindOneAsync(filter);

            if(category is null)
            {
                return new Category()
                {
                    Name = "Others"
                };
            }

            return category;
        }
    }
}
