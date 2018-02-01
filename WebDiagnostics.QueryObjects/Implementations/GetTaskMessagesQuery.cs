using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using WebDiagnostics.Domain;
using WebDiagnostics.Domain.Interfaces;
using WebDiagnostics.QueryObjects.Interfaces;

namespace WebDiagnostics.QueryObjects.Implementations
{

    public class GetTaskMessagesQuery : IQueryObject<List<TaskMessage>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? Success { get; set; }
    }

    public class GetTaskMessagesQueryHandler : IQueryHandler<GetTaskMessagesQuery, IContext, List<TaskMessage>>
    {
       
        public List<TaskMessage> Handle(GetTaskMessagesQuery queryObject, IContext context)
        {
            List<TaskMessage> response = null;

            //((QueueMonitorDbContext)context).Database.Log = message => Trace.Write(message);

            var query = context.TaskMessages.Include(x => x.User).Where(p => p.MessageDate >= queryObject.StartDate)
                .Where(p => p.MessageDate <= queryObject.EndDate);
            if (queryObject.Success.HasValue) { query = query.Where(p => p.Success == queryObject.Success.Value); }

            var queryResponse = query.Select(p => new
            {
                MessageContent = p.MessageContent,
                MessageDate = p.MessageDate,
                Success = p.Success,
                TaskMessageId = p.TaskMessageId,
                UserId = p.UserId,
                User = new
                {
                    UserId = p.User != null ? p.User.UserId : p.UserId,
                    UserLoginId = p.User != null ? p.User.UserLoginId : "",
                    Username = p.User != null ? p.User.Username : ""
                }
            })

            .OrderByDescending(x => x.MessageDate).ToList(); //now project to the return type, this SHOULD BE a CONTRACT NOT THE ENTITY!

            response = queryResponse.Select(mes => new TaskMessage
            {
                MessageContent = mes.MessageContent,
                MessageDate = mes.MessageDate,
                UserId = mes.UserId,
                Success = mes.Success,
                TaskMessageId = mes.TaskMessageId,
                User =
                    new User
                    {
                        UserId = mes.User.UserId,
                        UserLoginId = mes.User.UserLoginId,
                        Username = mes.User.Username
                    }
            }).ToList();

            return response;
        }
    }
}

