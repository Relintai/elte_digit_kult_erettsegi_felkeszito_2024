from datetime import datetime
import time

ADAT_INDEX_INDEX = 0
ADAT_INDEX_CIM = 1
ADAT_INDEX_SZERZO = 2
ADAT_INDEX_KATEGORIA = 3
ADAT_INDEX_TIPUS = 4
ADAT_INDEX_DB = 5
ADAT_INDEX_UTOLSOELADAS = 6
ADAT_INDEX_MERET = 7

adatok = list()

f = open("konyvek.csv", "r")
lines = f.readlines()
f.close()

for i in range(1, len(lines)):
    l = lines[i]

    l = l.replace("\r", "").replace("\n", "")
    s = l.split(",")

    if len(s) != 7:
        continue

    ad = dict()

    ad[ADAT_INDEX_INDEX] = int(s[0])
    ad[ADAT_INDEX_CIM] = s[1]
    ad[ADAT_INDEX_SZERZO] = s[2]
    ad[ADAT_INDEX_KATEGORIA] = s[3]
    ad[ADAT_INDEX_TIPUS] = s[4]
    ad[ADAT_INDEX_DB] = int(s[5])
    ad[ADAT_INDEX_UTOLSOELADAS] = datetime.strptime(s[6], "%Y-%m-%d")

    adatok.append(ad)

ad = None

print("1. Feladat")

print("A ténylegesen beolvasott adatsorok száma: " + str(len(adatok)))

print("")
print("2. Feladat")

regeny_szam = 0

for k in adatok:
    if k[ADAT_INDEX_TIPUS] == "Regény" and k[ADAT_INDEX_DB] > 0:
        regeny_szam += 1


print("Összesen " + str(regeny_szam) + " különböző regény van raktáron.")

print("")
print("3. Feladat")

legregebbi_konyv_index = None
utolso_datum = None

for i in range(len(adatok)):
    k = adatok[i]

    if k[ADAT_INDEX_DB] == 0:
        continue

    uelad = k[ADAT_INDEX_UTOLSOELADAS]

    if utolso_datum is None:
        utolso_datum = uelad
        legregebbi_konyv_index = i
        continue

    if uelad < utolso_datum:
        utolso_datum = uelad
        legregebbi_konyv_index = i

if legregebbi_konyv_index is not None:
    print("A legrégebben eladott könyv címe: " + adatok[legregebbi_konyv_index][ADAT_INDEX_CIM] + ", szerzője: " + adatok[legregebbi_konyv_index][ADAT_INDEX_SZERZO] + ".")
else:
    print("Nem volt találat!")


print("")
print("4. Feladat")

print("A raktáron levő programozásról szóló könyvek:")

szam = 0

for k in adatok:
    if k[ADAT_INDEX_TIPUS] == "Programozás" and k[ADAT_INDEX_DB] > 0:
        print(k[ADAT_INDEX_CIM])
        szam += 1

if szam == 0:
    print("Egy ilyen sincs.")

print("")
print("5. Feladat")

def megszamol(keresett_kategoria, adatok):
    szam = 0

    for k in adatok:
        if k[ADAT_INDEX_KATEGORIA] == keresett_kategoria:
            szam += k[ADAT_INDEX_DB]

    return szam

szepirodalom_szam = megszamol("Szépirodalom", adatok)

print("A Szépirodalom kategóriában " + str(szepirodalom_szam) + " db könyv van raktáron.")

print("")
print("6. Feladat")

print('kategória1:')
kategoria1 = input()

print('kategória2:')
kategoria2 = input()

kategoria1_szam = megszamol(kategoria1, adatok)
kategoria2_szam = megszamol(kategoria2, adatok)

if kategoria1_szam > kategoria2_szam:
    print("Az 1.kategóriában van több könyv a raktáron")
elif kategoria1_szam < kategoria2_szam:
    print("A 2.kategóriában van több könyv a raktáron")
else:
    print("Egyenlő")

print("")
print("7. Feladat")

for k in adatok:
    if k[ADAT_INDEX_DB] == 0:
        delta = datetime.today() - k[ADAT_INDEX_UTOLSOELADAS]

        print(k[ADAT_INDEX_SZERZO] + ": " + k[ADAT_INDEX_CIM] + ". " + str(delta.days))

print("")
print("8. Feladat")

print('határdátum (EV-HONAP-NAP):')
hd = input()


try:
    hatardatum = datetime.strptime(hd, "%Y-%m-%d")

    szam = 0

    for k in adatok:
        if k[ADAT_INDEX_DB] == 0 and k[ADAT_INDEX_UTOLSOELADAS] < hatardatum:
            print(k[ADAT_INDEX_SZERZO] + ": " + k[ADAT_INDEX_CIM])
            szam += 1

    if szam == 0:
        print("Nincs ilyen!")

except:
    print("A dátum formátuma helytelen!")

print("")
print("9. Feladat")

f = open("hiany.txt", "w")

for k in adatok:
    if k[ADAT_INDEX_DB] == 0:
        f.write(k[ADAT_INDEX_CIM] + "\n")

f.close()

print("A fájl kiíratása megtörtént!")

print("")

