world = input("enter world: ")
count = 0

for i in world:
    if i == " ":
        count = count + 1

print('Number world is ', count+1)
