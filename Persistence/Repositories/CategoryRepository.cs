using Application.Common;
using Application.Features.CategoryFeatures.CreateCategory;
using Application.Features.CategoryFeatures.GetCategory;
using Application.Features.CategoryFeatures.UpdateCategory;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MongoDB.Driver;
using System.Net;

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

        public async Task<CommonResponse> CreateCategory(CreateCategoryRequest createCategoryRequest)
        {
            var category = _mapper.Map<Category>(createCategoryRequest);
            category.ItemId = Guid.NewGuid().ToString();
            category.CreatedDate = DateTime.UtcNow;
            category.LastModifiedDate = DateTime.UtcNow;
            await _baseRepository.InsertOneAsync(category);

            return new CommonResponse(HttpStatusCode.Created,_mapper.Map<CreateCategoryResponse>(category));
        }

        public async Task<CommonResponse> DeleteCategory(string categoryId)
        {
            await _baseRepository.DeleteByIdAsync(categoryId);
            return new CommonResponse(HttpStatusCode.OK, "Successfully deleted");
        }

        public async Task<CommonResponse> GetCategory(GetCategoryRequest request)
        {
            var filter = Builders<Category>.Filter.Empty;
            if(!string.IsNullOrEmpty(request.CategoryId))
            {
                filter &= Builders<Category>.Filter.Eq(x => x.ItemId, request.CategoryId);
            }
            var totalCount = await _baseRepository.CountDocumentAsync(filter);
            var categories = await _baseRepository.FindAllAsync(filter, request.PageIndex, request.PageSize);
            return new CommonResponse(_mapper.Map<List<GetCategoryResponse>>(categories), totalCount);
        }

        public async Task<CommonResponse> UpdateCategory(UpdateCategoryRequest updateCategoryRequest)
        {
            var category = await _baseRepository.FindByIdAsync(updateCategoryRequest.ItemId);
            if (category == null) return new CommonResponse(HttpStatusCode.NotFound, "Please Provide Valid Request");
            category.ItemId = updateCategoryRequest.ItemId;
            category.Name = updateCategoryRequest.Name;
            category.IsDefault = updateCategoryRequest.IsDefault;
            category.IsExpense = updateCategoryRequest.IsExpense;
            category.LastModifiedDate = DateTime.UtcNow;
            await _baseRepository.UpdateOneAsync(category);
            return new CommonResponse(_mapper.Map<UpdateCategoryResponse>(category));
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
