using System.Xml.Serialization;

namespace szinelemzes
{
    internal class Program
    {
        record Felmeres {
            public int ora;
            public int perc;
            public string szin;

            public Felmeres(int Ora, int Perc, string Szin) { 
                this.ora = Ora;
                this.perc = Perc;
                this.szin = Szin;
            }
        }
        static void beolvas(string filename)
        {
            try
            {
                string[] data = File.ReadAllLines(filename);
                foreach (string item in data.Skip(1))
                {
                    string[] sor = item.Split(';');
                    Felmeres f = new Felmeres(int.Parse(sor[0]), int.Parse(sor[1]), sor[2]);
                    felmeresek.Add(f);
                }
            } catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
        static void feladat_2() {
            Console.WriteLine("2. Feladat\n\t"+felmeresek.Count+" mérés történt!");
        }
        static void feladat_3()
        {
            Console.WriteLine("3. Feladat");
            //Barna, Bézs, Fehér, Fekete, Kék, Lila, Narancs, Piros, Rózsaszín, Sárga, Szürke, Zöld.
            int[] darabok = new int[12];
            foreach (Felmeres item in felmeresek) {
                for (int i = 0; i < szinek.Length; i++) {
                    if (item.szin == szinek[i])
                    {
                        darabok[i]++;
                    }
                }
            }
            int max = 0;
            int maxi = 0;
            for (int i = 0; i < darabok.Length; i++) {
                if (darabok[i] > max)
                {
                    max = darabok[i];
                    maxi = i;
                }
            }
            Console.WriteLine("\tA legtöbbször mért szín a " + szinek[maxi] +" volt és "+max+" előfordulása volt.");

        }
        static void feladat_4() {
            Console.WriteLine("4. Feladat: Kérem a vizsgált órát:");
            int.TryParse(Console.ReadLine(), out int ora);
            foreach (var item in felmeresek)
            {
                if (item.ora == ora)
                {
                    Console.WriteLine("\t"+item.ora+":"+item.perc+" "+item.szin);
                }
            }
        }
        static void feladat_5()
        {
            Console.WriteLine("5. Feladat");
            int[] darabok = new int[12];
            foreach (Felmeres item in felmeresek)
            {
                for (int i = 0; i < szinek.Length; i++)
                {
                    if (item.szin == szinek[i])
                    {
                        darabok[i]++;
                    }
                }
            }
            for (int i = 0; i < szinek.Length; i++)
            {
                Console.WriteLine("\t" + szinek[i] +": " + darabok[i]);
            }
        }
        static int autoszam(int ora) {
            int db = 0;
            foreach (var item in felmeresek)
            {
                if (item.ora == ora)
                {
                    db++;
                } if (item.ora > ora)
                {
                    break;
                }
            }
            return db;
        }
        static void feladat_7() {
            HashSet<int> orak = new HashSet<int>();
            foreach (var item in felmeresek)
            {
                orak.Add(item.ora);
            }
            
            using (StreamWriter outputFile = new StreamWriter("autoathaladasok.txt")) {
                foreach (var ora in orak)
                {
                    outputFile.WriteLine(ora + ";" + autoszam(ora));
                }
            }
        }
        static List<Felmeres> felmeresek = new List<Felmeres>();
        static String[] szinek = { "Barna", "Bézs", "Fehér", "Fekete", "Kék", "Lila", "Narancs", "Piros", "Rózsaszín", "Sárga", "Szürke", "Zöld" };
        static void Main(string[] args)
        {
            beolvas("meresek.txt");
            feladat_2();
            feladat_3();
            feladat_4();
            feladat_5();
            feladat_7();
        }
    }
}
