using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticIndividual : Individual
{


    public GeneticIndividual(int[] topology) : base(topology)
    {
    }

    public override void Initialize()
    {
        for (int i = 0; i < totalSize; i++)
        {
            genotype[i] = Random.Range(-1.0f, 1.0f);
        }
    }

    public override void Crossover(Individual partner, float probability)
    {
        int first, second;
        first = Random.Range(0, genotype.Length - 2);
        second = Random.Range(first + 1, genotype.Length - 1);
		for (int i = first; i <= second; i++)
		{
			genotype[i] = ((GeneticIndividual) partner).genotype[i];
		}
    }

    public override void Mutate(float probability)
    {
        for (int i = 0; i < totalSize; i++)
        {
            if (Random.Range(0.0f, 1.0f) < probability)
            {
                genotype[i] = Random.Range(-1.0f, 1.0f);
            }
        }
    }

    public override Individual Clone()
    {
        GeneticIndividual new_ind = new GeneticIndividual(this.topology);

        genotype.CopyTo(new_ind.genotype, 0);
        new_ind.fitness = this.Fitness;
        new_ind.evaluated = false;

        return new_ind;
    }

}
