namespace ALGA_ZH_B;

public class FaHalmaz<T> where T : IComparable
{
    private FaElem<T>? gyoker;

    public virtual int Elemszam { get; protected set; }

    public FaHalmaz()
    {
        this.gyoker = null;
        this.Elemszam = 0;
    }

    public virtual void Beszur(T ertek)
    {
        Elemszam++;
        gyoker = ReszFabaBeszur(gyoker, ertek);
    }

    private FaElem<T> ReszFabaBeszur(FaElem<T>? p, T ertek)
    {
        if (p == null)
        {
            FaElem<T> uj = new FaElem<T>(ertek, null, null);
            return uj;
        }
        else
        {
            if (p.tart.CompareTo(ertek) > 0)
            {
                p.bal = ReszFabaBeszur(p.bal, ertek);
            }

            else if (p.tart.CompareTo(ertek) < 0)
            {
                p.jobb = ReszFabaBeszur(p.jobb, ertek);
            }
            
            else if (p.tart.CompareTo(ertek) == 0)
            {
                throw new Exception("Van mar ilyen ertek!");
            }
        }
        return p;
    }

    public bool Eleme(T ertek)
    {
        return ReszFaEleme(gyoker, ertek);
    }
    private bool ReszFaEleme(FaElem<T>? p, T ertek)
    {
        if (p != null)
        {
            if (p.tart.CompareTo(ertek) > 0)
            {
                return ReszFaEleme(p.bal, ertek);
            }
            else
            {
                if (p.tart.CompareTo(ertek) < 0)
                {
                    return ReszFaEleme(p.jobb, ertek);
                }
                else
                {
                    return true;
                }
            }
        }
        else return false;
    }
}