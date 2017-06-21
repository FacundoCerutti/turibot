using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GAF;
using GAF.Extensions;
using GAF.Operators;

namespace TuriBoBot.Model
{
    public class Genetico
    {
        private List<Destino> cities = new List<Destino>();
        private List<String> listaF = new List<string>();
        private List<object> atributosF = new List<object>();

        public List<Destino> Cities { get => cities; set => cities = value; }
        public List<String> ListaF { get => listaF; set => listaF = value; }
        public List<object> AtributosF { get => atributosF; set => atributosF = value; }

        public Genetico(List<Destino> destinos)
        {
            Cities = destinos;
            var population = new Population();
            for (var p = 0; p < 100; p++)
            {

                var chromosome = new Chromosome();
                for (var g = 0; g < Cities.ToArray().Length; g++)
                {
                    chromosome.Genes.Add(new Gene(g));
                }
                chromosome.Genes.ShuffleFast();
                population.Solutions.Add(chromosome);
            }
            var elite = new Elite(5);
            var crossover = new Crossover(0.8)
            {
                CrossoverType = CrossoverType.DoublePointOrdered
            };
            var mutate = new SwapMutate(0.02);
            var ga = new GeneticAlgorithm(population, CalculateFitness);
            //ga.OnGenerationComplete += ga_OnGenerationComplete;
            //ga.OnRunComplete += ga_OnRunComplete;
            ga.Operators.Add(elite);
            ga.Operators.Add(crossover);
            ga.Operators.Add(mutate);
            ga.Run(Terminate);
            var fittest = ga.Population.GetTop(1)[0];
            foreach (var gene in fittest.Genes)
            {
                listaF.Add(cities[(int)gene.RealValue].Name);
            }            
            var distanceToTravel = CalculateDistance(fittest);            
            atributosF.Add(fittest.Fitness);
            atributosF.Add(distanceToTravel);
        }

                

        private void ga_OnRunComplete(object sender, GaEventArgs e)
        {
            var fittest = e.Population.GetTop(1)[0];
            foreach (var gene in fittest.Genes)
            {
                listaF.Add(cities[(int)gene.RealValue].Name);                
            }
        }

        private void ga_OnGenerationComplete(object sender, GaEventArgs e)
        {
            var fittest = e.Population.GetTop(1)[0];
            var distanceToTravel = CalculateDistance(fittest);
            atributosF.Add(e.Generation);
            atributosF.Add(fittest.Fitness);
            atributosF.Add(distanceToTravel);            
        }

        
        public double CalculateFitness(Chromosome chromosome)
        {
            var distanceToTravel = CalculateDistance(chromosome);
            double fit = 1 - distanceToTravel / 10000;
            if (fit < 0)
            {
                return 0;
            }
            if (fit > 1)
            {
                return 1;
            }
            return fit;
        }

        private double CalculateDistance(Chromosome chromosome)
        {
            var distanceToTravel = 0.0;
            Destino previousDestino = null;

            //run through each Destino in the order specified in the chromosome
            foreach (var gene in chromosome.Genes)
            {
                var currentDestino = cities.ToArray()[Int64.Parse(gene.RealValue.ToString())];

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

        public bool Terminate(Population population,int currentGeneration, long currentEvaluation)
        {
            return currentGeneration < 400;
        }
    }
}