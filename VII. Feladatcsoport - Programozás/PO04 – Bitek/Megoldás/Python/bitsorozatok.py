
#1. Feladat
n = 32
m = 32

print("1. Feladat")

bitek = open("bitek.txt",'r').read().split('\n')

print("A fájl beolvasása megtörtént!")

#2. Feladat
print("\n2. Feladat")

feltetel = 40
volt = False

for i in range(n):
    oszloposszeg = 0
    for j in range(m):    
        oszloposszeg += int(bitek[j][i])
    
    if oszloposszeg > feltetel:    
        print((i + 1), ". oszlop")
        volt = True

if not volt:
    print("Nincs 40 feletti oszlopösszeg")



#3. Feladat
print("\n3. Feladat")

max = 0
soregyesosszeg = 0
soregyesosszegek = list()

for i in range(n):    
    for j in range(m):       
        if int(bitek[i][j]) == 1:
            soregyesosszeg += int(bitek[i][j])
    soregyesosszegek.append(soregyesosszeg)
    soregyesosszeg = 0
    if max < soregyesosszegek[i]:    
        max = soregyesosszegek[i]

for i in range(n):
    if soregyesosszegek[i] == max:    
        print((i + 1),". sor")
    
#4. Feladat
print("\n4. Feladat")

nullak = 0
egyesek = 0

for i in range(n):
    for j in range(m):    
        if int(bitek[i][j]) == 0:        
            nullak += 1        
        else:
            egyesek += 1
        
arany = float(nullak)/ egyesek

print("Arány: ", arany)

#5. Feladat
print("\n5. Feladat")

#teszt
teszt = list()
SOR = 6
for i in range(m):
    teszt.append(bitek[SOR][i])

def megszámol(tomb):
    sum = 0
    for i in range(0,len(tomb)):    
        if int(tomb[i]) == 1:        
            sum += 1
    return sum

print("Egyesek száma a ",SOR + 1,". sorban:", megszámol(teszt))


#6. Feladat
print("\n6. Feladat")
def paritas (bitsorozat):
    if megszámol(bitsorozat) % 2 == 0:
        return True
    else:
        return False
    
#Teszt
if paritas(teszt):
    print("A", SOR + 1, "sor egyeseinek a száma: páros")
else:
    print("A", SOR + 1, "sor egyeseinek a száma: páratlan")


#7. Feladat
print("\n7. Feladat")

bitsorozat = list()

print("Paritásbitek sorok szerint:")

for i in range(n):
    for j in range(m):
        bitsorozat.append(bitek[i][j])
    if paritas(bitsorozat):
        print("0")
    else:
        print("1")
    bitsorozat.clear()

#8. Feladat
print("\n8. Feladat")

keresendominta = [1,0,0,1]
#Teszt
TESZTSOR = 31

def tartalmazza(minta):
    for i in range(0,m-3):
        if minta[0] == bitek[TESZTSOR][i] and minta[1] == bitek[TESZTSOR][i + 1] and \
         minta[2] == bitek[TESZTSOR][i + 2] and minta[3] == bitek[TESZTSOR][i + 3]:
            return True
    return False    

if tartalmazza(keresendominta):
    print("A",TESZTSOR,". sor tartalmazza a mintát!")
else:
    print("A",TESZTSOR,". sor nem tartalmazza a mintát!")


#9. Feladat
print("\n9. Feladat")

inverzbitek = []

for i in range(n):
    sor = []
    for j in range(m):
        if int(bitek[i][j]) == 1:
            sor.append(0)
        else:
            sor.append(1)
    inverzbitek.append(sor)    

f = open("újbitek.txt", "w")
for i in range(n):
    for item in inverzbitek[i]:
        f.write(str(item))
    f.write("\n")
    
f.close()

print("A fájl kiíratása megtörtént!")
