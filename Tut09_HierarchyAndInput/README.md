 

## Aufgabe

- Macht die Bewegung des Roboters unabhängig von der Framerate.
- Fügt Steuerungen für die beiden anderen Achsen des Roboters ein.
- Fügt eine Möglichkeit ein, dass Benutzer die Kamera mit der Maus um das Geschehen drehen können:
  - Bei gedrückter linker  Maustaste
    ([`Mouse.LeftButton`](https://github.com/FUSEEProjectTeam/Fusee/blob/develop/src/Engine/Core/MouseDevice.cs#L174))
    soll die X-Komponente der aktuellen Mausgeschwindigkeit 
    ([`Mouse.Velocity.x`](https://github.com/FUSEEProjectTeam/Fusee/blob/develop/src/Engine/Core/MouseDevice.cs#L99))
    als Parameter der Berechnung einer Änderungsrate für '_camAngle' verwendet werden.
  -  _Für Fortgeschrittene_: Baut eine Dämpfung ein, die den üblichen "Swipe"-Effekt nachstellt: Durch Maustaste-Drücken, 
    horizontales Bewegen und Loslassen während der Bewegung soll die aktuelle Drehgeschwindigkeit
    zunächst beibehalten werden und dann mit der Zeit abnehmen, bis die Drehung nach einer
    gewissen Zeit zum Stillstand kommt.
- Fügt eine Greifhand (aus zwei oder drei weiteren Quadern) in die Hierarchie ein und ermöglicht Benutzern, diese
  zu Öffnen und zu Schließen. Wie kann gewährleistet werden, dass es Zustände wie "ganz offen" und "ganz geschlossen"
  gibt, die nicht über- oder unterschritten werden können?
- _Für Fortgeschrittene_: Schön wäre es, das Öffnen und Schließen jeweils durch einen einmaligen Tastendruck
  ([`Keyboard.GetKey()`](https://github.com/FUSEEProjectTeam/Fusee/blob/develop/src/Engine/Core/KeyboardDevice.cs#L35))
  triggern zu können, nachdem der jeweilige Vorgang (Öffnen oder Schließen) dann selbständig abläuft.


 










