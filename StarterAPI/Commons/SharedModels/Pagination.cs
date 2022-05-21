namespace StarterAPI.Commons.SharedModels
{

    //https://khalidabuhakmeh.com/cursor-paging-with-entity-framework-core-and-aspnet-core
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
            Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRecords) / Convert.ToDecimal(pagingQuery.perPage)));
            Total = totalRecords;
            Page = pagingQuery.page <= 0 ? 1 : pagingQuery.page;
            PerPage = pagingQuery.perPage;
            SortField = pagingQuery.sortField;
            SortOrder = pagingQuery.sortOrder;
        }

    }

    public class PagingQuery
    {
        public int page { get; set; } = 1;
        public int perPage { get; set; } = 10;
        public string sortOrder { get; set; } = string.Empty; // asc & desc
        public string sortField { get; set; } = string.Empty;
        //public string sortCode { get {
        //        return $"{sortField.ToLower().Trim()}-{sortOrder.ToLower()}";
        //    } 
        //}

        public string GetSortCode()
        {
            return $"{sortField.ToLower().Trim()}-{sortOrder.ToLower()}";
        }
    }

    public class PaginatedResult<TData>
    {
        public TData? Data { get; private set; }
        //public int TotalCount { get; private set; } = 0;

        public Pagination Pagination { get; private set; }


        public PaginatedResult(TData data, PagingQuery pagingQuery, int totalCount)
        {
            Data = data;
            //TotalCount = totalCount;

            Pagination = new Pagination(pagingQuery, totalCount);

        }
    }
}
