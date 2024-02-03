
//1. Feladat
using System.Runtime.Serialization;

Console.WriteLine("1. Feladat");
FileStream fs = new FileStream(@"bitek.txt", FileMode.Open, FileAccess.Read);
StreamReader sr = new StreamReader(fs);

int n = 32;
int m = 32;

int[,] bitek = new int[n,m];
string sorolvas;

for (int i = 0; i < n; i++)
{
    sorolvas = sr.ReadLine();    
    for (int j = 0; j < m; j++)
    {
        bitek[i, j] = int.Parse(sorolvas[j].ToString());
    }
}

sr.Close();
fs.Close();

/*
for (int i = 0;i < n; i++)
{
    for (int j = 0; j < m; j++)
    {
        Console.Write(bitek[i, j]);
    }
    Console.WriteLine();
}
*/

Console.WriteLine("A fájl beolvasása megtörtént!");

//2. Feladat
Console.WriteLine("\n2. Feladat");
int oszloposszeg;
int feltetel = 40;
bool volt = false;

for (int i = 0; i < m; i++)
{
    oszloposszeg = 0;
    for (int j = 0; j < n; j++)
    {
        oszloposszeg += bitek[j, i];
    }
    
    if (oszloposszeg > feltetel)
    {
        Console.WriteLine((i + 1) + ". oszlop");
        volt = true;
    }    
}

if (!volt)
{
    Console.WriteLine("Nincs 40 feletti oszlopösszeg");
}

//3. Feladat
Console.WriteLine("\n3. Feladat");

int max = 0;
int[] soregyesosszeg = new int[n];

for (int i = 0; i < n; i++)
{
    for (int j = 0; j < m; j++)
    {
        if (bitek[i,j] == 1)
        {
            soregyesosszeg[i] += bitek[i, j];
        }
    }

    if (max < soregyesosszeg[i])
    {
        max = soregyesosszeg[i];
    }
}

for (int i = 0; i < n; i++)
{
    if (soregyesosszeg[i] == max)
    {
        Console.WriteLine((i + 1) + ". sor");
    }
}

//4. Feladat
Console.WriteLine("\n4. Feladat");

int nullak = 0;
int egyesek = 0;

for (int i = 0; i < n; i++)
{
    for (int j = 0; j < m; j++)
    {
        if (bitek[i,j] == 0)
        {
            nullak += 1;
        }
        else
        {
            egyesek += 1;
        }
    }
}

double arany = double.Parse(nullak.ToString()) / egyesek;

Console.WriteLine("Arány: " + arany);

//5. Feladat
Console.WriteLine("\n5. Feladat");

//teszt
int[] teszt = new int[m];
const int sor = 6;
for (int i = 0; i < m; i++)
{
    teszt[i] = bitek[sor, i];
}

Console.WriteLine("Egyesek száma a {0}. sorban: {1}", sor + 1, megszámol(teszt));

int megszámol(int[] tomb)
{
    int sum = 0;
    for (int i = 0; i < tomb.Length; i++)
    {
        if (tomb[i] == 1)
        {
            sum += 1;
        }
    }
    return sum;
}


//6. Feladat
Console.WriteLine("\n6. Feladat");
bool paritas (int[] bitsorozat)
{
    if (megszámol(bitsorozat) % 2 == 0)
        return true;
    else
        return false;
}

//Teszt
if (paritas(teszt))
{
    Console.WriteLine("A {0}. sor egyeseinek a száma: páros", sor + 1);
}
else
    Console.WriteLine("A {0}. sor egyeseinek a száma: páratlan", sor + 1);


//7. Feladat
Console.WriteLine("\n7. Feladat");

int[] bitsorozat = new int[m];

Console.WriteLine("Paritásbitek sorok szerint:");

for (int i = 0; i < n; i++)
{
    for (int j = 0; j < m; j++)
    {
        bitsorozat[j] = bitek[i, j];
    }
    if (paritas(bitsorozat))
        Console.WriteLine("0");
    else
        Console.WriteLine("1");
}

//8. Feladat
Console.WriteLine("\n8. Feladat");

int[] keresendominta = {1,0,0,1};

//Teszt
const int tesztsor = 31;

if (tartalmazza(keresendominta))
{
    Console.WriteLine("A {0}. sor tartalmazza a mintát!", tesztsor);
}
else
    Console.WriteLine("A {0}. sor nem tartalmazza a mintát!", tesztsor);

bool tartalmazza(int[] minta)
{
    for (int i = 0; i < m - 3; i++)
    {
        if (minta[0] == bitek[tesztsor, i] && minta[1] == bitek[tesztsor, i + 1] &&
            minta[2] == bitek[tesztsor, i + 2] && minta[3] == bitek[tesztsor, i + 3])
        {
            return true;
        }
    }

    return false;    
}



//9. Feladat
Console.WriteLine("\n9. Feladat");

int[,] inverzbitek = new int[n, m];

for (int i = 0; i < n; i++)
{
    for (int j = 0; j < m; j++)
    {
        if (bitek[i,j] == 1)
        {
            inverzbitek[i, j] = 0;
        }
        else
            inverzbitek[i, j] = 1;
    }

}

FileStream fs2 = new FileStream(@"újbitek.txt", FileMode.Create);
StreamWriter sw2 = new StreamWriter(fs2);

for (int i = 0; i < n; i++)
{
    for (int j = 0; j < m; j++)
    {
        sw2.Write(inverzbitek[i,j]);
    }
    sw2.WriteLine();
}

sw2.Close();
fs2.Close();

Console.WriteLine("A fájl kiíratása megtörtént!");
