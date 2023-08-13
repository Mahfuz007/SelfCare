using Application.Features.ExpenseFeatures.AddExpense;
using Application.Features.ExpenseFeatures.GetAllExpense;
using Application.Features.ExpenseFeatures.GetExpense;
using Application.Features.ExpenseFeatures.UpdateExpense;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MongoDB.Driver;

namespace Persistence.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Expense> _baseRepository;

        public ExpenseRepository(IMapper mapper, IBaseRepository<Expense> baseRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }

        public async Task<AddExpenseResponse> AddExpense(AddExpenseRequest request)
        {
            var expense = _mapper.Map<Expense>(request);
            expense.ItemId = Guid.NewGuid().ToString();
            expense.CreatedDate = DateTime.UtcNow;
            expense.LastModifiedDate = DateTime.UtcNow;

            await _baseRepository.InsertOneAsync(expense);

            return _mapper.Map<AddExpenseResponse>(expense);
        }

        public async Task<bool> CheckIfExpenseExists(string expenseId)
        {
            var result = await _baseRepository.FindByIdAsync(expenseId);
            return result is not null;
        }

        public async Task<bool> DeleteExpense(string expenseId)
        {
            await _baseRepository.DeleteByIdAsync(expenseId);
            return true;
        }

        public async Task<List<GetAllExpenseResponse>> GetAllExpense()
        {
            var filter = Builders<Expense>.Filter.Empty;
            var expenses = await _baseRepository.FindAllAsync(filter);

            return _mapper.Map<List<GetAllExpenseResponse>>(expenses);
        }

        public async Task<GetExpenseResponse> GetExpenseById(string expenseId)
        {
            var expense = await _baseRepository.FindByIdAsync(expenseId);
            return _mapper.Map<GetExpenseResponse>(expense);
        }

        public async Task<UpdateExpenseResponse> UpdateExpense(UpdateExpenseRequest request)
        {
            var expense = _mapper.Map<Expense>(request);
            var currData = await this.GetExpenseById(request.ExpenseId);
            expense.CreatedDate = currData.CreatedDate;
            expense.LastModifiedDate = DateTime.UtcNow;
            await _baseRepository.ReplaceOneAsync(expense);
            return _mapper.Map<UpdateExpenseResponse>(expense);
        }
    }
}
