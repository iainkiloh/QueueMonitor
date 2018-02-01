using WebDiagnostics.QueryObjects.Interfaces;

namespace WebDiagnostics.QueryObjects.Interfaces
{
    public interface IQueryObject<out TResponse> { }
}

public interface IQueryHandler<in TQueryObject, in TContext,  out TResponse>
    where TQueryObject : IQueryObject<TResponse>
{
    TResponse Handle(TQueryObject queryObject, TContext context);
}






