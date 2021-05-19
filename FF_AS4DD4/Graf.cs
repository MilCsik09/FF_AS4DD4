using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF_AS4DD4
{
    abstract class Graf<T>
    {
        protected class El
        {
            public T hova;
            public double suly;
        }

        public abstract void UjCsucs(T tartalom);
        public abstract void UjEl(T honnan, T hova, double suly = 0, bool iranyitott = false);
        protected abstract List<El> Szomszedok(T csucs);

        protected abstract T AdottIndexuCsucs(int index);

        protected abstract int AdottCsucsIndexe(T csucs);

        protected abstract int CsucsokSzama { get; }


        public List<T> Dijkstra(T start, T cel, ref double osszsuly)
        {

            double[] d = new double[CsucsokSzama];
            T[] n = new T[CsucsokSzama];
            List<T> S = new List<T>();

            for (int i = 0; i < CsucsokSzama; i++)
            {
                T x = AdottIndexuCsucs(i);
                d[i] = double.PositiveInfinity;
                n[i] = default(T);
                S.Add(x);
            }

            d[AdottCsucsIndexe(start)] = 0;

            while (S.Count != 0)
            {
                T u = MinKivesz(S, d);
                foreach (El x in Szomszedok(u))
                {
                    int ind_x = AdottCsucsIndexe(x.hova);
                    int ind_u = AdottCsucsIndexe(u);

                    if (d[ind_u] + (x.hova as Helyszin).Orkok.Meret < d[ind_x])
                    {
                        d[ind_x] = d[ind_u] + (x.hova as Helyszin).Orkok.Meret;
                        n[ind_x] = u;
                    }
                }
            }

            int celindex = AdottCsucsIndexe(cel);
            osszsuly = d[celindex];

            List<T> allomasok = new List<T>();
            while (!cel.Equals(start))
            {
                allomasok.Add(cel);
                cel = n[celindex];
                celindex = AdottCsucsIndexe(cel);
            }
            allomasok.Add(start);
            allomasok.Reverse();

            return allomasok;

            ;

        }

        private T MinKivesz(List<T> S, double[] d)
        {
            int minindex = 0;
            double min = double.PositiveInfinity;

            for (int i = 0; i < S.Count; i++)
            {
                int idx = AdottCsucsIndexe(S[i]);
                double sulyertek = d[idx];
                if (sulyertek < min)
                {
                    min = sulyertek;
                    minindex = i;
                }
            }

            T torlendo = S[minindex];
            S.RemoveAt(minindex);

            return torlendo;
        }

    }

}
