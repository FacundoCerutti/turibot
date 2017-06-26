using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GAF;
using GAF.Extensions;
using GAF.Operators;
using System.Diagnostics;

namespace TuriBoBot.Model
{
    public class Genetico
    {
        private Population population = new Population();
        private List<Destino> cities = new List<Destino>();
        private List<String> listaF = new List<string>();
        private List<object> atributosF = new List<object>();
        private Double mutationrate = 0;
        private int terminateflag;
        private int terminatevalue;

        public Population Population { get => population; set => population = value; }
        public List<Destino> Cities { get => cities; set => cities = value; }
        public List<String> ListaF { get => listaF; set => listaF = value; }
        public List<object> AtributosF { get => atributosF; set => atributosF = value; }
        public Double Mutationrate { get => mutationrate; set => mutationrate = value; }
        public int Terminateflag { get => terminateflag; set => terminateflag = value; }
        public int Terminatevalue { get => terminatevalue; set => terminatevalue = value; }

        public Genetico(List<Destino> destinos,Double mutation,int terminflag,int termin)
        {
            Cities = destinos;
            Mutationrate = mutation;
            Terminateflag = terminflag;
            Terminatevalue = termin;
            
            for (var p = 0; p < 100; p++)
            {

                var chromosome = new Chromosome();
                for (var g = 1; g <= Cities.ToArray().Length; g++)
                {
                    chromosome.Genes.Add(new Gene(g));
                }
                chromosome.Genes.ShuffleFast();
                Destino tuc=new Destino("San Miguel de Tucuman", -26.817729748348082,-65.20398984827273);
                List<Destino> ciudades = new List<Destino>();
                ciudades.Add(tuc);
                foreach(Destino d in Cities)
                {
                    ciudades.Add(d);
                }
                Cities = ciudades;
                List<Gene> individuos = new List<Gene>();
                individuos.Add(new Gene(0));
                foreach(Gene g in chromosome.Genes)
                {
                    individuos.Add(g);
                    chromosome.Genes.Remove(g);
                }                
                foreach(Gene g in individuos)
                {
                    chromosome.Genes.Add(g);
                }
                Population.Solutions.Add(chromosome);
            }

            var elite = new Elite(5);
            var crossover = new Crossover(0.8)
            {
                CrossoverType = CrossoverType.DoublePointOrdered
            };
            var mutate = new SwapMutate(Mutationrate/100);
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
                
        private bool Terminate(Population population,int currentGeneration, long currentEvaluation)
        {
            int terminflag = Terminateflag;
            int terminvalue = Terminatevalue;
            if(terminflag==1)
            {
                Stopwatch sw = Stopwatch.StartNew();
                return sw.Elapsed.TotalMilliseconds>(terminvalue*1000);
            }
            if(terminflag==2)
            {
                return currentGeneration > terminvalue;
            }
            return currentGeneration > 400;            
        }
    }
}