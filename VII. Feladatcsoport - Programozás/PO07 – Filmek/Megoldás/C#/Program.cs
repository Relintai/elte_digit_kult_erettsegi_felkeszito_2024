using System;
using System.Text;

namespace Filmek
{
    internal class Program
    {
        struct Film
        {
            public string FilmNev;
            public List<String> Mufajok;
            public List<String> Rendezok;
            public int HosszH;
            public int HosszM;
            public int Pontok;
            public int Megjelenes;
        };


        static void Main(string[] args)
        {
            List<Film> adatok = new List<Film>();

            FileStream fs01 = new FileStream(@"filmek.csv", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs01);

            // Elso sort kihagyjuk
            sr.ReadLine();

            while (!sr.EndOfStream)
            {
                string l = sr.ReadLine();

                string[] s = l.Split(",");

                if (s.Length != 6)
                {
                    continue;
                }

                Film k = new Film();

                k.FilmNev = s[0];

                string[] mufajok = s[1].Split("|");
                k.Mufajok = new List<string>();
                k.Mufajok.AddRange(mufajok);


                string[] rendezok = s[2].Split("|");
                k.Rendezok = new List<string>();
                k.Rendezok.AddRange(rendezok);

                string[] hossz = s[3].Split(" ");

                k.HosszH = int.Parse(hossz[0].Replace("h", ""));
                k.HosszM = int.Parse(hossz[1].Replace("m", ""));

                k.Pontok = int.Parse(s[4]);
                k.Megjelenes = int.Parse(s[5]);

                adatok.Add(k);
            }


            sr.Close();
            fs01.Close();

            Console.WriteLine("1. Feladat");

            Console.WriteLine("A ténylegesen beolvasott adatsorok száma: " + adatok.Count);

            Console.WriteLine("");
            Console.WriteLine("2. Feladat");

            int legjobb_index = 0;

            for (int i = 1; i < adatok.Count; ++i)
            {
                if (adatok[i].Pontok > adatok[legjobb_index].Pontok)
                {
                    legjobb_index = i;
                }
            }

            Console.WriteLine(adatok[legjobb_index].FilmNev);

            Console.WriteLine("");
            Console.WriteLine("3. Feladat");

            for (int i = 0; i < adatok.Count; ++i)
            {
                Film f = adatok[i];

                if (f.HosszH < 2)
                {
                    string p = f.FilmNev;

                    for (int j = 0; j < f.Rendezok.Count; ++j)
                    {
                        string r = f.Rendezok[j];

                        p += ", ";
                        p += r;
                    }

                    Console.WriteLine(p);
                }
            }



            Console.WriteLine("");
            Console.WriteLine("4. Feladat");

            int szam = 0;

            for (int i = 0; i < adatok.Count; ++i)
            {
                Film f = adatok[i];

                if (f.Megjelenes == 1994)
                {
                    Console.WriteLine(f.FilmNev);
                    szam += 1;
                }
            }

            if (szam == 0)
            {
                Console.WriteLine("Egy ilyen sincs.");
            }

            Console.WriteLine("");
            Console.WriteLine("5. Feladat");

            int drama_szam = mufajszamol("Dráma", adatok);

            Console.WriteLine("A Dráma műfajban " + drama_szam + " db film van.");

            Console.WriteLine("");
            Console.WriteLine("6. Feladat");

            Console.WriteLine("műfaj:");
            string mufaj = Console.ReadLine();

            int mufaj_szam = mufajszamol(mufaj, adatok);

            Console.WriteLine("A " + mufaj + " műfajban " + mufaj_szam + " db film van.");

            Console.WriteLine("");
            Console.WriteLine("7. Feladat");

            Console.WriteLine("műfaj:");
            mufaj = Console.ReadLine();

            int hossz_h = 0;
            int hossz_m = 0;

            for (int i = 0; i < adatok.Count; ++i)
            {
                Film f = adatok[i];

                for (int j = 0; j < f.Mufajok.Count; ++j)
                {

                    if (f.Mufajok[j] == mufaj)
                    {
                        hossz_h += f.HosszH;
                        hossz_m += f.HosszM;
                        break;
                    }
                }
            }

            int hossz_mc = (int)(hossz_m / 60);

            hossz_h += hossz_mc;
            hossz_m -= hossz_mc * 60;

            Console.WriteLine(hossz_h + "h " + hossz_m + "m");

            Console.WriteLine("");
            Console.WriteLine("8. Feladat");

            int legrovidebb_index = 0;
            int leghosszabb_index = 0;
            int legrovidebb_hossz = adatok[0].HosszH * 60 + adatok[0].HosszM;
            int leghosszabb_hossz = legrovidebb_hossz;

            for (int i = 1; i < adatok.Count; ++i)
            {
                Film a = adatok[i];

                int hossz = a.HosszH * 60 + a.HosszM;

                if (hossz < legrovidebb_hossz)
                {
                    legrovidebb_index = i;
                    legrovidebb_hossz = hossz;
                }

                if (hossz > leghosszabb_hossz)
                {
                    leghosszabb_index = i;
                    leghosszabb_hossz = hossz;
                }
            }


            Console.WriteLine("Legrövidebb: " + adatok[legrovidebb_index].FilmNev);
            Console.WriteLine("Leghosszabb: " + adatok[leghosszabb_index].FilmNev);


            Console.WriteLine("");
            Console.WriteLine("9. Feladat");

            int index = 0;
            int leghosszabb_kar_szam = adatok[index].FilmNev.Length;

            for (int i = 1; i < adatok.Count; ++i)
            {
                Film a = adatok[i];

                int kar_szam = a.FilmNev.Length;

                if (kar_szam > leghosszabb_kar_szam)
                {
                    index = i;
                    leghosszabb_kar_szam = kar_szam;
                }
            }

            Console.WriteLine(adatok[index].FilmNev);


            Console.WriteLine("");
            Console.WriteLine("10. Feladat");

            Dictionary<int, int> evek = new Dictionary<int, int>();

            for (int i = 0; i < adatok.Count; ++i)
            {
                Film f = adatok[i];

                int ev = f.Megjelenes;

                if (evek.ContainsKey(ev))
                {
                    evek[ev] += 1;
                }
                else
                {
                    evek[ev] = 1;
                }
            }

            int legtobb_ev = 0;
            int legtobb_ev_szam = 0;

            foreach (int k in evek.Keys)
            {
                if (evek[k] > legtobb_ev_szam)
                {
                    legtobb_ev = k;
                    legtobb_ev_szam = evek[k];
                }
            }

            Console.WriteLine(legtobb_ev);


            Console.WriteLine("");
            Console.WriteLine("11. Feladat");

            Console.WriteLine("Határszám percben:");
            string hd = Console.ReadLine();

            try
            {
                int hatarszam = int.Parse(hd);

                szam = 0;

                for (int i = 0; i < adatok.Count; ++i)
                {
                    Film f = adatok[i];

                    int hossz = f.HosszH * 60 + f.HosszM;

                    if (hossz < hatarszam)
                    {
                        Console.WriteLine(f.FilmNev);
                        szam += 1;
                    }
                }

                if (szam == 0)
                {
                    Console.WriteLine("Nincs ilyen!");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("A bemenet formátuma helytelen!");
            }


            Console.WriteLine("");
            Console.WriteLine("12. Feladat");

            FileStream fs02 = new FileStream("tobbrendezo.txt", FileMode.Create, FileAccess.Write);
            StreamWriter fr = new StreamWriter(fs02);

            for (int i = 0; i < adatok.Count; ++i)
            {
                Film film = adatok[i];

                if (film.Rendezok.Count > 1)
                {
                    fr.Write(film.FilmNev + "\n");
                }

            }

            fr.Close();
            fs02.Close();

            Console.WriteLine("A fájl kiíratása megtörtént!");
            Console.WriteLine("");
        }

        static int mufajszamol(string keresett_mufaj, List<Film> adatok)
        {
            int szam = 0;

            for (int i = 0; i < adatok.Count; ++i)
            {
                Film f = adatok[i];

                for (int j = 0; j < f.Mufajok.Count; ++j)
                {
                    if (f.Mufajok[j] == keresett_mufaj)
                    {
                        szam += 1;
                        break;
                    }
                }
            }

            return szam;
        }
    }
}