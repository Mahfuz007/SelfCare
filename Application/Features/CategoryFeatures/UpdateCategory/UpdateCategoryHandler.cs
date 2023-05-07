using Application.Common;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UpdateCategory
{
    public sealed class UpdateCategoryHandler : IRequestHandler<UpdateCategoryRequest, UpdateCategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateCategoryRequest> _validator;
        public UpdateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper, IValidator<UpdateCategoryRequest> validator)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<UpdateCategoryResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.ToString());
            }

            var checkExistance = await _categoryRepository.BeAnExistingCategory(request.ItemId);
            if (!checkExistance) throw new BadImageFormatException("there is no entry existed with the item id");

            var category = _mapper.Map<Category>(request);
            category.LastModifiedDate = DateTime.UtcNow;
            await _categoryRepository.ReplaceOneAsync(category);
            return _mapper.Map<UpdateCategoryResponse>(category);
        }
    }
}
