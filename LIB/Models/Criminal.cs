using System;
using System.Collections.Generic;

namespace LIB.Models;

public partial class Criminal
{
    public int CriminalId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int RiskId { get; set; }

    public string? Image { get; set; }

    public DateTime CriminalSince { get; set; }

    public virtual CriminalRiskLevel Risk { get; set; } = null!;

    public virtual ICollection<Crime> Crimes { get; set; } = new List<Crime>();
}
