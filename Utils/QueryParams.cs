using System.ComponentModel.DataAnnotations;

namespace WizardFormBackend.Utils
{
    public class QueryParams
    {
        public string SearchTerm { get; set; } = string.Empty;
        public string SortField { get; set; } = string.Empty;
        public string SortDirection { get; set; } = "ascending";
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;
        [Range(5, 20)]
        public int PageSize { get; set; } = 5;
    }
}
