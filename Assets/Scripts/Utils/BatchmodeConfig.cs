using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Options;

public class BatchmodeConfig
{

    public static bool batchmode = false;

    private static bool processed = false;
    private static readonly object syncLock = new object();

    public static void HandleArgs(EvolvingCarControl game, MetaHeuristic engine)
    {


        lock (syncLock)
        {
            if (!processed)
            {
                // get the list of arguments 
                string[] args = Environment.GetCommandLineArgs();

                bool show_help = false;

                var algorithm = (GeneticAlgorithm)engine;

                OptionSet parser = new OptionSet() {
                    "Usage: ",
                    "",
                    {"batchmode", "run in batchmode",
                        v => batchmode = v != null
                    },
                    {"generations=", "the number of generations to execute.",
                        (int v) => engine.numGenerations = v
                    },
                    {"elitism=", "is the algorithm elitist.",
                        v => algorithm.elitist = v == "y"
                    },
                    {"elitism_size=", "number of elites.",
                        (int v) => algorithm.num_elites = v
                    },
                    {"pop_size=", "size of the population.",
                        (int v) => algorithm.populationSize = v
                    },
                    {"t_size=", "tournament size.",
                        (int v) => algorithm.tournamentSize = v
                    },
                    {"m_prob=", "mutation probability.",
                        (float v) => algorithm.mutationProbability = v
                    },
                    {"c_prob=", "crossover probability.",
                        (float v) => algorithm.crossoverProbability = v
                    },
                    {"log=", "the logger output filename to use.",
                        v => engine.logFilename = v
                    },
                    {"seed=", "the seed.",
                        (int v) => game.seed = v
                    },
                    { "h|help",  "show this message and exit",
                        v => show_help = v != null
                    },
                };

                try
                {
                    parser.Parse(args);
                    engine.InitPopulation();
                    processed = true;
                }
                catch (OptionException e)
                {
                    Console.Write("3dcar: ");
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try ` --help' for more information.");
                    Application.Quit();
                    return;
                }

                if (show_help)
                {
                    parser.WriteOptionDescriptions(Console.Out);
                    Application.Quit();
                    return;
                }

                Console.WriteLine(algorithm.elitist);
                Console.WriteLine(algorithm.num_elites);
                Console.WriteLine(algorithm.numGenerations);
                Console.WriteLine(algorithm.populationSize);
                Console.WriteLine(algorithm.tournamentSize);
                Console.WriteLine(algorithm.mutationProbability);
                Console.WriteLine(algorithm.crossoverProbability);


            }
        }

    }
}
