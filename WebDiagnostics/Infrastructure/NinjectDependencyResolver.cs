using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using WebDiagnostics.Domain;
using WebDiagnostics.Domain.Interfaces;
using WebDiagnostics.Domain.Repositories;
using WebDiagnostics.Domain.Services;
using WebDiagnostics.Interfaces;
using WebDiagnostics.QueryObjects.Interfaces;
using WebDiagnostics.Services;

namespace WebDiagnostics.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //add bindings here
            kernel.Bind<IEntityRepository<TaskMessage>>()
                .To<TaskMessageRepository>();

            kernel.Bind<IEntityRepository<User>>()
                .To<UserRepository>();

            kernel.Bind<IMailService>()
                .To<SmtpMailService>();

            kernel.Bind<IContext>()
                .To<QueueMonitorDbContext>();

            kernel.Bind<PrimaryKeyGeneratorFactory>()
                .To<ConcretePrimaryKeyGeneratorFactory>();

            kernel.Bind<IMediator>()
                .To<Mediator>();

        }
    }
}