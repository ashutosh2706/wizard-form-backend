using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace WizardFormBackend.Utils
{
    public class QueryParams
    {
        public string SearchTerm { get; set; } = string.Empty;
        public string SortField { get; set; } = string.Empty;
        [ValidateSortDirection(ErrorMessage = "Sort direction must be 'ascending' or 'descending'")]
        public string SortDirection { get; set; } = "ascending";
        [Range(1, int.MaxValue, ErrorMessage = "Page number should be >= 1")]
        public int PageNumber { get; set; } = 1;
        [Range(5, 20, ErrorMessage = "Page size should be >= 5 and <= 20")]
        public int PageSize { get; set; } = 5;
    }

    class ValidateSortDirectionAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string? _sortDirection = value?.ToString()?.ToLower();
            if(_sortDirection.IsNullOrEmpty()) { return false; }
            return _sortDirection == "ascending" || _sortDirection == "descending";
        }
    }
    
}
