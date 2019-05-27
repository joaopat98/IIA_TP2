using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GeneticAlgorithm : MetaHeuristic
{
    private class IndividualComparer : IComparer<Individual>
    {
        public int Compare(Individual x, Individual y)
        {
            return x.Fitness > y.Fitness ? -1 : 1;
        }
    }

    public float mutationProbability;
    public float crossoverProbability;
    public int tournamentSize;
    public bool elitist;

    public int num_elites;

    public override void InitPopulation()
    {
        population = new List<Individual>();
        // jncor 
        while (population.Count < populationSize)
        {
            GeneticIndividual new_ind = new GeneticIndividual(topology);
            new_ind.Initialize();
            population.Add(new_ind);
        }

    }

    //The Step function assumes that the fitness values of all the individuals in the population have been calculated.
    public override void Step()
    {
        updateReport();
        if (!elitist)
            num_elites = 0;
        population.Sort(new IndividualComparer());
        List<Individual> new_population = new List<Individual>();
        for (int i = 0; new_population.Count < populationSize - num_elites; i++)
        {
            Individual child = population[i].Clone();
            child.Crossover(population[(i + 1) % tournamentSize], crossoverProbability);
            new_population.Add(child);
            if (new_population.Count < populationSize - num_elites)
            {
                child = population[i].Clone();
                child.Crossover(population[(i + 2) % tournamentSize], crossoverProbability);
                new_population.Add(child);
            }
        }

        for (int i = 0; i < num_elites; i++)
        {
            new_population.Add(population[i].Clone());
        }

        foreach (var ind in new_population)
        {
            ind.Mutate(mutationProbability);
        }

        population = new_population;
        generation++;

        Debug.Log(population.Count);

        Console.WriteLine("gen " + generation + " of " + numGenerations + "...");
    }

}
