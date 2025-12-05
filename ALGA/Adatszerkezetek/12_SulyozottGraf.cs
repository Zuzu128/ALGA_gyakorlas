using System;

namespace OE.ALGA.Adatszerkezetek
{
    // 12. heti labor feladat - Tesztek: 12_SulyozottGrafTesztek.cs
    public class SulyozottEgeszGrafEl : EgeszGrafEl, SulyozottGrafEl<int>
    {
        public float Suly { get; }

        public SulyozottEgeszGrafEl(int honnan, int hova, float suly) : base(honnan, hova)
        {
            this.Suly = suly;
        }
    }

    public class CsucsmatrixSulyozottEgeszGraf : SulyozottGraf<int, SulyozottEgeszGrafEl>
    {
        int n;
        float[,] M;

        public CsucsmatrixSulyozottEgeszGraf(int n)
        {
            this.n = n;
            this.M = new float[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    M[i, j] = float.NaN;
                }
            }
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
                        if(!float.IsNaN(M[i, j]))
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
                FaHalmaz<int> csucsok = new FaHalmaz<int>();
                for (int i = 0; i < n; i++)
                {
                    csucsok.Beszur(i);
                }
                return csucsok;
            }
        }

        public Halmaz<SulyozottEgeszGrafEl> Elek
        {
            get
            {
                FaHalmaz<SulyozottEgeszGrafEl> elek = new FaHalmaz<SulyozottEgeszGrafEl>();
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if(!float.IsNaN(M[i, j]))
                        {
                            elek.Beszur(new SulyozottEgeszGrafEl(i, j, M[i, j]));
                        }
                    }
                }
                return elek;
            }
        }

        public float Suly(int honnan, int hova)
        {

            if (!float.IsNaN(M[honnan, hova]))
            {
                return M[honnan, hova];
            }
            else
            {
                throw new NincsElKivetel();
            }
        }

        public Halmaz<int> Szomszedai(int csucs)
        {
            FaHalmaz<int> szomszedai = new FaHalmaz<int>();
            for (int j = 0; j < n; j++)
            {
                if (!float.IsNaN(M[csucs, j]))
                {
                    szomszedai.Beszur(j);
                }
            }
            return szomszedai;
        }

        public void UjEl(int honnan, int hova, float suly)
        {
            M[honnan, hova] = suly;
        }

        public bool VezetEl(int honnan, int hova)
        {
            return !float.IsNaN(M[honnan, hova]);
        }
    }

    public class Utkereses
    {
        public static Szotar<V, float> Dijkstra<V, E>(SulyozottGraf<V, E> g, V start)
        {
            Szotar<V, float> L = new HasitoSzotarTulcsordulasiTerulettel<V, float>(g.CsucsokSzama);
            Szotar<V, V> P = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.CsucsokSzama);
            KupacPrioritasosSor<V> S = new KupacPrioritasosSor<V>(g.CsucsokSzama, (ez, ennel) => L.Kiolvas(ez) < L.Kiolvas(ennel));

            g.Csucsok.Bejar(x =>
            {
                L.Beir(x, float.MaxValue);
                S.Sorba(x);
            });

            L.Beir(start, 0);
            S.Frissit(start);

            while (!S.Ures)
            {
                V u = S.Sorbol();

                g.Szomszedai(u).Bejar(x =>
                {
                    float ujTav = L.Kiolvas(u) + g.Suly(u, x);
                    if (L.Kiolvas(x) > ujTav)
                    {
                        L.Beir(x, ujTav);
                        P.Beir(x, u);
                        S.Frissit(x);
                    }
                });
            }
            return L;
        }
    }

    public class FeszitofaKereses
    {
        public static Halmaz<E> Kruskal<V, E>(SulyozottGraf<V, E> g) where E : SulyozottGrafEl<V>, IComparable
        {
            Halmaz<E> A = new FaHalmaz<E>();
            Szotar<V, V> vhalmaz = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.CsucsokSzama);
            KupacPrioritasosSor<E> S = new KupacPrioritasosSor<E>(g.ElekSzama + 1, (e1, e2) => e1.Suly < e2.Suly);

            g.Csucsok.Bejar(v => vhalmaz.Beir(v, v));

            Func<V, V> Find = v =>
            {
                while (!vhalmaz.Kiolvas(v).Equals(v))
                    v = vhalmaz.Kiolvas(v);
                return v;
            };

            g.Elek.Bejar(S.Sorba);
            while (!S.Ures)
            {
                E e = S.Sorbol();
                V a = Find(e.Honnan);
                V b = Find(e.Hova);

                if (!a.Equals(b))
                {
                    A.Beszur(e);
                    vhalmaz.Beir(a, b);
                }
            }
            return A;
        }

        public static Szotar<V, V> Prim<V, E>(SulyozottGraf<V, E> g, V start)
        {
            Szotar<V, float> K = new HasitoSzotarTulcsordulasiTerulettel<V, float>(g.CsucsokSzama);
            Szotar<V, V> P = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.CsucsokSzama);
            Szotar<V, bool> latogatott = new HasitoSzotarTulcsordulasiTerulettel<V, bool>(g.CsucsokSzama);

            KupacPrioritasosSor<V> S = new KupacPrioritasosSor<V>(g.CsucsokSzama,
                (a, b) => K.Kiolvas(a) < K.Kiolvas(b));

            g.Csucsok.Bejar(v =>
            {
                K.Beir(v, float.MaxValue);
                latogatott.Beir(v, false);
                S.Sorba(v);
            });

            K.Beir(start, 0);
            S.Frissit(start);

            while (!S.Ures)
            {
                V u = S.Sorbol();

                if (latogatott.Kiolvas(u))
                    continue;

                latogatott.Beir(u, true);

                g.Szomszedai(u).Bejar(x =>
                {
                    if (!latogatott.Kiolvas(x) && g.Suly(u, x) < K.Kiolvas(x))
                    {
                        K.Beir(x, g.Suly(u, x));
                        P.Beir(x, u);
                        S.Frissit(x);
                    }
                });
            }

            return P;
        }
    }
}
