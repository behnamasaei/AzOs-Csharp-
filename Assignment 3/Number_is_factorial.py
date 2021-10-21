number = int(input('pleas enter your number : '))

i = 1
fac = i
while True:
    fac = fac*(i+1)
    if fac == number:
        print('Yes')
        print(i+1, '!')
        break
    if fac > number:
        print('No')
        break
    i += 1
