using Application.Common;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.CategoryFeatures.CreateCategory
{
    public sealed class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest, CreateCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<CreateCategoryRequest> _validator;

        public CreateCategoryHandler(IMapper mapper, ICategoryRepository categoryRepository, IValidator<CreateCategoryRequest> validator)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _validator = validator;
        }

        public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                Console.WriteLine(validationResult);
                throw new BadRequestException(validationResult.ToString());
            }
            var category = _mapper.Map<Category>(request);

            await _categoryRepository.InsertOneAsync(category);
            return _mapper.Map<CreateCategoryResponse>(category);
        }
    }
}
