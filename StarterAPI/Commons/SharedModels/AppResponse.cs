namespace StarterAPI.Commons.SharedModels
{
    public class AppResponse<TData>
    {
        public TData? Data { get; }

        public Pagination? Pagination { get; }

        public AppResponse(TData data)
        {
            Data = data;
            Pagination = null;
        }

        public AppResponse(TData data, Pagination pagination)
        {
            Data = data;
            Pagination = pagination;
        }
    }
}
