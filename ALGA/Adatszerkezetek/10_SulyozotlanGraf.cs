using System;
using System.Collections.Generic;

namespace OE.ALGA.Adatszerkezetek
{
    // 10. heti labor feladat - Tesztek: 10_SulyozatlanGrafTesztek.cs

    public class EgeszGrafEl : GrafEl<int>, IComparable
    {
        public int Honnan { get; }
        public int Hova { get; }

        public EgeszGrafEl(int honnan, int hova)
        {
            this.Honnan = honnan;
            this.Hova = hova;
        }

        public virtual int CompareTo(object? obj)
        {
            if (obj != null && obj is EgeszGrafEl b)
            {
                if (Honnan != b.Honnan)
                {
                    return Honnan.CompareTo(b.Honnan);
                }
                else
                {
                    return Hova.CompareTo(b.Hova);
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    public class CsucsmatrixSulyozatlanEgeszGraf : SulyozatlanGraf<int, EgeszGrafEl>
    {
        int n;
        bool[,] M;

        public CsucsmatrixSulyozatlanEgeszGraf(int n)
        {
            this.n = n;
            M = new bool[n, n];
        }

        public int CsucsokSzama
        {
            get
            {
                return n;
            }
        }

        public int ElekSzama
        {
            get
            {
                int count = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (M[i, j])
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }

        public Halmaz<int> Csucsok
        {
            get
            {
                Halmaz<int> csucsok = new FaHalmaz<int>();
                for (int cs = 0; cs < n; cs++)
                {
                    csucsok.Beszur(cs);
                }
                return csucsok;
            }
        }

        public Halmaz<EgeszGrafEl> Elek
        {
            get
            {
                Halmaz<EgeszGrafEl> elek = new FaHalmaz<EgeszGrafEl>();
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (M[i, j])
                        {
                            elek.Beszur(new EgeszGrafEl(i, j));
                        }
                    }
                }
                return elek;
            }
        }

        public Halmaz<int> Szomszedai(int csucs)
        {
            Halmaz<int> szomszedok = new FaHalmaz<int>();
            for (int j = 0; j < n; j++)
            {
                if (M[csucs, j])
                {
                    szomszedok.Beszur(j);
                }
            }
            return szomszedok;
        }

        public void UjEl(int honnan, int hova)
        {
            M[honnan, hova] = true;
        }

        public bool VezetEl(int honnan, int hova)
        {
            return M[honnan, hova];
        }
    }

    public static class GrafBejarasok
    {
        public static Halmaz<V> SzelessegiBejaras<V, E>(Graf<V, E> g, V start, Action<V> muvelet) where V : IComparable
        {
            Sor<V> S = new LancoltSor<V>();
            Halmaz<V> F = new FaHalmaz<V>();

            S.Sorba(start);
            F.Beszur(start);

            while (!S.Ures)
            {
                V k = S.Sorbol();
                muvelet(k);
                g.Szomszedai(k).Bejar(x =>
                {
                    if (!(F.Eleme(x)))
                    {
                        S.Sorba(x);
                        F.Beszur(x);
                    }
                });
            }
            return F;
        }

        public static Halmaz<V> MelysegiBejaras<V, E>(Graf<V, E> g, V start, Action<V> muvelet) where V : IComparable
        {
            Halmaz<V> F = new FaHalmaz<V>();
            MelysegiBejarasRekurzio(g, start, ref F, muvelet);
            return F;
        }

        private static void MelysegiBejarasRekurzio<V, E>(Graf<V, E> g, V k, ref Halmaz<V> F, Action<V> muvelet)
        {
            F.Beszur(k);
            muvelet(k);

            for (int i = 0; i < g.CsucsokSzama; i++)
            {
                V x = (V)(object)i;
                if (g.VezetEl(k, x) && !F.Eleme(x))
                {
                    MelysegiBejarasRekurzio(g, x, ref F, muvelet);
                }
            }
        }
    }
}