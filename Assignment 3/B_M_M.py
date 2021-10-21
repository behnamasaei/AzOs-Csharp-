number1 = int(input('enter first number : '))
number2 = int(input('enter second number : '))


def bmm(a, b):

    if a > b:
        small = b
    else:
        small = a
    for i in range(1, small+1):
        if((a % i == 0) and (b % i == 0)):
            bmm = i

    return bmm


print("The bmm of {} and {} is : {} ".format(
    number1, number2, bmm(number1, number2)))
