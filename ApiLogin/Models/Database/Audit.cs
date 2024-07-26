using System;
using System.Collections.Generic;

namespace ApiLogin.Models.Database;

public partial class Audit
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Action { get; set; }

    public string? Description { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual User User { get; set; } = null!;
}
