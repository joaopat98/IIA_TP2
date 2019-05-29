from pathlib import Path

for filename in Path('').glob('**/*.csv'):
    f = open(filename, 'r')
    text = f.read()
    new_text = text.replace(" ", ",")
    f.close()
    f = open(filename, 'w')
    f.write(new_text)
    f.close()