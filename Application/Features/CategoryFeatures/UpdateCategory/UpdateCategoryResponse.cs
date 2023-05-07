namespace Application.Features.UpdateCategory
{
    public sealed record UpdateCategoryResponse
    {
        public string Name { get; set; }
        public string ItemId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsExpense { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
