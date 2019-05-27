using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        for (int i = 0; i < tournamentSize - Mathf.Floor(num_elites / (float)2); i++)
        {
            Individual child = population[i].Clone();
            child.Crossover(population[(i + 1) % tournamentSize], crossoverProbability);
            new_population.Add(child);
            child = population[i].Clone();
            child.Crossover(population[(i + 2) % tournamentSize], crossoverProbability);
            new_population.Add(child);
        }

        if (num_elites % 2 == 1)
            new_population.RemoveAt(new_population.Count - 1);

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
    }

}
