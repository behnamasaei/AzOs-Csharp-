Hour = int(input('Hour: '))
Min = int(input('Minute: '))
Sec = int(input('Second: '))

print('{}:{}:{}'.format(Hour,Min,Sec))
print(Hour*3600 + Min*60 + Sec , ' Sec.')