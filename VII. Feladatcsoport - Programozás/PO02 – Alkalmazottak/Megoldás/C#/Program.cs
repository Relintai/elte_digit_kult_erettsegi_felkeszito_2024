using System.Diagnostics.Metrics;
using System.Globalization;
using System.Text;

namespace Alkalmazottak
{
    internal class Program
    {

        static void Main(string[] args)
        {        
            FileStream fsr = new FileStream(@"személyek.txt",FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fsr);

            List<string> adat = new List<string>();

            while (!sr.EndOfStream)
            {
                adat.Add(sr.ReadLine());
            }

            sr.Close();
            fsr.Close();
            
            int adatsorok_szama = adat.Count - 1;   //van fejléc

            List<string> nev = new List<string>();
            List<string> poszt = new List<string>();
            List<string> reszleg = new List<string>();
            List<int> fizetes = new List<int>();
            List<DateTime> kezdes = new List<DateTime>();
            List<DateTime> vege = new List<DateTime>();
            
            DateTime datum = new DateTime();

            for (int i = 1; i < adatsorok_szama + 1; i++)
            {
                nev.Add(adat[i].Split(",")[0]);
                poszt.Add(adat[i].Split(",")[1]);
                reszleg.Add(adat[i].Split(",")[2]);
                fizetes.Add(int.Parse(adat[i].Split(",")[3]));
                kezdes.Add(DateTime.Parse(adat[i].Split(",")[4]));                
                
                if (DateTime.TryParse(adat[i].Split(",")[5], out datum))
                {
                    vege.Add(datum);
                }
                else
                {
                    vege.Add(default(DateTime));    //DateTime.MinValue, 0001,1,1
                }
                
            }
            
            Console.WriteLine("1. Feladat");
            Console.WriteLine("A beolvasott adatsorok száma: " + adatsorok_szama + "\n");

            Console.WriteLine("2. Feladat");            
            int bér = 0;
            for (int i = 0; i < adatsorok_szama; i++)
            {
                if (reszleg[i] == "Informatika")
                {
                    bér += fizetes[i];
                }
            }
            Console.WriteLine("Informatikai dolgozók összes bére: " + bér + "\n");
            
            Console.WriteLine("3. Feladat");            
            DateTime legregebbi = kezdes[0];
            for (int i = 1; i < adatsorok_szama; i++)
            {
                if (legregebbi > kezdes[i] && vege[i] != default(DateTime)) 
                {
                    legregebbi = kezdes[i];
                }
            }

            Console.WriteLine(legregebbi.Date.ToString("yyyy-MM-dd") + "\n");

            Console.WriteLine("4. Feladat");
            for (int i = 0; i < adatsorok_szama; i++)
            {   
                if (poszt[i] == "Adatbázis Adminisztrátor")
                {
                    Console.WriteLine(nev[i]);
                }
            }
            Console.WriteLine("\n");

            Console.WriteLine("5. Feladat");
            const string keresettrészleg = "Marketing";
            Console.WriteLine("A " + keresettrészleg + " alkalmazottainak száma: " + Megszámol("Marketing",reszleg) + "\n");

            
            Console.WriteLine("6. Feladat");
            
            Console.Write("részleg1: ");
            string részleg1 = Console.ReadLine();
            
            Console.Write("részleg2: ");
            string részleg2 = Console.ReadLine();

            int összeg1 = 0;
            int összeg2 = 0;
            for (int i = 0; i < adatsorok_szama; i++)
            {
                if (reszleg[i] == részleg1)
                {
                    összeg1 += fizetes[i];
                }
                if (reszleg[i] == részleg2)
                {
                    összeg2 += fizetes[i];
                }
            }

            if (összeg1 > összeg2)
            {
                Console.WriteLine("Az 1. részleg keresete nagyobb");
            }
            else if(összeg1 < összeg2)
            {
                Console.WriteLine("A 2. részleg keresete nagyobb");
            }
            else
            {
                Console.WriteLine("Egyenlő");
            }
            
            Console.WriteLine("\n7. Feladat");            
            for (int i = 0; i < adatsorok_szama; i++)
            {
                if (vege[i] != default(DateTime))    //feltétel, hogy már nem dolgozik
                {
                    Console.WriteLine(nev[i] + " " + (vege[i] - kezdes[i]).Days);
                }
            }

            Console.WriteLine("\n8. Feladat");
            
            Console.Write("határdátum: ");            

            DateTime határdátum = new DateTime();
            if (DateTime.TryParse(Console.ReadLine(), out határdátum))
            {
                bool volt_találat = false;                
                for (int i = 0; i < adatsorok_szama; i++)
                {
                    if (határdátum > kezdes[i])
                    {
                        Console.WriteLine(nev[i]);
                        volt_találat = true;
                    }
                }
                if (!volt_találat)
                {
                    Console.WriteLine("Nincs ilyen!");
                }
            }
            
            Console.WriteLine("\n9. Feladat");
            string név;
            FileStream fsr2 = new FileStream(@"azonosak.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fsr2);

            for (int i = 0; i < adatsorok_szama - 1; i++)
            {
                név = nev[i];
                for (int j = i + 1; j < adatsorok_szama; j++)
                {
                    if (nev[i] == nev[j])
                    {
                        sw.WriteLine(nev[i]);
                    }
                }
            }

            sw.Close();
            fsr2.Close();

            Console.WriteLine("A fájl kiíratása megtörtént!");

        }

        static int Megszámol(string keresettrészleg, List<string>részlegek)
        {
            int számláló = 0;
            for (int i = 0; i < részlegek.Count; i++)
            {
                if (keresettrészleg == részlegek[i])
                {
                    számláló++;
                }
            }
            return számláló;
        }       
    }
}