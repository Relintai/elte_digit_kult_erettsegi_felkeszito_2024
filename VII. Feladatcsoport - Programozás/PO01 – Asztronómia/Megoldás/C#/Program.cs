//1. feladat
using System.Text;

FileStream fs01 = new FileStream(@"égitest.txt", FileMode.Open, FileAccess.Read);
StreamReader sr = new StreamReader(fs01);

List<string> data = new List<string>();

while (!sr.EndOfStream)
{
    data.Add(sr.ReadLine());
}

sr.Close();
fs01.Close();

int num_of_lines = data.Count;

List<string> names = new List<string>();
List<string> types = new List<string>();
List<double> masses = new List<double>();
List<double> radiuses = new List<double>();
List<int> ages = new List<int>();
List<int> moons = new List<int>();

for (int i = 0; i < num_of_lines; i++)
{
    names.Add(data[i].Split(";")[0]);
    types.Add(data[i].Split(";")[1]);
    masses.Add(double.Parse(data[i].Split(";")[2]));
    radiuses.Add(double.Parse(data[i].Split(";")[3]));
    ages.Add(int.Parse(data[i].Split(";")[4]));
    moons.Add(int.Parse(data[i].Split(";")[5]));
}

//2. feladat
int counter = 0;
for (int i = 0; i < num_of_lines; i++)
{
    if (types[i].Equals("Exobolygó"))
    {
        counter++;
    }
}

Console.WriteLine("Az Exobolygók száma: {0}", counter);

//3. feladat

double max = masses[0];
double nums_of_max = 1;
List<string> hits = new List<string>();
hits.Add(names[0]);

for (int i = 1; i < num_of_lines; i++)
{
    if (max == masses[i])
    {
        nums_of_max++;
        hits.Add(names[i]);
    }

    if (max < masses[i])
    {
        max = masses[i];
        nums_of_max = 1;
        hits.Clear();
        hits.Add(names[i]);
    }    
}

Console.Write("\nA legnagyobb tömegű égitestek száma: {0}, amely(ek): ", nums_of_max);
for (int i = 0; i < hits.Count; i++)
{
    Console.Write(hits[i] + ", ");
}


//4. feladat
int Keres(string name)
{
    int age;
    int i = 0;

    while (i < num_of_lines && name != names[i])
    {
        i++;
    }

    return ages[i];
}

//5. feladat

Random rand = new Random();

int chosen = rand.Next(0, num_of_lines-1);

Console.WriteLine("\n\nTippelje meg a {0} korát!", names[chosen]);

int tip = -1;

while (tip != ages[chosen])
{
    tip = int.Parse(Console.ReadLine());

    if (Keres(names[chosen]) > tip )
    {
        Console.WriteLine("Idősebb!");
    }
    if (Keres(names[chosen]) < tip)
    {
        Console.WriteLine("Fiatalabb!");
    }
    if (Keres(names[chosen]) == tip)
    {
        Console.WriteLine("Eltalálta!");
    }
}

//6. feladat

double minradius = radiuses[0];
double maxradius = radiuses[0];
int minindex = 0;
int maxindex = 0;

int index;
for (index = 1; index < num_of_lines; index++)
{
    if (minradius > radiuses[index])
    {
        minradius = radiuses[index];
        minindex = index;
    }
    if (maxradius < radiuses[index])
    {
        maxradius = radiuses[index];
        maxindex = index;
    }
}

Console.WriteLine("\nA(z) {0} és a(z) {1} között legnagyobb a méretkülönbség, értéke: {2}", names[minindex], names[maxindex], maxradius-minradius);

//7. feladat

string older(string name01, string name02)
{
    string return_value = "";

    if (Keres(name01) > Keres(name02))
    {
        return_value = "az 1. égitest idősebb!";
    }

    else if (Keres(name01) < Keres(name02))
    {
        return_value = "a 2. égitest idősebb";
    }

    else
    {
        return_value = "a két égitest kora megyegyezik!";
    }

    return return_value;
}

int randnum01 = rand.Next(0, num_of_lines - 1);
int randnum02 = rand.Next(0, num_of_lines - 1);

while (randnum01 == randnum02)
{
    randnum02 = rand.Next(0, num_of_lines - 1);
}

Console.WriteLine("\nA(z) {0} és a(z) {1} égitest korának viszonya: {2}",names[randnum01], names[randnum02],older(names[randnum01], names[randnum02]));

//8. és 9. feladat
Console.Write("\nKérem adja meg a holdak számának határát:");

int limit = int.Parse(Console.ReadLine());

List<string> results = new List<string>();

FileStream fs02 = new FileStream("holdak.txt", FileMode.Create, FileAccess.Write);
StreamWriter sw = new StreamWriter(fs02);

sw.WriteLine(limit);

for (int i = 0; i < num_of_lines; i++)
{
    if (moons[i] >= limit)
    {
        results.Add(names[i]);
    }
}

if (results.Count != 0)
{
    foreach (var item in results)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(item + ", ");
        Console.Write(sb);
        sw.Write(sb);
    }
}
else
{
    Console.WriteLine("Nincs ilyen égitest!");
    sw.WriteLine("Nincs ilyen égitest!");
}

sw.Close();
fs02.Close();