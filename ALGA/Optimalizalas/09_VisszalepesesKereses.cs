using System;
using System.Collections.Generic;
using System.Linq;

namespace OE.ALGA.Optimalizalas
{
    // 9. heti labor feladat - Tesztek: 09VisszalepesesKeresesTesztek.cs

    public class VisszalepesesOptimalizacio<T>
    {
        protected int n;
        protected int[] M; //lehetséges részmegoldások száma
        protected T[,] R; //M[szint, i] lesz az szint. szint i. részmegoldása.
        protected Func<int, T, bool> ft; //első korlát függvény
        protected Func<int, T, T[], bool> fk; //második korlát függvény
        protected Func<T[], float> josag;

        public VisszalepesesOptimalizacio(int n, int[] M, T[,] R, Func<int, T, bool> ft, Func<int, T, T[], bool> fk, Func<T[], float> josag)
        {
            this.n = n;
            this.M = M;
            this.R = R;
            this.fk = fk;
            this.josag = josag;
            this.ft = ft;
            LepesSzam = 0;
        }
        public int LepesSzam { get; protected set; }
        protected virtual void BackTrack(int szint, T[] E, ref bool van, ref T[] O)
        {
            for (int i = 0; i < M[szint]; i++)
            {
                LepesSzam++;
                if (ft(szint, R[szint, i]) && fk(szint, R[szint, i], E))
                {
                    E[szint] = R[szint, i];
                    if (szint == n - 1)
                    {
                        if (!van || josag(E) > josag(O))
                        {
                            Array.Copy(E, O, E.Length);
                        }
                        van = true;
                    }
                    else
                    {
                        BackTrack(szint + 1, E, ref van, ref O);
                    }
                }
            }
        }
        public T[] OptimalisMegoldas()
        {
            T[] E = new T[n];
            T[] O = new T[n];
            bool van = false;

            BackTrack(0, E, ref van, ref O);
            return O;
        }
    }
    public class VisszalepesesHatizsakPakolas
    {
        protected HatizsakProblema problema;
        public VisszalepesesHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
        }
        public int LepesSzam { get; protected set; }
        public virtual bool[] OptimalisMegoldas()
        {
            int[] M = new int[problema.m];
            bool[,] R = new bool[problema.m, 2];
            for (int i = 0; i < problema.m; i++)
            {
                M[i] = 2;
                R[i, 0] = true;
                R[i, 1] = false;
            }
            VisszalepesesOptimalizacio<bool> visszalepesoptimalizacio = new VisszalepesesOptimalizacio<bool>(
                problema.m, M, R,
                (szint, r) => !r || problema.w[szint] <= problema.Wmax,
                (szint, r, E) => {
                    int suly = 0;
                    for (int i = 0; i < szint; i++) if (E[i]) suly += problema.w[i];
                    return suly <= problema.Wmax && (!r || suly + problema.w[szint] <= problema.Wmax);
                },
                problema.OsszErtek
            );

            bool[] eredmeny = visszalepesoptimalizacio.OptimalisMegoldas();
            LepesSzam = visszalepesoptimalizacio.LepesSzam;
            return eredmeny;
        }
        public float OptimalisErtek()
        {
            return problema.OsszErtek(OptimalisMegoldas());
        }
    }
    public class SzetvalasztasEsKorlatozasOptimalizacio<T> : VisszalepesesOptimalizacio<T>
    {
        protected Func<int, T[], int> fb;
        public SzetvalasztasEsKorlatozasOptimalizacio(int n, int[] M, T[,] R, Func<int, T, bool> ft, Func<int, T, T[], bool> fk, Func<T[], float> josag, Func<int, T[], int> fb)
            : base(n, M, R, ft, fk, josag)
        {
            this.fb = fb;
        }

        protected override void BackTrack(int szint, T[] E, ref bool van, ref T[] O)
        {
            //LepesSzam++; //46
            for (int i = 0; i < M[szint]; i++)
            {
                LepesSzam++; //92
                if (ft(szint, R[szint, i]) && fk(szint, R[szint, i], E))
                {
                    E[szint] = R[szint, i];
                    if (szint == n - 1)
                    {
                        //LepesSzam++; //21
                        if (!van || josag(E) > josag(O))
                        {
                            //LepesSzam++; //3
                            Array.Copy(E, O, E.Length);
                        }
                        van = true;
                    }
                    else if (josag(E) + fb(szint, E) > josag(O))
                    {
                        //LepesSzam++; //45
                        BackTrack(szint + 1, E, ref van, ref O);
                    }
                }
            }
        }
    }
    public class SzetvalasztasEsKorlatozasHatizsakPakolas : VisszalepesesHatizsakPakolas
    {
        public SzetvalasztasEsKorlatozasHatizsakPakolas(HatizsakProblema problema) : base(problema)
        {
        }

        public override bool[] OptimalisMegoldas()
        {
            int[] M = Enumerable.Repeat(2, problema.m).ToArray();
            bool[,] R = new bool[problema.m, 2];
            for (int i = 0; i < problema.m; i++)
            {
                R[i, 0] = true;
                R[i, 1] = false;
            }

            SzetvalasztasEsKorlatozasOptimalizacio<bool> optimalizacio = new SzetvalasztasEsKorlatozasOptimalizacio<bool>(
                problema.m, M, R,
                (szint, r) => !r || problema.w[szint] <= problema.Wmax,
                (szint, r, E) => {
                    int suly = 0;
                    for (int i = 0; i < szint; i++) if (E[i]) suly += problema.w[i];
                    return suly <= problema.Wmax && (!r || suly + problema.w[szint] <= problema.Wmax);
                },
                problema.OsszErtek,
                (szint, E) => {
                    int becsultErtek = 0;
                    for (int i = szint; i < problema.m; i++)
                    {
                        if (problema.OsszSuly(E) + problema.w[i] <= problema.Wmax) becsultErtek += (int)problema.p[i];
                    }
                    return becsultErtek;
                }
            );

            bool[] eredmeny = optimalizacio.OptimalisMegoldas();
            LepesSzam = optimalizacio.LepesSzam;
            return eredmeny;
        }

    }


}
