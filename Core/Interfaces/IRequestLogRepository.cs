using Core.Entities;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IRequestLogRepository : IRepository<RequestLog>
    {
        int GetTotalNumberOfRequestsProcessed();
        IEnumerable<object> GetNumberOfRequestsByResponseCode();
        int GetTotalNumberOfRequestsWithResponseCodeStartingWith(string startString);
        int GetTotalNumberOfRequestsWithResponseCode(string responseCode);
        double GetAverageResponseTime();
        double GetMinResponseTime();
        double GetMaxResponseTime();
        IEnumerable<object> GetStats();
    }
}
