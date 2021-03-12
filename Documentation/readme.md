# Documentation

Use this file to fill in your documentation

Projektet delat i 3 delar och vi bör implementera dessa i nummerföljd.

1 Fluent API för att planera tågen
  * Travelplan

2 Development and ORM
  * Skapa en egen ORM för dataprovidern
  * Ska kunna läsa, skriva och spara till travelplan
 
3 Simulera tågspåret
  * Varje tåg ska ha sin egen tråd utan vetskapen om andra tåg som kör på spåret.
  * Tåget ska stanna automatiskt på stationen.
  * Mr. Carlos ska bestämma när dom åker igen.
  * Kör som fan med tågen!

Interfaces
  * Itravelplan
    * List<object> TimeTable { get; }
    * object Train { get; }
    * void Save(string path);
    * void Load(string path);


Klasser
  * TrackDescription
  * TrackOrm (Tas efter ORM-genomgången på torsdag.)
  * Train
  * Station
  * Operator (mr. Carlos)

Metoder
  * FileReader?
  * FileWriter? 
  * Prepare

Test
  * TrackOrmTest

Filer
  * Controller log
  * Passengers
  * Stations
  * TimeTable
  * Trains
  * TrainTrack 1
  * TrainTrack 2
  * TrainTrack 3

Ett tåg är en kopp. ett tåg kan innehålla obegränsat med ingredienser som här är stationer. 
En TimeTable är då ett recept liknande espressomaskinen. Skillnaden är att tåget i det här fallet ska kunna köra igenom från punkt till punkt.
TimeTable kan representera ett gäng larm, som en klocka som talar om att tåget ska komma till givna punkter på given tidpunkt.
Station 1 - Larm 1, Station 2 - Larm 2 osv.
Tåg är nåt fysiskt som vi inte exakt kan simulera. Vi kan göra tåget som en till klocka som vi kan använda för att stämma av mot TimeTable.
Om tåget kommit i tid så stämmer det, annars inte.
Tåget går från A - B vi säger 100 km. så kan vi räkna ut hur lång tid det tar från för tåget att färdas. Sen kan vi lägga till väntetider på stationerna för att räkna ut hur fort tåget måste köra mellan stationerna.
Kan gå att lösa tågfärd, tågstopp med thredding. Vi kan nog köra med 3 trådar som jobbar med varandra.
Tråd 1 start
tråd 2 färd
tråd 3 stationsstopp
Ex: Start tråd 1 -> färd tråd 2 -> färd -> färd -> tråd 1 -> stationsstopp tråd 3 -> tråd 3 -> tråd 3 -> fortsätt tråd 1 -> tråd 2 -> tråd 2 -> Stopp tråd 1.

En trainplanner visar upp alla tåg som kör och kan se om tågen kan köras. Komplicerat!!

Vart hör grejorna hemma?
 * Ska metoderna NextStation, StartStation, EndStation och GeneratePlan ligga i TrainPlanner, train eller TimeTable?
 * TrainPlanner är kanske vart tåget ska åka någonstans eller i TimeTablen?
 * Kan vi stoppa in alla trains med respektive timeplan i TrainPlanner?
 * Operatören kollar om/när tågen kan åka. Så vad behöver han?
 * Vi vill ha lättåtkomst till tågen och passagerarna.
 * Hur viktig är TrainPlanner eller TimeTablen?

1 Vi provar med att koppla in TimeTable i Train. TimeTablen kan vara null.
   * Vi gör en funktion som lägger in värden i listan AddTimeTable
Detta funkade inte optimalt så vi lägger in listan på TimeTable och Train in i TrainPlanner och låter TrainPlanner sköta allt. Vi kommer att byta dessa listor när vi kör ORM.
Vi kommer att ändra på tillgängligheten på allt så småningom när vi får det att funka.



Tankar om Carlos Meck
   * Gets a list with timetables
   * Create plans for trains using timetables (trainplanner API)
   * Assign plans to trains here, not in trainplanner, nuh uh
   * He needs the POWER...to start and stop trains
   * So he also needs a way to check the track for issues
   * Om det snöar en decimeter, stäng ner allt!!! LAPPMÖGEL!!!
   * Behöver öråd för att bestämma om Mr.Carlos är egentligen Mulle Meck

        //plan.NextStation(station1).NextStation(station2).NextStation(station1).NextStation(station2)

Tankar om tracks
 * Antingen så kan varje trackpeace länka till den som är före och efter för att kedja dom
 * Vid till exempel en korsning så kan man se till att checka traintracksen efter hinder och annat i vägen.
 * Allt är noder.
 * 

Nästa steg är:
 * Fixa läsning av tracks (false)
 * Skriv till fil (false)
 * Få tågen att röra sig efter banan (false)
 * Fixa klocka (false)
 * Få till mer avancerat track (false)
 * Starta stoppa (false)
