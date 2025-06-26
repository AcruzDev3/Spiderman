using System;
using System.Collections.Generic;
using System.Xml;

namespace LIB.DAL;

public partial class HeroCrime
{
    public int Id { get; set; }

    public int IdHero { get; set; }

    public int IdCrime { get; set; }

    public virtual Crime IdCrimeNavigation { get; set; } = null!;

    public virtual User IdHeroNavigation { get; set; } = null!;

    public static HeroCrime CreateModel(int idCrime, int idHero)
    {
        return new HeroCrime
        {
            IdCrime = idCrime,
            IdHero = idHero
        };
    }
}
