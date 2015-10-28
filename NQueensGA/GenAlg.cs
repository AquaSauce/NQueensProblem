using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQueensGA
{

    class GenAlg
    {
        const int NUM_QUEENS = 8;
        const int POP_SIZE = 500;
        const int MUTATION = 10; // VALUE IS (X/100)
        static Board[] POPULATION = new Board[POP_SIZE];

        public static void Main()
        {
            Board solution = geneticAlg();

            Console.Out.WriteLine(solution.toString() + "\n");
            solution.printBoard();
            Console.ReadLine();
        }

        /// <summary>
        /// crossover - Performs a random single crossover two parents
        /// </summary>
        /// <param name="parentX">First Parent</param>
        /// <param name="parentY">Second Parent</param>
        /// <returns>Child as a result of a crossover</returns>
        public static Board crossover(Board parentX, Board parentY)
        {
            Board child;
            Random r = new Random();

            int crossoverPoint = r.Next(1, NUM_QUEENS - 1);

            // Obtain both parts of the array from the parents, then produce a single child
            int[] firstHalf = parentX.getBoard().Take(crossoverPoint).ToArray();
            int[] secondHalf = parentY.getBoard().Skip(crossoverPoint).Take(NUM_QUEENS - crossoverPoint).ToArray();
            int[] childArray = firstHalf.Concat(secondHalf).ToArray();

            child = new Board(childArray);

            return child;
            
        }

        /// <summary>
        /// createPopulation - Creates the initial population
        /// </summary>
        public static void createPopulation()
        {
            int[] initParent = new int[NUM_QUEENS];
            Random r = new Random();

            // Populating the population with random initial parents
            for (int i = 0; i < POP_SIZE; i++)
            {
                for (int j = 0; j < initParent.Length; j++)
                {
                    initParent[j] = r.Next(1, NUM_QUEENS + 1);
                }

                POPULATION[i] = new Board((int[])initParent.Clone());
            }

        }

        /// <summary>
        /// chooseParent - Randomly chooses a weighted parent from the
        ///                population, by level of fitness.
        /// </summary>
        /// <returns>Parent</returns>
        public static Board chooseParent()
        {
            Random r = new Random();
            int total = 0;

            // Get current total fitness
            for(int i = 0; i < POPULATION.Length; i++)
            {
                total += POPULATION[i].getFitness();
            }

            int random = r.Next(0, total);

            // Choose random parent, higher fitness has higher chance
            for(int i = 0; i < POPULATION.Length; i++)
            {
                if(random < POPULATION[i].getFitness())
                {
                    return POPULATION[i];
                }

                random = random - POPULATION[i].getFitness();
            }

            return null;
        }

        /// <summary>
        /// geneticAlg - Performs the genetic algorithm 
        /// </summary>
        /// <returns>Child which contains a valid solution</returns>
        public static Board geneticAlg()
        {
            Board child;
            Random r = new Random();
            Board[] tempPopulation = new Board[POP_SIZE];

            int highestFitness = 0;
            int generation = 0;

            createPopulation();

            while (true)
            {
                generation++;

                // Begin creation of new generation
                for (int i = 0; i < POP_SIZE; i++)
                { 
                    // Choose two parents and create a child
                    child = crossover(chooseParent(), chooseParent());

                    // Check to see if child is a solution
                    if (child.solved())
                    {
                        Console.Out.WriteLine("Fitness: " + child.getFitness() + " Generation: " + generation);
                        return child;
                    }

                    // Mutation change
                    if (MUTATION > r.Next(0, 100))
                    {
                        child.mutate();
                    }

                    // Check childs fitness
                    if (child.getFitness() > highestFitness)
                    {
                        highestFitness = child.getFitness();
                    }

                    tempPopulation[i] = child;
                }

                POPULATION = tempPopulation;
                Console.Out.WriteLine("Fitness: " + highestFitness + " Generation: " + generation);
            }
        }
    }
}
