using System.Linq;
using WebDiagnostics.Domain;
using WebDiagnostics.Domain.Interfaces;
using WebDiagnostics.QueryObjects.Interfaces;

namespace WebDiagnostics.QueryObjects.Commands
{

    public class EditUserCommand : ICommand<User>
    {
        public User User { get; set; }
    }

    //simple method to demonstrate command handler usage
    public class EditUserCommandHandler : ICommandHandler<EditUserCommand, IContext, User>
    {

        public User Handle(EditUserCommand command, IContext context)
        {
            var user = context.Users.Where(x => x.UserId == command.User.UserId).FirstOrDefault();

            user.UserLoginId = "testchange";

            context.Save();

            return user;
        }

    }
}
