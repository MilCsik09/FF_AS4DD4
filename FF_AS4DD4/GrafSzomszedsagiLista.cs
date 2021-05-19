using System.Collections.Generic;

namespace FF_AS4DD4
{
    class GrafSzomszedsagiLista<T> : Graf<T>
    {
        List<T> tartalmak;
        List<List<El>> szomszedok;

        protected override int CsucsokSzama
        {
            get
            {
                return tartalmak.Count;
            }
        }

        public GrafSzomszedsagiLista()
        {
            tartalmak = new List<T>();
            szomszedok = new List<List<El>>();
        }

        public override void UjCsucs(T tartalom)
        {
            tartalmak.Add(tartalom);
            szomszedok.Add(new List<El>());
        }

        public override void UjEl(T honnan, T hova, double suly = 0, bool iranyitott = false)
        {
            int index = tartalmak.IndexOf(honnan);
            szomszedok[index].Add(new Graf<T>.El()
            {
                hova = hova,
                suly = suly,
            });

            if (!iranyitott)
            {
                index = tartalmak.IndexOf(hova);
                szomszedok[index].Add(new Graf<T>.El()
                {
                    hova = honnan,
                    suly = suly,
                });
            }


        }

        public List<T> Szomszedok(int index)
        {
            List<T> szomszed = new List<T>();
            for (int i = 0; i < szomszedok[index].Count; i++)
            {
               szomszed.Add(szomszedok[index][i].hova);
            }
            return szomszed;
        }

        protected override List<El> Szomszedok(T csucs)
        {
            int index = tartalmak.IndexOf(csucs);
            return szomszedok[index];
        }

        protected override T AdottIndexuCsucs(int index)
        {
            return tartalmak[index];
        }

        protected override int AdottCsucsIndexe(T csucs)
        {
            return tartalmak.IndexOf(csucs);
        }
    }

}
