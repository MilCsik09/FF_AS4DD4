using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF_AS4DD4
{
    
    class LancoltLista<T>
    {
        public delegate void BejaroHandler(T tartalom);
        class ListaElem<T>
        {
            public T Tartalom { get; set; }
            public ListaElem<T> Kovetkezo { get; set; }
        }



        private ListaElem<T> fej;

        public int Meret { get; private set; }

        public void Beszuras(T elem)
        {
            ListaElem<T> uj = new ListaElem<T>();
            uj.Tartalom = elem;
            uj.Kovetkezo = fej;
            fej = uj;
            Meret++;


        }

        public void Torles(T torlendo)
        {
            ListaElem<T> e = null;
            ListaElem<T> p = fej;

            while (p != null && !(p.Tartalom.Equals(torlendo)))
            {
                e = p;
                p = p.Kovetkezo;
            }
            if (p != null)
            {
                if (e == null)
                {
                    fej = p.Kovetkezo;
                    Meret--;
                }
                else
                {
                    e.Kovetkezo = p.Kovetkezo;
                    Meret--;
                }
            }
            
        }

        public void Bejaras(BejaroHandler metodus)
        {
            BejaroHandler _metodus = metodus;
            ListaElem<T> p = fej;

            while (p != null)
            {
                _metodus?.Invoke(p.Tartalom);
                p = p.Kovetkezo;
            }

        }








    }
}
