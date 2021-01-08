# Oversteek simulator
## Table of contents
1. [Inleiding](#Inleiding)
2. [Groepsleden](#Groepsleden)
3. [Korte samenvatting](#Korte-samenvatting)
4. [Installatie](#Installatie)
5. [Verloop simulatie](#Verloop-simulatie)
6. [Observaties, acties en beloningen](#Observaties-acties-en-beloningen)
7. [Beschrijving objecten](#beschrijving-objecten)
8. [Scripts](#Scripts)
9. [Beschrijving gedragingen objecten](#beschrijving-gedragingen-objecten)
10. [Verloop van de training](#verloop-van-de-training)
11. [Resultaten training](#resultaten-training)
11. [Roadblocks](#Roadblocks)

## Groepsleden
| Naam                   | S-nummer |
| ---------------------- | -------- |
| Anne Toussaint         | s106511  |
| Kirishalini Kanagarasa | s108145  |
| Yalda Fazlehaq         | s108051  |
| Kobe De Peuter         | s107571  |
| Matthias Verschorren   | s103579  |

## Inleiding
Voor het vak VR Experience kregen wij de opdracht om een applicatie te bedenken waarbij zowel Virtual Reality als ~~ML-Agents~~ (machine learning) een meerwaarde zal zijn.
Hiervoor maken we gebruik van Unity.
In dit document zal u een goede uitleg over het project krijgen, wat we hier allemaal in gedaan hebben en hoe alles in zijn werking gegaan is.

## Korte samenvatting
Voor dit project hebben wij geopteerd om een VR omgeving te maken waarin kinderen zullen kunnen leren om veilig de straat over te steken.
Het is de bedoeling dat ze leren hoe ze veilig kunnen oversteken, zonder enig gevaar.

Hier zullen de autos volledig door een getrainde AI worden bestuurd om zo echte bestuurders te simuleren.

## Installatie

| Programma/tool/asset  | Versie         |  Naam                        |
| ---                   | ---            | ---                          |
| Unity                 | 2019.4.16f1    |                              |
| Python                | 3.8.1 of hoger |                              |
| ML Agents             | 1.0.5          |                              |
| Tensorboard           | 2.3.1          |                              |
| Oculus XR plugin      | 1.6.1          |                              |
| XR Plugin Management  | 3.2.17         |                              |
| Unity asset           | 2.2.0          | Character Pack Free Sample   |
| Unity asset           | 1.0.0          | HQ Racing Car Model No.1203  |
| Unity asset           | 23.1.0         | Oculus Integration  |

![Unity asset character](images/installatie/character_pack.png)

![Unity asset car model](images/installatie/car_model.png)

![Unity asset oculus integration](images/installatie/oculus.png)

## Verloop simulatie
Wanneer de simulatie start zal de speler een random positie en random rotatie toegekend krijgen.
Op dit moment zullen er ook auto's beginnen verschijnen die op de weg rijden.

## Observaties, acties en beloningen

### Beloning structuur
Voor onze beloningen hebben we verschillende tabellen aangezien we met meerde AI agents zullen werken en hierdoor zal elke agent ook een specifieke reward structure hebben.

| Agent                 | Rijdt tegen speler   | Komt op bestemming  | Rijdt te snel | Niet op bestemming | Raakt auto | Afstand tot bestemming | Op verboden locatie                 |
| ---                   | ---                  | ---                 | ---           | ---                | ---        | ---                    | ---                                 |
| Goede auto            | -1*                  | +1*                 | -0.1          | -0.001             | -0.8*      | NVT                    | NVT                                 |
| Slechte auto          | -0.5*                | +1*                 | -0.1          | -0.002             | -0.8*      | NVT                    | NVT                                 |
| Player                | NVT                  | +1*                 | NVT           | -0.001             | -1*        | variabel               | -0.0001 op gras, -0.0002 op de weg  |
| *Eindigd ook episode  |

## Beschrijving objecten
### Auto

![Model](images/objects/car/model.png)

![Scaling](images/objects/car/transform.png)

Voor de autos werd een asset package gebruikt, om ons veel werk te besparen en toch een mooie auto te hebben. Om deze models op een goede grootte te krijgen, hebben we een schaal van 1.5 toegepast. We hebben gekozen voor een verschil in kleur om aan te duiden wat een goede en wat een slechte auto is, dit betekend dat we telkens de twee prefabs moeten aanpassen. Om aan te duiden dat dit effectief een auto is voegen we de Car tag toe.

![Collider](images/objects/car/collider.png)

In het originele object is de collider niet perfect hoe we hem willen, dus verwijderen we het collider object dat in het auto model zit. Zelf voegen we een box collider component toe en veranderen we de grootte zodat die rond de hele auto zit.

![ML Behaviour](images/objects/car/behaviour.png)

![Components](images/objects/car/settings.png)

Om de training goed te kunnen laten verlopen moeten we de behaviour parameters goed instellen. De behaviour name hangt af van welk script je toevoegd. Beide autos hebben 4 observations en 1 action branch size met 3 mogelijke waarden. Omdat we nog niets hebben getrained kunnen we nog geen model toevoegen.

Beide autos hebben een script nodig die de effectieve agent toevoegd. Voor de gele auto hebben we het GoodCar script, voor de blauwe hebben we het BadCar script. Deze twee scripts verschillen enkel in het reward systeem, voor de rest maken ze gebruik van dezelfde code.

Om ervoor te zorgen dat de auto objecten effectief kan zien werd er een ray perception sensor toegevoegd. Het is belangrijk dat de sensor een aantal tags kan detecteren: player, car en crossing. Ook veranderen we het aantal rays, de ray breedte en de ray lengte. Ook willen we dat de sensor een beetje naar beneden kijkt, dus stellen we dit ook in. Om ervoor te zorgen dat we manueel de autos kunnen besturen voegen we een decision requester toe.

### Player

Om problemen te vermijden tussen de machine learning en de efffectieve applicatie, hebben we 2 speler aparte objecten voorzien.

#### VR Player

![Tag](images/objects/vr-player/tag.png)

In de `Asset/Oculus/VR/Prefabs` bevindt zich de `OVRPlayerController`, deze zullen we gebruiken als basis voor onze speler. Natuurlijk ook hier voegen we weer een tag toe, dit keer Player. Omdat onze applicatie een VR applicatie is, is een model hiervoor niet nodig.

![Components](images/objects/vr-player/ovr_components.png)

In de componenten die de controller bevat veranderen we enkel de acceleratie zodat we sneller bewegen.

![Rigidbody](images/objects/vr-player/rigidbody.png)

Zelf voegen we nog een rigidbody toe, om beweging toe te voegen. Hierbij zetten we gravity uit en zorgen ervoor dat alle X, Y en Z veranderingen uitstaan. Deze zorgen voor ongewenste bijwerkingen. Omdat we enkel x en z bewegingen hebben is dit geen probleem.

#### ML Player

![Tag](images/objects/ml-player/tag.png)

![Rigidbody](images/objects/ml-player/rigidbody.png)

Ons hoofd object geen zelf geen vorm, enkel componenten en de Player tag. We voegen een rigidbody toe, ook weer zonder gravity en zetten we de rotatie volledig uit. Voor de gewone beweging zetten we enkel de Y coordinaat uit, zodat we niet door muren kunnen gaan.
Ook voegen we een box collider toe, om ervoor te zorgen dat we aanrakingen kunnen detecteren.

![ML Behaviour](images/objects/ml-player/behaviour.png)

De speler heeft 1 observatie en 2 soorten acties, met telkens 2 waarden. Om de speler niet te lang te laten rondlopen, stellen we een limiet van 50.000 stappen op. Ook hier is een decesion requester nodig voor manuele beweging.

![Model](images/objects/ml-player/model.png)

![Transform](images/objects/ml-player/model_transform.png)

Voor deze speler maken gebruik van een asset package. Deze voegen we toe aan ons hoofd object en vergroten we met 1.5.

![Ray Perception](images/objects/ml-player/perception1.png)

![Ray Perception](images/objects/ml-player/perception2.png)

Aan het model voegen we ook 3 ray perception sensors toe. Twee om rechtdoor te kijken, telkens op een andere hoogte, en een om naar beneden te kijken. De te detecteren tags zijn voor alle drie dezelfde: Car, Finish, Footpath, Crossing, Road en Grass. Alle sensoren hebben een breedte van 40 graden. Daarnaast hebben we voor elke sensor een aantal waarden veranderd, zodat alles goed detecteerbaar is.

### Zebrapad

Voor het zebrapad hebben we een object met enkel de Crossing tag.

![Model](images/objects/crossing/model.png)

![Transform](images/objects/crossing/transform.png)

![Collider](images/objects/crossing/collider.png)

In dat object zitten meerdere witte strepen. Om de detectie te garanteren hebben we een grote box collider toegevoegd zodat we kunnen controleren wanneer er verschillende entities in contact komen met het zebrapad.

### Finish

![Finish](images/objects/finish.png)

De finish is een enorm simpel object met de Finish tag en een box collider.  

### Scene

![Scene](images/scene.png)

In deze foto kunnen we zien dat er een basisscene opgesteld is waarop we een oversteekpunt, voetgangerspad en een weg kunnen observeren. We hebben bewust gekozen om de scene basic te houden om zo meer op de functionaliteit te kunnen letten waardoor we meer vooruitgang konden boeken.

## Scripts
### Car
```C#

```
### Good car
```C#

```
### Bad car
```C#

```
### Environment
```C#
```
### Spawnpoint
```C#
```
### Player trigger
```C#
```
### Player
```C#

```

## Beschrijving gedragingen objecten
### Auto
Bij het gedrag van de auto zien we dat de auto's vooruit zullen rijden om zo aan hun gedesigneerde eindzone te geraken. Indien ze een speler klaar zien staan aan het voetpad zullen de auto's moeten stoppen om zo de speler over te laten.

Echter hebben we er ook voor gezorgd dat we enkele auto's niet laten stoppen om zo het verkeer het beste te simuleren.
Zo zal de speler kunnen leren omgaan met auto's die zoals in een echte verkeerssituatie niet zullen stoppen.

Indien we trainen met de auto zullen we de Player agent (Hieronder vermeld) ook nodig hebben aangezien ze in samenhang zullen trainen om zo de auto's te leren stoppen indien ze een speler detecteren.

### Player
Indien de auto agents training nodig hebben zal u een player object in de scene kunnen steken die ervoor zorgt dat er een speler met een agent zal beginnen rondwandelen en trachten op zoek te gaan naar het oversteekpunt. Hier moet getracht worden eerst de player te trainen om efficient opzoek te gaan naar het oversteekpunt.

## Verloop van de training
Om de auto's op een goede manier te trainen hoe het verkeer in het echt zou lopen hebben we geopteerd om de speler ook een agent toe te kennnen tijdens de training.
Dit zal ervoor zorgen dat wanneer we de training starten de auto's zullen leren om te gaan met een onvoorspelbare speler.

Om de training te starten kan u in de projectmap een python of anaconda terminal opendoen en daarin zal u de volgende commands moeten invoeren.

Indien u anaconda gebruikt zal u eerst de omgeving moeten aanzetten. Dit kan u doen door het commando:
```
conda activate <Naam ML agents environment>
```
Daarna kan u de training starten met het commando:
```
mlagents-learn YAML --run-id <Naam>
```
Indien u de resultaten van de training wilt bekijken al dan niet live kan u in een nieuwe terminal dit commando uitvoeren:
```
tensorboard --logdir results
```


In het volgende hoofdstuk zullen we meer uitbreiden over de resultaten die we hebben geobserveerd van onze training.
## Resultaten training
In totaal hebben wijzelf de training op 3 verschillende machines laten draaien. Hieronder zullen we de resultaten tonen en hier een summiere uitleg bij geven.



|           | Agent          |  Duur training    |
| --------- | -------------- | ----------------- |
| Blauw     | Bad car        |  13H 52M 25S      |
| Oranje    | Good car       |  13H 56M 45S      |
| Grijs     | Player         |  13H 56M 21S      |


![Anne Training](md_images/anneTraining.png)

Bij deze trainingset hebben we opgemerkt dat de auto's zowel de `Good car` als `Bad car` veel te snel reden dan ze mochten rijden. Hierdoor is de curve van de auto reward redelijk eentonig aangezien de auto's altijd aan hetzelfde tempo naar de eindmeet reden.

Bij de `Player` zien we echter dat deze curve veel meer fluctuatie heeft.

Tijdens de training hebben we hier geobserveerd dat indien de `Player` dicht genoeg bij de finish spawnt dat na een bepaalde duur training de speler hier wel zich naartoe begeeft, echter zien we wel dat de `Player` niet altijd het oversteekpunt neemt en dus gewoon over de weg zich begeeft. Ook probeert de `Player` auto's te vermijden.

Indien de `Player` te ver spawned dan merken we op dat de speler cirkels begint te draaien. Momenteel is hier nog geen oplossing voor gevonden en zal de `Player` vaak niet over de eindmeet geraken.

De dieptepunten die u op de grijze lijn kan zien is dus het moment dat hierboven besproken werd waar de `Player` rond zat te draaien.

# Slotwoord
### Roadblocks
We hebben enkele roadblocks ervaren die we zeker moesten oplossen om zo het werkende te krijgen.
Enkele van deze roadblocks zijn het dubbel tellen van de collisions waarbij wanneer de player aangereden werd tijdens training dat de score na het resetten van de environment nog meetelde waardoor de speler agent op een score van -1 begon.

Alsook hebben we een roadblock gehad dat de speler door het toevoegen van gravity begon te vliegen in de lucht tegenstrijdig met het toevoegen van gravity natuurlijk, na het verwijderen van gravity was dit opgelost.


[Back to top](#Oversteek-simulator)
