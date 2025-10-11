using System;
using System.Collections.Generic;

namespace LIB.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public string? Image { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Crime> Crimes { get; set; } = new List<Crime>();
}
