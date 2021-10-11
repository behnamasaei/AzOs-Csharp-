import random
# score
Score = {
    'HumanScore': 0,
    'pc1Score': 0,
    'pc2Score': 0,
}

Select = {
    'Human': 0,
    'pc1': 0,
    'pc2': 0,
}

for i in range(5):
    print('------------------')
    print('You={}  vs  PC1={}  vs  PC2={}'.format(
        Score['HumanScore'], Score['pc1Score'], Score['pc2Score']))
    print('Select one...')
    print('1. Up')
    print('2. Down')
    Select['Human'] = int(input())
    Select['pc1'] = random.randint(1, 2)
    Select['pc2'] = random.randint(1, 2)
    print('h={}  pc1={}  pc2={}'.format(
        Select['Human'], Select['pc1'], Select['pc2']))

    if(Select['Human'] != Select['pc1'] and Select['Human'] != Select['pc2']):
        Score['HumanScore'] += 1

    if(Select['pc1'] != Select['Human'] and Select['pc1'] != Select['pc2']):
        Score['pc1Score'] += 1

    if(Select['pc2'] != Select['pc1'] and Select['pc2'] != Select['Human']):
        Score['pc2Score'] += 1


print('Winner ', max(Score.items()))
