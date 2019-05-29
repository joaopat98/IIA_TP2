from pathlib import Path

for amb in filter(lambda a: a.name.startswith("amb"), Path("").glob("*")):

    print(amb)
    for test in filter(lambda t: t.is_dir(), Path(amb).glob("*")):
        sheets = []
        lines = 1000
        for csv in test.glob("*.csv"):
            f = csv.open()
            sheets.append(f.readlines())
            lines = min(lines, len(sheets[-1]))
            f.close()
        join = test.joinpath("join.csv").open(mode='w')
        for i in range(lines):
            if i == 0:
                for j in range(len(sheets)):
                    if j < len(sheets) - 1:
                        join.write(sheets[j][i][:-1] + ", ,")
                    else:
                        join.write(sheets[j][i])
            elif sheets[0][i] != "\n":
                for j in range(len(sheets)):
                    if j < len(sheets) - 1:
                        join.write(sheets[j][i][:-1] + ", ,")
                    else:
                        join.write(sheets[j][i])
            else:
                break
        join.close()