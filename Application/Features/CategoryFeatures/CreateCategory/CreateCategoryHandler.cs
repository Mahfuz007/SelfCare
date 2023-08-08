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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<CreateCategoryRequest> _validator;

        public CreateCategoryHandler(ICategoryRepository categoryRepository, IValidator<CreateCategoryRequest> validator)
        {
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
            
            return await _categoryRepository.CreateCategory(request);
        }
    }
}
