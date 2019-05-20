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
        population.Sort(new IndividualComparer());
        List<Individual> new_population = new List<Individual>();
        int parents_continue = (int)Mathf.Floor(tournamentSize * 0.1f);
        for (int i = 0; i < tournamentSize - parents_continue / 2; i++)
        {
            Individual child = population[i].Clone();
            child.Crossover(population[(i + 1) % tournamentSize], 0);
            new_population.Add(child);
            child = population[i].Clone();
            child.Crossover(population[(i + 2) % tournamentSize], 0);
            new_population.Add(child);
        }

        for (int i = 0; i < parents_continue; i++)
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
