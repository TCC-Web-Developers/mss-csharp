using StarterAPI.Commons.SharedModels;
using StarterAPI.Dto;

namespace StarterAPI.Queries
{
    public class GetStudentLedgerQuery : PagingQuery
    {
        public string? studentName { get; set; } = string.Empty;
        public string? courseName { get; set; } = string.Empty;
        public string? studentNo { get; set; } = string.Empty;
    }

    
}
