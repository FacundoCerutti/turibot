using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GAF;
using GAF.Extensions;
using GAF.Operators;

namespace TuriBoBot.Model
{
    internal class Genetico
    {
        private static List<Destino> _cities;
        private static void Main(string[] args)
        {

            //get our cities
            _cities = CreateCities().ToList();

            //Each Destino can be identified by an integer within the range 0-15
            //our chromosome is a special case as it needs to contain each Destino 
            //only once. Therefore, our chromosome will contain all the integers
            //between 0 and 15 with no duplicates.

            //We can create an empty population as we will be creating the 
            //initial solutions manually.
            var population = new Population();

            //create the chromosomes
            for (var p = 0; p < 100; p++)
            {

                var chromosome = new Chromosome();
                for (var g = 0; g < 16; g++)
                {
                    chromosome.Genes.Add(new Gene(g));
                }
                chromosome.Genes.ShuffleFast();
                population.Solutions.Add(chromosome);
            }

            //create the elite operator
            var elite = new Elite(5);

            //create the crossover operator
            var crossover = new Crossover(0.8)
            {
                CrossoverType = CrossoverType.DoublePointOrdered
            };

            //create the mutation operator
            var mutate = new SwapMutate(0.02);

            //create the GA
            var ga = new GeneticAlgorithm(population, CalculateFitness);

            //hook up to some useful events
            ga.OnGenerationComplete += ga_OnGenerationComplete;
            ga.OnRunComplete += ga_OnRunComplete;

            //add the operators
            ga.Operators.Add(elite);
            ga.Operators.Add(crossover);
            ga.Operators.Add(mutate);

            //run the GA
            ga.Run(Terminate);
        }

        static void ga_OnRunComplete(object sender, GaEventArgs e)
        {
            var fittest = e.Population.GetTop(1)[0];
            foreach (var gene in fittest.Genes)
            {
                Console.WriteLine(_cities[(int)gene.RealValue].Name);
            }
        }

        private static void ga_OnGenerationComplete(object sender, GaEventArgs e)
        {
            var fittest = e.Population.GetTop(1)[0];
            var distanceToTravel = CalculateDistance(fittest);
            Console.WriteLine("Generation: {0}, Fitness: {1}, Distance: { 2}", e.Generation, fittest.Fitness, distanceToTravel);
        }

        private static IEnumerable<Destino> CreateCities()
        {
            var cities = new List<Destino>
            {
                new Destino("Tafi del Valle",-26.851975945944233 ,-65.71111467217486 ),
                new Destino("Mar del Sur",-38.341162211004026 ,-57.99522928695682),
                new Destino("Punta del Este",-34.93660924934044 ,-54.92869676494137 ),
                new Destino("Cartagena",10.390831615024997 ,-75.48121677175287),
                new Destino("Camboriu",-26.99383781773793 , -48.63535161733398),
                new Destino("Punta Cana",18.582944976844704 ,-68.39808664838557),
                new Destino("Mendoza",-32.89026783069922 ,-68.84547283535159 ),
                new Destino("Miami",25.761021331096117 ,-80.19205767510374),
                new Destino("Bariloche", -41.13367467729619, -71.31137579608152),
                new Destino("Guayaquil", -2.16986698986024,-79.9199138987549),
                new Destino("Islha do Mel",-25.515731076600932 , -48.33269297642824),
                new Destino("Orlando", 28.53809132152661 ,-81.37965049743651),
                new Destino("Machu Pichu",-13.163141203713916 ,-72.54496289999997),
                new Destino("Roma",41.902742 ,12.496242),
                new Destino("Venecia",45.433776016477786 ,12.327938325351639),
                new Destino("Paris", 48.85542056220146,2.345192532753355),
                new Destino("Valencia",39.46701475595226 ,-0.371034483107342),
                new Destino("Portugal", 38.712702401270796,-9.14286366823353),
                new Destino("Grecia",37.974319357400795 ,23.73644296320502),
                new Destino("Egipto",26.939078097403748,30.795659800000067),
                new Destino("Hawaii", 20.47188350858674,-157.505),
                new Destino("Carlos Paz", -31.41222809215237,-64.49973669999997),
                new Destino("Mar del Plata",-38.01764738461685 ,-57.600534049999965),
                new Destino("Floreanopolis", -27.614479856117843,-48.48282474999996),
                new Destino("Cancun",21.121371172643965 ,-86.84931025000003),
                new Destino("Costa Rica",9.633931465220899 ,-84.25418434999995),
                new Destino("Las Vegas", 36.12519506431158,-115.17499999999995),
                new Destino("Amsterdam",52.37464788349741 ,4.898614199999997),
                new Destino("Tokyo",35.7076595041669 ,139.73150234936531)

            };

            return cities;
        }

        public static double CalculateFitness(Chromosome chromosome)
        {
            var distanceToTravel = CalculateDistance(chromosome);
            return 1 - distanceToTravel / 10000;
        }

        private static double CalculateDistance(Chromosome chromosome)
        {
            var distanceToTravel = 0.0;
            Destino previousDestino = null;

            //run through each Destino in the order specified in the chromosome
            foreach (var gene in chromosome.Genes)
            {
                var currentDestino = _cities[(int)gene.RealValue];

                if (previousDestino != null)
                {
                    var distance = previousDestino.GetDistanceFromPosition(currentDestino.Latitude,
                                                                        currentDestino.Longitude);

                    distanceToTravel += distance;
                }

                previousDestino = currentDestino;
            }

            return distanceToTravel;
        }

        public static bool Terminate(Population population,
            int currentGeneration, long currentEvaluation)
        {
            return currentGeneration > 400;
        }
    }
}



