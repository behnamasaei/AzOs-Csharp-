size = int(input('enter size of array: '))

array = []
check = 0


print('enter arry numbers:')
while size > 0:
    h = int(input())
    array.append(h)
    size -= 1

check_Sort = True
for i in range(len(array)):
    numberCheck = array[i]
    if i == len(array):
        break
    for j in range(i+1 , len(array)):
        if(numberCheck > array[j]):
            check_Sort = False
            break

if (check_Sort):
    print('Sorted')
else:
    print('Not sorted!')