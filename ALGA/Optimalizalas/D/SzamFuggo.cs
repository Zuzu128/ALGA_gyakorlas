namespace ALGA_ZH_D;

public class SzamFuggo : IComparable<SzamFuggo>, IFuggo<SzamFuggo>
{
    public int Szam { get; set; }
    public SzamFuggo? ToleFugg { get; set; }

    public bool FuggValakitol => ToleFugg != null;

    public SzamFuggo(int szam, SzamFuggo? toleFugg = null)
    {
        this.Szam = szam;
        this.ToleFugg = toleFugg;
    }

    public int CompareTo(SzamFuggo? other)
    {
        if (other == null) return 1;
        return this.Szam.CompareTo(other.Szam);
    }

    public override string ToString()
    {
        return $"Szám: {Szam}, Függ: {(ToleFugg != null ? ToleFugg.Szam.ToString() : "null")}";
    }
}