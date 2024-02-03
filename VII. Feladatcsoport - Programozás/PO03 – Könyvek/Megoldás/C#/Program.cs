using System;
using System.Text;

namespace Konyvek
{
    internal class Program
    {
        struct Konyv
        {
            public int Index;
            public string Cim;
            public string Szerzo;
            public string Kategoria;
            public string Tipus;
            public int Db;
            public DateTime UtolsoEladas;
        };

        static void Main(string[] args)
        {
            List<Konyv> adatok = new List<Konyv>();

            FileStream fs01 = new FileStream(@"konyvek.csv", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs01);

            bool elsoSor = false;

            while (!sr.EndOfStream)
            {
                string l = sr.ReadLine();

                if (!elsoSor)
                {
                    elsoSor = true;
                    continue;
                }

                string[] s = l.Split(",");

                if (s.Length != 7)
                {
                    continue;
                }

                Konyv k = new Konyv();

                k.Index = int.Parse(s[0]);
                k.Cim = s[1];
                k.Szerzo = s[2];
                k.Kategoria = s[3];
                k.Tipus = s[4];
                k.Db = int.Parse(s[5]);
                k.UtolsoEladas = DateTime.Parse(s[6]);

                adatok.Add(k);
            }

            sr.Close();
            fs01.Close();

            Console.WriteLine("1. Feladat");

            Console.WriteLine("A ténylegesen beolvasott adatsorok száma: " + adatok.Count);


            Console.WriteLine("");
            Console.WriteLine("2. Feladat");


            int regeny_szam = 0;

            for (int i = 0; i < adatok.Count; ++i)
            {
                Konyv k = adatok[i];

                if (k.Tipus == "Regény" && k.Db > 0)
                {
                    regeny_szam += 1;
                }
            }

            Console.WriteLine("Összesen " + regeny_szam + " különböző regény van raktáron.");

            Console.WriteLine("");
            Console.WriteLine("3. Feladat");

            int legregebbi_konyv_index = -1;
            DateTime utolso_datum = new DateTime();

            for (int i = 0; i < adatok.Count; ++i)
            {
                Konyv k = adatok[i];

                if (k.Db == 0)
                {
                    continue;
                }

                if (legregebbi_konyv_index == -1)
                {
                    utolso_datum = k.UtolsoEladas;
                    legregebbi_konyv_index = i;
                    continue;
                }

                if (k.UtolsoEladas < utolso_datum)
                {
                    utolso_datum = k.UtolsoEladas;
                    legregebbi_konyv_index = i;
                }
            }

            if (legregebbi_konyv_index != -1)
            {
                Console.WriteLine("A legrégebben eladott könyv címe: " + adatok[legregebbi_konyv_index].Cim + ", szerzője: " + adatok[legregebbi_konyv_index].Szerzo + ".");
            }
            else
            {
                Console.WriteLine("Nem volt találat!");
            }

            Console.WriteLine("");
            Console.WriteLine("4. Feladat");

            Console.WriteLine("A raktáron levő programozásról szóló könyvek:");

            int szam = 0;

            for (int i = 0; i < adatok.Count; ++i)
            {
                Konyv k = adatok[i];

                if (k.Tipus == "Programozás" && k.Db > 0)
                {
                    Console.WriteLine(k.Cim);
                    szam += 1;
                }
            }

            if (szam == 0)
            {
                Console.WriteLine("Egy ilyen sincs.");
            }

            Console.WriteLine("");
            Console.WriteLine("5. Feladat");

            int szepirodalom_szam = megszamol("Szépirodalom", adatok);

            Console.WriteLine("A Szépirodalom kategóriában " + szepirodalom_szam + " db könyv van raktáron.");

            Console.WriteLine("");
            Console.WriteLine("6. Feladat");

            Console.WriteLine("kategória1:");
            string kategoria1 = Console.ReadLine();

            Console.WriteLine("kategória2:");
            string kategoria2 = Console.ReadLine();

            int kategoria1_szam = megszamol(kategoria1, adatok);
            int kategoria2_szam = megszamol(kategoria2, adatok);

            if (kategoria1_szam > kategoria2_szam)
            {
                Console.WriteLine("Az 1.kategóriában van több könyv a raktáron");
            }
            else if (kategoria1_szam < kategoria2_szam)
            {
                Console.WriteLine("A 2.kategóriában van több könyv a raktáron");
            }
            else
            {
                Console.WriteLine("Egyenlő");
            }


            Console.WriteLine("");
            Console.WriteLine("7. Feladat");


            for (int i = 0; i < adatok.Count; ++i)
            {
                Konyv k = adatok[i];

                if (k.Db == 0)
                {
                    TimeSpan delta = DateTime.Today - k.UtolsoEladas;

                    Console.WriteLine(k.Szerzo + ": " + k.Cim + ". " + delta.Days);
                }
            }


            Console.WriteLine("");
            Console.WriteLine("8. Feladat");


            Console.WriteLine("Határdátum:");
            string hd = Console.ReadLine();


            try
            {
                DateTime hatardatum = DateTime.Parse(hd);

                szam = 0;

                for (int i = 0; i < adatok.Count; ++i)
                {
                    Konyv k = adatok[i];

                    if (k.Db == 0 && k.UtolsoEladas < hatardatum)
                    {
                        Console.WriteLine(k.Szerzo + ": " + k.Cim);
                    }
                }

                szam += 1;

                if (szam == 0)
                {
                    Console.WriteLine("Nincs ilyen!");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("A dátum formátuma helytelen!");
            }

            Console.WriteLine("");
            Console.WriteLine("9. Feladat");

            FileStream fs02 = new FileStream("hiany.txt", FileMode.Create, FileAccess.Write);
            StreamWriter f = new StreamWriter(fs02);

            for (int i = 0; i < adatok.Count; ++i)
            {
                Konyv k = adatok[i];

                if (k.Db == 0)
                {
                    f.Write(k.Cim + "\n");
                }
            }

            f.Close();
            fs02.Close();

            Console.WriteLine("A fájl kiíratása megtörtént!");
            Console.WriteLine("");

        }


        static int megszamol(string keresett_kategoria, List<Konyv> adatok)
        {
            int szam = 0;

            for (int i = 0; i < adatok.Count; ++i)
            {
                Konyv k = adatok[i];

                if (k.Kategoria == keresett_kategoria)
                {
                    szam += k.Db;
                }
            }

            return szam;
        }
    }
}