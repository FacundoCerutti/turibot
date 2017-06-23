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

        //Base de hechos
        // Variables temporales que permiten guardar el contexto
        private double age = 0;
        private double budget = 0;
        private string pur = "";
        private string temp = "";
        private double mut = 0;
        private int terminflag = 0;
        private int termin = 0;        
        private int control = 0;

        // Respuestas defusificadas del sistema
        


        private List<Destino> respuesta4 = new List<Destino>();
        public List<Destino> Respuesta4 { get => respuesta4; set => respuesta4 = value; }    


        private Destino[] destinos = {
                new Destino("Tafi del Valle",-26.851975945944233 ,-65.71111467217486), //0
                new Destino("San Pedro de Colalao",-26.851975945944233 ,-65.71111467217486), //1 SIN LAT LONG
                new Destino("Tilcara",-26.851975945944233 ,-65.71111467217486), //2 SIN LAT LONG
                new Destino("Salta",-24.786047683039044 ,-65.42148768167725), //3
                new Destino("Mar del Sur",-38.341162211004026 ,-57.99522928695682), //4
                new Destino("Rosario",-32.94973412170926,-60.643268974719255), //5
                new Destino("Mendoza",-32.89026783069922 ,-68.84547283535159 ), //6
                new Destino("CABA",-32.94973412170926,-60.643268974719255), //7 SIN LAT LONG
                new Destino("Mar del Plata",-38.01764738461685 ,-57.600534049999965), //8
                new Destino("Chapadmalal",-38.173056012484466,-57.65194399999996), //9
                new Destino("San Clemente del Tuyu",-32.94973412170926,-60.643268974719255), //10 SIN LAT LONG
                new Destino("Valparaiso",-33.04712394984877,-71.61270079075314), //11
                new Destino("Bariloche", -41.13367467729619, -71.31137579608152), //12
                new Destino("Rio Gallegos",-51.62850216426361,-69.27174925000003), //13
                new Destino("Punta del Este",-34.93660924934044 ,-54.92869676494137 ), //14
                new Destino("Gualeguaychu", -2.16986698986024,-79.9199138987549), //15 SIN LAT LONG
                new Destino("Iquique",-20.244825658942073,-70.13884915), //16 
                new Destino("Praia dos Ingleses",-27.433058905678887,-48.39451121434019), //17
                new Destino("Puerto Iguazu",-32.94973412170926,-60.643268974719255), //18 SIN LAT LONG
                new Destino("Camboriu",-26.99383781773793 , -48.63535161733398), //19
                new Destino("Encarnacion",-32.94973412170926,-60.643268974719255), //20 Sin LAT LONG
                new Destino("Amsterdam",52.37464788349741 ,4.898614199999997), //21
                new Destino("Copenaghue",55.67130717687403,12.560838799999942), //22
                new Destino("Reykjavik",55.67130717687403,12.560838799999942), //23 Sin Lat Long
                new Destino("Londres",51.528868434293244,-0.10161819999996169), //24 Sin LAT LONG
                new Destino("Orlando", 28.53809132152661 ,-81.37965049743651), //25
                new Destino("Miami",25.761021331096117 ,-80.19205767510374), //26
                new Destino("Nueva York",40.70597954587119,-73.9780035), //27
                new Destino("Branson",40.70597954587119,-73.9780035), //28 SIN LAT LONG
                new Destino("Cancun",21.121371172643965 ,-86.84931025000003), //29
                new Destino("Punta Cana",18.582944976844704 ,-68.39808664838557), //30
                new Destino("Villa Pehuenia",-32.94973412170926,-60.643268974719255), //31 SIN LAT LONG
                new Destino("Fin del Mundo",-32.94973412170926,-60.643268974719255), //32 SIN LAT LONG
                new Destino("La Cumbre",-32.94973412170926,-60.643268974719255), //33 SIN LAT LONG
                new Destino("El Delta",-32.94973412170926,-60.643268974719255), //34 SIN LAT LONG
                new Destino("Pinamar",-27.433058905678887,-48.39451121434019), //35 SIN LAT LONG
                new Destino("Carlos Paz", -31.41222809215237,-64.49973669999997), //36
                new Destino("Miramar",-27.433058905678887,-48.39451121434019), //37 SIN LAT LONG
                new Destino("Puerto Natales",-27.433058905678887,-48.39451121434019), //38 SIN LAT LONG
                new Destino("Santiago de Chile",-33.47302199866159,-70.62983129999998), //39 
                new Destino("La Paz",-16.520736280532606,-68.0915129), //40
                new Destino("Montevideo",-34.82002728272268,-56.23020604999999), //41
                new Destino("Machu Pichu",-13.163141203713916 ,-72.54496289999997), //42
                new Destino("Quito",-33.04712394984877,-71.61270079075314), //43 SIN LAT LONG
                new Destino("Lima",35.7076595041669 ,139.73150234936531), //44 SIN LAT LONG
                new Destino("Buzios",-34.93660924934044 ,-54.92869676494137 ), //45 SIN LAT LONG
                new Destino("Guayaquil", -2.16986698986024,-79.9199138987549), //46
                new Destino("Isla del Sol",-24.72265917422125 ,133.08742210624996), //47 SIN LAT LONG
                new Destino("Roma",41.902742 ,12.496242), //48
                new Destino("Venecia",45.433776016477786 ,12.327938325351639), //49
                new Destino("Paris", 48.85542056220146,2.345192532753355), //50
                new Destino("Portugal", 38.712702401270796,-9.14286366823353), //51
                new Destino("San Andres",-38.01764738461685 ,-57.600534049999965), //52 SIN LAT LONG
                new Destino("Isla Margarita",-24.786047683039044 ,-65.42148768167725), //53 SIN LAT LONG
                new Destino("Amaicha",-24.786047683039044 ,-65.42148768167725), //54 SIN LAT LONG
                new Destino("Tigre",39.46701475595226 ,-0.371034483107342), //55 SIN LAT LONG
                new Destino("Villa Gesell",-37.26077166271962 , -56.97232467846674), //56
                new Destino("Piedra del Aguila",-40.04724751557836,-70.07602309999999), //57
                new Destino("Florianopolis", -27.614479856117843,-48.48282474999996), //58
                new Destino("Caioba",10.390831615024997 ,-75.48121677175287), //59 SIN LAT LONG
                new Destino("Rio de Janeiro",-22.91133405231811,-43.448333950000006), //60
                new Destino("Natal",37.974319357400795 ,23.73644296320502), //61 SIN LAT LONG
                new Destino("Hamburgo", 24.663164421891352, -77.92157250156248), //62 SIN LAT LONG
                new Destino("Roterdam",-38.55491790589638,-58.73876668955076), //63 SIN LAT LONG
                new Destino("Las Vegas", 36.12519506431158,-115.17499999999995), //64
                new Destino("Los Angeles",34.02109014442116,-118.41173249999997), //65
                new Destino("Acapulco",-38.55491790589638,-58.73876668955076), //66 SIN LAT LONG
                new Destino("Mexico DF",10.390831615024997 ,-75.48121677175287), //67 SIN LAT LONG
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

            if (control == 3)
            {
                await context.PostAsync("Hola, contesta la pregunta");
                control = 0;
                context.Wait(MessageNameReceived);
            }
            if (control == 4)
            {
                await context.PostAsync("Hello, please answer the question you stupid human");
                control = 0;
                context.Wait(MessageNameReceived);
            }
            if (control == 5)
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
            if (control == 0)
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
            if (control == 7)
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
                await context.PostAsync("Que tan mutable es la poblacion?");
                control = 0;
                context.Wait(MessageMutReceived);
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

        public async Task MessageMutReceived(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            mut = Double.Parse(activity.Text);
            await context.PostAsync("Como quiere que corte?");
            context.Wait(MessageTerminateReceived);
        }

        public async Task MessageTerminateReceived(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            if(activity.Text.Contains("tiempo"))
            {
                terminflag = 1;
                await context.PostAsync("Cuanto tiempo correra...?");
                context.Wait(MessageTerminateFlag1Received);
            }
            if(activity.Text.Contains("corridas"))
            {  
                terminflag = 2;
                await context.PostAsync("Cuantas corridas quiere...?");
                context.Wait(MessageTerminateFlag2Received);
            }           
        }

        public async Task MessageTerminateFlag1Received(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            termin = Double.Parse(activity.Text);
            await context.PostAsync("Como le gusta el clima?");
            context.Wait(MessageTemperatureReceived);
        }

        public async Task MessageTerminateFlag2Received(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            termin = Double.Parse(activity.Text);
            await context.PostAsync("Como le gusta el clima?");
            context.Wait(MessageTemperatureReceived);
        }

        public async Task MessageTemperatureReceived(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            control = Temperaturecontrolar(activity.Text);
            if (control == 0)
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
                        respuesta4.Add(Destinos[0]);
                        respuesta4.Add(Destinos[1]);
                        respuesta4.Add(Destinos[2]);
                        respuesta4.Add(Destinos[3]);
                        break;
                    case 100:
                        respuesta4.Add(Destinos[4]);
                        respuesta4.Add(Destinos[5]);
                        respuesta4.Add(Destinos[6]);
                        respuesta4.Add(Destinos[7]);

                        break;
                    case 150:
                        respuesta4.Add(Destinos[4]);
                        respuesta4.Add(Destinos[8]);
                        respuesta4.Add(Destinos[9]);
                        respuesta4.Add(Destinos[10]);

                
                break;
                    case 200:
                        respuesta4.Add(Destinos[11]);
                        respuesta4.Add(Destinos[12]);
                        respuesta4.Add(Destinos[13]);
                        respuesta4.Add(Destinos[8]);

                        break;
                    case 250:
                        respuesta4.Add(Destinos[11]);
                        respuesta4.Add(Destinos[14]);
                        respuesta4.Add(Destinos[15]);
                        respuesta4.Add(Destinos[16]);

                        break;
                    case 300:
                        respuesta4.Add(Destinos[17]);
                        respuesta4.Add(Destinos[18]);
                        respuesta4.Add(Destinos[19]);
                        respuesta4.Add(Destinos[20]);
                        break;
                    case 350:
                        respuesta4.Add(Destinos[21]);
                        respuesta4.Add(Destinos[22]);
                        respuesta4.Add(Destinos[23]);
                        respuesta4.Add(Destinos[24]);

                        break;
                    case 400:
                        respuesta4.Add(Destinos[25]);
                        respuesta4.Add(Destinos[26]);
                        respuesta4.Add(Destinos[27]);
                        respuesta4.Add(Destinos[28]);
                        break;
                    case 450:
                        respuesta4.Add(Destinos[25]);
                        respuesta4.Add(Destinos[26]);
                        respuesta4.Add(Destinos[29]);
                        respuesta4.Add(Destinos[30]);

                        break;
                    case 500: //4f
                        respuesta4.Add(Destinos[12]);
                        respuesta4.Add(Destinos[6]);
                        respuesta4.Add(Destinos[31]);
                        respuesta4.Add(Destinos[32]);

                        break;
                    case 550: //4t
                        respuesta4.Add(Destinos[6]);
                        respuesta4.Add(Destinos[8]);
                        respuesta4.Add(Destinos[33]);
                        respuesta4.Add(Destinos[34]);

                        break;
                    case 600: //4c
                        respuesta4.Add(Destinos[8]);
                        respuesta4.Add(Destinos[35]);
                        respuesta4.Add(Destinos[36]);
                        respuesta4.Add(Destinos[37]);

                        break;
                    case 650://5f
                        respuesta4.Add(Destinos[38]);
                        respuesta4.Add(Destinos[18]);
                        respuesta4.Add(Destinos[46]);
                        respuesta4.Add(Destinos[47]);

                        break;
                    case 700://5t
                        respuesta4.Add(Destinos[41]);
                        respuesta4.Add(Destinos[42]);
                        respuesta4.Add(Destinos[43]);
                        respuesta4.Add(Destinos[44]);

                        break;
                    case 750://5c
                        respuesta4.Add(Destinos[45]);
                        respuesta4.Add(Destinos[18]);
                        respuesta4.Add(Destinos[46]);
                        respuesta4.Add(Destinos[47]);

                        break;
                    case 800://6f
                        respuesta4.Add(Destinos[48]);
                        respuesta4.Add(Destinos[49]);
                        respuesta4.Add(Destinos[50]);
                        respuesta4.Add(Destinos[21]);

                        break;
                    case 850://6t
                        respuesta4.Add(Destinos[48]);
                        respuesta4.Add(Destinos[49]);
                        respuesta4.Add(Destinos[50]);
                        respuesta4.Add(Destinos[51]);

                        break;
                    case 900://6c
                        respuesta4.Add(Destinos[29]);
                        respuesta4.Add(Destinos[30]);
                        respuesta4.Add(Destinos[52]);
                        respuesta4.Add(Destinos[53]);

                        break;
                    case 950://7f
                        respuesta4.Add(Destinos[0]);
                        respuesta4.Add(Destinos[1]);
                        respuesta4.Add(Destinos[3]);
                        respuesta4.Add(Destinos[54]);

                        break;
                    case 1000://7t
                        respuesta4.Add(Destinos[5]);
                        respuesta4.Add(Destinos[8]);
                        respuesta4.Add(Destinos[36]);
                        respuesta4.Add(Destinos[55]);
    
                        break;
                    case 1050://7c
                        respuesta4.Add(Destinos[8]);
                        respuesta4.Add(Destinos[35]);
                        respuesta4.Add(Destinos[37]);
                        respuesta4.Add(Destinos[56]);

                        break;
                    case 1100://8f
                        respuesta4.Add(Destinos[12]);
                        respuesta4.Add(Destinos[16]);
                        respuesta4.Add(Destinos[42]);
                        respuesta4.Add(Destinos[57]);

                        break;
                    case 1150://8t
                        respuesta4.Add(Destinos[14]);
                        respuesta4.Add(Destinos[16]);
                        respuesta4.Add(Destinos[42]);
                        respuesta4.Add(Destinos[43]);

                        break;
                    case 1200://8c
                        respuesta4.Add(Destinos[58]);
                        respuesta4.Add(Destinos[59]);
                        respuesta4.Add(Destinos[60]);
                        respuesta4.Add(Destinos[61]);

                        break;
                    case 1250://9f
                        respuesta4.Add(Destinos[21]);
                        respuesta4.Add(Destinos[48]);
                        respuesta4.Add(Destinos[62]);
                        respuesta4.Add(Destinos[63]);

                        break;
                    case 1300://9t
                        respuesta4.Add(Destinos[25]);
                        respuesta4.Add(Destinos[26]);
                        respuesta4.Add(Destinos[64]);
                        respuesta4.Add(Destinos[65]);

                        break;
                    case 1350://9c
                        respuesta4.Add(Destinos[29]);
                        respuesta4.Add(Destinos[30]);
                        respuesta4.Add(Destinos[66]);
                        respuesta4.Add(Destinos[67]);

                        break;
                }
                control = 0;
                try { 
                Genetico genetics = new Genetico(respuesta4,mut,terminflag,termin);
                await context.PostAsync("Su ruta ideal es: ");
                foreach (string s in genetics.ListaF)
                {
                    await context.PostAsync(s);
                }
                await context.PostAsync("La Fitness y Distancia son: ");
                foreach (object o in genetics.AtributosF)
                {
                    await context.PostAsync(o.ToString());
                }                
                context.Wait(MessageRestartReceived);
                }
                catch(Exception e)
                {
                    await context.PostAsync(e.ToString());
                }
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
            if (text.Contains("cual es tu nombre") || text.Contains("como te llamas") ||
               text.Contains("di tu nombre") || text.Contains("dime tu nombre"))
            {
                return 1;
            }
            if (text.Contains("cual") || text.Contains("como") || text.Contains("cuando") ||
               text.Contains("dime mi") || text.Contains("di mi") || text.Contains("cuantos") ||
               text.Contains("por que") || text.Contains("que"))
            {
                return 2;
            }
            if (text.Contains("hola") || text.Contains("holis") || text.Contains("aloha"))
            {
                return 3;
            }
            if (text.Contains("hi") || text.Contains("hello") || text.Contains("good m") ||
               text.Contains("good a") || text.Contains("evening"))
            {
                return 4;
            }
            if (text.Contains("amas") || text.Contains("inteligente") || text.Contains("alma") ||
               text.Contains("dios") || text.Contains("diablo") || text.Contains("cielo"))
            {
                return 5;
            }
            return 0;
        }

        public int Namecontrolar(string values)
        {
            var text = values.ToLower();
            if (text.Contains("cual es mi nombre") || text.Contains("como me llamo") ||
               text.Contains("di mi nombre") || text.Contains("dime mi nombre"))
            {
                return 6;
            }
            return Controlar(values);
        }

        public int Agecontrolar(string values)
        {
            var text = values.ToLower();
            if (text.Contains("cual es mi edad") || text.Contains("cuantos años tengo") ||
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