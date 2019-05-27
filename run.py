from os import system
from random import randint

elitism = input("is elitist(y/n): ")
num_elites = 0 if elitism == "n" else int(input("number of elites: "))
num_generations = int(input("num generations: "))
pop_size = int(input("population size: "))
t_size = int(input("tournament size: "))
m_prob = float(input("mutation probability: "))
c_prob = float(input("crossover probability: "))
num_tests = int(input("number of tests: "))

for i in range(num_tests):
    filename = "elitism-{},num_elites-{},num_gens-{},pop_size-{},t_size-{},m_prob-{},c_prob-{},test-{}".format(elitism,num_elites,num_generations,pop_size,t_size,"{}".format(m_prob).replace(".",","),"{}".format(c_prob).replace(".",","),i)
    cmd = ".\IIA_TP2.exe -batchmode -nographics --batchmode --elitism={} --elitism_size={} --generations={} --pop_size={} --t_size={} --m_prob={} --c_prob={} --seed={} --log={}".format(elitism,num_elites,num_generations,pop_size,t_size,m_prob,c_prob, i, filename)
    print("test {}:".format(i))
    system(cmd)