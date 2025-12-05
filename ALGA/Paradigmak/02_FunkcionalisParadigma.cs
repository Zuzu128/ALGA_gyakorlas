using System;
using System.Collections;
using System.Collections.Generic;

namespace OE.ALGA.Paradigmak
{
    // 2. heti labor feladat - Tesztek: 02_FunkcionálisParadigmaTesztek.cs

    public class FeltetelesFeladatTarolo<T> : FeladatTarolo<T>, IEnumerable, IEnumerable<T> where T : IVegrehajthato
    {
        public Func<T, bool> BejaroFeltetel { get; set; }

        public FeltetelesFeladatTarolo(int meret) : base(meret)
        {
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            Func<T, bool> feltetel = BejaroFeltetel ?? (_ => true);
            return new FeltetelesFeladatTaroloBejaro<T>(tarolo, n, feltetel);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void FeltetelesVegrehajtas(Func<T, bool> feltetel)
        {
            for (int i = 0; i < n; i ++)
            {
                if (feltetel(tarolo[i]))
                {
                    tarolo[i].Vegrehajtas();
                }
            }
        }

        public class FeltetelesFeladatTaroloBejaro<T> : IEnumerator<T>
        {
            T[] tarolo;
            int n;
            int aktualisIndex = -1;
            Func<T, bool> bejaroFeltetel;
            T current;

            public FeltetelesFeladatTaroloBejaro(T[] tarolo, int n, Func<T, bool> feltetel)
            {
                this.tarolo = tarolo;
                this.n = n;
                bejaroFeltetel = feltetel;
            }

            public T Current => current;

            object IEnumerator.Current => current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                while(++aktualisIndex < n)
                {
                    if (bejaroFeltetel(tarolo[aktualisIndex]))
                    {
                        current = tarolo[aktualisIndex];
                        return true;
                    }
                }
                return false;
            }

            public void Reset()
            {
                aktualisIndex = -1;
            }
        }
    }
}
