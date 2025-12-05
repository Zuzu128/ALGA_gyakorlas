using System;

namespace OE.ALGA.Optimalizalas
{
    // 7. heti labor feladat - Tesztek: 07_NyersEroTesztek.cs

    public class NyersEro<T>
    {
        int m;
        Func<int, T> generator;
        Func<T, float> josag;

        public int LepesSzam { get; private set; }

        public NyersEro(int m, Func<int, T> generator, Func<T, float> josag)
        {
            this.m = m;
            this.generator = generator;
            this.josag = josag;
        }

        public T OptimalisMegoldas()
        {
            T o = generator(1);
            for (int i = 2; i <= m; i++)
            {
                T x = generator(i);
                LepesSzam++;
                if (josag(x) > josag(o))
                {
                    o = x;
                }
            }
            return o;
        }
    }

    public class HatizsakProblema
    {
        public int m;
        public int Wmax;
        public int[] w;
        public float[] p;

        public HatizsakProblema(int m, int wmax, int[] w, float[] p)
        {
            this.m = m;
            Wmax = wmax;
            this.w = w;
            this.p = p;
        }

        public float OsszErtek(bool[] pakolas)
        {
            if (pakolas == null || pakolas.Length == 0)
            {
                return 0;
            }

            float s = 0;
            int n = Math.Min(pakolas.Length, p.Length);

            for (int i = 0; i < n; i++)
            {
                if (pakolas[i])
                {
                    s += p[i];    
                }
            }

            return s;
        }

        public int OsszSuly(bool[] pakolas)
        {
            if (pakolas == null || pakolas.Length == 0)
            {
                return 0;
            }

            int s = 0;
            int n = Math.Min(pakolas.Length, w.Length);

            for (int i = 0; i < n; i++)
            {
                if (pakolas[i])
                {
                    s += w[i];
                }
            }

            return s;
        }

        public bool Ervenyes(bool[] pakolas)
        {
            int s = OsszSuly(pakolas);
            return s <= Wmax;
        }
    }

    public class NyersEroHatizsakPakolas
    {
        public int LepesSzam { get; private set; }
        HatizsakProblema problema;

        public NyersEroHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
        }

        public bool[] Generator (int i)
        {
            bool[] pakolas = new bool[problema.m];

            for (int j = 0; j < problema.m; j++)
            {
                pakolas[j] = ((i >> j) & 1) == 1; 
            }

            return pakolas;
        }

        private float Josag(bool[] pakolas)
        {
            if (!problema.Ervenyes(pakolas))
            {
                return -1;
            }
            return problema.OsszErtek(pakolas);
        }

        public bool[] OptimalisMegoldas()
        {
            int megoldasokSzama = (int)Math.Pow(2, problema.m);
            var nyersEro = new NyersEro<bool[]>(megoldasokSzama, Generator, Josag);
            bool[] optimalisPakolas = nyersEro.OptimalisMegoldas();
            LepesSzam = nyersEro.LepesSzam;
            return optimalisPakolas;
        }

        public float OptimalisErtek()
        {
            bool[] optimalis = OptimalisMegoldas();
            return problema.OsszErtek(optimalis);
        }
    }
}
