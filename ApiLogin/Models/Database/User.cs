using System;
using System.Collections.Generic;

namespace ApiLogin.Models.Database;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastLogin { get; set; }

    public string? UserStatus { get; set; }

    public virtual ICollection<Audit> Audits { get; set; } = new List<Audit>();

    public virtual ICollection<Error> Errors { get; set; } = new List<Error>();

    public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
