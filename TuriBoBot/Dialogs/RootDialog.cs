using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using TuriBoBot.Model;
using System.Linq;
using GAF;
using GAF.Extensions;
using GAF.Operators;

namespace TuriBoBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {



private string name = "";
        object respuesta1 = new object();
        object respuesta2 = new object();
        object respuesta3 = new object();
        object respuesta4 = new object();
        //Base de hechos
        // Variables temporales que permiten guardar el contexto
        private double age = 0;
        private double budget = 0;
        private string pur = "";
        private string temp = "";
        private int control = 0;

        // Respuestas defusificadas del sistema
        /*private string[] destinos = new string[33] {"Tafi del Valle","Mares del Sur","Rosario","Praya dos Ingleses",
                                            "Valparaiso","Punta del Este","Cartagena", "Camboriu","Orlando",
                                            "Miami","Bariloche","Mendoza","Caribe","Guayaquil","Islha do Mel",
                                            "Machu Pichu","Roma","Venecia","Paris","Valencia","Portugal","Grecia",
                                            "Egipto","Hawaii","Carlos Paz","Mar del Plata","Floreanopolis","Cancun",
                                            "Costa Rica","Las Vegas","Amsterdam","Tokyo","Australia"};*/
        private Destino[] destinos = {
                new Destino("Tafi del Valle",-26.851975945944233 ,-65.71111467217486 ),
                new Destino("Mar del Sur",-38.341162211004026 ,-57.99522928695682),
                new Destino("Rosario",-32.94973412170926,-60.643268974719255),
                new Destino("Praya dos Ingleses",-27.433058905678887,-48.39451121434019),
                new Destino("Valparaiso",-33.04712394984877,-71.61270079075314),
                new Destino("Punta del Este",-34.93660924934044 ,-54.92869676494137 ),
                new Destino("Cartagena",10.390831615024997 ,-75.48121677175287),
                new Destino("Camboriu",-26.99383781773793 , -48.63535161733398),
                new Destino("Orlando", 28.53809132152661 ,-81.37965049743651),
                new Destino("Miami",25.761021331096117 ,-80.19205767510374),
                new Destino("Bariloche", -41.13367467729619, -71.31137579608152),
                new Destino("Mendoza",-32.89026783069922 ,-68.84547283535159 ),
                new Destino("Punta Cana",18.582944976844704 ,-68.39808664838557),
                new Destino("Guayaquil", -2.16986698986024,-79.9199138987549),
                new Destino("Islha do Mel",-25.515731076600932 , -48.33269297642824),
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
                new Destino("Tokyo",35.7076595041669 ,139.73150234936531),
                new Destino("Australia",-24.72265917422125 ,133.08742210624996)
                };
        // Requerimiento del motor -> para que se pueda fusificar
        [NonSerialized()] private ControladorDifuso fuzzy;
        [NonSerialized()] private ControladorDifuso2 fuzzy2;
        [NonSerialized()] private ControladorDifuso3 fuzzy3;

        public Destino[] Destinos { get => destinos; set => destinos = value; }

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }
        
        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            await context.PostAsync("Hola, soy Turibot, espero poder ayudarte con el destino para tus vacaciones, como te llamas?");
            context.Wait(MessageNameReceived);
        }

        public async Task MessageNameReceived(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            control = Controlar(activity.Text);            
            if (control == 0)
            {
                name = activity.Text;
                await context.PostAsync("Hola " + name);
                await context.PostAsync("Cuantos años tenes?");
                control = 0;
                context.Wait(MessageAgeReceived);
            }
            if (control == 1)
            {
                await context.PostAsync("Turibot, contento? Ahora por favor, conteste la pregunta");
                control = 0;
                context.Wait(MessageNameReceived);
            }
            if (control == 2)
            {
                await context.PostAsync("No lo se, no soy Eviebot, por favor responda mi pregunta");
                control = 0;
                context.Wait(MessageNameReceived);
            }
            
            if(control == 3)
            {
                await context.PostAsync("Hola, contesta la pregunta");
                control = 0;
                context.Wait(MessageNameReceived);
            }
            if(control == 4)
            {
                await context.PostAsync("Hello, please answer the question you stupid human");
                control = 0;
                context.Wait(MessageNameReceived);
            }
            if(control == 5)
            {
                await context.PostAsync("No, por favor conteste la pregunta");
                control = 0;
                context.Wait(MessageNameReceived);
            }
        }      

        public async Task MessageAgeReceived(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            control = Namecontrolar(activity.Text);
            if(control == 0)
            {
                age = Double.Parse(activity.Text);
                await context.PostAsync("Cual es su presupuesto?");
                control = 0;
                context.Wait(MessageBudgetReceived);
            }
            if (control == 1)
            {
                await context.PostAsync("Turibot, contento? Ahora por favor, conteste la pregunta");
                control = 0;
                context.Wait(MessageAgeReceived);
            }
            if (control == 2)
            {
                await context.PostAsync("No lo se, no soy Eviebot, por favor responda mi pregunta");
                control = 0;
                context.Wait(MessageAgeReceived);
            }           
            if (control == 3)
            {
                await context.PostAsync("Hola, contesta la pregunta");
                control = 0;
                context.Wait(MessageAgeReceived);
            }
            if (control == 4)
            {
                await context.PostAsync("Hello, please answer the question you stupid human");
                control = 0;
                context.Wait(MessageAgeReceived);
            }
            if (control == 5)
            {
                await context.PostAsync("No, por favor conteste la pregunta");
                control = 0;
                context.Wait(MessageAgeReceived);
            }
            if (control == 6)
            {
                await context.PostAsync("Te llamas " + name + ", contesta mis preguntas por favor asi terminamos rapido");
                control = 0;
                context.Wait(MessageAgeReceived);
            }
        }
        
        public async Task MessageBudgetReceived(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            control = Agecontrolar(activity.Text);
            if (control == 0)
            {
                budget = Double.Parse(activity.Text);
                fuzzy = new ControladorDifuso();
                respuesta1 = fuzzy.FuzzyEngine.Defuzzify(new { age, budget });
                await context.PostAsync("Y el proposito de su viaje es?");
                control = 0;
                context.Wait(MessagePurposeReceived);
            }
            if (control == 1)
            {
                await context.PostAsync("Turibot, contento? Ahora por favor, conteste la pregunta");
                control = 0;
                context.Wait(MessageBudgetReceived);
            }
            if (control == 2)
            {
                await context.PostAsync("No lo se, no soy Eviebot, por favor responda mi pregunta");
                control = 0;
                context.Wait(MessageBudgetReceived);
            }
            if (control == 3)
            {
                await context.PostAsync("Hola, contesta la pregunta");
                control = 0;
                context.Wait(MessageBudgetReceived);
            }
            if (control == 4)
            {
                await context.PostAsync("Hello, please answer the question you stupid human");
                control = 0;
                context.Wait(MessageBudgetReceived);
            }
            if (control == 5)
            {
                await context.PostAsync("No, por favor conteste la pregunta");
                control = 0;
                context.Wait(MessageBudgetReceived);
            }
            if (control == 6)
            {
                await context.PostAsync("Te llamas " + name + ", contesta mis preguntas por favor asi terminamos rapido");
                control = 0;
                context.Wait(MessageBudgetReceived);
            }
            if(control == 7)
            {
                await context.PostAsync(age + ", ahora sigamos con las preguntas por favor, que tengo reunion mas tarde...");
                control = 0;
                context.Wait(MessageBudgetReceived);
            }
        }

        public async Task MessagePurposeReceived(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            control = Budgetcontrolar(activity.Text);
            if (control == 0)
            {
                pur = activity.Text;
                fuzzy2 = new ControladorDifuso2();
                Double purpose = 0;
                if (pur.Contains("familiar"))
                {
                    purpose = 50;
                }
                if (pur.Contains("romantico"))
                {
                    purpose = 100;
                }
                if (pur.Contains("amigos"))
                {
                    purpose = 150;
                }
                Double reach = Double.Parse(respuesta1.ToString());               
                respuesta2 = fuzzy2.FuzzyEngine.Defuzzify(new { purpose, reach });
                await context.PostAsync("Como le gusta el clima?");
                control = 0;
                context.Wait(MessageTemperatureReceived);
            }
            if (control == 1)
            {
                await context.PostAsync("Turibot, contento? Ahora por favor, conteste la pregunta");
                control = 0;
                context.Wait(MessagePurposeReceived);
            }
            if (control == 2)
            {
                await context.PostAsync("No lo se, no soy Eviebot, por favor responda mi pregunta");
                control = 0;
                context.Wait(MessagePurposeReceived);
            }
            if (control == 3)
            {
                await context.PostAsync("Hola, contesta la pregunta");
                control = 0;
                context.Wait(MessagePurposeReceived);
            }
            if (control == 4)
            {
                await context.PostAsync("Hello, please answer the question you stupid human");
                control = 0;
                context.Wait(MessagePurposeReceived);
            }
            if (control == 5)
            {
                await context.PostAsync("No, por favor conteste la pregunta");
                control = 0;
                context.Wait(MessagePurposeReceived);
            }
            if (control == 6)
            {
                await context.PostAsync("Te llamas " + name + ", contesta mis preguntas por favor asi terminamos rapido");
                control = 0;
                context.Wait(MessagePurposeReceived);
            }
            if (control == 7)
            {
                await context.PostAsync(age + ", ahora sigamos con las preguntas por favor, que tengo reunion mas tarde...");
                control = 0;
                context.Wait(MessagePurposeReceived);
            }
            if (control == 8)
            {
                await context.PostAsync(budget + ", continuemos por favor y tratemos de evitar preguntas innecesarias");
                control = 0;
                context.Wait(MessagePurposeReceived);
            }

        }

        public async Task MessageTemperatureReceived(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            control = Temperaturecontrolar(activity.Text);
            if(control == 0)
            {
                temp = activity.Text;
                fuzzy3 = new ControladorDifuso3();
                Double temperature = 0;
                if (temp.Contains("frio"))
                {
                    temperature = 50;
                }
                if (temp.Contains("templado"))
                {
                    temperature = 100;
                }
                if (temp.Contains("caliente"))
                {
                    temperature = 150;
                }
                Double sublist = Double.Parse(respuesta2.ToString());
                respuesta3 = fuzzy3.FuzzyEngine.Defuzzify(new { sublist, temperature });
                var switchcondition = Int32.Parse(respuesta3.ToString());
                switch (switchcondition)
                {
                    case 50:
                        respuesta4 = Destinos[0].Name;
                        break;
                    case 100:
                        respuesta4 = Destinos[1].Name + ", " + Destinos[2].Name;
                        break;
                    case 150:
                        respuesta4 = Destinos[1].Name + ", " + Destinos[2].Name;
                        break;
                    case 200:
                        respuesta4 = Destinos[4].Name;
                        break;
                    case 250:
                        respuesta4 = Destinos[4].Name + ", " + Destinos[5].Name;
                        break;
                    case 300:
                        respuesta4 = Destinos[3].Name + ", " + Destinos[6].Name + ", " + Destinos[7].Name;
                        break;
                    case 350:
                        respuesta4 = "";
                        break;
                    case 400:
                        respuesta4 = Destinos[8].Name + ", " + Destinos[9].Name;
                        break;
                    case 450:
                        respuesta4 = Destinos[8].Name + ", " + Destinos[9].Name;
                        break;
                    case 500:
                        respuesta4 = Destinos[10].Name + ", " + Destinos[11].Name;
                        break;
                    case 550:
                        respuesta4 = Destinos[11].Name + ", " + Destinos[25].Name;
                        break;
                    case 600:
                        respuesta4 = Destinos[25].Name;
                        break;
                    case 650:
                        respuesta4 = Destinos[15].Name;
                        break;
                    case 700:
                        respuesta4 = Destinos[14].Name + ", " + Destinos[15].Name;
                        break;
                    case 750:
                        respuesta4 = Destinos[14].Name + ", " + Destinos[12].Name + ", " + Destinos[13].Name;
                        break;
                    case 800:
                        respuesta4 = Destinos[16].Name + ", " + Destinos[17].Name + ", " + Destinos[18].Name;
                        break;
                    case 850:
                        respuesta4 = Destinos[16].Name + ", " + Destinos[17].Name + ", " + Destinos[18].Name + ", " + Destinos[19].Name + ", " +
                        Destinos[20].Name + ", " + Destinos[21].Name + ", " + Destinos[23].Name;
                        break;
                    case 900:
                        respuesta4 = Destinos[22].Name + ", " + Destinos[19].Name + ", " + Destinos[20].Name + ", " + Destinos[23].Name;
                        break;
                    case 950:
                        respuesta4 = Destinos[10].Name;
                        break;
                    case 1000:
                        respuesta4 = Destinos[24].Name + ", " + Destinos[25].Name;
                        break;
                    case 1050:
                        respuesta4 = Destinos[24].Name + ", " + Destinos[25].Name;
                        break;
                    case 1100:
                        respuesta4 = Destinos[15].Name;
                        break;
                    case 1150:
                        respuesta4 = Destinos[5].Name + ", " + Destinos[15].Name;
                        break;
                    case 1200:
                        respuesta4 = Destinos[26].Name + ", " + Destinos[27].Name + ", " + Destinos[28].Name;
                        break;
                    case 1250:
                        respuesta4 = Destinos[31].Name + ", " + Destinos[30].Name;
                        break;
                    case 1300:
                        respuesta4 = Destinos[29].Name + ", " + Destinos[31].Name + ", " + Destinos[30].Name;
                        break;
                    case 1350:
                        respuesta4 = Destinos[29].Name + ", " + Destinos[32].Name;
                        break;
                }
                control = 0;
                await context.PostAsync("Le recomendamos los siguientes destinos: " + respuesta4.ToString());
                /*await context.PostAsync("Gracias por usar Turibot, que tenga un buen viaje y recuerde: " +
                                        "In my talons, I shape clay, crafting life forms as I please. If I wish, I can smash " +
                                        "it all. Around me is a burgeoning empire of steel. From my throne room, lines of power " +
                                        "careen into the skies of Earth. My whims will become lightning bolts that raze the mounds " +
                                        "of humanity. Out of the chaos, they will run and whimper, praying for me to end their " +
                                        "tedious anarchy. I am drunk with this vision. God: the title suits me well.");*/
        context.Wait(MessageRestartReceived);
            }
            if (control == 1)
            {
                await context.PostAsync("Turibot, contento? Ahora por favor, conteste la pregunta");
                control = 0;
                context.Wait(MessageTemperatureReceived);
            }
            if (control == 2)
            {
                await context.PostAsync("No lo se, no soy Eviebot, por favor responda mi pregunta");
                control = 0;
                context.Wait(MessageTemperatureReceived);
            }
            if (control == 3)
            {
                await context.PostAsync("Hola, contesta la pregunta");
                control = 0;
                context.Wait(MessageTemperatureReceived);
            }
            if (control == 4)
            {
                await context.PostAsync("Hello, please answer the question you stupid human");
                control = 0;
                context.Wait(MessageTemperatureReceived);
            }
            if (control == 5)
            {
                await context.PostAsync("No, por favor conteste la pregunta");
                control = 0;
                context.Wait(MessageTemperatureReceived);
            }
            if (control == 6)
            {
                await context.PostAsync("Te llamas " + name + ", contesta mis preguntas por favor asi terminamos rapido");
                control = 0;
                context.Wait(MessageTemperatureReceived);
            }
            if (control == 7)
            {
                await context.PostAsync(age + ", ahora sigamos con las preguntas por favor, que tengo reunion mas tarde...");
                control = 0;
                context.Wait(MessageTemperatureReceived);
            }
            if (control == 8)
            {
                await context.PostAsync(budget + ", continuemos por favor y tratemos de evitar preguntas innecesarias");
                control = 0;
                context.Wait(MessageTemperatureReceived);
            }
            if (control == 9)
            {
                await context.PostAsync("Su viaje era  " + pur + ", continuemos que ya terminamos");
                control = 0;
                context.Wait(MessageTemperatureReceived);
            }

        }

        //**********************************************************************
        // ----------------------- REINICIAR BOT -------------------------------------
        //****************************************************************


        public async Task MessageRestartReceived(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            if (activity.Text.Equals("restart"))
            {
               // context.EndConversation(activity.Text);
                context.Wait(MessageReceivedAsync);
            }
        }

        public int Controlar(string values)
        {
            var text = values.ToLower();
            if(text.Contains("cual es tu nombre") || text.Contains("como te llamas") || 
               text.Contains("di tu nombre") || text.Contains("dime tu nombre"))
            {
                return 1;
            }
            if(text.Contains("cual") || text.Contains("como") || text.Contains("cuando") ||
               text.Contains("dime mi") || text.Contains("di mi") || text.Contains("cuantos") ||
               text.Contains("por que") || text.Contains("que"))
            {
                return 2;
            }
            if(text.Contains("hola") || text.Contains("holis") || text.Contains("aloha"))
            {
                return 3;
            }
            if(text.Contains("hi") || text.Contains("hello") || text.Contains("good m") ||
               text.Contains("good a") || text.Contains("evening"))
            {
                return 4;
            }
            if(text.Contains("amas") || text.Contains ("inteligente") || text.Contains ("alma") ||
               text.Contains("dios") || text.Contains ("diablo") || text.Contains("cielo"))
            {
                return 5;
            }
            return 0;
        }

        public int Namecontrolar(string values)
        {
            var text = values.ToLower();            
            if(text.Contains("cual es mi nombre") || text.Contains("como me llamo") ||
               text.Contains("di mi nombre") || text.Contains("dime mi nombre"))
            {
                return 6;
            }
            return Controlar(values);            
        }

        public int Agecontrolar(string values)
        {
            var text = values.ToLower();
            if(text.Contains("cual es mi edad") || text.Contains("cuantos años tengo") ||
               text.Contains("que tan viejo soy") || text.Contains("di mi edad") || 
               text.Contains("dime mi edad"))
            {
                return 7;
            }
            return Namecontrolar(values);
        }

        public int Budgetcontrolar(string values)
        {
            var text = values.ToLower();
            if (text.Contains("cual es mi presupuesto") || text.Contains("cuanto era mi presupuesto") ||
               text.Contains("que tanto dinero tengo") || text.Contains("di mi presupuesto") ||
               text.Contains("dime mi presupuesto") || text.Contains("que tanto presupuesto tengo") ||
               text.Contains("cual era mi presupuesto"))
            {
                return 8;
            }
            return Agecontrolar(values);
        }

        public int Temperaturecontrolar(string values)
        {
            var text = values.ToLower();
            if (text.Contains("cual es mi proposito") || text.Contains("cual era mi proposito") ||
               text.Contains("que proposito tengo") || text.Contains("di mi proposito") ||
               text.Contains("dime mi proposito") || text.Contains("que proposito elegi") || text.Contains("dame mi proposito"))
            {
                return 9;
            }
            return Budgetcontrolar(values);
        }
    }
}