namespace AlgaMintaZh;

public interface ISzines
{
    public string Szin { get; }
    public void Atszinez(string szin);
}

class Program
{
    static void Main(string[] args)
    {
        //1. feladat
        Szines szines = new Szines("piros");
        Szines szines2 = new Szines("kek");
        Szines szines3 = new Szines("zold");
        Szines szines4 = new Szines("zold");
        Szines szines5 = new Szines("zold");
        
        LancoltLista<ISzines> lancoltLista = new LancoltLista<ISzines>();
        lancoltLista.Beszur(0, szines);
        lancoltLista.Beszur(1, szines2);
        lancoltLista.Beszur(2, szines3);
        lancoltLista.Beszur(3, szines4);
        lancoltLista.Beszur(4, szines5);
        Console.WriteLine(lancoltLista.Elemszam);
        
        //2. feladat
        Console.WriteLine("Eredeti szin: " + lancoltLista.Kiolvas(1).Szin);
        lancoltLista.Kiolvas(1).Atszinez("lila");
        Console.WriteLine("Modositott szin: " + lancoltLista.Kiolvas(1).Szin);

        //4. feladat
        var kivalogatott = lancoltLista.Kivalogat("zold");
        Console.WriteLine("Kivalogatott lista tartalma:");
        for (int i = 0; i < kivalogatott.Elemszam; i++)
        {
            Console.WriteLine(kivalogatott.Kiolvas(i).Szin);
        }
        
        //5. feladat
        bool Csere(Szines elem)
        {
            if (elem.Szin == "piros")
            {
                return true;
            }
            else return false;
        }
        //stb
    }
}

public class LancElem<T>
{
    public T tart;
    public LancElem<T>? kov;

    public LancElem(T tart, LancElem<T>? kov)
    {
        this.tart = tart;
        this.kov = kov;
    }
}

public class LancoltLista<T> where T : ISzines 
{
    private LancElem<T>? fej;
    public int Elemszam { get; private set; }

    public LancoltLista()
    {
        fej = null;
    }

    public void Beszur(int index, T ertek)
    {
        if (fej == null || index == 0)
        {
            LancElem<T>? uj = new LancElem<T>(ertek, fej);
            fej = uj;
        }
        else
        {
            LancElem<T>? p = fej;
            int i = 1;
            while (p.kov != null && i < index)
            {
                p = p.kov;
                i++;
            }

            if (i <= index)
            {
                LancElem<T>? uj = new LancElem<T>(ertek, p.kov);
                p.kov = uj;
            }
            else throw new Exception("Hibas index");
        }
        Elemszam++;
    }

    public T Kiolvas(int index)
    {
        LancElem<T>? p = fej;
        int i = 0;

        while (p != null && i < index)
        {
            p = p.kov;
            i++;
        }

        if (p != null)
        {
            return p.tart;
        }
        else throw new Exception("Hibas index");
    }

    public void SzinCsere(string mirol, string mire)
    {
        LancElem<T> p = fej;
        while (p != null)
        {
            if (p.tart.Szin.Equals(mirol))
            {
                p.tart.Atszinez(mire);
            }
            p = p.kov;
        }
    }

    public LancoltLista<T> Kivalogat(string szin)
    {
        LancoltLista<T> kivalogatott = new LancoltLista<T>();
        LancElem<T> p = fej;

        int index = 0;
        while (p != null)
        {
            if (p.tart.Szin.Equals(szin))
            {
                kivalogatott.Beszur(index, p.tart);
                index++;
            }

            p = p.kov;
        }
        return kivalogatott;
    }

    public void SzinCsere(Func<T, bool> kellcsere, string ujszin)
    {
        LancElem<T> p = fej;
        while (p != null)
        {
            if (kellcsere(p.tart))
            {
                p.tart.Atszinez(ujszin);
            }

            p = p.kov;
        }
    }
}

public class Szines : ISzines
{
    public string Szin { get; private set; }

    public Szines(string szin)
    {
        this.Szin = szin;
    }
    public void Atszinez(string szin)
    {
        this.Szin = szin;
    }
}