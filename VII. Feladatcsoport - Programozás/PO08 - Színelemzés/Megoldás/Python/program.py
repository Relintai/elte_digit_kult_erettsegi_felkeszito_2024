
class Felmeres:
    # int
    ora = 0
    # int 
    perc = 0
    # string 
    szin = ""

    def __init__(self, Ora, Perc, Szin):
        self.ora = Ora
        self.perc = Perc
        self.szin = Szin

def beolvas(filename):
    adatok = []

    fl = open(filename, "r")
    data = fl.readlines()
    fl.close()

    for i in range(1, len(data)):
        item = data[i]

        item = item.replace("\r", "").replace("\n", "")
        sor = item.split(";")

        f = Felmeres(int(sor[0]), int(sor[1]), sor[2])

        adatok.append(f)

    return adatok
        

def feladat_2(felmeresek):
    print("2. Feladat\n\t" + str(len(felmeresek)) + " mérés történt!")

def feladat_3(felmeresek, szinek):
    print("3. Feladat")
    #Barna, Bézs, Fehér, Fekete, Kék, Lila, Narancs, Piros, Rózsaszín, Sárga, Szürke, Zöld. -> 12
    darabok = []

    for i in range(12):
        darabok.append(0)

    for item in felmeresek:
        for i in range(len(szinek)):
            if (item.szin == szinek[i]):
                darabok[i] += 1
            
    maxe = 0
    maxi = 0
    for i in range(len(darabok)):
        if (darabok[i] > maxe):
            maxe = darabok[i]
            maxi = i
        
    print("\tA legtöbbször mért szín a " + str(szinek[maxi]) + " volt és " + str(maxe) + " előfordulása volt.")

def feladat_4(felmeresek, szinek):
    print("4. Feladat: Kérem a vizsgált órát:")

    ora = int(input())

    for item in felmeresek:
        if item.ora == ora:
            print("\t" + str(item.ora) + ":" + str(item.perc) + " " + item.szin)
        
def feladat_5(felmeresek, szinek):
    print("5. Feladat")

    darabok = []

    for i in range(12):
        darabok.append(0)

    for item in felmeresek:
        for i in range(len(szinek)):
            if item.szin == szinek[i]:
                darabok[i] += 1

    for i in range(len(szinek)):
        print("\t" + str(szinek[i]) + ": " + str(darabok[i]))

def autoszam(ora, felmeresek):
    db = 0

    for item in felmeresek:
        if item.ora == ora:
            db += 1
        
        if item.ora > ora:
            break

    return db


def feladat_7(felmeresek):
    orak = dict()

    for item in felmeresek:
        orak[item.ora] = True

    with open("autoathaladasok.txt", "w") as f:
        for ora in orak.keys():
            f.write(str(ora) + ";" + str(autoszam(ora, felmeresek)) + "\n")

felmeresek = beolvas("meresek.txt")
szinek = [ "Barna", "Bézs", "Fehér", "Fekete", "Kék", "Lila", "Narancs", "Piros", "Rózsaszín", "Sárga", "Szürke", "Zöld" ]

feladat_2(felmeresek)
feladat_3(felmeresek, szinek)
feladat_4(felmeresek, szinek)
feladat_5(felmeresek, szinek)
feladat_7(felmeresek)

