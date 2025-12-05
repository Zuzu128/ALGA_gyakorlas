namespace ALGA_ZH_A;

public class LancoltLista<T>
{
    private LancElem<T>? fej;
    public virtual int Elemszam { get; protected set; }

    public LancoltLista()
    {
        this.fej = null;
        Elemszam = 0;
    }

    public virtual void Beszur(int index, T ertek)
    {
        if (this.fej is null || index == 0)
        {
            LancElem<T>? uj = new LancElem<T>(ertek, this.fej);
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
                LancElem<T>? uj = new LancElem<T>(ertek, p.kov);
                p.kov = uj;
            }
            else throw new Exception("Hibás index!");
        }

        this.Elemszam++;
    }

    public virtual void Torles(T ertek)
    {
        LancElem<T>? p = this.fej;
        LancElem<T>? e = null;

        do
        {
            while (p is not null && !p.tart.Equals(ertek))
            {
                e = p;
                p = p.kov;
            }

            if (p is not null)
            {
                LancElem<T>? q = p.kov;
                if (e is null)
                {
                    this.fej = q;
                }
                else
                {
                    e.kov = q;
                }

                p = q;
            }
        } while (p is not null);

        this.Elemszam--;
    }
}