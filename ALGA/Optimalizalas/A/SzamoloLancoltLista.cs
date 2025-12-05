namespace ALGA_ZH_A;

public class SzamoloLancoltLista<T> : LancoltLista<T>
{
    private LancElem<T>? fej;

    public override int Elemszam
    {
        get
        {
            int darabszam = 0;
            LancElem<T>? p = this.fej;
            while (p is not null)
            {
                darabszam += p.darabszam;
                p = p.kov;
            }
            return darabszam;
        }
    }
    
    public override void Beszur(int index, T ertek)
    {
        bool vane = false;

        LancElem<T>? elem = this.fej;
        while (elem is not null)
        {
            if (elem.tart.Equals(ertek))
            {
                vane = true;
                elem.darabszam++;
            }

            elem = elem.kov;
        }

        if (vane == false)
        {
            if (this.fej is null || index == 0)
            {
                LancElem<T>? uj = new LancElem<T>(ertek, this.fej, 1);
                this.fej = uj;
            }
            else
            {
                LancElem<T>? p = this.fej;
                int i = 1;

                while (p.kov is not null && i < index)
                {
                    p = p.kov;
                    i++;
                }

                if (i <= index)
                {
                    LancElem<T>? uj = new LancElem<T>(ertek, p.kov, 1);
                    p.kov = uj;
                }
                else throw new Exception("Hibás index!");
            }
        }
    }

    public override void Torles(T ertek)
    {
        LancElem<T>? p = this.fej;
        LancElem<T>? e = null;

        while (p is not null) 
        {
            if (p.tart.Equals(ertek))
            {
                if (p.darabszam > 1)
                {
                    p.darabszam--;
                }
                else
                {
                    if (e is null) 
                    {
                        this.fej = p.kov;
                    }
                    else
                    {
                        e.kov = p.kov; 
                    }
                }
                return; 
            }

            e = p;
            p = p.kov;
        }
    }

    public void DarabSzamModositas(Func<T, int, int> modosito)
    {
        LancElem<T>? p = this.fej;
        while (p is not null)
        {
            int modositott = modosito(p.tart, p.darabszam);
            p.darabszam = modositott;
            p = p.kov;
        }
    }
}