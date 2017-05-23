using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FLS;
using FLS.Rules;

namespace TuriBoBot.Model
{
    public class ControladorDifuso2
    {
        private IFuzzyEngine fuzzyEngine = new FuzzyEngineFactory().Default();
        private LinguisticVariable purpose = new LinguisticVariable("purpose");
        private LinguisticVariable sublist = new LinguisticVariable("sublist");
        private LinguisticVariable reach = new LinguisticVariable("reach");

        private List<object> rules = new List<object>();

        public ControladorDifuso2()
        {
            //Variables Linguisticas
            // Proposito
            var familiar = Purpose.MembershipFunctions.AddTriangle("familiar", 0, 50, 100);
            var romantic = Purpose.MembershipFunctions.AddTriangle("romantico", 50, 100, 150);
            var friends = Purpose.MembershipFunctions.AddTriangle("amigos", 100, 150, 200);
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
            // Alcance
            var national = Reach.MembershipFunctions.AddTriangle("nacional", 0, 50, 100);
            var latin = Reach.MembershipFunctions.AddTriangle("latino", 50, 100, 150);
            var international = Reach.MembershipFunctions.AddTriangle("internacional", 100, 150, 200);

            //Reglas
            //Proposito/Alcance = Subconjuntos
            // Reglas que proporcionan la inferencia para los resultados
            Rules.Add(Rule.If(Purpose.Is(familiar).And(Reach.Is(national))).Then(Sublist.Is(conjunto1)));       //rule17
            Rules.Add(Rule.If(Purpose.Is(familiar).And(Reach.Is(latin))).Then(Sublist.Is(conjunto2)));          //rule18
            Rules.Add(Rule.If(Purpose.Is(familiar).And(Reach.Is(international))).Then(Sublist.Is(conjunto3)));  //rule19
            Rules.Add(Rule.If(Purpose.Is(romantic).And(Reach.Is(national))).Then(Sublist.Is(conjunto4)));       //rule20
            Rules.Add(Rule.If(Purpose.Is(romantic).And(Reach.Is(latin))).Then(Sublist.Is(conjunto5)));          //rule21
            Rules.Add(Rule.If(Purpose.Is(romantic).And(Reach.Is(international))).Then(Sublist.Is(conjunto6)));  //rule22
            Rules.Add(Rule.If(Purpose.Is(friends).And(Reach.Is(national))).Then(Sublist.Is(conjunto7)));        //rule23
            Rules.Add(Rule.If(Purpose.Is(friends).And(Reach.Is(latin))).Then(Sublist.Is(conjunto8)));           //rule24
            Rules.Add(Rule.If(Purpose.Is(friends).And(Reach.Is(international))).Then(Sublist.Is(conjunto9)));   //rule25

            //Adding the rules to the Fuzzy Engine
            foreach (FuzzyRule rule in Rules)
            {
                FuzzyEngine.Rules.Add(rule);
            }
        }

        public LinguisticVariable Purpose { get => purpose; set => purpose = value; }
        public LinguisticVariable Reach { get => reach; set => reach = value; }
        public LinguisticVariable Sublist { get => sublist; set => sublist = value; }
        public IFuzzyEngine FuzzyEngine { get => fuzzyEngine; set => fuzzyEngine = value; }
        public List<object> Rules { get => rules; set => rules = value; }
    }
}