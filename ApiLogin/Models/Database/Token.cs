using System;
using System.Collections.Generic;

namespace ApiLogin.Models.Database;

public partial class Token
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Token1 { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public virtual User User { get; set; } = null!;
}
