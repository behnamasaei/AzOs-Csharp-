import random
words_bank = ['tree', 'book', 'linux', 'python', 'asp']

word = random.choice(words_bank)
user_true_chars = []
joon = 7

while True:
    for i in range(len(word)):
        if word[i] in user_true_chars:
            print(word[i], end='')
        else:
            print('-', end='')

    user_char = input('Please enter a charachter: ')

    if user_char in word:
        user_true_chars.append(user_char)
        print('✔')
    else:
        joon = -1
        print('❌')

    if len(user_true_chars) == len(word):
        print(word)
        print('You win!')
        break

    if joon == 0:
        print('Game over')
        break
