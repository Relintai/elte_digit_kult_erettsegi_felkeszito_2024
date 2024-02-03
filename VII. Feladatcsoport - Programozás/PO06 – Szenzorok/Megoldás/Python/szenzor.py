from datetime import datetime
from adat import szenzor


#1. feladat

print("1. feladat") 

f = open("mérések.txt", "r")
sorok = f.readlines()
f.close()

szenzoradatok = list()

for i in range(0, len(sorok)):
    l = sorok[i]    
    s = l.split(",")

    sor = dict()
    
    sor[0] = datetime.strptime(s[0], '%Y-%m-%d %H:%M:%S')
    sor[1] = s[1]
    sor[2] = s[2]

    szenzoradatok.append(sor)


print("Mérések száma: ", str(len(szenzoradatok))) 

print("\n2. feladat") 

szenzorstruktúra = list()

for i in range(0, len(szenzoradatok)):
    sz = szenzor(szenzoradatok[i][0], szenzoradatok[i][1], szenzoradatok[i][2])
    szenzorstruktúra.append(sz)

for i in range (0,5):
    print(szenzorstruktúra[i].dátum,", ",szenzorstruktúra[i].kód,", ",szenzorstruktúra[i].mérés, end = "")


print("\n3. feladat") 

print("Kérek egy méréséi értéket: ") 

bekér = input()

érték = float(bekér) 

ciklus = 0 

while ciklus <= len(szenzorstruktúra) - 1 and érték != float(szenzorstruktúra[ciklus].mérés) :
    ciklus = ciklus + 1


if ciklus <= len(szenzorstruktúra) - 1:
    print("Szenzor kódja: ", szenzorstruktúra[ciklus].kód) 
else:
    print("Nincs ilyen érték!") 


print("\n4. feladat") 

s001 = list() 

ciklus2 = 0 
SZENZORID = "S001" 

while (ciklus2 != len(szenzorstruktúra)):

    if SZENZORID == szenzorstruktúra[ciklus2].kód:    
        s001.append(szenzorstruktúra[ciklus2].mérés)
    
    ciklus2 = ciklus2 + 1


min = s001[0] 
darab = 1

for i in range(1, len(s001)):

    if s001[i] == min:
        darab = darab + 1
    
    if s001[i] < min:
        min = s001[i] 
        darab = 1 
    


print("S001 minimális értéke: ", min, end="") 
print("Minimális értékek száma: ", darab)

print("\n5. feladat") 

SZID = "S002" 
szum = 0.0
mennyi = 0 

for i in range(0, len(szenzorstruktúra)):
    if szenzorstruktúra[i].kód == SZID:    
        szum += float(szenzorstruktúra[i].mérés)
        mennyi = mennyi + 1

átlag = szum / mennyi 

print("S002 szenzor méréseinek átlaga: ", round(átlag,2)) 

print("\n6. feladat") 

SZENZORID3 = "S003" 
s003érték = list() 
s003dátum = list() 

for i in range(1, len(szenzorstruktúra)):
    if SZENZORID3 == szenzorstruktúra[i].kód:    
        s003érték.append(szenzorstruktúra[i].mérés) 
        s003dátum.append(szenzorstruktúra[i].dátum) 
    
különbség = abs(float(s003érték[1]) - float(s003érték[0]))
dátum01 = szenzorstruktúra[0].dátum 
dátum02 = szenzorstruktúra[1].dátum 

for i in range(2, len(s003érték)):
    if különbség < abs(float(s003érték[i]) - float(s003érték[i - 1])):    
        különbség = abs(float(s003érték[i]) - float(s003érték[i - 1])) 
        dátum01 = s003dátum[i - 1] 
        dátum02 = s003dátum[i] 
    


print(különbség)

print("Maximális különbség két egymást követő mérésnél: ", dátum01, " ", dátum02) 

print("\n7. feladat") 

def listáz(alsóhatár, felsőhatár):
    for i in range(0, len(szenzorstruktúra)):    
        if alsóhatár < float(szenzorstruktúra[i].mérés) and float(szenzorstruktúra[i].mérés) < felsőhatár:        
            print(szenzorstruktúra[i].mérés, end="")
        
#Teszt
listáz(17.3, 19)

print("\n8. feladat") 

def száztízszázalék(mérés1, mérés2):
    if mérés1 > mérés2 * 1.1:    
        return "Az első mérés nagyobb a második mérés 110%-nál"     
    if mérés1 < mérés2 * 1.1:    
        return "Az első mérés kisebb a második mérés 110%-nál"
    return "Az első mérés egyenlő a második mérés 110%-val" 
    
#Teszt
print(száztízszázalék(float(szenzorstruktúra[0].mérés), float(szenzorstruktúra[1].mérés))) 

print("\n9. feladat") 

fahrenheit = 0.0 

f = open("fahrenheit.txt", "w")

for i in range(0, len(szenzorstruktúra)):
    #([°C]×9 / 5)+32 = [°F]
    fahrenheit = (float(szenzorstruktúra[i].mérés) * 9 / 5) + 32 
    f.write(str(fahrenheit))
    f.write("\n")

f.close()

print("A fájl kiírása megtörtént!")