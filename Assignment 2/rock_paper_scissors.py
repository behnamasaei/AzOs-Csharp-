import random

"""
scores
"""
humanScore = 0
ComScore = 0


for i in range(5):
    print('You:',humanScore , '  VS  PC:',ComScore)
    print('Select one... \n1. Rock \n2. Paper \n3. Scissors')
    select = int(input())

    options = ['rock', 'paper', 'scissors']

    human = options[1]

    if(select > 0 and select < 4):
        human = options[select-1]
    Computer = options[random.randint(0, 2)]

    if(human == Computer):
        print(human, Computer)
        continue

    elif((human == options[0] and Computer == options[2])
         or (human == options[1] and Computer == options[0])
         or (human == options[2] and Computer == options[1])):
        humanScore += 1

    else:
        ComScore += 1

    print(human, Computer)
