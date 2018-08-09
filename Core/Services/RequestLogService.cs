using System;
using System.Collections.Generic;
using Core.Interfaces;
using Core.Entities;
using System.Threading.Tasks;
using System.Linq;

namespace Core.Services
{
    public class RequestLogService : IRequestLogService
    {
        private readonly IRequestLogRepository _requestLogRepository;
        private readonly IAsyncRepository<RequestLog> _asyncRequestLogRepository;


        public RequestLogService(IRequestLogRepository requestLogRepository, IAsyncRepository<RequestLog> asyncRequestLogRepository)
        {
            _requestLogRepository = requestLogRepository;
            _asyncRequestLogRepository = asyncRequestLogRepository;
        }

        public async Task CreateRequestLog(string RequestMethod, string ResponseStatusCode, string UrlPath, long ElapsedTicks)
        {
            var requestLog = new RequestLog
            {
                RequestMethod = RequestMethod,
                ResponseStatusCode = ResponseStatusCode,
                UrlPath = UrlPath,
                ElapsedTicks = ElapsedTicks,
                CreatedAt = new DateTimeOffset(DateTime.Now)
            };

            await _asyncRequestLogRepository.AddAsync(requestLog);
        }

        public int GetTotalNumberOfRequestsProcessed()
        {
            return _requestLogRepository.GetTotalNumberOfRequestsProcessed();
        }

        public IEnumerable<object> GetNumberOfRequestsByResponseCode()
        {
            return _requestLogRepository.GetNumberOfRequestsByResponseCode();
        }

        public int GetTotalNumberOfRequestsWithResponseCodeStartingWith(string startString)
        {
            return _requestLogRepository.GetTotalNumberOfRequestsWithResponseCodeStartingWith(startString);
        }

        public int GetTotalNumberOfRequestsWithResponseCode(string responseCode)
        {
            return _requestLogRepository.GetTotalNumberOfRequestsWithResponseCode(responseCode);
        }

        public double GetAverageResponseTime()
        {
            return _requestLogRepository.GetAverageResponseTime();
        }

        public double GetMinResponseTime()
        {
            return _requestLogRepository.GetMinResponseTime();
        }

        public double GetMaxResponseTime()
        {
            return _requestLogRepository.GetMaxResponseTime();
        }

        public object GetStats()
        {
            var stats = _requestLogRepository.GetStats().First();
            var type = stats.GetType();
            var count = (int)type.GetProperty("Count").GetValue(stats);
            var average = (double)type.GetProperty("Average").GetValue(stats);
            var maxValue = (long)type.GetProperty("Max").GetValue(stats);
            var minValue = (long)type.GetProperty("Min").GetValue(stats);

            return new
            {
                Count = count,
                Average = TimeSpan.FromTicks(Convert.ToInt64(average)).TotalSeconds,
                Max = TimeSpan.FromTicks(maxValue).TotalSeconds,
                Min = TimeSpan.FromTicks(minValue).TotalSeconds
            };
        }

    }
}
