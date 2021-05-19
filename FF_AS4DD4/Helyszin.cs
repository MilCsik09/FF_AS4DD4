using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF_AS4DD4
{
    class Helyszin
    {
        public string Nev { get; private set; }
        public LancoltLista<Ork> Orkok { get; private set; }

        public bool Szabad { get; private set; }

        public Helyszin(string nev, bool szabad = true)
        {
            Nev = nev;
            Szabad = szabad;
            Orkok = new LancoltLista<Ork>();
        }

        public void UjOrk(int mennyiseg = 1)
        {
            if (!Szabad) {
                for (int i = 0; i < mennyiseg; i++)
                {
                    Orkok.Beszuras(new Ork());
                }
            }
            else
            {
                Console.WriteLine("Ez a terület szabad, az orkok hozzáadása sikertelen!");
            }

        }



    }
}
