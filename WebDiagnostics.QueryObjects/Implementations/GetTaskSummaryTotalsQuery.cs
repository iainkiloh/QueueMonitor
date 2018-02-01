using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using WebDiagnostics.Contracts;
using WebDiagnostics.Domain.Interfaces;
using WebDiagnostics.QueryObjects.Interfaces;

namespace WebDiagnostics.QueryObjects.Implementations
{

    public class GetTaskSummaryTotalsQuery : IQueryObject<TaskStatistics>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetTaskSummaryTotalsQueryHandler : IQueryHandler<GetTaskSummaryTotalsQuery, IContext, TaskStatistics>
    {
      
        public TaskStatistics Handle(GetTaskSummaryTotalsQuery queryObject, IContext context)
        {
            var stats = context.TaskMessages.Where(x => x.MessageDate >= queryObject.StartDate && x.MessageDate <= queryObject.EndDate).GroupBy(g => 1)
           .Select(g => new TaskStatistics
           {
               TotalMessages = g.Count(),
               Success = g.Count(i => i.Success),
               Failures = g.Count(i => !i.Success)
           });

            if (stats.FirstOrDefault() == null)
            {
                return new TaskStatistics {Failures = 0, Success = 0, TotalMessages = 0};
            }

            return stats.FirstOrDefault();
        }

    }
    
}

