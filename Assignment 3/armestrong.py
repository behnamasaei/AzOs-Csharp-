Number = int(input('enter your number: '))

h = Number
size = len(str(Number))
j = 0
while h > 0:
    count = h % 10
    j = j+count**size
    h //= 10


if j == Number:
    print('Yes')
else:
    print('No')
