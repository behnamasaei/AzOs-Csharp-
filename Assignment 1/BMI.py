Height =  float(input("Enter Height: "))
Weight =  float(input("Enter Weight: "))

BMI = Weight / (pow(Height,2))

if BMI<18.5:
    print("Under Weight")

if BMI>=18.5 and BMI<25:
    print("Normal")

if BMI>=25 and BMI<30:
    print("Over Weight")

if BMI>=30 and BMI<35:
    print("Obese")

if BMI>=35:
    print("Extermely Obese")