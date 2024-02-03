
from datetime import datetime

f = open("személyek.txt","r",encoding="utf8")
sorok = f.readlines()
f.close()

ADATSOROK_SZÁMA = len(sorok) - 1   #van fejléc

nev = list()
poszt = list()
reszleg = list()
fizetes = list()
kezdes = list()
vege = list()

for i in range(1,ADATSOROK_SZÁMA + 1):
    l = sorok[i].replace('\n','')
    s = l.split(",")
    nev.append(s[0])
    poszt.append(s[1])
    reszleg.append(s[2])
    fizetes.append(s[3])
    kezdes.append(s[4])
    vege.append(s[5])

print("1. Feladat")
print("A beolvasott adatsorok száma: ", ADATSOROK_SZÁMA, "\n")

print("2. Feladat")         
bér = 0
for i in range(0,ADATSOROK_SZÁMA):
    if reszleg[i] == "Informatika":
        bér += int(fizetes[i])

print("Informatikai dolgozók összes bére: ",bér, "\n")

print("3. Feladat")            
legregebbi = kezdes[0]
for i in range(1, ADATSOROK_SZÁMA):
    if legregebbi > kezdes[i]:    
        legregebbi = kezdes[i]
    
print(legregebbi)

print("\n4. Feladat")
for i in range(0,ADATSOROK_SZÁMA):   
    if poszt[i] == "Adatbázis Adminisztrátor":
        print(nev[i])
print("\n")


print("5. Feladat")

def Megszámol(keresettrészleg, részlegek):
    számláló = 0
    for i in range(0,len(részlegek)):
        if keresettrészleg == részlegek[i]:
            számláló = számláló + 1
    
    return számláló
       

#Teszt
KERESETTRÉSZLEG = "Marketing"
print("A ", KERESETTRÉSZLEG, " alkalmazottainak száma: ", Megszámol("Marketing",reszleg), "\n")


print("6. Feladat")

print("részleg1: ")
részleg1 = input()
print("részleg2: ")
részleg2 = input()
összeg1 = 0
összeg2 = 0

for i in range(0,ADATSOROK_SZÁMA):
    if reszleg[i] == részleg1:    
        összeg1 += int(fizetes[i])    
    if reszleg[i] == részleg2:
        összeg2 += int(fizetes[i])

if összeg1 > összeg2:
    print("Az 1. részleg keresete nagyobb")
elif összeg1 < összeg2:
    print("A 2. részleg keresete nagyobb")
else:
    print("Egyenlő")

print("\n7. Feladat")            
for i in range(0,ADATSOROK_SZÁMA):
    if (vege[i] != ""):    #feltétel, hogy már nem dolgozik
        d1 = datetime.date(datetime.strptime(vege[i],'%Y-%m-%d'))
        d2 = datetime.date(datetime.strptime(kezdes[i],'%Y-%m-%d'))

        print(nev[i]," ",  (d1 - d2).days)
        

print("\n8. Feladat")

print("határdátum: ")
határdátum = input()
volt_találat = False
for i in range(0,ADATSOROK_SZÁMA):
    if határdátum > kezdes[i]:    
        print(nev[i])
        volt_találat = True       
    
if not volt_találat:    
    print("Nincs ilyen!")

print("\n9. Feladat")

f = open("azonosak.txt","w")

for i in range(0,ADATSOROK_SZÁMA - 1):
    név = nev[i]
    for j in range(i+1,ADATSOROK_SZÁMA):    
        if nev[i] == nev[j]:        
            f.writelines(nev[i])
            f.writelines("\n")

f.close()

print("A fájl kiíratása megtörtént!")
