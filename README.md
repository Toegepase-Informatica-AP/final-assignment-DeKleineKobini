# Oversteek simulator
# Table of contents
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
Voor het vak VR-Experience kregen wij de opdracht om een spel te bedenken waarbij zowel Virtual Reality als ML-Agents een meerwaarde zal zijn.
Hiervoor maken we gebruik van Unity.
In dit document zal u een goede uitleg over het project krijgen, wat we hier allemaal in gedaan hebben en hoe alles in zijn werking gegaan is. 

## Korte samenvatting
Voor dit project hebben wij geopteerd om een VR omgeving te maken waarin kinderen zullen kunnen leren om veilig de straat over te steken.
Het is de bedoeling dat ze leren hoe ze veilig kunnen oversteken.

Hier zullen de autos volledig door een getrainde AI worden bestuurd om zo echte bestuurders te simuleren.

## Installatie

| Programma/tool/asset  | Versie         |  Naam                      |
| --------------------- | -------------- | -------------------------- |
| Unity                 | 2019.4.16f1    |                            |
| Python                | 3.8.1 of hoger |                            |
| ML Agents             | 1.0.5          |                            |
| Tensorboard           | 2.3.1          |                            |
| Oculus XR plugin      | 1.6.1          |                            |
| Unity asset           | /              | Character Pack Free Sample |

![Unity asset character](md_images/characterpack.png)

## Verloop simulatie
Wanneer de simulatie start zal de speler een random positie toegekend krijgen. 
Op dit moment zullen er ook auto's beginnen verschijnen die op de weg rijden 

## Observaties, acties en beloningen

### Beloning structuur
Voor onze beloningen hebben we verschillende tabellen aangezien we met meerde AI agents zullen werken en hierdoor zal elke agent ook een specifieke reward structure hebben.
|Agent          |Rijdt tegen speler   |Komt op bestemming   |Rijdt te snel   |Niet op bestemming   |Raakt auto          |
|---            |---                  |---                  |---             |---                  |---                 |
|Goede auto     |-1                   |+1                   |-0.1            |-0.001               |-0.8                |
|Slechte auto   |-0.5                 |+1                   |-0.1            |-0.002               |-0.8                |
|Player         |NVT                  |+1                   |NVT             |-0.001               |-1                  |

## Beschrijving objecten
### Auto
![Car image](md_images/carimage.png)

![Car settings](md_images/carSettings.png)

![Car box Collider](md_images/carBoxCollider.png)

Aan de auto werd een ray perception sensor, Decision requester en het good car script toegevoegd.
Zorg ook zeker dat de box collider verandert word zodat deze de auto goed encapsuleert.

Bij de 2de auto prefab zien we dat er een bad car script aan toegevoegd werd.
De settings voor alle 2 de auto's zijn compleet hetzelfde enkel het aangevoegd script (Goodcar, Badcar) verandert.

### Player
![Player image](md_images/playerModelImage.png)

![Player info](md_images/playerInfo.png)

![Player info2](md_images/playerInfo2.png)

![Player eyes](md_images/playerEyes.png)

![Player eyes down](md_images/playerEyesDown.png)

Bij de speler zien we dat er ray perception word toegevoegd in empty game objects om zo het peripheraal zicht na te bootsen.

Alsook moet er op het player object zelf ook nog de box collider worden aangepast en de nodige scripts toegevoegd Bvb: `OVR Player controller`

### Pedestrian Crossing
![Pedestrian Crossing](md_images/stripeImage.png)

![Pedestrian crossing info](md_images/stripeInfo.png)

Hier zien we een streep van het zebrapad waar de speler over zal kunnen wandelen. Daarboven kan een box collider geobserveerd worden die bovenop de streep hangt zodat we kunnen controleren wanneer  er verschillende entities in contact komen met het zebrapad.

### Scene
![Scene image](md_images/sceneImage.png)

In deze foto kunnen we zien dat er een basisscene opgesteld is waarop we een zebrapad, voetgangerspad en een weg kunnen observeren. We hebben bewust gekozen om de scene basic te houden om zo meer op de functionaliteit te kunnen letten waardoor we meer vooruitgang konden boeken.
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

