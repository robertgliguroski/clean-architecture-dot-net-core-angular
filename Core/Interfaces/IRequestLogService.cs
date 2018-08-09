using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRequestLogService
    {
        Task CreateRequestLog(string RequestMethod, string ResponseStatusCode, string UrlPath, long ElapsedTicks);
        int GetTotalNumberOfRequestsProcessed();
        IEnumerable<object> GetNumberOfRequestsByResponseCode();
        int GetTotalNumberOfRequestsWithResponseCodeStartingWith(string startString);
        int GetTotalNumberOfRequestsWithResponseCode(string responseCode);
        double GetAverageResponseTime();
        double GetMinResponseTime();
        double GetMaxResponseTime();
        object GetStats();
    }
}
