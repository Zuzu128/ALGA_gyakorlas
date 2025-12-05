namespace ALGA_ZH_D;

public class FaHalmaz<T> where T : IComparable<T>
{
    protected FaElem<T>? gyoker;    
    
    public FaHalmaz()
    {
        gyoker = null;
    }

    public void Beszur(T ertek)
    {
        gyoker = ReszfabaBeszur(gyoker, ertek);
    }
    
    private FaElem<T> ReszfabaBeszur(FaElem<T>? p, T ertek)
    {
        if (p is null)
        {
            FaElem<T> uj = new FaElem<T>(ertek, null, null);
            return uj;
        }
        else if (p.tart.CompareTo(ertek) > 0)
        {
            p.bal = ReszfabaBeszur(p.bal, ertek);
        }
        else if (p.tart.CompareTo(ertek) < 0)
        {
            p.jobb = ReszfabaBeszur(p.jobb, ertek);
        }

        return p;
    }

    public bool Eleme(T ertek)
    {
        return ReszfaEleme(gyoker, ertek);
    }
    
    private bool ReszfaEleme(FaElem<T>? p, T ertek)
    {
        if (p is not null)
        {
            if (p.tart.CompareTo(ertek) > 0)
            {
                return ReszfaEleme(p.bal, ertek);
            }
            else if (p.tart.CompareTo(ertek) < 0)
            {
                return ReszfaEleme(p.jobb, ertek);
            }
            else return true;
        }
        else return false;
    }
    
    public void PreOrderBejaras(Action<T> muvelet)
    {
        ReszfaPreOrderBejaras(gyoker, muvelet);
    }

    private void ReszfaPreOrderBejaras(FaElem<T>? p, Action<T> muvelet)
    {
        if (p is not null)
        {
            muvelet(p.tart);
            ReszfaPreOrderBejaras(p.bal, muvelet);
            ReszfaPreOrderBejaras(p.jobb, muvelet);
        }
    }
}