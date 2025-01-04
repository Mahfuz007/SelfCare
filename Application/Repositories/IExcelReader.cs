using Microsoft.AspNetCore.Http;
using System.Collections.Immutable;

namespace Application.Repositories
{
    public interface IExcelReader
    {
        Task<Stream> GetStremFromFile(IFormFile file);
        IEnumerable<T> GetRecords<T>(Stream stream, ImmutableDictionary<string, int>PropertyMap, int firstRowIndex);
    }
}
