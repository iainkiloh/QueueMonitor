using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebDiagnostics.Contracts;


namespace WebDiagnostics.ViewModels
{
    public class TaskViewModel
    {
        [Required(ErrorMessage="Please enter a start date ")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Please enter an end date ")]
        public DateTime? EndDate { get; set; }

        public TaskStatistics Statistics { get; set; }

        public List<TaskMessageViewModel> TaskMessages { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public TaskResultStatus StatusOptions { get; set; }
        public TaskResultStatus SelectedStatus { get; set; }

        public enum TaskResultStatus
        {
            All,
            Success,
            Fail
        }

        public string SelectedStatusString
        {
            get
            {
                if (SelectedStatus == TaskResultStatus.Success) { return "Success"; }
                if (SelectedStatus == TaskResultStatus.Fail) { return "Fail"; }
                return "All";
            }
        }

        public string StartDateText
        {
            get { return StartDate.HasValue ? StartDate.Value.ToString("dd-MM-yyyy HH:mm") : "Not Specified"; }
        }
        public string EndDateText
        {
            get { return EndDate.HasValue ? EndDate.Value.ToString("dd-MM-yyyy HH:mm") : "Not Specified"; }
        }

        public string StartDateLink
        {
            get { return StartDate.HasValue ? StartDate.Value.ToString("dd-MM-yyyy") : "Not Specified"; }
        }
        public string EndDateLink
        {
            get { return EndDate.HasValue ? EndDate.Value.ToString("dd-MM-yyyy") : "Not Specified"; }
        }
    }
}
