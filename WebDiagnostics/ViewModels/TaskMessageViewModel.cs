using System;
using System.ComponentModel.DataAnnotations;

namespace WebDiagnostics.ViewModels
{
    public class TaskMessageViewModel
    {
        public Guid TaskMessageId { get; set; }

        [Required]
        [StringLength(1024)]
        public string MessageContent { get; set; }

        [Required]
        public int UserId { get; set; }

        public string UserDisplayName { get; set; }

        public bool Success { get; set; }

        public DateTime MessageDate { get; set; }

        public string MessageDateText
        {
            get { return MessageDate.ToString("dd-MM-yyyy HH:mm"); }
        }

        [StringLength(1024)]
        public string ExceptionDetail { get; set; }

    }
}
