namespace ALGA_ZH_B;

public class SzamoloFaHalmaz<T> : FaHalmaz<T> where T : IComparable
{
    private FaElem<T>? gyoker;

    public override int Elemszam
    {
        get
        {
            int osszeg = 0;

            Bejaras(gyoker, x =>
            {
                if (x != null)
                {
                    osszeg += x.darabszam;
                }
            });


            return osszeg;
        }
    }

    public SzamoloFaHalmaz()
    {
        this.gyoker = null;
        Elemszam = 0;
    }

    public override void Beszur(T ertek)
    {
        gyoker = ReszFabaBeszur(gyoker, ertek);
    }

    private FaElem<T> ReszFabaBeszur(FaElem<T>? p, T ertek)
    {
        if (p == null)
        {
            FaElem<T> uj = new FaElem<T>(ertek, null, null, 1);
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
                p.darabszam++;
            }
        }

        return p;
    }

    public void Bejaras(FaElem<T>? p, Action<FaElem<T>?> muvelet)
    {
        if (p != null)
        {
            muvelet(p);
            Bejaras(p.bal, muvelet);
            Bejaras(p.jobb, muvelet);
        }
    }

    public SzamoloFaHalmaz<T> Szures(T minDarabSzam)
    {
        SzamoloFaHalmaz<T> uj = new SzamoloFaHalmaz<T>();

        Bejaras(gyoker, x =>
        {
            if (x.darabszam.CompareTo(minDarabSzam) >= 0)
            {
                uj.Beszur(x.tart);
            }
        });

        return uj;
    }

    public SzamoloFaHalmaz<T> Szures(Func<T, int, bool> feltetel)
    {
        SzamoloFaHalmaz<T> uj = new SzamoloFaHalmaz<T>();
        
        Bejaras(gyoker, x =>
        {
            if (feltetel(x.tart, x.darabszam))
            {
                uj.Beszur(x.tart);
            }
        });

        return uj;
    }
}