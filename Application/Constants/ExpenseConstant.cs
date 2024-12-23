using System.Collections.Immutable;

namespace Application.Constants
{
    public static class ExpenseConstant
    {
        public const int ExpenseExcelFirstIndex = 2;
        public static readonly ImmutableDictionary<string, int> PropertyColumnMap = new Dictionary<string, int>
        {
            { "CreatedDate", 1 },
            { "Name", 2 },
            { "Amount", 3 }
        }.ToImmutableDictionary();
    }
}
