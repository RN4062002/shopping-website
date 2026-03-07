using System;
using System.Collections.Generic;

namespace ecomServer.Models;

public partial class ErrorLog
{
    public int ErrorId { get; set; }

    public string? ErrorMessage { get; set; }

    public string? StackTrace { get; set; }

    public string? Path { get; set; }

    public string? Method { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedAt { get; set; }
}
