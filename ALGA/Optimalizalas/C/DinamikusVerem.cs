namespace ALGA_ZH_C;

public class DinamikusVerem<T> : Verem<T> where T : IHelyettesitheto<T>
{
    private T[] E;
    private int n;

    public DinamikusVerem(int meret) : base(meret)
    {
        this.E = new T[meret];
        n = 0;
    }

    public override void Verembe(T ertek)
    {
        if (n == E.Length)
        {
            T[] uj = new T[E.Length * 2];
            for (int i = 0; i < E.Length; i++)
            {
                uj[i] = E[i];
            }

            E = uj;
        }
        E[n++] = ertek;
    }

    public int Megszamol(Func<T, bool> feltetel)
    {
        int szamlalo = 0;
        for (int i = 0; i < n; i++)
        {
            if (feltetel(E[i]))
            {
                szamlalo++;
            }
        }
        return szamlalo;
    }

    public override T Verembol()
    {
        if (n > 0)
        {
            T x = E[--n];
        
            if (!x.Helyettesitendo)
            {
                return x;  
            }
            else
            {
                for (int i = n -1; i >= 0; i--)
                {
                    if (E[i].Helyettesitheti(x))
                    {
                        T helyettesito = E[i];
                        
                        for (int j = i; j < n - 1; j++)
                        {
                            E[j] = E[j + 1];
                        }
                        n--; 

                        return helyettesito;
                    }
                }
            }

            return x;
        }
        throw new Exception("Nincs helyettesíthető elem");
    }

    public T VerembolCserevel()
    {
        if (n > 0)
        {
            T x = E[--n];  
       
            if (!x.Helyettesitendo)
            {
                return x;
            }
            else
            {
                for (int i = n - 1; i >= 0; i--)
                {
                    if (E[i].Helyettesitheti(x))
                    {
                        T helyettesito = E[i];
                        E[n - 1] = helyettesito;
                        
                        for (int j = i; j < n - 1; j++)
                        {
                            E[j] = E[j + 1];
                        }
                        n--;

                        return helyettesito;
                    }
                }
            }

            return x;
        }
        throw new Exception("Nincs helyettesíthető elem");
    }
}