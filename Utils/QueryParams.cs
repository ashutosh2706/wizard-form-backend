using System.ComponentModel.DataAnnotations;

namespace WizardFormBackend.Utils
{
    public class QueryParams
    {
        private string _sortDirection = "ascending";
        public string SearchTerm { get; set; } = string.Empty;
        public string SortField { get; set; } = string.Empty;
        public string SortDirection
        {
            get { return _sortDirection; }
            set
            {
                if (value.ToLower() == "ascending" || value.ToLower() == "descending")
                {
                    _sortDirection = value.ToLower();
                }
                else
                {
                    throw new ArgumentException("Sort direction must be 'ascending' or 'descending'");
                }
            }
        }
        [Range(1, int.MaxValue, ErrorMessage = "Page number should be >= 1")]
        public int PageNumber { get; set; } = 1;
        [Range(5, 20, ErrorMessage = "Page size should be >= 5 and <= 20")]
        public int PageSize { get; set; } = 5;
    }
}
