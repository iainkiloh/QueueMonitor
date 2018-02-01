using WebDiagnostics.Domain.Interfaces;

namespace WebDiagnostics.QueryObjects.Interfaces
{
    public interface IMediator
    {
        TResponse Request<TResponse>(IQueryObject<TResponse> queryObject, IContext context);
        TResult Send<TResult>(ICommand<TResult> commandObject, IContext context);
    }
}
