namespace ALGA_ZH_C;

public class Verem<T>
{
    private T[] E;
    private int n;
    
    public int Elemszam => n;

    public Verem(int meret)
    {
        this.E = new T[meret];
        n = 0;
    }

    public virtual void Verembe(T ertek)
    {
        if (n < E.Length - 1)
        {
            E[n++] = ertek;
        }
        else throw new Exception("Nincs hely");
    }

    public virtual T Verembol()
    {
        if (n > 0)
        {
            T ertek = E[n-1];
            n--;
            return ertek;
        }
        else throw new Exception("Nincs elem");
    }
}