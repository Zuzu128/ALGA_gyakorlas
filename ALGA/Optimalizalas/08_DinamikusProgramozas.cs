using System;

namespace OE.ALGA.Optimalizalas
{
    // 8. heti labor feladat - Tesztek: 08_DinamikusProgramozasTesztek.cs

    public class DinamikusHatizsakPakolas
    {
        HatizsakProblema problema;
        public int LepesSzam { get; private set; }

        public DinamikusHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
        }

        private int[,] TablazatFeltoltes()
        {
            int n = problema.m;
            int wMax = problema.Wmax;
            int[,] F = new int[n + 1, wMax + 1];
            LepesSzam = 0;

            for (int t = 0; t <= n; t++)
            {
                F[t, 0] = 0;
            }

            for (int h = 0; h <= wMax; h++)
            {
                F[0, h] = 0;
            }

            for (int t = 1; t <= n; t++)
            {
                for (int h = 1; h <= wMax; h++)
                {
                    LepesSzam++;

                    if (h >= problema.w[t - 1])
                    {
                        F[t, h] = (int)Math.Max(F[t - 1, h], F[t - 1, h - problema.w[t - 1]] + problema.p[t - 1]);
                    }

                    else
                    {
                        F[t, h] = F[t - 1, h];
                    }
                }
            }
            return F;
        }

        public bool[] OptimalisMegoldas()
        {
            int n = problema.m;
            int wMax = problema.Wmax;
            var F = TablazatFeltoltes();

            bool[] megoldas = new bool[n];
            int h = wMax;

            for (int t = n; t > 0; t--)
            {
                if (F[t, h] != F[t - 1, h])
                {
                    megoldas[t - 1] = true;
                    h -= problema.w[t - 1];
                }
                else
                {
                    megoldas[t - 1] = false;
                }
            }
            return megoldas;
        }

        public int OptimalisErtek()
        {
            var F = TablazatFeltoltes();
            return F[problema.m, problema.Wmax];
        } 
    }
}
