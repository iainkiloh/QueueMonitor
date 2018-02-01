using WebDiagnostics.Domain.Interfaces;
using WebDiagnostics.QueryObjects.Commands;
using WebDiagnostics.QueryObjects.Implementations;
using WebDiagnostics.QueryObjects.Interfaces;

namespace WebDiagnostics.Services
{

    public class Mediator : IMediator
    {

        public TResponse Request<TResponse>(IQueryObject<TResponse> queryObject, IContext context)
        {
            if (queryObject is GetTaskSummaryTotalsQuery)
            {
                var handler = new GetTaskSummaryTotalsQueryHandler();
                var response = (TResponse)(object)handler.Handle((GetTaskSummaryTotalsQuery)queryObject, context);
                return response;
            }
            if (queryObject is GetTaskMessagesQuery)
            {
                var handler = new GetTaskMessagesQueryHandler();
                var response = (TResponse)(object)handler.Handle((GetTaskMessagesQuery)queryObject, context);
                return response;
            }
            if (queryObject is GetTaskMessagesPagedQuery)
            {
                var handler = new GetTaskMessagesPagedQueryHandler();
                var response = (TResponse)(object)handler.Handle((GetTaskMessagesPagedQuery)queryObject, context);
                return response;
            }
            if (queryObject is CountTaskMessagesQuery)
            {
                var handler = new CountTaskMessagesQueryHandler();
                var response = (TResponse)(object)handler.Handle((CountTaskMessagesQuery)queryObject, context);
                return response;
            }

       
            return (TResponse)new object();
        }


        public TResult Send<TResult>(ICommand<TResult> commandObject, IContext context)
        {
            if (commandObject is EditUserCommand)
            {
                var handler = new EditUserCommandHandler();
                var response = (TResult)(object)handler.Handle((EditUserCommand)commandObject, context);
                return response;
            }

          
            return (TResult)new object();
        }
    }
}
