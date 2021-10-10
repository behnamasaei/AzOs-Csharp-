import math
from mpmath import *

num1 = float(input('number 1: '))
num2 = float(input('number 2: '))

print('Calculater...')
print('1. sum')
print('2. sub')
print('3. mul')
print('4. div')
print('5. sin')
print('6. cos')
print('7. tan')
print('8. cot')
print('9. log')

select = input()

match select:
    case '1':
        print(num1+num2)

    case '2':
        print(num1-num2)

    case '3':
        print(num1*num2)

    case '4':
        if(num2!=0):
            print(num1/num2)
    
    case '5':
        print(math.sin(num1))

    case '6':
        print(math.cos(num1))

    case '7':
        print(math.tan(num1))

    case '8':
        print(cot(num1))

    case '9':
        print(math.log(num1,num2))