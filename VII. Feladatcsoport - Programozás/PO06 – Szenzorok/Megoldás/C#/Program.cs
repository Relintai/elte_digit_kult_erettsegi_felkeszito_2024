//1. feladat
using System;
using System.Diagnostics.CodeAnalysis;
using szenzorok;

Console.WriteLine("1. feladat");
FileStream fs = new FileStream(@"mérések.txt", FileMode.Open, FileAccess.Read);
StreamReader sr = new StreamReader(fs);

List<string> szenzoradatok = new List<string>();

while (!sr.EndOfStream)
{
    szenzoradatok.Add(sr.ReadLine());
}
sr.Close();
fs.Close();

Console.WriteLine("Mérések száma: " + szenzoradatok.Count);

Console.WriteLine("\n2. feladat");

List<szenzor> szenzorstruktúra = new List<szenzor>(); 

szenzor s = new szenzor();

for (int i = 0; i < szenzoradatok.Count; i++)
{
    s.dátum = DateTime.Parse(szenzoradatok[i].Split(',')[0].ToString());
    s.kód = szenzoradatok[i].Split(',')[1];
    s.mérés = double.Parse(szenzoradatok[i].Split(',')[2].ToString());
    szenzorstruktúra.Add(s);
}

for (int i = 0; i < 5; i++)
{
    Console.WriteLine(szenzorstruktúra[i].dátum + ", " + szenzorstruktúra[i].kód + ", " + szenzorstruktúra[i].mérés);
}

Console.WriteLine("\n3. feladat");

Console.Write("Kérek egy méréséi értéket: ");

string bekér = Console.ReadLine();

double érték = double.Parse(bekér);

int ciklus = 0;

while (ciklus <= szenzorstruktúra.Count - 1 && érték != szenzorstruktúra[ciklus].mérés)
{
    ciklus++;
}

if (ciklus <= szenzorstruktúra.Count - 1)
{
    Console.WriteLine("Szenzor kódja: " + szenzorstruktúra[ciklus].kód);
}
else
{
    Console.WriteLine("Nincs ilyen érték!");
}

Console.WriteLine("\n4. feladat");

List<double> s001 = new List<double>();

int ciklus2 = 0;
const string szenzorID = "S001";

while (ciklus2 != szenzorstruktúra.Count)
{
    if (szenzorID == szenzorstruktúra[ciklus2].kód)
    {
        s001.Add(szenzorstruktúra[ciklus2].mérés);
    }
    ciklus2++;
}

double min = s001[0];
int darab = 1;

for (int i = 1; i < s001.Count; i++)
{
    if (s001[i] == min)
    {
        darab++;
    }
    if (s001[i] < min)
    {
        min = s001[i];
        darab = 1;
    }
}

Console.WriteLine("S001 minimális értéke: " + min);
Console.WriteLine("Minimális értékek száma: " + darab);

Console.WriteLine("\n5. feladat");

const string szID = "S002";
double szum = 0;
int mennyi = 0;

for (int i = 0; i < szenzorstruktúra.Count; i++)
{
    if (szenzorstruktúra[i].kód == szID)
    {
        szum += szenzorstruktúra[i].mérés;
        mennyi++;
    }
}

double átlag = szum / mennyi;


Console.WriteLine("S002 szenzor méréseinek átlaga: " + Math.Round(átlag,2));


Console.WriteLine("\n6. feladat");

const string szenzorID3 = "S003";
List<double> s003érték = new List<double>();
List<DateTime> s003dátum = new List<DateTime>();

for (int i = 0; i < szenzorstruktúra.Count; i++)
{
    if (szenzorID3 == szenzorstruktúra[i].kód)
    {
        s003érték.Add(szenzorstruktúra[i].mérés);
        s003dátum.Add(szenzorstruktúra[i].dátum);
    }
}

double különbség = Math.Abs(s003érték[1] - s003érték[0]);
DateTime dátum01 = szenzorstruktúra[0].dátum;
DateTime dátum02 = szenzorstruktúra[1].dátum;

for (int i = 2; i < s003érték.Count; i++)
{
    if (különbség < Math.Abs(s003érték[i] - s003érték[i - 1]))
    {
        különbség = Math.Abs(s003érték[i] - s003érték[i - 1]);
        dátum01 = s003dátum[i - 1];
        dátum02 = s003dátum[i];
    }
}

Console.WriteLine(különbség.ToString());

Console.WriteLine("Maximális különbség két egymást követő mérésnél: " + dátum01 + " " + dátum02);

Console.WriteLine("\n7. feladat");

//Teszt
listáz(17.3, 19);
void listáz(double alsóhatár, double felsőhatár)
{
    for (int i = 0; i < szenzorstruktúra.Count; i++)
    {
        if (alsóhatár < szenzorstruktúra[i].mérés && szenzorstruktúra[i].mérés < felsőhatár)
        {
            Console.WriteLine(szenzorstruktúra[i].mérés);
        }
    }
}

Console.WriteLine("\n8. feladat");

//Teszt
Console.WriteLine(száztízszázalék(szenzorstruktúra[0].mérés, szenzorstruktúra[1].mérés));

string száztízszázalék(double mérés1, double mérés2)
{
    if (mérés1 > mérés2 * 1.1)
    {
        return "Az első mérés nagyobb a második mérés 110%-nál";
    }
    if (mérés1 < mérés2 * 1.1)
    {
        return "Az első mérés kisebb a második mérés 110%-nál";
    }
    
    return "Az első mérés egyenlő a második mérés 110%-val";
    
}

Console.WriteLine("\n9. feladat");

double fahrenheit = 0.0;

FileStream fs2 = new FileStream(@"fahrenheit.txt", FileMode.Create);
StreamWriter sw2 = new StreamWriter(fs2);

for (int i = 0; i < szenzorstruktúra.Count; i++)
{
    //([°C]×9 / 5)+32 = [°F]
    fahrenheit = (szenzorstruktúra[i].mérés * 9 / 5) + 32;
    sw2.WriteLine(fahrenheit);
}

sw2.Close();
fs2.Close();

Console.WriteLine("A fájl kiírása megtörtént!");
