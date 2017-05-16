using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FLS;
using FLS.Rules;

namespace TuriBoBot.Model
{
    public class ControladorDifuso3
    {
        private IFuzzyEngine fuzzyEngine = new FuzzyEngineFactory().Default();
        private LinguisticVariable temperature = new LinguisticVariable("temperature");
        private LinguisticVariable results = new LinguisticVariable("results");
        private LinguisticVariable sublist = new LinguisticVariable("sublist");

        private List<object> rules = new List<object>();        

        public ControladorDifuso3()
        {
            //Variables Linguisticas
            // Temperatura
            var cold = Temperature.MembershipFunctions.AddTriangle("frio", 0, 50, 100);
            var warm = Temperature.MembershipFunctions.AddTriangle("templado", 50, 100, 150);
            var hot = Temperature.MembershipFunctions.AddTriangle("caliente", 100, 150, 200);
            // Subconjuntos
            var conjunto1 = Sublist.MembershipFunctions.AddTriangle("conjunto 1", 0, 50, 100);
            var conjunto2 = Sublist.MembershipFunctions.AddTriangle("conjunto 2", 50, 100, 150);
            var conjunto3 = Sublist.MembershipFunctions.AddTriangle("conjunto 3", 100, 150, 200);
            var conjunto4 = Sublist.MembershipFunctions.AddTriangle("conjunto 4", 150, 200, 250);
            var conjunto5 = Sublist.MembershipFunctions.AddTriangle("conjunto 5", 200, 250, 300);
            var conjunto6 = Sublist.MembershipFunctions.AddTriangle("conjunto 6", 250, 300, 350);
            var conjunto7 = Sublist.MembershipFunctions.AddTriangle("conjunto 7", 300, 350, 400);
            var conjunto8 = Sublist.MembershipFunctions.AddTriangle("conjunto 8", 350, 400, 450);
            var conjunto9 = Sublist.MembershipFunctions.AddTriangle("conjunto 9", 400, 450, 500);
            // Resultados
            //Conjunto 1f
            var resultado1 = Results.MembershipFunctions.AddTriangle("resultado 1", 0, 50, 100);
            //Conjunto 1t
            var resultado2 = Results.MembershipFunctions.AddTriangle("resultado 2", 50, 100, 150);
            //Conjunto 1c
            var resultado3 = Results.MembershipFunctions.AddTriangle("resultado 3", 100, 150, 200);
            //Conjunto 2f
            var resultado4 = Results.MembershipFunctions.AddTriangle("resultado 4", 150, 200, 250);
            //Conjunto 2t
            var resultado5 = Results.MembershipFunctions.AddTriangle("resultado 5", 200, 250, 300);
            //Conjunto 2c
            var resultado6 = Results.MembershipFunctions.AddTriangle("resultado 6", 250, 300, 350);
            //Conjunto 3f
            var resultado7 = Results.MembershipFunctions.AddTriangle("resultado 7", 300, 350, 400);
            //Conjunto 3t
            var resultado8 = Results.MembershipFunctions.AddTriangle("resultado 8", 350, 400, 450);
            //Conjunto 3c
            var resultado9 = Results.MembershipFunctions.AddTriangle("resultado 9", 400, 450, 500);
            //Conjunto 4f
            var resultado10 = Results.MembershipFunctions.AddTriangle("resultado 10", 450, 500, 550);
            //Conjunto 4t
            var resultado11 = Results.MembershipFunctions.AddTriangle("resultado 11", 500, 550, 600);
            //Conjunto 4c
            var resultado12 = Results.MembershipFunctions.AddTriangle("resultado 12", 550, 600, 650);
            //Conjunto 5f
            var resultado13 = Results.MembershipFunctions.AddTriangle("resultado 13", 600, 650, 700);
            //Conjunto 5t
            var resultado14 = Results.MembershipFunctions.AddTriangle("resultado 14", 650, 700, 750);
            //Conjunto 5c
            var resultado15 = Results.MembershipFunctions.AddTriangle("resultado 15", 700, 750, 800);
            //Conjunto 6f
            var resultado16 = Results.MembershipFunctions.AddTriangle("resultado 16", 750, 800, 850);
            //Conjunto 6t
            var resultado17 = Results.MembershipFunctions.AddTriangle("resultado 17", 800, 850, 900);
            //Conjunto 6c
            var resultado18 = Results.MembershipFunctions.AddTriangle("resultado 18", 850, 900, 950);
            //Conjunto 7f
            var resultado19 = Results.MembershipFunctions.AddTriangle("resultado 19", 900, 950, 1000);
            //Conjunto 7t
            var resultado20 = Results.MembershipFunctions.AddTriangle("resultado 20", 950, 1000, 1050);
            //Conjunto 7c
            var resultado21 = Results.MembershipFunctions.AddTriangle("resultado 21", 1000, 1050, 1100);
            //Conjunto 8f
            var resultado22 = Results.MembershipFunctions.AddTriangle("resultado 22", 1050, 1100, 1150);
            //Conjunto 8t
            var resultado23 = Results.MembershipFunctions.AddTriangle("resultado 23", 1100, 1150, 1200);
            //Conjunto 8c
            var resultado24 = Results.MembershipFunctions.AddTriangle("resultado 24", 1150, 1200, 1250);
            //Conjunto 9f
            var resultado25 = Results.MembershipFunctions.AddTriangle("resultado 25", 1200, 1250, 1300);
            //Conjunto 9t
            var resultado26 = Results.MembershipFunctions.AddTriangle("resultado 26", 1250, 1300, 1350);
            //Conjunto 9c
            var resultado27 = Results.MembershipFunctions.AddTriangle("resultado 27", 1300, 1350, 1400);

            //Reglas
            //Subconjuntos/Temperatura = Respuesta
            //Conjunto 1
            Rules.Add(Rule.If(Sublist.Is(conjunto1).And(Temperature.Is(cold))).Then(Results.Is(resultado1)));   //rule26
            Rules.Add(Rule.If(Sublist.Is(conjunto1).And(Temperature.Is(warm))).Then(Results.Is(resultado2)));   //rule27
            Rules.Add(Rule.If(Sublist.Is(conjunto1).And(Temperature.Is(hot))).Then(Results.Is(resultado3)));    //rule28
            //Conjunto 2
            Rules.Add(Rule.If(Sublist.Is(conjunto2).And(Temperature.Is(cold))).Then(Results.Is(resultado4)));   //rule29
            Rules.Add(Rule.If(Sublist.Is(conjunto2).And(Temperature.Is(warm))).Then(Results.Is(resultado5)));   //rule30
            Rules.Add(Rule.If(Sublist.Is(conjunto2).And(Temperature.Is(hot))).Then(Results.Is(resultado6)));    //rule31
            //Conjunto 3
            Rules.Add(Rule.If(Sublist.Is(conjunto3).And(Temperature.Is(cold))).Then(Results.Is(resultado7)));   //rule32
            Rules.Add(Rule.If(Sublist.Is(conjunto3).And(Temperature.Is(warm))).Then(Results.Is(resultado8)));   //rule33
            Rules.Add(Rule.If(Sublist.Is(conjunto3).And(Temperature.Is(hot))).Then(Results.Is(resultado9)));    //rule34 ;)
            //Conjunto 4
            Rules.Add(Rule.If(Sublist.Is(conjunto4).And(Temperature.Is(cold))).Then(Results.Is(resultado10)));  //rule35
            Rules.Add(Rule.If(Sublist.Is(conjunto4).And(Temperature.Is(warm))).Then(Results.Is(resultado11)));  //rule36
            Rules.Add(Rule.If(Sublist.Is(conjunto4).And(Temperature.Is(hot))).Then(Results.Is(resultado12)));   //rule37
            //Conjunto 5
            Rules.Add(Rule.If(Sublist.Is(conjunto5).And(Temperature.Is(cold))).Then(Results.Is(resultado13)));  //rule38
            Rules.Add(Rule.If(Sublist.Is(conjunto5).And(Temperature.Is(warm))).Then(Results.Is(resultado14)));  //rule39
            Rules.Add(Rule.If(Sublist.Is(conjunto5).And(Temperature.Is(hot))).Then(Results.Is(resultado15)));   //rule40
            //Conjunto 6
            Rules.Add(Rule.If(Sublist.Is(conjunto6).And(Temperature.Is(cold))).Then(Results.Is(resultado16)));  //rule41
            Rules.Add(Rule.If(Sublist.Is(conjunto6).And(Temperature.Is(warm))).Then(Results.Is(resultado17)));  //rule42
            Rules.Add(Rule.If(Sublist.Is(conjunto6).And(Temperature.Is(hot))).Then(Results.Is(resultado18)));   //rule43
            //Conjunto 7
            Rules.Add(Rule.If(Sublist.Is(conjunto7).And(Temperature.Is(cold))).Then(Results.Is(resultado19)));  //rule44
            Rules.Add(Rule.If(Sublist.Is(conjunto7).And(Temperature.Is(warm))).Then(Results.Is(resultado20)));  //rule45
            Rules.Add(Rule.If(Sublist.Is(conjunto7).And(Temperature.Is(hot))).Then(Results.Is(resultado21)));   //rule46
            //Conjunto 8
            Rules.Add(Rule.If(Sublist.Is(conjunto8).And(Temperature.Is(cold))).Then(Results.Is(resultado22)));  //rule47
            Rules.Add(Rule.If(Sublist.Is(conjunto8).And(Temperature.Is(warm))).Then(Results.Is(resultado23)));  //rule48
            Rules.Add(Rule.If(Sublist.Is(conjunto8).And(Temperature.Is(hot))).Then(Results.Is(resultado24)));   //rule49
            //Conjunto 9
            Rules.Add(Rule.If(Sublist.Is(conjunto9).And(Temperature.Is(cold))).Then(Results.Is(resultado25)));  //rule50
            Rules.Add(Rule.If(Sublist.Is(conjunto9).And(Temperature.Is(warm))).Then(Results.Is(resultado26)));  //rule51
            Rules.Add(Rule.If(Sublist.Is(conjunto9).And(Temperature.Is(hot))).Then(Results.Is(resultado27)));   //rule52


            //Adding the rules to the Fuzzy Engine
            foreach (FuzzyRule rule in Rules)
            {
                FuzzyEngine.Rules.Add(rule);
            }

        }
        public LinguisticVariable Temperature { get => temperature; set => temperature = value; }
        public LinguisticVariable Results { get => results; set => results = value; }
        public IFuzzyEngine FuzzyEngine { get => fuzzyEngine; set => fuzzyEngine = value; }
        public List<object> Rules { get => rules; set => rules = value; }
        public LinguisticVariable Sublist { get => sublist; set => sublist = value; }       
    }
}