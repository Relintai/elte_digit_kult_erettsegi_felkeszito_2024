using System;
using System.Collections.Generic;
using System.IO;

namespace PO5
{
    internal class Program
    {
        private class Fuvar
        {
            public string Megnevezes { get; set; }
            public int Tavolsag { get; set; }
            public double Uzemanyag { get; set; }
            public int Suly { get; set; }
            public int Honap { get; set; }
            public int Nap { get; set; }

            public double Fogyasztas { get { return (Uzemanyag / Tavolsag) * 100.0; } }

            public Fuvar(string megnevezes, int tavolsag, double uzemanyag, int suly, int honap, int nap)
            {
                Megnevezes = megnevezes;
                Tavolsag = tavolsag;
                Uzemanyag = uzemanyag;
                Suly = suly;
                Honap = honap;
                Nap = nap;
            }
        }

        private static Fuvar[] fuvarok;


        static void Main(string[] args)
        {
            string[] adatok = File.ReadAllLines(@"D:\Egyetem\Kiadvány\05_Fuvar\Adatok.csv");
            fuvarok = new Fuvar[adatok.Length];
            for (int i = 0; i < adatok.Length; i++)
            {
                string[] adatsor = adatok[i].Split(';');
                fuvarok[i] = new Fuvar(adatsor[0], Convert.ToInt32(adatsor[1]), Convert.ToDouble(adatsor[2].Replace('.',',')), Convert.ToInt32(adatsor[3]), Convert.ToInt32(adatsor[4]), Convert.ToInt32(adatsor[5]));
            }

            int maximumIndex = 0;
            for (int i = 1; i < fuvarok.Length; i++)
            {
                if (fuvarok[i].Fogyasztas > fuvarok[maximumIndex].Fogyasztas)
                {
                    maximumIndex = i;
                }
            }
            Console.WriteLine($"A legtöbbet fogasztott jármű neve: {fuvarok[maximumIndex].Megnevezes}, fogyasztása: {fuvarok[maximumIndex].Fogyasztas}, ekkor {fuvarok[maximumIndex].Suly} kg súlyt szálított.");

            List<string> osszesNev = new List<string>();
            foreach (Fuvar f in fuvarok)
            {
                if (!osszesNev.Contains(f.Megnevezes)) { osszesNev.Add(f.Megnevezes); }
            }
            string[] export = new string[osszesNev.Count + 1];
            export[0] = DateTime.Now.ToShortDateString();
            for (int i = 0; i < osszesNev.Count; i++)
            {
                export[i + 1] = $"{osszesNev[i]}: {adatokJarmurol(osszesNev[i])}";
            }
            File.WriteAllLines("export.txt", export);

            Console.WriteLine(Legtobb(5,17,7,14));
            Nagyobb(200);
            Console.WriteLine(Osszes("Teher 2.", 5,17,7,21));
            Magas(15.1);

            Console.ReadKey();
        }

        private static string adatokJarmurol(string nev)
        {
            int bejegyzesek = 0;
            int tavolsagOsszeg = 0;
            double fogyasztasOsszeg = 0.0;
            int sulyOsszeg = 0;

            foreach (Fuvar f in fuvarok)
            {
                if (f.Megnevezes == nev)
                {
                    bejegyzesek++;
                    tavolsagOsszeg += f.Tavolsag;
                    fogyasztasOsszeg += f.Fogyasztas;
                    sulyOsszeg += f.Suly;
                }
            }

            return $"Teljes megtett távolság: {tavolsagOsszeg} km, Átlagfogyasztás: {fogyasztasOsszeg / bejegyzesek} l/100 km, Összesített súly: {sulyOsszeg} kg";
        }

        private static string Legtobb(int honaptol, int naptol, int honapig, int napig)
        {
            Dictionary<string, int> nevFuvar = new Dictionary<string, int>();
 
            foreach (Fuvar f in fuvarok)
            {
                if (f.Honap >= honaptol && f.Honap <= honapig && (f.Honap != honaptol || f.Nap >= naptol) && (f.Honap != honapig || f.Nap <= napig))
                {
                    if (!nevFuvar.ContainsKey(f.Megnevezes))
                    {
                        nevFuvar.Add(f.Megnevezes, 1);
                    }
                    else 
                    {
                        nevFuvar[f.Megnevezes]++;
                    }
                }
            }

            string vissza = "Nem történt fuvar";
            if (nevFuvar.Keys.Count > 0)
            {
                int maximum = 0;
                foreach (string i in nevFuvar.Keys)
                {
                    if (nevFuvar[i] > maximum) 
                    {
                        maximum = nevFuvar[i];
                        vissza = i;
                    }
                }
            }

            return vissza;
        }

        private static void Nagyobb(int suly)
        {
            foreach (Fuvar f in fuvarok)
            {
                if (f.Suly > suly)
                {
                    Console.WriteLine($"{f.Honap.ToString("00")}.{f.Nap.ToString("00")} {f.Megnevezes}");
                }
            }
        }

        private static int Osszes(string nev, int honaptol, int naptol, int honapig, int napig)
        {
            int tavolsag = 0;

            foreach (Fuvar f in fuvarok)
            {
                if (f.Megnevezes == nev && f.Honap >= honaptol && f.Honap <= honapig && (f.Honap != honaptol || f.Nap >= naptol) && (f.Honap != honapig || f.Nap <= napig))
                {
                    tavolsag += f.Tavolsag;
                }
            }

            return tavolsag;
        }

        private static void Magas(double fogyasztas)
        {
            List<string> kiirtak = new List<string>();
            foreach (Fuvar f in fuvarok)
            {
                if (f.Fogyasztas > fogyasztas)
                {
                    if (!kiirtak.Contains(f.Megnevezes))
                    {
                        Console.WriteLine(f.Megnevezes);
                        kiirtak.Add(f.Megnevezes);
                    }
                }
            }
        }


    }
}
