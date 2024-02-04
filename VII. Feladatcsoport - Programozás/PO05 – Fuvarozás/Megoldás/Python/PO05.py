
from datetime import datetime
from tkinter import N

class Fuvar:
    def __init__(self, Megnevezes, Tavolsag, Uzemanyag, Suly, Honap, Nap):
        self.Megnevezes = Megnevezes
        self.Tavolsag = Tavolsag
        self.Uzemanyag = Uzemanyag
        self.Suly = Suly
        self.Honap = Honap
        self.Nap = Nap
    
    @property
    def fogyasztas(self):
        return (self.Uzemanyag / self.Tavolsag)*100.0

fuvarok = []
f = open("Adatok.csv", "r")
for tSor in f:
    sor = tSor.split(';')
    fuvarok.append(Fuvar(sor[0],int(sor[1]),float(sor[2]),int(sor[3]),int(sor[4]),int(sor[5])))
f.close()

def AdatokJarmurol(nev):
    bejegyzesek = 0
    tavolsagOsszeg = 0
    fogyasztasOsszeg = 0.0
    sulyOsszeg = 0

    for f in fuvarok:
        if f.Megnevezes == nev:
            bejegyzesek+=1
            tavolsagOsszeg += f.Tavolsag
            fogyasztasOsszeg += f.fogyasztas
            sulyOsszeg += f.Suly

    return "Teljes megtett tavolsag: " + str(tavolsagOsszeg) + " km, Atlagfogyasztas: " + str(fogyasztasOsszeg / bejegyzesek) + " l/100 km, Osszesitett suly:  " + str(sulyOsszeg) + " kg"

def Legtobb(honaptol,naptol, honapig, napig):
    nevFuvar = dict()

    for f in fuvarok:
        if f.Honap >= honaptol and f.Honap <= honapig and (f.Honap != honaptol or f.Nap >= naptol) and (f.Honap != honapig or f.Nap <= napig):
            if f.Megnevezes not in nevFuvar.keys():
                nevFuvar[f.Megnevezes] = 1
            else:
                nevFuvar[f.Megnevezes] += 1

    vissza = "Nem tortent fuvar"
    if len(nevFuvar.keys()) > 0:
        maximum=0
        for i in nevFuvar.keys():
            if nevFuvar[i] > maximum:
                maximum = nevFuvar[i]
                vissza = i

    return vissza

def Nagyobb(suly):
    for f in fuvarok:
        if f.Suly > suly:
            print(str(f.Honap) + "." + str(f.Nap) + " " + f.Megnevezes)


def Osszes(nev, honaptol,naptol, honapig, napig):
    tavolsag = 0

    for f in fuvarok:
        if f.Megnevezes == nev and f.Honap >= honaptol and f.Honap <= honapig and (f.Honap != honaptol or f.Nap >= naptol) and (f.Honap != honapig or f.Nap <= napig):
            tavolsag += f.Tavolsag

    return tavolsag

def Magas(fogyasztas):
    kiirtak = []

    for f in fuvarok:
        if f.fogyasztas > fogyasztas:
            if f.Megnevezes not in kiirtak:
                print(f.Megnevezes)
                kiirtak.append(f.Megnevezes)


maximumIndex=0
for i in range(1,len(fuvarok)):
    #print(fuvarok[i].fogyasztas)
    if fuvarok[maximumIndex].fogyasztas < fuvarok[i].fogyasztas:
        maximumIndex = i

print("A legtobbet fogyasztott jarmu neve: " + fuvarok[maximumIndex].Megnevezes + ", fogyasztasa: " + str(fuvarok[maximumIndex].fogyasztas) + ", ekkor " + str(fuvarok[maximumIndex].Suly) + " kg sulyt szalitott." )


osszesNev = []
for f in fuvarok:
    if f.Megnevezes not in osszesNev:
        osszesNev.append(f.Megnevezes)

kimenet = []
kimenet.append(str(datetime.today().year) + "." + str(datetime.today().month) + "." + str(datetime.today().day) + "\n")
for n in osszesNev:
    kimenet.append(n + ": " + AdatokJarmurol(n) + "\n")

f = open("export.txt", "w")
f.writelines(kimenet)
f.close()

print(Legtobb(5,17,7,14))
Nagyobb(200)
print(Osszes("Teher 2.", 5,17,7,21))
Magas(15.1)