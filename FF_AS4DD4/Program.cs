using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF_AS4DD4
{
    class Program
    {
        static Random rand = new Random();
        static GrafSzomszedsagiLista<Helyszin> terkep = new GrafSzomszedsagiLista<Helyszin>();
        static List<Helyszin> helyszinek = new List<Helyszin>();

        public static void Init(int maxel = 2, int maxork = 500)
        {
            int maxEl = maxel; //Maximális élek száma / helyszín
            int maxOrk = maxork; //Maximális orkok száma egy helyen.

            helyszinek.Add(new Helyszin("Megye"));
            helyszinek.Add(new Helyszin("Minas-Tirith", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Pelargir", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Harondor", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Osgiliath", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Vasudvard", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Fangorn-erdő", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Lothlórien", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Moria", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Suhatag", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Dagorlad", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Erebor", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Völgyzugoly", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Edoras", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Harad-sivatag", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Helm-szurdok", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Dúnharg", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Morannon", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Barad-dúr", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Moria", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Harondor", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Pelargir", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Bakföld", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Arthedain", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Mithlond", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Lindon", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Doriath", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Ossiriand", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Gondolin", rand.NextDouble() > 0.5));
            helyszinek.Add(new Helyszin("Mordor", false));

            for (int i = 0; i < helyszinek.Count; i++)
            {
                if (!helyszinek[i].Szabad) helyszinek[i].UjOrk(rand.Next(maxOrk));
                terkep.UjCsucs(helyszinek[i]);
            }

            for (int i = 0; i < helyszinek.Count-1; i++)    //Összekötöm a gráf összes pontját, hogy biztosan ne legyen olyan csúcs, ahova nem vezet él.
            {
                terkep.UjEl(helyszinek[i], helyszinek[i + 1]);
            }

            for (int i = 0; i < helyszinek.Count; i++)      //Minden csúcshoz randomizáltan hozzáadunk éleket.
            {
                int elek = (int)(rand.NextDouble() * maxEl);//Meghatározzuk, hogy hány élt fogunk az adott csúcsba hozzáadni.
                for (int j = 0; j < elek; j++)
                {
                    int index = (int)(rand.NextDouble() * helyszinek.Count);//Kiválasztunk a gráfból egy random csúcsot.
                    while (index == i)                                      //Amennyiben, a random csúcs, megegyezik az éppen aktuális csúccsal, addig generáljuk
                    {                                                       //újra az indexet, amíg egy másik csúcst nem kapunk.
                        index = (int)(rand.NextDouble() * helyszinek.Count);
                    }


                    terkep.UjEl(helyszinek[i], helyszinek[index]);



                }


            }

        }


        static void Main(string[] args)
        {

            Init(4,1000); //Létrehozza a helyszíneket (random hogy melyik helyszín szabad, és az orkok száma is random változik), hozzáadja a gráfhoz, és létrehozza a random éleket.



            Console.WriteLine("Kérem válasszon egy helyszínt, az alábbiak közül: ");
            for (int i = 0; i < helyszinek.Count; i++)
            {
                Console.WriteLine((i + 1) + " : " + helyszinek[i].Nev);
            }
            bool szam = false;
            int index = 0;
            while (!szam)
            {
                try
                {
                    index = int.Parse(Console.ReadLine()) - 1;
                    if (index + 1 > helyszinek.Count) throw new IndexOutOfRangeException();
                    else szam = true;
                }
                catch (Exception)
                {

                    Console.WriteLine("Kérem a felsoroltak közül adjon értéket!\n");
                }
            }

            if (!(helyszinek[index].Szabad))
            {
                Console.WriteLine("Ez a terület Szauron kezén van!");
            }
            else
            {
                List<Helyszin> szomszedok = terkep.Szomszedok(index);
                Console.WriteLine("A terület szabad, szomszédja(i): ");
                for (int i = 0; i < szomszedok.Count; i++)
                {
                    Console.WriteLine(szomszedok[i].Nev);
                }
                Console.WriteLine("\n");
            }
            double legyozendoOrkokSzama = 0;
            List<Helyszin> utvonal = terkep.Dijkstra(helyszinek[0], helyszinek[helyszinek.Count - 1], ref legyozendoOrkokSzama);
            Console.WriteLine("Ahhoz hogy elérjünk mordorba, " + legyozendoOrkokSzama + " db orkon kell magunkat átverekedni, és a következő útvonalon haladhatunk: ");
            for (int i = 0; i < utvonal.Count; i++)
            {
                Console.WriteLine(utvonal[i].Nev);
            }
            ;



        }
    }
}
