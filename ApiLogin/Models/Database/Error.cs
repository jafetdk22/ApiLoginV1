using System;
using System.Collections.Generic;

namespace ApiLogin.Models.Database;

public partial class Error
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? ErrorMessage { get; set; }

    public string? StackTrace { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual User? User { get; set; }
}
