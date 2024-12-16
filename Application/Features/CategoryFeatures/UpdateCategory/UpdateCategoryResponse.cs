﻿namespace Application.Features.UpdateCategory
{
    public sealed record UpdateCategoryResponse
    {
        public string Name { get; set; } = string.Empty;
        public string ItemId { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public bool IsExpense { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
