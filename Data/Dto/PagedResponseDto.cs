﻿namespace WizardFormBackend.Data.Dto
{
    public class PagedResponseDto<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public IEnumerable<T>? Items { get; set; }
    }
}
