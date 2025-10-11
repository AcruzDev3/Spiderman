using System;
using System.Collections.Generic;

namespace LIB.Models;

public partial class Crime
{
    public int CrimeId { get; set; }

    public int AddressId { get; set; }

    public int GradeId { get; set; }

    public int TypeId { get; set; }

    public string? Description { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public bool Status { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual CrimeGrade Grade { get; set; } = null!;

    public virtual CrimeType Type { get; set; } = null!;

    public virtual ICollection<Criminal> Criminals { get; set; } = new List<Criminal>();

    public virtual ICollection<User> Heroes { get; set; } = new List<User>();
}
