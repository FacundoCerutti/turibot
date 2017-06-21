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
        private int control = 0;

        // Respuestas defusificadas del sistema
        /*private string[] destinos = new string[33] {"Tafi del Valle","Mares del Sur","Rosario","Praya dos Ingleses",
                                            "Valparaiso","Punta del Este","Cartagena", "Camboriu","Orlando",
                                            "Miami","Bariloche","Mendoza","Caribe","Guayaquil","Islha do Mel",
                                            "Machu Pichu","Roma","Venecia","Paris","Valencia","Portugal","Grecia",
                                            "Egipto","Hawaii","Carlos Paz","Mar del Plata","Floreanopolis","Cancun",
                                            "Costa Rica","Las Vegas","Amsterdam","Tokyo","Australia"};*/


        private List<Destino> respuesta4 = new List<Destino>();
        public List<Destino> Respuesta4 { get => respuesta4; set => respuesta4 = value; }    


        private Destino[] destinos = {

                new Destino("Tafi del Valle",-26.851975945944233 ,-65.71111467217486), //0
                new Destino("Mar del Sur",-38.341162211004026 ,-57.99522928695682), //1
                new Destino("Rosario",-32.94973412170926,-60.643268974719255), //2
                new Destino("Praia dos Ingleses",-27.433058905678887,-48.39451121434019), //3
                new Destino("Valparaiso",-33.04712394984877,-71.61270079075314), //4
                new Destino("Punta del Este",-34.93660924934044 ,-54.92869676494137 ), //5
                new Destino("Cartagena",10.390831615024997 ,-75.48121677175287), //6
                new Destino("Camboriu",-26.99383781773793 , -48.63535161733398), //7
                new Destino("Orlando", 28.53809132152661 ,-81.37965049743651), //8
                new Destino("Miami",25.761021331096117 ,-80.19205767510374), //9
                new Destino("Bariloche", -41.13367467729619, -71.31137579608152), //10
                new Destino("Mendoza",-32.89026783069922 ,-68.84547283535159 ), //11
                new Destino("Punta Cana",18.582944976844704 ,-68.39808664838557), //12
                new Destino("Guayaquil", -2.16986698986024,-79.9199138987549), //13
                new Destino("Ilha do Mel",-25.515731076600932 , -48.33269297642824), //14
                new Destino("Machu Pichu",-13.163141203713916 ,-72.54496289999997), //15
                new Destino("Roma",41.902742 ,12.496242), //16
                new Destino("Venecia",45.433776016477786 ,12.327938325351639), //17
                new Destino("Paris", 48.85542056220146,2.345192532753355), //18
                new Destino("Valencia",39.46701475595226 ,-0.371034483107342), //19
                new Destino("Portugal", 38.712702401270796,-9.14286366823353), //20
                new Destino("Grecia",37.974319357400795 ,23.73644296320502), //21
                new Destino("Egipto",26.939078097403748,30.795659800000067), //22
                new Destino("Hawaii", 20.47188350858674,-157.505),  //23
                new Destino("Carlos Paz", -31.41222809215237,-64.49973669999997), //24
                new Destino("Mar del Plata",-38.01764738461685 ,-57.600534049999965), //25
                new Destino("Florianopolis", -27.614479856117843,-48.48282474999996), //26
                new Destino("Cancun",21.121371172643965 ,-86.84931025000003), //27
                new Destino("Costa Rica",9.633931465220899 ,-84.25418434999995), //28
                new Destino("Las Vegas", 36.12519506431158,-115.17499999999995), //29
                new Destino("Amsterdam",52.37464788349741 ,4.898614199999997), //30
                new Destino("Tokyo",35.7076595041669 ,139.73150234936531), //31
                new Destino("Australia",-24.72265917422125 ,133.08742210624996), //32
                new Destino("Salta",-24.786047683039044 ,-65.42148768167725), //33
                new Destino("San Salvador deJujuy",-24.187594772531707,-57.99522928695682), //34
                new Destino("Pumamarca",-23.747368495757712,-65.49558444428713), //35
                new Destino("Gral Guemes",-24.6674657111551 ,-65.04445834565432), //36
                new Destino("Villa Gesell",-37.26077166271962 , -56.97232467846674), //37
                new Destino("Necochea",-38.55491790589638,-58.73876668955076), //38
                new Destino("La Habana",23.109212553455293 ,-82.35730248203123 ), //39
                new Destino("Varadero",23.179927280558548 ,-81.19275170078123), //40
                new Destino("Bahamas", 24.663164421891352, -77.92157250156248), //41
                new Destino("Jamaica",18.115996789211756,-77.27600254999999), //43
                new Destino("Texas",31.320770908156707 , -100.07666699999999), //44
                new Destino("Madrid",40.43807216375375,-3.6795366500000455), //45
                new Destino("Barcelona",41.39483307195536,2.148767850000013), //46
                new Destino("Andorra",42.51159491455753,1.5372319000000516), //47
                new Destino("Firenze",43.7799816219018 ,11.240967899999987), //48
                new Destino("Venecia",45.40592607135017 ,12.381742600000052), //49
                new Destino("Pisa",43.70675963600526,10.395378150000056), //50
                new Destino("Napoles",40.85401461311923,14.24660234999999), //51
                new Destino("Milan",45.462781053585175,9.177732249999963), //52
                new Destino("Marrakech",31.63465728585671,-8.007853100000034), //53
                new Destino("Ontario",49.85949425501147,-84.73801349999997), //54
                new Destino("Toronto",43.71839831777034,-79.37805805000005), //55
                new Destino("Aruba",12.517588773960593,-69.96489695000002), //56
                new Destino("Washington",38.899413812995974,-77.01456655000004), //57
                new Destino("Perito Moreno",-50.50267565633805,-73.12969520000001), //58
                new Destino("La Quiaca",-22.107109323942463,-65.59761639999999), //59
                new Destino("Bogota",4.759261022090123 ,-74.05005567187497), //60
                new Destino("Termas del Rio Hondo",-27.49868164432165,-64.86062673610842), //61
                new Destino("La Banda Sgo", 25.479077049880313,80.3362798039062), //62
                new Destino("Sudafrica",-30.928911907385604 ,24.419605953125018), //63
                new Destino("Zimbabue", -19.1122790150743,-9.14286366823353), //64
                new Destino("Mozambique",-17.422261307295187 ,35.581715328125014), //65
                new Destino("Filipinas", 11.81631122268848,122.62175424999998), //66
                new Destino("Toronto",43.718398317770394 ,-79.37805805000004), //67
                new Destino("Abu Dabi", 24.386766222944743,54.5599079000001), //68
                new Destino("Dubai",25.074684222019012 ,55.228105400000004), //69
                new Destino("Praga",50.10623344195831 ,14.46461676230478), //70
                new Destino("Bruselas", 50.847322623630326,4.3352222310547806), //71
                new Destino("Marruecos",31.887245138782074 ,-7.084723100000019), //72
                new Destino("Dublin",53.354082245731995 ,-6.263495072656273), //73
                new Destino("Oslo",59.92495462112562 ,10.746087446874956), //74
                new Destino("Moscu",55.75517837193684 ,37.61864604062495), //75
                new Destino("Seul",25.04617487541967 ,126.94848002499995), //76
                new Destino("Taipéi",37.57974722826311 ,121.55968607968745), //77
                new Destino("Pekín",39.92691230708022 ,116.43456400937495), //78
                new Destino("Ciudad del Cabo",-33.92477924756511 ,18.436517134374956), //79
                new Destino("Berna",46.95468173826602 ,7.394870050000009), //80
                new Destino("Belfort",47.6397387084714 ,6.8616690345459075), //81
                new Destino("Varsovia",52.22781978311614 ,21.020814389770518), //82
                new Destino("Mumbai",19.10367966718735 ,72.85705706555176), //83
                new Destino("Katmandu",27.703013170933225 ,85.33203265148926), //84
                new Destino("Timbu",27.479064248094673 ,89.62768694836426), //85
                new Destino("Kabúl",34.57897978368012 ,69.22058245617677), //86
                new Destino("Bagdad",33.303013967916065 ,44.347535581176764), //87
                new Destino("Beirut",33.8978046095563 ,35.506288022583014), //88
                new Destino("Chipre",34.98503036907409 ,33.171693296020514), //89
                new Destino("Ankara",39.93925033171775 ,32.850343198364264), //90
                new Destino("Tiraba",41.33147624215961 ,19.820558042114268), //91
                new Destino("Edimburgo",55.95998214955297 ,-3.191679506713858), //92
                new Destino("Niamey",13.517870000342125 ,2.151795835083017), //93
                new Destino("Aruba",12.490246122159919 ,-69.9528489403076), //94
                new Destino("Panamá",8.990920477074882 ,-79.52606061022948), //95
                new Destino("San Diego",32.73879995278152 ,-117.16247418444823), //96
                new Destino("Portland",45.527539975787185 ,-122.72155621569823), //97
                new Destino("Kansas",39.110908334470956 ,-94.57458355944823), //98
                new Destino("Durango",24.02391839545969 ,-104.65453961413573), //99
                new Destino("Monterrey",25.698492951278638 ,-100.32592633288573), //100
                new Destino("Nueva Orleans",29.952583637993495 ,-90.07568219226073), //101
                new Destino("Montreal",45.496744943572374 ,-73.58520367663573), //102
                new Destino("Auckland",-36.829046602337506 ,174.7552504249268), //103
                new Destino("Isla de Pascua",-27.098193292101058,-109.3596715572998), //104
                new Destino("Berlin",52.50786264022465,13.426145399999996), //105
                new Destino("Londres",51.528868434293244,-0.10161819999996169), //106
                new Destino("Brighton",50.83743441187283,-0.10618969999995898), //107
                new Destino("La Paz",-16.520736280532606,-68.0915129), //108
                new Destino("Sucre",-19.02066128620186,-65.25979219999999), //109
                new Destino("Iquique",-20.244825658942073,-70.13884915), //110
                new Destino("Santiago de Chile",-33.47302199866159,-70.62983129999998), //111
                new Destino("Rosario",-32.952341378871026,-60.698157700000024), //112
                new Destino("Rio de Janeiro",-22.91133405231811,-43.448333950000006), //113
                new Destino("Punta Arenas",-53.14179225916762,-70.9062672), //114
                new Destino("Rio Grande",35.08336701115399,-106.72069699999997), //115
                new Destino("Helsinki",60.16427693520438,25.040267549999953), //116
                new Destino("Marsella",43.28049138346577,5.405138999999963), //117
                new Destino("Ibiza",38.97448920399977,1.4173878500000683), //118
                new Destino("Cordoba",-31.399466095395084,-64.194344), //119
                new Destino("Posadas",-27.53553270003656,-55.82821376752929), //120
                new Destino("Vaticano",41.9038164530039,12.452061200000003), //121
                new Destino("Fermo",43.163242677069974,13.727786400000014), //122
                new Destino("Concepcion del Uruguay",-32.473769222072164,-58.27003285000001), //123
                new Destino("San Marino",43.94289830618427,12.46009325), //124
                new Destino("Jerusalen",31.79635921136502,35.17535900000007), //125
                new Destino("San Fernando del Valle de Catamarca",-28.46454389615909,-65.77515985000002), //126
                new Destino("Copenaghe",55.67130717687403,12.560838799999942), //127
                new Destino("Montevieo",-34.82002728272268,-56.23020604999999), //128
                new Destino("Chernobyl",51.27530099076648,30.221885499999985), //129
                new Destino("Boston",42.313431516450144,-71.05715705), //130
                new Destino("Dallas",32.82089678362361,-96.73133960000001), //131
                new Destino("Los Angeles",34.02109014442116,-118.41173249999997), //132
                new Destino("Nueva York",40.70597954587119,-73.9780035), //133
                new Destino("Medellin",6.268688560352701,-75.59639199999998), //134
                new Destino("Valle del Cauca",4.038988439228275,-76.625429), //135
                new Destino("Paysandu",-32.270805198069425,-58.07905199999999), //136
                new Destino("Viedma",-40.82502941696214,-63.00013960000001), //137
                new Destino("Santa Rosa",-36.6194019944866,-64.30123565000002), //138
                new Destino("Piedra del Aguila",-40.04724751557836,-70.07602309999999), //139
                new Destino("Las Grutas",-40.80702911339735,-65.08517265), //140
                new Destino("Chapadmalal",-38.173056012484466,-57.65194399999996), //142
                new Destino("Las Leñas",-35.1479106111807,-70.08166840000001), //143
                new Destino("Rio Gallegos",-51.62850216426361,-69.27174925000003) //144

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
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 0)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }


                        break;
                    case 100:
                        respuesta4.Add(Destinos[1]);
                        respuesta4.Add(Destinos[2]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 1 || nro != 2)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }

                        break;
                    case 150:
                        respuesta4.Add(Destinos[1]);
                        respuesta4.Add(Destinos[2]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if(nro!=1 || nro != 2)
                            { 
                            respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                
                break;
                    case 200:
                        respuesta4.Add(Destinos[4]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 4)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }

                        break;
                    case 250:
                        respuesta4.Add(Destinos[4]);
                        respuesta4.Add(Destinos[5]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 4 || nro != 5)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }

                        break;
                    case 300:
                        respuesta4.Add(Destinos[3]);
                        respuesta4.Add(Destinos[6]);
                        respuesta4.Add(Destinos[7]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 3 || nro != 6 || nro!=7)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 350:
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 1 || nro != 2)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 400:
                        respuesta4.Add(Destinos[8]);
                        respuesta4.Add(Destinos[9]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 8 || nro != 9)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 450:
                        respuesta4.Add(Destinos[8]);
                        respuesta4.Add(Destinos[9]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 8 || nro != 9)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 500:
                        respuesta4.Add(Destinos[10]);
                        respuesta4.Add(Destinos[11]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 10 || nro != 11)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 550:
                        respuesta4.Add(Destinos[11]);
                        respuesta4.Add(Destinos[25]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 11 || nro != 25)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 600:
                        respuesta4.Add(Destinos[25]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 25)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 650:
                        respuesta4.Add(Destinos[15]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 15)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 700:
                        respuesta4.Add(Destinos[14]);
                        respuesta4.Add(Destinos[15]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 14 || nro != 15)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 750:
                        respuesta4.Add(Destinos[14]);
                        respuesta4.Add(Destinos[12]);
                        respuesta4.Add(Destinos[13]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 14 || nro != 12 || nro!=13)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 800:
                        respuesta4.Add(Destinos[16]);
                        respuesta4.Add(Destinos[17]);
                        respuesta4.Add(Destinos[18]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 16 || nro != 17 || nro != 18)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 850:
                        respuesta4.Add(Destinos[16]);
                        respuesta4.Add(Destinos[17]);
                        respuesta4.Add(Destinos[18]);
                        respuesta4.Add(Destinos[19]);
                        respuesta4.Add(Destinos[20]);
                        respuesta4.Add(Destinos[21]);
                        respuesta4.Add(Destinos[23]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 16 || nro != 17 || nro != 18 || nro != 19 || nro != 20 || nro != 21 || nro != 23)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 900:
                        respuesta4.Add(Destinos[19]);
                        respuesta4.Add(Destinos[20]);
                        respuesta4.Add(Destinos[22]);
                        respuesta4.Add(Destinos[23]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 19 || nro != 20 || nro != 22 || nro != 23)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 950:
                        respuesta4.Add(Destinos[10]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 10)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 1000:
                        respuesta4.Add(Destinos[24]);
                        respuesta4.Add(Destinos[25]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 24 || nro != 25)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 1050:
                        respuesta4.Add(Destinos[24]);
                        respuesta4.Add(Destinos[25]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 24 || nro != 25)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 1100:
                        respuesta4.Add(Destinos[15]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 15)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 1150:
                        respuesta4.Add(Destinos[5]);
                        respuesta4.Add(Destinos[15]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 5 || nro != 15)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 1200:
                        respuesta4.Add(Destinos[26]);
                        respuesta4.Add(Destinos[27]);
                        respuesta4.Add(Destinos[28]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 26 || nro != 27 || nro != 28)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 1250:
                        respuesta4.Add(Destinos[30]);
                        respuesta4.Add(Destinos[31]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 30 || nro != 31)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 1300:
                        respuesta4.Add(Destinos[29]);
                        respuesta4.Add(Destinos[30]);
                        respuesta4.Add(Destinos[31]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 29 || nro != 30 || nro != 31)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                    case 1350:
                        respuesta4.Add(Destinos[29]);
                        respuesta4.Add(Destinos[32]);
                        for (int i = 0; i <= 19; i++)
                        {
                            Random rnd = new Random();
                            int nro = rnd.Next(0, 141);
                            if (nro != 29 || nro != 32)
                            {
                                respuesta4.Add(Destinos[nro]);
                            }
                            else
                            {
                                i--;
                            }
                        }
                        break;
                }
                control = 0;
                try { 
                Genetico genetics = new Genetico(respuesta4);
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