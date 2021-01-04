# Oversteek simulator

## Inleiding
In dit document zal u een goede uitleg over het project krijgen en wat we hier allemaal in gedaan hebben en hoe alles in werking gegaan is. 

## Korte samenvatting
Voor dit project hebben wij geopteerd om een VR omgeving te maken waarin kinderen zullen kunnen leren om veilig de straat over te steken en hoe ze deze situaties moeten interpreteren.

Hier zullen de autos volledig door een getrainde AI worden bestuurd om zo echte bestuurders te simuleren.

In dit document zal u een korte uitleg krijgen over hoe het project werkt.

## Installatie
- Unity V2019.4.16f1
- ML Agents V1.0.5
- Oculus XR plugin V1.6.1

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

# Beschrijving objecten
### Auto
![Car image](md_images/carimage.png)

![Car settings](md_images/carSettings.png)

![Car box Collider](md_images/carBoxCollider.png)

Aan de auto werd een ray perception sensor, Decision requester en het good car script toegevoegd.
Zorg ook zeker dat de box collider verandert word zodat deze de auto goed encapsuleert.

Bij de 2de auto prefab zien we dat er een bad car script aan toegevoegd werd.
De settings voor alle 2 de auto's zijn compleet hetzelfde enkel het aangevoegd script (Goodcar, Badcar) verandert.
### Auto scripts
```C#
public class GoodCar : Car
{
    public override void AddNotOnDestinationReward()
    {
        AddReward(-0.001f);
    }

    public override void AddMovesTooFastReward()
    {
        if (environment == null)
        {
            environment = GetComponentInParent<Environment>();
        }

        if (environment != null && rb.velocity.x > environment.maxSpeed)
        {
            AddReward(-0.1f);
        }
    }

    public override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerController"))
        {
            AddReward(-1f);
            Destroy(this.gameObject);
            EndEpisode();
        }
        else if (other.gameObject.tag.Contains("Destination"))
        {
            AddReward(1f);
            Destroy(this.gameObject);
            EndEpisode();
        }
        else if (other.gameObject.CompareTag("Car"))
        {
            AddReward(-0.8f);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            EndEpisode();
        }
    }
}
```
### Player
![Player image](md_images/playerModelImage.png)
### Pedestrian Crossing
![Pedestrian Crossing](md_images/stripeImage.png)
Hier zien we een streep van het zebrapad waar de speler over zal kunnen wandelen. Daarboven kan een box collider geobserveerd worden die bovenop de streep hangt zodat we kunnen controleren wanneer  er verschillende entities in contact komen met het zebrapad.

### Scene
![Scene image](md_images/sceneImage.png)
In deze foto kunnen we zien dat er een basisscene opgesteld is waarop we een zebrapad, voetgangerspad en een weg kunnen observeren. We hebben bewust gekozen om de scene basic te houden om zo meer op de functionaliteit te kunnen letten waardoor we meer vooruitgang konden boeken.
# Beschrijving gedragingen objecten
### Auto
Bij het gedrag van de auto zien we dat de auto's vooruit zullen rijden om zo aan hun gedesigneerde eindzone te geraken. Indien ze een speler klaar zien staan aan het voetpad zullen de auto's moeten stoppen om zo de speler over te laten. 

Echter hebben we er ook voor gezorgd dat we enkele auto's niet laten stoppen om zo het verkeer het beste te simuleren.
Zo zal de speler kunnen leren omgaan met auto's die zoals in een echte verkeerssituatie niet zullen stoppen.

# Verloop van de training
Om de auto's op een goede manier te trainen hoe het verkeer in het echt zou lopen hebben we geopteerd om de speler ook een agent toe te kennnen tijdens de training. 
Dit zal ervoor zorgen dat wanneer we de training starten de auto's zullen leren om te gaan met een onvoorspelbare speler. 

In het volgende hoofdstuk zullen we meer uitbreiden over de resultaten die we hebben geobserveerd van onze training.
# Resultaten training