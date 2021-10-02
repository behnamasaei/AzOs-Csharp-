Line1 = float(input("Enter Line1: "))
Line2 = float(input("Enter Line2: "))
Line3 = float(input("Enter Line3: "))

if ((Line1+Line2) > Line3) and ((Line1+Line3) > Line2) and ((Line3+Line2) > Line1):
    print("We can create Tringle")
else:
    print("We can not create Tringle")