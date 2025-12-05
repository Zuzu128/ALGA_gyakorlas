namespace ALGA_ZH_D;

public class FuggoFaHalmaz<T> : FaHalmaz<T> where T : IComparable<T>, IFuggo<T>
{
    private Func<T, bool> elfogadhato;

    public FuggoFaHalmaz(Func<T, bool> elfogadhato)
    {
        this.elfogadhato = elfogadhato;
    }

    public FaHalmaz<T> ProblemasElemek()
    {
        FaHalmaz<T> uj = new FaHalmaz<T>();
        PreOrderBejaras(x =>
        {
            if (x.FuggValakitol && x.ToleFugg.Equals(default(T)) || !elfogadhato(x))
            {
                    uj.Beszur(x);
            }
        });
        return uj;
    }

    public void ProblemasElemekTorlese()
    {
        ReszfaProblemasElemekTorlese(gyoker);
    }

    private FaElem<T>? ReszfaProblemasElemekTorlese(FaElem<T>? p)
    {
        if (p is null) return null; 
        
        p.bal = ReszfaProblemasElemekTorlese(p.bal);
        p.jobb = ReszfaProblemasElemekTorlese(p.jobb);

        if (p.tart.FuggValakitol && p.tart.ToleFugg.Equals(default(T)) && !elfogadhato(p.tart))
        {
            if (p.bal is null && p.jobb is null)
            {
                return null;
            }
            else if (p.bal is null)
            {
                return p.jobb;
            }
            else if (p.jobb == null)
            {
                return p.bal;
            }
            else
            {
                p.bal = KetgyerekesTorles(p, p.bal);
            }
        }
        return p;
    }

    private FaElem<T>? KetgyerekesTorles(FaElem<T> e, FaElem<T> r)
    {
        if (r.jobb is not null)
        {
            r.jobb = KetgyerekesTorles(e, r.jobb);
            return r;
        }
        else
        {
            e.tart = r.tart;
            return r.bal;
        }
    }
}