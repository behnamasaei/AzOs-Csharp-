Time = int(input('Second: '))

Hour = int( Time / 3600)
Time %= 3600

Min = int(Time / 60)
Time %= 60

print('{}:{}:{}'.format(Hour,Min,Time))