using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebDiagnostics.Domain;
using WebDiagnostics.Domain.Interfaces;
using WebDiagnostics.Infrastructure.Attributes;
using WebDiagnostics.Interfaces;
using WebDiagnostics.QueryObjects.Interfaces;
using WebDiagnostics.ViewModels;

namespace WebDiagnostics.Controllers
{
    //[CheckAdminUser] not required for demo site
    public class TaskDetailController : BaseController
    {
        
        private readonly IEntityRepository<TaskMessage> _repository;
        private IMailService _mailService;

        public TaskDetailController(IEntityRepository<TaskMessage> repository, IMailService mailService, IContext context, IMediator mediator)
            : base(context, mediator)
        {
            _repository = repository;
            _mailService = mailService;
        }

        public ActionResult Index(string id)
        {
            var vm = new TaskMessageViewModel();
            Guid idValue = Guid.Parse(id);

            var data = DataContext.TaskMessages
               .Include(x => x.User).FirstOrDefault(x => x.TaskMessageId == idValue);

            ConstructViewModel(data,ref vm);
            return View(vm);
        }

        private void ConstructViewModel(TaskMessage data, ref TaskMessageViewModel vm)
        {
            vm.ExceptionDetail = data.ExceptionDetail;
            vm.MessageContent = data.MessageContent;
            vm.MessageDate = data.MessageDate;
            vm.Success = data.Success;
            vm.TaskMessageId = data.TaskMessageId;
            vm.UserId = data.UserId;
            vm.UserDisplayName = data.User != null ? data.User.Username : "System";
        }

    }
}