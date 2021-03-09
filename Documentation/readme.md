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
  *

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
