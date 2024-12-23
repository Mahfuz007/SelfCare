using Application.Repositories;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Collections.Immutable;

namespace Persistence.Repositories
{
    public class ExcelReader : IExcelReader
    {
        private readonly ImmutableDictionary<Type, Func<object, object>> typeConverters = new Dictionary<Type, Func<object, object>>
        {
            { typeof(DateTime), value => DateTime.FromOADate((double)value) }
        }.ToImmutableDictionary();

        public async Task<Stream> GetStremFromFile(IFormFile file)
        {
            var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            return stream;
        } 

        public IEnumerable<T> GetRecords<T>(Stream stream, ImmutableDictionary<string, int> PropertyMap, int firstRowIndex)
        {
            var records = new List<T>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets[0];
            var properties = typeof(T).GetProperties();

            for (int row = firstRowIndex; row <= worksheet.Dimension.Rows; row++)
            {
                T instance = Activator.CreateInstance<T>();

                foreach(var property in properties)
                {
                    if(PropertyMap.TryGetValue(property.Name, out int ColumnNumber))
                    {
                        var obj = worksheet.Cells[row, ColumnNumber].Value;
                        if (obj is not null)
                        {
                            var converter = typeConverters.GetValueOrDefault(property.PropertyType, value => Convert.ChangeType(value, property.PropertyType));
                            var convertedValue = converter(obj);
                            property.SetValue(instance, convertedValue);
                        }
                    }
                }
                records.Add(instance);
            }
            return records;
        }
    }
}
