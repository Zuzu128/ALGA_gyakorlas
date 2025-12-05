using System;
using System.Collections;
using System.Collections.Generic;

namespace OE.ALGA.Adatszerkezetek
{
    // 3. heti labor feladat - Tesztek: 03_TombImplementacioTesztek.cs

    public class TombVerem<T> : Verem<T>
    {
        T[] E;
        int n = 0;

        public TombVerem(int meret)
        {
            E = new T[meret];
        }

        public bool Ures
        {
            get
            {
                return n == 0;
            }
        }

        public T Felso()
        {
            if (!Ures)
            {
                return E[n - 1];
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }

        public void Verembe(T ertek)
        {
            if (n < E.Length)
            {
                E[n] = ertek;
                n++;
            }
            else
            {
                throw new NincsHelyKivetel();
            }
        }

        public T Verembol()
        {
            if (!Ures)
            {
                return E[--n];
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }
    }

    public class TombSor<T> : Sor<T>
    {
        T[] E;
        int e = 0;
        int u = 0;
        int n = 0;

        public bool Ures
        {
            get
            {
                return n == 0;
            }
        }

        public TombSor(int meret)
        {
            E = new T[meret];
        }

        public T Elso()
        {
            if (!Ures)
            {
                return E[e];
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }

        public void Sorba(T ertek)
        {
            if (n < E.Length)
            {
                E[u] = ertek;
                u = (u + 1) % E.Length;
                n++;
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
                T ertek = E[e];
                e = (e + 1) % E.Length;
                n--;
                return ertek;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }
    }

    public class TombLista<T> : Lista<T>, IEnumerable<T>
    {
        T[] E;
        int n = 0;

        public TombLista(int meret)
        {
            E = new T[meret];
        }

        public TombLista() : this(1) { }

        public int Elemszam { get { return n; } }

        public void Bejar(Action<T> muvelet)
        {
            for (int i = 0; i < n; i++)
            {
                muvelet(E[i]);
            }
        }

        public void Beszur(int index, T ertek)
        {
            if (index < 0 || index > n)
            {
                throw new HibasIndexKivetel();
            }
            if (n == E.Length)
            {
                MeretNoveles();
            }
            for (int i = n; i > index; i--)
            {
                E[i] = E[i - 1];
            }
            E[index] = ertek;
            n++;
        }

        public void Hozzafuz(T ertek)
        {
            Beszur(n, ertek);
        }

        public T Kiolvas(int index)
        {
            if (index <= n)
            {
                return E[index];
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Modosit(int index, T ertek)
        {
            if (index <= n)
            {
                E[index] = ertek;
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Torol(T ertek)
        {
            int db = 0;
            for (int i = 0;  i < n; i++)
            {
                if (E[i].Equals(ertek))
                {
                    db++;
                }
                else
                {
                    E[i - db] = E[i];
                }
            }
            n = n - db;
        }

        private void MeretNoveles()
        {
            T[] EMasolat = E;
            E = new T[EMasolat.Length * 2];
            for (int i = 0; i < n; i++)
            {
                E[i] = EMasolat[i];
            }
            Array.Clear(EMasolat);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new TombListaBejaro<T>(E, n);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class TombListaBejaro<T> : IEnumerator<T>
    {
        T[] E;
        int n;
        int aktualisIndex = -1;

        T IEnumerator<T>.Current => E[aktualisIndex];

        public object Current => Current;

        public TombListaBejaro(T[] E, int n)
        {
            this.E = E;
            this.n = n;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            while(++aktualisIndex < n)
            {
                return true;
            }
            return false;
        }

        public void Reset()
        {
            aktualisIndex = -1;
        }
    }
}
