using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebDiagnostics.Contracts;
using WebDiagnostics.Domain;
using WebDiagnostics.Domain.Interfaces;
using WebDiagnostics.Infrastructure.Attributes;
using WebDiagnostics.Infrastructure.FIlters;
using WebDiagnostics.QueryObjects.Implementations;
using WebDiagnostics.QueryObjects.Interfaces;
using WebDiagnostics.ViewModels;

namespace WebDiagnostics.Controllers
{
    [ExceptionFilter]
    //[CheckAdminUser] not used for example site
    public class QueueMonitorController : BaseController
    {
        //default page size
        public int PageSize = 5;

        public QueueMonitorController(IContext context, IMediator mediator)
            : base(context, mediator)
        {
        
        }

        //entry route on initial loading of view
        public ActionResult Index(string startDate, string endDate, string status, int page = 1)
        {

            //for demo purposes we update the task dates here
            ((QueueMonitorDbContext)DataContext).UpdateMessageDates();

            bool? success = GetStatusBooleanFromString(status);
            var selectedStatus = GetStatusEnumFromString(status);

            var vm = new TaskViewModel
            {
                StartDate = FormatDateTimeObject(startDate, new TimeSpan(0, 0, 0)), //start
                EndDate = FormatDateTimeObject(endDate, new TimeSpan(23, 59, 59)), //end
                SelectedStatus = selectedStatus
            };

            //validate the model and return view if not valid
            ValidateFilters(vm);
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            
            var getTotalsQueryObject = new GetTaskSummaryTotalsQuery { StartDate = vm.StartDate.Value, EndDate = vm.EndDate.Value };
            var statistics = Mediator.Request(getTotalsQueryObject, DataContext);

            var getCountsQueryObject = new CountTaskMessagesQuery { StartDate = vm.StartDate.Value, EndDate = vm.EndDate.Value, Success = success };

            //add the paging info to the view model
            vm.PagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = Mediator.Request(getCountsQueryObject, DataContext),
                StartDate = FormatDateTimeObject(startDate, new TimeSpan(0, 0, 0)), //start
                EndDate = FormatDateTimeObject(endDate, new TimeSpan(23, 59, 59)), //end
                SelectedStatus = selectedStatus,
                OrderByProperty = "MessageDate",
                OrderByDescending = true

            };

            ConstructViewModel(statistics, new List<TaskMessageContract>(), ref vm);
            return View(vm);
        }

        public PartialViewResult GetTaskMessageData(TaskViewModel data)
        {
            bool? success = GetStatusBooleanFromString(data.SelectedStatusString);

            var EndDate = FormatDateTimeObject(data.EndDate.Value.ToString(), new TimeSpan(23, 59, 59));

            var getTasksPagedQueryObject = new GetTaskMessagesPagedQuery { StartDate = data.StartDate.Value, EndDate = EndDate, Success = success, PageSize = data.PagingInfo.ItemsPerPage, OrderByDescending = true, OrderByProperty = "MessageDate", PageNumber = 2 };
            var taskMessages = Mediator.Request(getTasksPagedQueryObject, DataContext);

            //convert to ViewModel
            List<TaskMessageViewModel> dataResponse = ConstructTaskMessageViewModelList(taskMessages);

            return PartialView(dataResponse);

        }

        public JsonResult GetTaskMessageDataJson(TaskViewModel data)
        {

            bool? success = GetStatusBooleanFromEnum(data.SelectedStatus);
           
            //validate the incoming POST data
            ValidateFilters(data);
            //if we have an error we return it as error code 400 (Bad Request) and with the ModelState errors collection
            if (!ModelState.IsValid)
            {
                //set error status, and return ModelState errors
                Response.StatusCode = 400;
                return Json(ModelState.Values.SelectMany(x => x.Errors), JsonRequestBehavior.AllowGet);
            }

            //configure the end date to use the entire day (i.e up to 23:59:59)
            var EndDate = FormatDateTimeObject(data.EndDate.Value.ToString(), new TimeSpan(23, 59, 59));

            //set and execute the QueryObject
            var getTasksPagedQueryObject = new GetTaskMessagesPagedQuery { StartDate = data.StartDate.Value, EndDate = EndDate, Success = success, PageSize = data.PagingInfo.ItemsPerPage, OrderByDescending = data.PagingInfo.OrderByDescending, OrderByProperty = data.PagingInfo.OrderByProperty, PageNumber = 1 };
            var taskMessages = Mediator.Request(getTasksPagedQueryObject, DataContext);

            var viewModelJson = new TaskViewModel();

            //convert and add to ViewModel
            List<TaskMessageViewModel> dataResponse = ConstructTaskMessageViewModelList(taskMessages);
            viewModelJson.TaskMessages = dataResponse;

            var getTotalsQueryObject = new GetTaskSummaryTotalsQuery { StartDate = data.StartDate.Value, EndDate = EndDate };
            var statistics = Mediator.Request(getTotalsQueryObject, DataContext);
            //add to view model
            viewModelJson.Statistics = statistics;

            //get totals summary data
            var getCountsQueryObject = new CountTaskMessagesQuery { StartDate = data.StartDate.Value, EndDate = EndDate, Success = success };

            viewModelJson.StartDate = data.StartDate.Value;
            viewModelJson.EndDate = EndDate;
            viewModelJson.SelectedStatus = data.SelectedStatus;
            
            //get the paging info
            viewModelJson.PagingInfo = new PagingInfo()
            {
                CurrentPage = 1, //always reverts to page 1 when we change the filter or sort the data
                ItemsPerPage = data.PagingInfo.ItemsPerPage,
                TotalItems = Mediator.Request(getCountsQueryObject, DataContext),
                StartDate = data.StartDate.Value, //startDate, new TimeSpan(0, 0, 0)), //start
                EndDate = EndDate,
                SelectedStatus = data.SelectedStatus,
                OrderByProperty = data.PagingInfo.OrderByProperty,
                OrderByDescending = data.PagingInfo.OrderByDescending
            };

            return Json(viewModelJson, JsonRequestBehavior.AllowGet);
        }

      
        public PartialViewResult PageLinks(PagingInfo data)
        {
            return PartialView(data);
        }

        //executed when clicking a page link button
        public JsonResult PageDataJson(PagingInfo  data)
        {
            bool? success = GetStatusBooleanFromEnum(data.SelectedStatus);
            
            var EndDate = FormatDateTimeObject(data.EndDate.ToString(), new TimeSpan(23, 59, 59));

            var getTasksPagedQueryObject = new GetTaskMessagesPagedQuery { StartDate = data.StartDate, EndDate = EndDate, Success = success, PageSize = data.ItemsPerPage, OrderByDescending = data.OrderByDescending, OrderByProperty = data.OrderByProperty, PageNumber = data.CurrentPage };
            var taskMessages = Mediator.Request(getTasksPagedQueryObject, DataContext);

            //convert to response type required
            List<TaskMessageViewModel> dataResponse = ConstructTaskMessageViewModelList(taskMessages);
           
            return Json(dataResponse, JsonRequestBehavior.AllowGet);
        }


        private List<TaskMessageViewModel> ConstructTaskMessageViewModelList(List<TaskMessageContract> taskMessages)
        {
            //convert to response type required
            List<TaskMessageViewModel> response = new List<TaskMessageViewModel>();
            foreach (var item in taskMessages)
            {
                var vm = new TaskMessageViewModel();
                vm.MessageDate = item.MessageDate;
                vm.ExceptionDetail = item.ExceptionDetail;
                vm.MessageContent = item.MessageContent;
                vm.Success = item.Success;
                vm.TaskMessageId = item.TaskMessageId;
                vm.UserDisplayName = item.Username;
                vm.UserId = item.UserId;
                response.Add(vm);
            }
            return response;
        }


        /// <summary>
        /// map data from domain model to our ViewModel
        /// </summary>
        /// <param name="statistics"></param>
        /// <param name="taskMessages"></param>
        /// <param name="vm"></param>
        private void ConstructViewModel(TaskStatistics statistics, List<TaskMessageContract> taskMessages, ref TaskViewModel vm)
        {
            vm.Statistics = new TaskStatistics
            {
                TotalMessages = statistics.TotalMessages,
                Success = statistics.Success,
                Failures = statistics.Failures
            };

            vm.TaskMessages = ConstructTaskMessageViewModelList(taskMessages);

        }

       
        private bool ValidDate(string date)
        {
            DateTime parsedDate;
            if (!DateTime.TryParse(date, out parsedDate))
            {
                return false;
            }
            return true;

        }

        private DateTime FormatDateTimeObject(string stringDate, TimeSpan ts)
        {

            DateTime response = DateTime.Now;
            response = response.Date + ts;
            if (!string.IsNullOrEmpty(stringDate) && ValidDate(stringDate))
            {
                response = DateTime.Parse(stringDate);
                response = response.Date + ts;
            }
            return response;
        }

        //check data range for querything the domain model is not set to a ridiculous values
        //i.e. ensure the query which is going to run does not cause issues
        private void ValidateFilters(TaskViewModel data)
        {
            if (ModelState.IsValid)
            {
                if ((data.EndDate.Value - data.StartDate.Value).Days > 14)
                {
                    ModelState.AddModelError("", "The selected date range cannot be greater than 2 weeks");
                }
                if ((data.EndDate.Value - data.StartDate.Value).Days < 0)
                {
                    ModelState.AddModelError("", "Please ensure the end date is greater than the start date");
                }
            }
        }

        private bool? GetStatusBooleanFromString(string status)
        {
            bool? success = null;
            switch (status)
            {
                case "Success":
                    success = true;
                    break;
                case "Fail":
                    success = false;
                    break;
            }
            return success;
        }

        private bool? GetStatusBooleanFromEnum(TaskViewModel.TaskResultStatus selectedStatus)
        {
            bool? success = null;
            switch (selectedStatus)
            {
                case TaskViewModel.TaskResultStatus.Success:
                    success = true;
                    break;
                case TaskViewModel.TaskResultStatus.Fail:
                    success = false;
                    break;
            }
            return success;
        }

        public TaskViewModel.TaskResultStatus GetStatusEnumFromString(string status)
        {
            var selectedStatus = TaskViewModel.TaskResultStatus.All;
            switch (status)
            {
                case "Success":
                    selectedStatus = TaskViewModel.TaskResultStatus.Success;
                    break;
                case "Fail":
                    selectedStatus = TaskViewModel.TaskResultStatus.Fail;
                    break;
            }
            return selectedStatus;
        }
    }
}

