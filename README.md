# Mandatory2DGameFramework

- [Features](#features)
- [Installation](#installation)
- [Konfiguration](#konfiguration)
- [Eksempel](#eksempel)
- [Arkitektur](#arkitektur)

Et fleksibelt og konfigurerbart mini‑framework til tur‑baserede 2D‑spil i C#.

Frameworket er udviklet som en del af faget **Advanced Software Construction** og demonstrerer brugen af SOLID‑principperne samt en række klassiske design patterns: Template, Strategy, Observer, Composite, Decorator, Factory, Singleton.

---

## Features

- 2D‑verden med dynamisk størrelse (konfigureret via XML)
- Creatures med position, inventory, strategi‑baseret kamp og observer‑notifikationer
- Attack‑ og defence‑items med Composite og Decorator patterns
- Looting‑system med automatisk fjernelse af objekter fra verden
- Logging via singleton‑logger med udskiftelige TraceListeners
- Klar til brug som NuGet‑pakke

---

## Installation

Dotnet CLI

```PowerShell
dotnet add package Katerina2DGameFramework --version 1.0.0
```

PowerShell

```PowerShell
Install-Package Katerina2DGameFramework
```

## Konfiguration

Frameworket læser konfiguration fra en XML‑fil:

```xml
<GameConfig>
  <World>
    <MaxX>20</MaxX>
    <MaxY>10</MaxY>
  </World>
  <GameDifficulty>Beginner</GameDifficulty>
</GameConfig>
```

## Eksempel

```csharp
var world = World.FromConfig(GameConfigLoader.Load("gameconfig.xml"));

var warrior = new Warrior("Laezel");
var mage = new Mage("Gale");

world.AddCreature(warrior);
world.AddCreature(mage);

var sword = new AttackItem("Sword", 10, 1, 5);
warrior.Loot(sword);

warrior.PerformHit(mage);
Console.WriteLine(mage);
```

## Arkitektur

World

- Indeholder creatures og world objects
- Observerer creatures (hit, death, loot)
- Factory: World.FromConfig

Creature

- Template Method: PerformHit
- Strategy: Aggressive, Balanced, Defensive
- Observer: registrerer world
- Inventory: attack + defence items
- Looting med vægtbegrænsning

AttackItem / DefenceItem

- Composite: kombiner flere items
- Decorator: buff/debuff
- Operator overload: +

Logger

- Singleton
- Understøtter udskiftelige TraceListeners
