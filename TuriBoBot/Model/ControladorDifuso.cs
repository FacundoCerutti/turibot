using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FLS;
using FLS.Rules;

namespace TuriBoBot.Model
{
    public class ControladorDifuso 
    {

        private IFuzzyEngine fuzzyEngine = new FuzzyEngineFactory().Default();
        private LinguisticVariable age = new LinguisticVariable("age");
        private LinguisticVariable budget = new LinguisticVariable("budget");        
        private LinguisticVariable reach = new LinguisticVariable("reach");
        
        
        
        private List<object> rules = new List<object>();        
        
        

        public ControladorDifuso()
        {
            //Variables Linguisticas
            // Edad
            var vyoung = Age.MembershipFunctions.AddTriangle("muy joven", 15, 15, 18);
            var young = Age.MembershipFunctions.AddTriangle("joven", 15, 18, 30);
            var adult = Age.MembershipFunctions.AddTriangle("adulto", 18, 30, 50);
            var mature = Age.MembershipFunctions.AddTriangle("mayor", 30, 50, 80);           
            // Presupuesto
            var low = Budget.MembershipFunctions.AddTriangle("bajo", 1000, 1000, 7000);
            var medium = Budget.MembershipFunctions.AddTriangle("medio", 5000, 7000, 20000);
            var high = Budget.MembershipFunctions.AddTriangle("alto", 18000, 20000, 40000);
            var vhigh = Budget.MembershipFunctions.AddTriangle("muy alto", 35000, 40000, 100000);
            // Alcance
            var national = Reach.MembershipFunctions.AddTriangle("nacional", 0, 50, 100);
            var latin = Reach.MembershipFunctions.AddTriangle("latino", 50, 100, 150);
            var international = Reach.MembershipFunctions.AddTriangle("internacional", 100, 150, 200);


            //Reglas
            // Edad/Presupuesto = Alcance
            // Muy joven
            Rules.Add(Rule.If(Age.Is(vyoung).And(Budget.Is(low))).Then(Reach.Is(national)));                    //rule1            
            Rules.Add(Rule.If(Age.Is(vyoung).And(Budget.Is(medium))).Then(Reach.Is(national)));                 //rule2
            Rules.Add(Rule.If(Age.Is(vyoung).And(Budget.Is(high))).Then(Reach.Is(latin)));                      //rule3
            Rules.Add(Rule.If(Age.Is(vyoung).And(Budget.Is(vhigh))).Then(Reach.Is(international)));             //rule4
            // Joven
            Rules.Add(Rule.If(Age.Is(young).And(Budget.Is(low))).Then(Reach.Is(national)));                     //rule5
            Rules.Add(Rule.If(Age.Is(young).And(Budget.Is(medium))).Then(Reach.Is(latin)));                     //rule6
            Rules.Add(Rule.If(Age.Is(young).And(Budget.Is(high))).Then(Reach.Is(latin)));                       //rule7
            Rules.Add(Rule.If(Age.Is(young).And(Budget.Is(vhigh))).Then(Reach.Is(international)));              //rule8
            // Adulto
            Rules.Add(Rule.If(Age.Is(adult).And(Budget.Is(low))).Then(Reach.Is(national)));                     //rule9
            Rules.Add(Rule.If(Age.Is(adult).And(Budget.Is(medium))).Then(Reach.Is(latin)));                     //rule10
            Rules.Add(Rule.If(Age.Is(adult).And(Budget.Is(high))).Then(Reach.Is(international)));               //rule11
            Rules.Add(Rule.If(Age.Is(adult).And(Budget.Is(vhigh))).Then(Reach.Is(international)));              //rule12
            // Mayor
            Rules.Add(Rule.If(Age.Is(mature).And(Budget.Is(low))).Then(Reach.Is(national)));                    //rule13
            Rules.Add(Rule.If(Age.Is(mature).And(Budget.Is(medium))).Then(Reach.Is(latin)));                    //rule14
            Rules.Add(Rule.If(Age.Is(mature).And(Budget.Is(high))).Then(Reach.Is(international)));              //rule15
            Rules.Add(Rule.If(Age.Is(mature).And(Budget.Is(vhigh))).Then(Reach.Is(international)));             //rule16
            
            
            //Adding the rules to the Fuzzy Engine
            foreach(FuzzyRule rule in Rules)
            {
                FuzzyEngine.Rules.Add(rule);               
            }            
        }

       

        
        public LinguisticVariable Budget { get => budget; set => budget = value; }
        public LinguisticVariable Age { get => age; set => age = value; }
        public IFuzzyEngine FuzzyEngine { get => fuzzyEngine; set => fuzzyEngine = value; }
        public LinguisticVariable Reach { get => reach; set => reach = value; }
        
        
        public List<object> Rules { get => rules; set => rules = value; }
       
               
    }
}