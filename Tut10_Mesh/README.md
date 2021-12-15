
## Aufgabe

git status
git add
git commit

### Teil 1

- X ~~Testet den Code für unterschiedliche Werte für `segments` und macht euch klar, dass jeweils nur das letzte Dreieck fehlt.~~
- X ~~Fügt die Vermaschung für das letzte Dreieck _nach der Schleife_ hinzu, so dass der Zylinder-Boden geschlossen erscheint. Für das
  letzte Dreieck müssen nur Einträge in den `tris`-Array erfolgen. Alle nötigen Punkte für das letzte Dreieck sind bereits
  im `verts`-Array enthalten.~~
- X ~~Wählt einen kleinen Wert für `segments` und zeichnet ein Bild mit den Vertices und den Dreiecken des entstehenden Zylinder-Bodens.
  Schreibt die Array-Inhalte für `verts` und `tris` auf. Zeichnet Verbindungslinien zwischen der Boden-Skizze und den Array-Inhalten,
  die erklären, wie die Array-Inhalte die Geometrie beschreiben.~~

X ~~Hinweise: Die Abgabe muss den Code aus SimpleMeshes.cs enthalten. Scannt/fotografiert Eure Skizze und legt diese der Abgabe als Bild/Pdf hinzu.~~
![Mein Gedankengang](../LastTri.png) Original als .pur

### Teil 2

Der bis hierher erarbeitete Code soll nun so erweitert werden, dass ein Zylinder entsteht.

In dieser Übung sollen - neben dem Verständnis für Geometriedaten - auch Herangehensweisen an komplexe Aufgaben
geübt werden. Es ist klar, das vieles nicht auf Anhieb funktioniert. Wichtig ist, dass man sich Strategien
zur Fehleranalyse aneignet. Einen ersten Versuch zusammenhacken, ausprobieren, merken, dass es nicht geht und
dann aufgeben gehört nicht dazu. Dazu gehört:

- In kleinen Schritten arbeiten und Zwischenergebnisse testen.
- Skizzen anfertigen
- von Hand ausrechnen, was zu erwarten ist, dann im Debugger die Daten analysieren,
- Code-Teile auskommentieren, auf Verdacht abändern
- Parameter ändern (z.B. mal mit 3 oder 4 statt 8 Segmenten testen) und das Ergebnis interpretieren

### Tipps und Hinweise

Hier zunächst ein paar beachtenswerte Tatsachen und Hinweise:

- Ziel sollte es sein, den Algorithmus nach wie vor mit nur einer Schleife aufzubauen, in der die Zählvariable `i`
  die Segmente durchnummeriert.

- Wie im Abschnitt [Zylinder-Aufbau](#zylinder-aufbau) zu sehen ist, muss jeder Randpunkt zwei mal vorhanden sein
  - einmal für die Deckfläche mit der Normalen nach oben (bzw. unten für die untere Deckfläche).
  - einmal für die Mantelfläche mit der Normalen radial in der selben Richtung, die der Punkt vom Ursprung
    entfernt liegt. Der Normalenvektor für diese Punkte lässt sich also
    [genauso](#idee-des-algorithmus) berechnen, wie die Punkt-Koordinate
    selbst, nur, dass der Normalenvektor die Länge 1 haben soll und somit nicht mit dem Radius multipliziert werden
    muss:
    - x_normale: cos i*δ
    - z_normale: sin i*δ
    - y_normale: 0 (***IMMER***)

- Somit beträgt der Punktebedarf für den gesamten Zylinder: `4*segments + 2`. Pro Segment-Kante vier Punkte:

  - einer für die obere Deckfläche (Normale nach oben)
  - einer für den oberen Rand des Mantelflächenabschnittes (Normale horizontal)
  - einer für den unteren  Rand des Mantelflächenabschnittes (Normale horizontal)
  - einer für die untere Deckfläche (Normale nach unten)
  Zusätzlich die beiden Mittelpunkte der oberen und unteren Deckfläche.

- Die Array-Größe für `tris` für den vollständigen Zylinder beträgt `4 * 3 * segments`:
  - Jede der zwei Deckfläche besteht aus `segments` Dreiecken: `2 * 3 * segments` Einträge für die Deckflächen.
  - Jedes Segment der Mantelfläche besteht aus einem Viereck, das aus zwei Dreiecken aufgebaut werden muss:
    `2 * 3 * segments` Einträge für die Mantelfläche.

- Die untere Deckfläche kann analog zur oberen aufgebaut werden, allerdings müssen die Dreiecke in umgekehrter
  Umlaufrichtung im `tris`-Array angegeben werden, damit sie nach außen hin sichtbar sind. Dreiecke sind immer
  nur aus der Richtung sichtbar, aus der ihre Eckpunkte in umgekehrtem Uhrzeigersinn auf dem Bildschirm erscheinen.

- Die Y-Koordinate der Eckpunkte der oberen Deckfläche/des oberen Mantelrandes soll bei `0.5f * height` liegen,
  die der Unterseiten-Punkte soll bei `-0.5f * height` liegen.

- Empfohlen wird folgende Punktanordnung in den `verts`- und `norms`-Arrays:
  - Die beiden Mittelpunkte der beiden Deckflächen sollten ans Ende der Arrays (Indizes `4*segments` (oben)
    und `4*segments+1` unten).
  - Jeweils vier aufeinanderfolgende Punkte im Array bilden die vier Punkte einer Segment-Kante in
    dieser Reihenfolge:
    - `verts[4*i + 0]`: obere Deckfläche,
    - `verts[4*i + 1]`: oberer Mantelrand,
    - `verts[4*i + 2]`: unterer Mantelrand,
    - `verts[4*i + 3]`: untere Deckfläche,

- Empfohlen wird folgende Anordnung der Indizes im `tris` Array: Jeweils 12 aufeinanderfolgende Einträge bilden
  die vier Dreiecke eines Segmentes in folgender Reihenfolge:
  - Dreieck des oberen Deckflächensegmentes (drei Einträge)
  - erstes Dreieck des viereckigen Mantelflächensegmentes (drei Einträge)
  - zweites Dreieck des viereckigen Mantelflächensegmentes (drei Einträge)
  - Dreieck des unteren Deckflächensegmentes (drei Einträge)

- Somit ergeben sich für jedes Segment `i` (nach der Nummerierung in o.a. Skizze) folgende Beziehungen
  zwischen `tris`-Array und `verts`-  (bzw. `norms`-) Array:

   ```C#
      // top triangle
      tris[12*(i-1) + 0] = (ushort) 4*segments;       // top center point
      tris[12*(i-1) + 1] = (ushort) 4*i     + 0;      // current top segment point
      tris[12*(i-1) + 2] = (ushort) 4*(i-1) + 0;      // previous top segment point

      // side triangle 1
      tris[12*(i-1) + 3] = (ushort) 4*(i-1) + 2;      // previous lower shell point
      tris[12*(i-1) + 4] = (ushort) 4*i     + 2;      // current lower shell point
      tris[12*(i-1) + 5] = (ushort) 4*i     + 1;      // current top shell point

      // side triangle 2
      tris[12*(i-1) + 6] = (ushort) 4*(i-1) + 2;      // previous lower shell point
      tris[12*(i-1) + 7] = (ushort) 4*i     + 1;      // current top shell point
      tris[12*(i-1) + 8] = (ushort) 4*(i-1) + 1;      // previous top shell point

      // bottom triangle
      tris[12*(i-1) + 9]  = (ushort) 4*segments+1;    // bottom center point
      tris[12*(i-1) + 10] = (ushort) 4*(i-1) + 3;     // current bottom segment point
      tris[12*(i-1) + 11] = (ushort) 4*i     + 3;     // previous bottom segment point
   ```

### Einzelschritte

1. Verändert im bestehenden Code alle Vertex-Koordinaten so, dass diese um `0.5f * height` Einheiten entlang der Y-Achse    nach oben verschoben werden (Mittelpunkt nicht vergessen).

2. Erweitert die Arrays auf die o.A. Größen und verändert zunächst die Berechnungen der bestehenden Indizes nach
   obigem Schema, so dass nach wie vor nur die obere Deckfläche angelegt wird, allerdings schon an den endgültigen
   Array-Positionen in `tris`, `norms` und `verts`.

3. Fügt Vertices und Dreickslisteneinträge für die untere Deckfläche hinzu. Zur visuellen Kontrolle könnt
   Ihr den Zylinder um seine X- statt um seine Y-Achse rotieren lassen.

4. Fügt Vertices und Dreickslisteneinträge für die Mantelfläche hinzu.

5. Falls noch nicht geschehen: Fügt das Dreiecksvermaschen für alle Flächen des letzten Segmentes außerhalb der
   Schleife hinzu.

### Alternativen / Ausblick / Für Fortgeschrittene

Wem der Einstieg zu schwierig ist und wer noch etwas mehr Sicherheit mit den Grundlagen benötigt, sollte zunächst mal versuchen,
die Methode `SimpleMeshes.CreateTetrahedron()` oder `SimpleMeshes.CreatePyramid()` zu implementieren. Bei beiden
Körpern steht, wie beim Cuboid, zur Compile-Zeit fest, aus wieviel Punkten und Flächen sie bestehen. Somit kann
ohne Schleifen und variable Indexberechnung gearbeitet werden. Stattdessen können, wie beim Cuboid, direkt die Punkte
und Flächen ein die entsprechenden Arrays eingetragen werden und die Indizes direkt als Zahlenwerte eingetragen werden.

Wer mit dem Zylinder gut zurecht kam, kann sich überlegen, wie aus den dabei gewonnenen Erkenntnissen die Methoden
`SimpleMeshes.CreateConeFrustum()` (leicht) oder auch die Methode `SimpleMeshes.CreateTorus()` (schwerer) implementiert
werden können.
