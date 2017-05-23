using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using TuriBoBot.Model;

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
        private string[] destinos = new string[33] {"Tafi del Valle","Mares del Sur","Rosario","Praya dos Ingleses",
                                            "Valparaiso","Punta del Este","Cartagena", "Camboriu","Orlando",
                                            "Miami","Bariloche","Mendoza","Caribe","Guayaquil","Islha do Mel",
                                            "Machu Pichu","Roma","Venecia","Paris","Valencia","Portugal","Grecia",
                                            "Egipto","Hawaii","Carlos Paz","Mar del Plata","Floreanopolis","Cancun",
                                            "Costa Rica","Las Vegas","Amsterdam","Tokyo","Australia"};

        // Requerimiento del motor -> para que se pueda fusificar
        [NonSerialized()] private ControladorDifuso fuzzy;
        [NonSerialized()] private ControladorDifuso2 fuzzy2;
        [NonSerialized()] private ControladorDifuso3 fuzzy3;

        public string[] Destinos { get => destinos; set => destinos = value; }

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
                        respuesta4 = Destinos[0];
                        break;
                    case 100:
                        respuesta4 = Destinos[1] + ", " + Destinos[2];
                        break;
                    case 150:
                        respuesta4 = Destinos[1] + ", " + Destinos[2];
                        break;
                    case 200:
                        respuesta4 = Destinos[4];
                        break;
                    case 250:
                        respuesta4 = Destinos[4] + ", " + Destinos[5];
                        break;
                    case 300:
                        respuesta4 = Destinos[3] + ", " + Destinos[6] + ", " + Destinos[7];
                        break;
                    case 350:
                        respuesta4 = "";
                        break;
                    case 400:
                        respuesta4 = Destinos[8] + ", " + Destinos[9];
                        break;
                    case 450:
                        respuesta4 = Destinos[8] + ", " + Destinos[9];
                        break;
                    case 500:
                        respuesta4 = Destinos[10] + ", " + Destinos[11];
                        break;
                    case 550:
                        respuesta4 = Destinos[11] + ", " + Destinos[25];
                        break;
                    case 600:
                        respuesta4 = Destinos[25];
                        break;
                    case 650:
                        respuesta4 = Destinos[15];
                        break;
                    case 700:
                        respuesta4 = Destinos[14] + ", " + Destinos[15];
                        break;
                    case 750:
                        respuesta4 = Destinos[14] + ", " + Destinos[12] + ", " + Destinos[13];
                        break;
                    case 800:
                        respuesta4 = Destinos[16] + ", " + Destinos[17] + ", " + Destinos[18];
                        break;
                    case 850:
                        respuesta4 = Destinos[16] + ", " + Destinos[17] + ", " + Destinos[18] + ", " + Destinos[19] + ", " +
                        Destinos[20] + ", " + Destinos[21] + ", " + Destinos[23];
                        break;
                    case 900:
                        respuesta4 = Destinos[22] + ", " + Destinos[19] + ", " + Destinos[20] + ", " + Destinos[23];
                        break;
                    case 950:
                        respuesta4 = Destinos[10];
                        break;
                    case 1000:
                        respuesta4 = Destinos[24] + ", " + Destinos[25];
                        break;
                    case 1050:
                        respuesta4 = Destinos[24] + ", " + Destinos[25];
                        break;
                    case 1100:
                        respuesta4 = Destinos[15];
                        break;
                    case 1150:
                        respuesta4 = Destinos[5] + ", " + Destinos[15];
                        break;
                    case 1200:
                        respuesta4 = Destinos[26] + ", " + Destinos[27] + ", " + Destinos[28];
                        break;
                    case 1250:
                        respuesta4 = Destinos[31] + ", " + Destinos[30];
                        break;
                    case 1300:
                        respuesta4 = Destinos[29] + ", " + Destinos[31] + ", " + Destinos[30];
                        break;
                    case 1350:
                        respuesta4 = Destinos[29] + ", " + Destinos[32];
                        break;
                    default:
                        respuesta4 = "";
                        break;
                }
                control = 0;
                await context.PostAsync("Les recomendamos los siguientes destinos: " + respuesta4.ToString());
                await context.PostAsync("Gracias por usar Turibot, que tenga un buen viaje y recuerde: " +
                                        "In my talons, I shape clay, crafting life forms as I please. If I wish, I can smash " +
                                        "it all. Around me is a burgeoning empire of steel. From my throne room, lines of power " +
                                        "careen into the skies of Earth. My whims will become lightning bolts that raze the mounds " +
                                        "of humanity. Out of the chaos, they will run and whimper, praying for me to end their " +
                                        "tedious anarchy. I am drunk with this vision. God: the title suits me well.");
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