import random

n = int(input('enter n: '))
number = []

while n > 0:
    randomNum = random.randint(0,200)
    if randomNum not in number:
        number.append(randomNum)
        n = n-1

print(number)
