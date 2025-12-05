namespace ALGA_ZH_C;

public class Szam : IHelyettesitheto<Szam>
{
    public int Ertek { get; set; }
    
    public Szam(int ertek)
    {
        Ertek = ertek;
    }
    
    public bool Helyettesitendo => Ertek % 2 == 0;
    
    public bool Helyettesitheti(Szam masik)
    {
        return masik.Ertek > this.Ertek;
    }
    
    public override string ToString()
    {
        return Ertek.ToString();
    }
}