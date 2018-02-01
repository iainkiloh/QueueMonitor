using System;
using System.Linq;
using WebDiagnostics.Domain;
using WebDiagnostics.Domain.Interfaces;
using WebDiagnostics.QueryObjects.Interfaces;

namespace WebDiagnostics.QueryObjects.Implementations
{
    public class CountTaskMessagesQuery : IQueryObject<int>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? Success { get; set; }
    }

    public class CountTaskMessagesQueryHandler : IQueryHandler<CountTaskMessagesQuery, IContext, int>
    {
      
        public int Handle(CountTaskMessagesQuery queryObject, IContext context)
        {
            Func<TaskMessage, bool> startDatePredicate = e => e.MessageDate >= queryObject.StartDate;
            Func<TaskMessage, bool> endDatePredicate = e => e.MessageDate <= queryObject.EndDate;
            Func<TaskMessage, bool> queryPredicate = e => startDatePredicate(e) && endDatePredicate(e);
            if (queryObject.Success.HasValue)
            {
                queryPredicate = e => startDatePredicate(e) && endDatePredicate(e) && e.Success.Equals(queryObject.Success);
            }
            return context.TaskMessages.Where(queryPredicate).Count();
        }
    }

}
