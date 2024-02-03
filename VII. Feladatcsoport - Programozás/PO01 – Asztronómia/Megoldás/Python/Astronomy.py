import random

#1. feladat
f = open("égitest.txt", "r",encoding="utf8")
data = f.readlines()
f.close()

NUM_OF_LINES = len(data)

names = list()
types = list()
masses = list()
radiuses = list()
ages = list()
moons = list()

for i in range(0, NUM_OF_LINES):
    l = data[i].replace('\n','')
    s = l.split(";")
    names.append(s[0])
    types.append(s[1])
    masses.append(s[2])
    radiuses.append(s[3])
    ages.append(s[4])
    moons.append(s[5])

#2. feladat
counter = 0
for i in range(0, NUM_OF_LINES):
    if types[i] == "Exobolygó":
        counter += 1
    
print("Az Exobolygók száma: ", counter)

#3. feladat
max = masses[0]
nums_of_max = 1
hits = list()
hits.append(names[0])

for i in range(1,NUM_OF_LINES):
    if max == masses[i]:    
        nums_of_max += 1
        hits.append(names[i])    

    if max < masses[i]:    
        max = masses[i]
        nums_of_max = 1
        hits.clear()
        hits.append(names[i])

print("\nA legnagyobb tömegű égitestek száma: ", nums_of_max, "amely(ek): ", ', '.join(hits))

#4. feladat
def Keres(name):
    i = 0
    while i < NUM_OF_LINES and name != names[i]:    
        i += 1   

    return ages[i]


#5. feladat
chosen = random.randint(0, NUM_OF_LINES-1)

print("\nTippelje meg a ", names[chosen], "korát!")

tip = -1

while int(tip) != int(ages[chosen]):
    tip = input()
    if Keres(names[chosen]) > tip:    
        print("Idősebb!")
    
    if Keres(names[chosen]) < tip:
        print("Fiatalabb!")
    
    if Keres(names[chosen]) == tip:
        print("Eltalálta!")

#6. feladat
minradius = radiuses[0]
maxradius = radiuses[0]
minindex = float(0.0)
maxindex = float(0.0)


for index in  range(1, NUM_OF_LINES):
    if float(minradius) > float(radiuses[index]): 
        minradius = radiuses[index]
        minindex = index
    
    if float(maxradius) < float(radiuses[index]):   
        maxradius = radiuses[index]
        maxindex = index

print("\nA(z) ", names[minindex]," és a(z) ", names[maxindex], "között legnagyobb a méretkülönbség, értéke: ", str(float(maxradius) - float(minradius)))


#7. feladat
def older(name01, name02):
    if int(Keres(name01)) > int(Keres(name02)):
        return_value = "az 1. égitest idősebb!"
    elif int(Keres(name01)) < int(Keres(name02)):
        return_value = "a 2. égitest idősebb"
    else:
        return_value = "a két égitest kora megyegyezik!"   

    return return_value

randnum01 = random.randint(0, NUM_OF_LINES - 1)
randnum02 = random.randint(0, NUM_OF_LINES - 1)

while randnum01 == randnum02:
    randnum02 = random.randint(0, NUM_OF_LINES - 1)

print("\nA(z) ", names[randnum01]," és a(z) ", names[randnum02], " égitest korának viszonya: ", older(names[randnum01], names[randnum02]))


#8. és 9. feladat
print("\nKérem adja meg a holdak számának határát:")

limit = input()

results = list()

f = open("holdak.txt","w")

f.writelines(limit)
f.write("\n")

for i in range(0, NUM_OF_LINES):
    if int(moons[i]) >= int(limit):    
        results.append(names[i])
        
if len(results) != 0: 
    print(", ".join(results))
    f.writelines(", ".join(results))
    f.write("\n")
else:
    print("Nincs ilyen égitest!")
    f.writelines("Nincs ilyen égitest!")
