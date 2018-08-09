using Core.Entities;
using Core.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Infrastructure.Data
{
    public class RequestLogRepository : EfRepository<RequestLog>, IRequestLogRepository
    {
        public RequestLogRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public int GetTotalNumberOfRequestsProcessed()
        {
            return _dbContext.RequestLogs.Count();
        }

        public IEnumerable<object> GetNumberOfRequestsByResponseCode()
        {
            var logs = (
                from requestLog in _dbContext.RequestLogs
                group requestLog by requestLog.ResponseStatusCode into g
                select new
                {
                    ResponseStatusCode = g.Key,
                    NumberOfRequestPerResponseCode = g.Count()
                });
            return logs.ToList();
        }

        public int GetTotalNumberOfRequestsWithResponseCodeStartingWith(string startString)
        {
            return _dbContext.RequestLogs.Where(r => r.ResponseStatusCode.StartsWith(startString)).Count();
        }

        public int GetTotalNumberOfRequestsWithResponseCode(string responseCode)
        {
            return _dbContext.RequestLogs.Where(r => r.ResponseStatusCode == responseCode).Count();
        }

        public double GetAverageResponseTime()
        {
            return _dbContext.RequestLogs.Average(r => r.ElapsedTicks);
        }

        public double GetMinResponseTime()
        {
            return _dbContext.RequestLogs.Min(r => r.ElapsedTicks);
        }

        public double GetMaxResponseTime()
        {
            return _dbContext.RequestLogs.Max(r => r.ElapsedTicks);
        }

        public IEnumerable<object> GetStats()
        {
            var stats = _dbContext.RequestLogs.GroupBy(i => 1)
                .Select(g => new {
                    Count = g.Count(),
                    Average = g.Average(r => r.ElapsedTicks),
                    Min = g.Min(r => r.ElapsedTicks),
                    Max = g.Max(r => r.ElapsedTicks)
                });
            return stats.ToList();
        }
    }
}
