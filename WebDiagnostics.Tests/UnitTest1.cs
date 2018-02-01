using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebDiagnostics.Domain;
using WebDiagnostics.Domain.Interfaces;
using WebDiagnostics.Domain.Repositories;
using WebDiagnostics.Domain.Services;
using WebDiagnostics.QueryObjects.Commands;
using WebDiagnostics.QueryObjects.Implementations;
using WebDiagnostics.QueryObjects.Interfaces;
using WebDiagnostics.Services;

namespace WebDiagnostics.Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestInitialize]
        public void UpdateTaskDateDate()
        {
            var context = new QueueMonitorDbContext();
            context.UpdateMessageDates();
        }

        [TestMethod]
        public void Test_Repo_TaskMessage_GetById()
        {
            IEntityRepository<TaskMessage> _repo = new TaskMessageRepository(new QueueMonitorDbContext(new ConcretePrimaryKeyGeneratorFactory()));
            var response = _repo.GetById(new Guid("7c67b33d-a135-4fb1-82f2-3dba55bbfa1f"));
            Assert.IsNotNull(response);

        }

        [TestMethod]
        public void Test_Mediator_GetTasks()
        {
            var queryObject = new GetTaskMessagesQuery {StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now};
            IMediator mediator = new Mediator();
            var response = mediator.Request(queryObject, new QueueMonitorDbContext());
            Assert.IsNotNull(response);

        }

        [TestMethod]
        public void Test_Mediator_TaskCount()
        {
            
            var queryObject = new CountTaskMessagesQuery {StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now};
            IMediator mediator = new Mediator();
            var response = mediator.Request(queryObject, new QueueMonitorDbContext());

            Assert.IsNotNull(response);

        }

        [TestMethod]
        public void Test_Mediator_Task_Totals()
        {
           
            IMediator mediator = new Mediator();
            var queryObject = new GetTaskSummaryTotalsQuery { StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now };
            var response = mediator.Request(queryObject, new QueueMonitorDbContext());

            Assert.IsNotNull(response);

        }


        [TestMethod]
        public void TestMethod_EditUserCommandHandler_Simulate_Post_()
        {
            IEntityRepository<User> _repo = new UserRepository(new QueueMonitorDbContext(new ConcretePrimaryKeyGeneratorFactory()));
            var userResponse = _repo.GetById(1);
            Assert.IsNotNull(userResponse);
          
            //now update user email
            userResponse.UserLoginId = "test1Upd";

            //apply change using the command object
            IMediator mediator = new Mediator();
            var commandObject = new EditUserCommand { User = userResponse };
            var response = mediator.Send(commandObject, new QueueMonitorDbContext());


            Assert.IsNotNull(response);
            Assert.IsNotNull(response.UserLoginId == "test1Upd");

        }

        [TestMethod]
        public void TestMethod_GetTaskMessages_Paged_Case1()
        {
            var queryObject = new GetTaskMessagesPagedQuery
            {
                StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now,
                PageNumber = 1, PageSize = 10, OrderByDescending = true, OrderByProperty = "MessageDate"
            };

            IMediator mediator = new Mediator();
            var response = mediator.Request(queryObject, new QueueMonitorDbContext());
            Assert.IsNotNull(response);

        }

        [TestMethod]
        public void TestMethod_GetTaskMessages_Paged_Case2()
        {
            var queryObject = new GetTaskMessagesPagedQuery
            {
                StartDate = DateTime.Now.AddDays(-20),
                EndDate = DateTime.Now,
                PageNumber = 1,
                PageSize = 20,
                OrderByDescending = true,
                OrderByProperty = "Success"
            };

            IMediator mediator = new Mediator();
            var response = mediator.Request(queryObject, new QueueMonitorDbContext());
            Assert.IsNotNull(response);

        }

        [TestMethod]
        public void TestMethod_GetTaskMessages_Paged_Case3()
        {
            var queryObject = new GetTaskMessagesPagedQuery
            {
                StartDate = DateTime.Now.AddDays(-20),
                EndDate = DateTime.Now,
                PageNumber = 1,
                PageSize = 20,
                OrderByDescending = false,
                OrderByProperty = "User.USR_DisplayName"
            };

            IMediator mediator = new Mediator();
            var response = mediator.Request(queryObject, new QueueMonitorDbContext());
            Assert.IsNotNull(response);

        }

        //test that the custom key generator infrastructure works
        [TestMethod]
        public void TestMethod_SaveExample1()
        {
            IEntityRepository<Example> _repo = new ExampleRepository(new QueueMonitorDbContext(new ConcretePrimaryKeyGeneratorFactory()));
            var item = new Example();

            item.ExampleDesc = "example Description from test";
           
            var response = _repo.Save(item);
            Assert.IsNotNull(response);

        }

        [TestMethod]
        public void TestMethod_GetExampleRecord()
        {
            IEntityRepository<Example> _repo = new ExampleRepository(new QueueMonitorDbContext(new ConcretePrimaryKeyGeneratorFactory()));
            var response = _repo.GetById("814843687");
            Assert.IsNotNull(response);

        }


       


    }
}
