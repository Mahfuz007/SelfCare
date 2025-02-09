﻿using Application.Common;
using Application.Features.ExpenseFeatures.AddExpense;
using Application.Features.ExpenseFeatures.GetExpense;
using Application.Features.ExpenseFeatures.GetTotalExpense;
using Application.Features.ExpenseFeatures.UpdateExpense;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MongoDB.Driver;
using System.Net;

namespace Persistence.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Expense> _baseRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ExpenseRepository(IMapper mapper, IBaseRepository<Expense> baseRepository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<CommonResponse> AddExpense(AddExpenseRequest request)
        {
            var expense = _mapper.Map<Expense>(request);
            expense.ItemId = Guid.NewGuid().ToString();
            expense.CreatedDate = DateTime.UtcNow.ToUniversalTime();
            expense.LastModifiedDate = DateTime.UtcNow.ToUniversalTime();
            await UpdateDescriptionAndCategoryDetails(request.Description, request.Name, expense);

            await _baseRepository.InsertOneAsync(expense);

            return new CommonResponse(HttpStatusCode.Created,_mapper.Map<AddExpenseResponse>(expense));
        }

        public async Task<bool> CheckIfExpenseExists(string expenseId)
        {
            var result = await _baseRepository.FindByIdAsync(expenseId);
            return result is not null;
        }

        public async Task<CommonResponse> DeleteExpense(string expenseId)
        {
            await _baseRepository.DeleteByIdAsync(expenseId);
            return new CommonResponse(HttpStatusCode.OK, "Successfully Deleted!");
        }

        public async Task<CommonResponse> GetExpenses(GetExpenseRequest request)
        {
            var filter = Builders<Expense>.Filter.Empty;
            if (!string.IsNullOrWhiteSpace(request.ExpenseId))
            {
                filter &= Builders<Expense>.Filter.Eq(x => x.ItemId, request.ExpenseId);
            }
            if (!string.IsNullOrWhiteSpace(request.ExpenseName))
            {
                filter &= Builders<Expense>.Filter.Eq(x => x.Name, request.ExpenseName);
            }
            if (!string.IsNullOrEmpty(request.CategoryName))
            {
                filter &= Builders<Expense>.Filter.Eq(x => x.CategoryName, request.CategoryName);
            }
            if(request.StartDate != DateTime.MinValue)
            {
                filter &= Builders<Expense>.Filter.Gte(x => x.CreatedDate, UtilityService.GetStartOfDayUtc(request.StartDate));
            }
            if(request.EndDate != DateTime.MinValue)
            {
                filter &= Builders<Expense>.Filter.Lte(x => x.CreatedDate, UtilityService.GetEndOfDayUtc(request.EndDate));
            }


            var (expenses, totalCount) = await _baseRepository.GetItemsWithCountAsync(filter, request.PageIndex, request.PageSize);
            return new CommonResponse(expenses, totalCount);
        }

        public async Task<GetExpenseResponse> GetExpenseById(string expenseId)
        {
            var expense = await _baseRepository.FindByIdAsync(expenseId);
            return _mapper.Map<GetExpenseResponse>(expense);
        }

        public async Task<CommonResponse> GetExpenseSummery(GetExpenseSummeryRequest request)
        {
            var filter = Builders<Expense>.Filter.Empty;
            if (request.CreatedBy is not null) filter &= Builders<Expense>.Filter.Eq(x => x.CreatedBy, request.CreatedBy);
            if (request.CategoryId is not null) filter &= Builders<Expense>.Filter.Eq(x => x.CategoryId, request.CategoryId);
            if (request.StartDate.HasValue && request.StartDate != DateTime.MinValue)
            {
                filter &= Builders<Expense>.Filter.Gte(x => x.CreatedDate , UtilityService.GetStartOfDayUtc(request.StartDate.Value));
            }

            if(request.EndDate.HasValue && request.EndDate != DateTime.MinValue)
            {
                filter &= Builders<Expense>.Filter.Lte(x => x.CreatedDate, UtilityService.GetEndOfDayUtc(request.EndDate.Value));
            }

            var expenses = await _baseRepository.GetItemsAsync(filter);
            var totalAmount = expenses.Sum(x => x.Amount);
            var details = expenses
                            .GroupBy(x => x.CategoryName)
                            .ToDictionary(
                                    xd => xd.Key, 
                                    xd =>
                                    {
                                        var sum = xd.Sum(p => p.Amount);
                                        var count = xd.Count();
                                        return new SummeryDetails()
                                        {
                                            Total = sum,
                                            Percentage = Math.Round((sum / totalAmount) * 100, 2),
                                            Avarage = Math.Round(sum / count, 2),
                                            Count = count,
                                            HeightAmount = xd.Max(p => p.Amount)
                                        };
                                    }
                            );

            var response = new GetExpenseSummeryResponse()
            {
                TotalAmount = totalAmount,
                Details = details
            };

            return new CommonResponse(response, expenses.Count);
        }

        public async Task<CommonResponse> UpdateExpense(UpdateExpenseRequest request)
        {
            var expense = _mapper.Map<Expense>(request);
            var currData = await this.GetExpenseById(request.ExpenseId);
            expense.CreatedDate = currData.CreatedDate;
            expense.LastModifiedDate = DateTime.UtcNow.ToUniversalTime();
            expense.ImportedExcelName = currData.ImportedExcelName;
            await UpdateDescriptionAndCategoryDetails(request.Description, request.Name, expense);
            await _baseRepository.ReplaceOneAsync(expense);
            return new CommonResponse(_mapper.Map<UpdateExpenseResponse>(expense));
        }

        private async Task UpdateDescriptionAndCategoryDetails(string description, string name, Expense expense)
        {
            if (string.IsNullOrEmpty(description)) expense.Description = name;

            var category = await _categoryRepository.GetSpecificExpenseCategory(expense);
            expense.CategoryName = category.Name;
            expense.CategoryId = category.ItemId;
        }

        public async Task<CommonResponse> ProcessImportedExpense(IEnumerable<Expense> expenses, string excelName)
        {
            int index = 1;
            var validExpenses = new List<Expense>();
            foreach (var expense in expenses)
            {
                if (string.IsNullOrEmpty(expense.Name) || expense.CreatedDate == default) continue;
                if (await checkIfExcelImportExist(excelName + "_" + index)) continue;
                expense.ItemId = Guid.NewGuid().ToString();
                expense.ImportedExcelName = excelName + "_"+ index++;
                await UpdateDescriptionAndCategoryDetails(description: expense.Description,name: expense.Name,expense: expense);
                expense.CreatedDate = new DateTime(
                                            expense.CreatedDate.Year,
                                            expense.CreatedDate.Month,
                                            expense.CreatedDate.Day,
                                            expense.CreatedDate.Hour,
                                            expense.CreatedDate.Minute,
                                            expense.CreatedDate.Second,
                                            DateTimeKind.Utc).AddHours(-6);
                expense.LastModifiedDate= DateTime.UtcNow.ToUniversalTime();
                validExpenses.Add(expense);
            }

            if(validExpenses.Count > 0) await _baseRepository.InsertManyAsync(validExpenses.ToList());

            return new CommonResponse(new { TotalImportedExpenseCount = expenses.Count(), InsertedExpenseCount = validExpenses.Count });
        }

        private async Task<bool> checkIfExcelImportExist(string excelEntryName)
        {
            var filter = Builders<Expense>.Filter.Eq(x => x.ImportedExcelName, excelEntryName);
            var result = await _baseRepository.CountDocumentAsync(filter);
            return result > 0;
        }
    }
}
