namespace StarterAPI.Commons.SharedModels
{
    public class Pagination
    {
        public int Page { get; private set; }
        public int PerPage { get; private set; }
        public int Pages { get; private set; }
        public int Total { get; private set; }
        public string SortOrder { get; private set; } = string.Empty;
        public string SortField { get; private set; } = string.Empty;

        public Pagination(PagingQuery pagingQuery, int totalRecords)
        {
            Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRecords / pagingQuery.PerPage)));
            Total = totalRecords;
            Page = pagingQuery.Page;
            PerPage = pagingQuery.PerPage;
            SortField = pagingQuery.SortField;
            SortOrder = pagingQuery.SortOrder;
        }
    }

    public class PagingQuery
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public string SortOrder { get; set; } = string.Empty;
        public string SortField { get; set; } = string.Empty;
    }
}
