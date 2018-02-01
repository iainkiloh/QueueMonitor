namespace WebDiagnostics.Contracts
{
     public class TaskMessageContract
     {
            public System.Guid TaskMessageId { get; set; }
            public string MessageContent { get; set; }
            public bool Success { get; set; }
            public System.DateTime MessageDate { get; set; }
            public string ExceptionDetail { get; set; }
            public int UserId { get; set; }
            public string Username { get; set; }
    }
}
