using System;

namespace OE.ALGA.Adatszerkezetek
{
    // 11. heti labor feladat - Tesztek: 11_KupacTesztek.cs
    public class Kupac<T> //where T : IComparable
    {
        protected T[] E;
        protected int n;
        protected Func<T, T, bool> nagyobbPrioritas;

        public Kupac(T[] e, int n, Func<T, T, bool> nagyobbPrioritas)
        {
            E = e;
            this.n = n;
            this.nagyobbPrioritas = nagyobbPrioritas;
            KupacotEpit();
        }

        public static int Szulo(int i)
        {
            return i / 2;//(int)Math.Floor(i / 2.0);
        }

        public static int Bal(int i)
        {
            return 2 * i;
        }

        public static int Jobb (int i)
        {
            return 2 * i + 1;
        }

        protected void Kupacol (int i)
        {
            int b = Bal(i);
            int j = Jobb(i);
            int max = (b < n && nagyobbPrioritas(E[b], E[i])) ? b : i;

            if (j < n && nagyobbPrioritas(E[j], E[max]))
            {
                max = j;
            }

            if (max != i)
            {
                (E[max], E[i]) = (E[i], E[max]);
                Kupacol(max);
            }

            /*if (b <= n && E[b].CompareTo(E[i]) > 0)
            {
                max = b;
            }
            else
            {
                max = i;
            }

            if (j <= n && E[j].CompareTo(E[max]) > 0)
            {
                max = j;
            }

            if (max != i)
            {
                (E[i], E[max]) = (E[max], E[i]);
                Kupacol(max);
            }*/
        }

        protected void KupacotEpit()
        {
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Kupacol(i);
            }
            /*for (int i = (int)Math.Floor(n / 2.0); i > 1; i--)
            {
                Kupacol(i);
            }*/
        }
    }

    public class KupacRendezes<T> : Kupac<T> where T : IComparable
    {
        public KupacRendezes(T[] E) : base(E, E.Length, (x,y) => x.CompareTo(y) > 0)
        {

        }

        public void Rendezes()
        {
            for (int i = n - 1; i >= 1; i--)
            {
                (E[0], E[i]) = (E[i], E[0]);
                n--;
                Kupacol(0);
            }
            /*KupacotEpit();
            for (int i = n; i > 2; i--)
            {
                (E[0], E[i]) = (E[i], E[0]);
                n = n - 1;
                Kupacol(0);
            }*/
        }
    }

    public class KupacPrioritasosSor<T> : Kupac<T>, PrioritasosSor<T> //where T : IComparable
    {
        public bool Ures
        {
            get
            {
                return n == 0;
            }
        }

        public KupacPrioritasosSor(int meret, Func<T, T, bool> nagyobbPrioritas) : base(new T[meret], 0, nagyobbPrioritas)
        {

        }

        private void KulcsotFelvisz(int i)
        {
            int sz = Szulo(i);
            if (sz >= 0 && nagyobbPrioritas(E[i], E[sz]))
            {
                (E[i], E[sz]) = (E[sz], E[i]);
                KulcsotFelvisz(sz);
            }
        }

        public void Sorba(T ertek)
        {
            if (n < E.Length)
            {
                n++;
                E[n - 1] = ertek;
                KulcsotFelvisz(n - 1);
            }
            else
            {
                throw new NincsHelyKivetel();
            }
        }

        public T Sorbol()
        {
            if (!Ures)
            {
                T max = E[0];
                E[0] = E[n - 1];
                n--;
                Kupacol(0);
                return max;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }

        public T Elso()
        {
            if (!Ures)
            {
                return E[0];
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }

        public void Frissit(T ertek)
        {
            int i = 0;

            while (i <= n && !object.Equals(E[i], ertek))//!(E[i].CompareTo(ertek) == 0))
            {
                i++;
            }

            if (i < n)
            {
                KulcsotFelvisz(i);
                Kupacol(i);
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }
    }
}