

from datetime import datetime
import time

adatok = list()

f = open("filmek.csv", "r")
lines = f.readlines()
f.close()

for i in range(1, len(lines)):
    l = lines[i]

    l = l.replace("\r", "").replace("\n", "")
    s = l.split(",")

    if len(s) != 6:
        continue

    ad = dict()

    ad["FILMNEV"] = s[0]
    ad["MUFAJ"] = s[1].split("|")
    ad["RENDEZO"] = s[2].split("|")

    ad["HOSSZ_H"] = int(s[3].split(" ")[0].replace("h", ""))
    ad["HOSSZ_M"] = int(s[3].split(" ")[1].replace("m", ""))

    ad["PONTOK"] = int(s[4])
    ad["MEGJELENES"] = int(s[5])

    adatok.append(ad)

ad = None

print("1. Feladat")

print("A ténylegesen beolvasott adatsorok száma: " + str(len(adatok)))

print("")
print("2. Feladat")

legjobb_index = 0

for i in range(1, len(adatok)):
    if adatok[i]["PONTOK"] > adatok[legjobb_index]["PONTOK"]:
        legjobb_index = i

print(adatok[legjobb_index]["FILMNEV"])

print("")
print("3. Feladat")

for f in adatok:
    if f["HOSSZ_H"] < 2:
        p = f["FILMNEV"]

        for r in f["RENDEZO"]:
            p += ", "
            p += r

        print(p)


print("")
print("4. Feladat")

szam = 0

for f in adatok:
    if f["MEGJELENES"] == 1994:
        print(f["FILMNEV"])
        szam += 1

if szam == 0:
    print("Egy ilyen sincs.")

print("")
print("5. Feladat")

def mufajszamol(keresett_mufaj, adatok):
    szam = 0

    for k in adatok:
        for m in k["MUFAJ"]:
            if m == keresett_mufaj:
                szam += 1
                break

    return szam

drama_szam = mufajszamol("Dráma", adatok)

print("A Dráma műfajban " + str(drama_szam) + " db film van.")

print("")
print("6. Feladat")

print('műfaj:')
mufaj = input()

mufaj_szam = mufajszamol(mufaj, adatok)

print("A " + mufaj + " műfajban " + str(mufaj_szam) + " db film van.")

print("")
print("7. Feladat")

print('műfaj:')
mufaj = input()

hossz_h = 0
hossz_m = 0

for k in adatok:
    for m in k["MUFAJ"]:
        if m == mufaj:
            hossz_h += k["HOSSZ_H"]
            hossz_m += k["HOSSZ_M"]
            break

hossz_mc = int(hossz_m / 60)

hossz_h += hossz_mc
hossz_m -= hossz_mc * 60

print(str(hossz_h) + "h " + str(hossz_m) + "m")

print("")
print("8. Feladat")

legrovidebb_index = 0
leghosszabb_index = 0
legrovidebb_hossz = adatok[0]["HOSSZ_H"] * 60 + adatok[0]["HOSSZ_M"]
leghosszabb_hossz = legrovidebb_hossz

for i in range(1, len(adatok)):
    a = adatok[i]

    hossz = a["HOSSZ_H"] * 60 + a["HOSSZ_M"]

    if hossz < legrovidebb_hossz:
        legrovidebb_index = i
        legrovidebb_hossz = hossz

    if hossz > leghosszabb_hossz:
        leghosszabb_index = i
        leghosszabb_hossz = hossz


print("Legrövidebb: " + adatok[legrovidebb_index]["FILMNEV"])
print("Leghosszabb: " + adatok[leghosszabb_index]["FILMNEV"])

print("")
print("9. Feladat")

index = 0
leghosszabb_kar_szam = len(adatok[index]["FILMNEV"])

for i in range(1, len(adatok)):
    a = adatok[i]

    kar_szam = len(a["FILMNEV"])

    if kar_szam > leghosszabb_kar_szam:
        index = i
        leghosszabb_kar_szam = kar_szam


print(adatok[index]["FILMNEV"])

print("")
print("10. Feladat")

evek = dict()

for f in adatok:
    ev = f["MEGJELENES"]

    if ev in evek:
        evek[ev] += 1
    else:
        evek[ev] = 1

legtobb_ev = 0
legtobb_ev_szam = 0

for k in evek.keys():
    if evek[k] > legtobb_ev_szam:
        legtobb_ev = k
        legtobb_ev_szam = evek[k]

print(str(legtobb_ev))

print("")
print("11. Feladat")

print('Határszám percben:')
hd = input()

try:
    hatarszam = int(hd)

    szam = 0

    for f in adatok:
        hossz = f["HOSSZ_H"] * 60 + f["HOSSZ_M"]

        if hossz < hatarszam:
            print(f["FILMNEV"])
            szam += 1

    if szam == 0:
        print("Nincs ilyen!")

except:
    print("A bemenet formátuma helytelen!")

print("")
print("12. Feladat")

f = open("tobbrendezo.txt", "w")

for film in adatok:
    if len(film["RENDEZO"]) > 1:
        f.write(film["FILMNEV"] + "\n")

f.close()

print("A fájl kiíratása megtörtént!")

print("")

