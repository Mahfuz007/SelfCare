using Domain.Common;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Application.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IQueryable<T> AsQueryAble();
        IEnumerable<T> FilterBy(Expression<Func<T, bool>> filterExpression);
        T FindOne (Expression<Func<T, bool>> filterExpression);
        Task<T> FindOneAsync (Expression<Func<T, bool>> filterExpression);
        T FindById(string id);
        Task<T> FindByIdAsync (string id);
        List<T> FindAll(FilterDefinition<T> filterExpression);
        Task<List<T>> FindAllAsync (FilterDefinition<T> filterExpression, int pageNo = 0, int pageSize = 100);
        void InsertOne(T document);
        Task InsertOneAsync(T document);
        void InsertMany(ICollection<T> collection);
        Task InsertManyAsync(ICollection<T> collection);
        void ReplaceOne(T document);
        Task ReplaceOneAsync(T document);
        Task UpdateOneAsync(T document);
        void DeleteOne(Expression<Func<T, bool>>filterExpression);
        Task DeleteOneAsync(Expression<Func<T, bool>>filterExpression);
        void DeleteById(string id);
        Task DeleteByIdAsync(string id);
        void DeleteMany(Expression<Func<T, bool>> filterExpression);
        Task DeleteManyAsync(Expression<Func<T, bool>>filterExpression);
        Task<long> CountDocumentAsync(FilterDefinition<T> filterExpression);
    }
}
