namespace WebDiagnostics.QueryObjects.Interfaces
{
    public interface ICommand<out TResult> { }

    public interface ICommandHandler<in TCommand, in TContext, out TResult>
    where TCommand : ICommand<TResult>
    {
        TResult Handle(TCommand command, TContext context);
    }

}
