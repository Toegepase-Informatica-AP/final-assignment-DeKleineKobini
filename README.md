# Oversteek simulator

## Korte samenvatting
Voor dit project hebben wij geopteerd om een VR omgeving te maken waarin kinderen zullen kunnen leren om veilig de straat over te steken en hoe ze deze situaties moeten interpreteren.

Hier zullen de autos volledig door een getrainde AI worden bestuurd om zo echte bestuurders te simuleren.
## Installatie

## Inleiding

## Verloop simulatie

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
# Beschrijving gedragingen objecten

# Resultaten training