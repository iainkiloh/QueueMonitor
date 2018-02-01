using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using WebDiagnostics.Contracts;
using WebDiagnostics.Domain;
using WebDiagnostics.Domain.Extensions;
using WebDiagnostics.Domain.Interfaces;
using WebDiagnostics.QueryObjects.Interfaces;

namespace WebDiagnostics.QueryObjects.Implementations
{
    public class GetTaskMessagesPagedQuery : IQueryObject<List<TaskMessageContract>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? Success { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderByProperty { get; set; }
        public bool OrderByDescending { get; set; }
       
    }

    public class GetTaskMessagesPagedQueryHandler : IQueryHandler<GetTaskMessagesPagedQuery, IContext, List<TaskMessageContract>>
    {

        public List<TaskMessageContract> Handle(GetTaskMessagesPagedQuery queryObject, IContext context)
        {
            
            //((QueueMonitorDbContext)context).Database.Log = message => Trace.Write(message);

            //work out the property to order by  - if one is specified
            //set default to MessageDate Descending if none is specified
            if (string.IsNullOrEmpty(queryObject.OrderByProperty))
            {
                queryObject.OrderByProperty = "MessageDate";
                queryObject.OrderByDescending = true;
            }

            var querySource = context.TaskMessages.Include(x => x.User).Where(p => p.MessageDate >= queryObject.StartDate)
                .Where(p => p.MessageDate <= queryObject.EndDate);
            if (queryObject.Success.HasValue) { querySource  = querySource.Where(p => p.Success == queryObject.Success.Value); }

            if (queryObject.OrderByDescending)
            {
                querySource = querySource.OrderByPropertyDescending(queryObject.OrderByProperty);
            }
            else
            {
                querySource = querySource.OrderByProperty(queryObject.OrderByProperty);
            }

            var queryResponse = querySource.Select(p => new TaskMessageContract
            {
                MessageContent = p.MessageContent,
                MessageDate = p.MessageDate,
                Success = p.Success,
                TaskMessageId = p.TaskMessageId,
                UserId = p.UserId,
                Username = p.User != null ? p.User.Username : "",
            })
            .Skip((queryObject.PageNumber - 1) * queryObject.PageSize)
                .Take(queryObject.PageSize).ToList();

            return queryResponse;

           
        }
    }
}
