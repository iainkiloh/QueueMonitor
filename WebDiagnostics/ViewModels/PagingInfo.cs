using System;

namespace WebDiagnostics.ViewModels
{
    public class PagingInfo
    {

        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OrderByProperty { get; set; }
        public bool OrderByDescending { get; set; }
        public TaskViewModel.TaskResultStatus SelectedStatus { get; set; }
       

    }
}