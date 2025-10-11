using System;
using System.Collections.Generic;

namespace LIB.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public int Number { get; set; }

    public string Side { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string? Image { get; set; }

    public virtual ICollection<Crime> Crimes { get; set; } = new List<Crime>();
}
