# Opdracht week 2: Containerization

## Van git naar docker

### 1. Git terugblik

Het is mogelijk om een specifiek bestand uit een commit op te halen en over te zetten naar een andere branch. Hiervoor is een specifiekere `git checkout` voor beschikbaar.

```shell
git checkout [-f|--ours|--theirs|-m|--conflict=<style>] [<tree-ish>] --pathspec-from-file=<file> [--pathspec-file-nul]
```
Als er dus op de branch genaamd `test` een `example.txt` zou staan ziet dit er als volgt uit:

```shell
git checkout test -- example.txt
```

Vervolgens spring je met checkout over naar de branch waar je het bestand wilt plaatsen en kan met `git add` het bestand worden toegevoegd aan de branch.

```shell
git add example.txt
```

Vervolgens hoef je enkel nog een commit te maken om dit bestand over te zetten.

Een andere optie is om een commit te maken waarin enkel het ene bestand is gewijzigd en met `git cherry-pick` de specifieke commit over te zetten in de target-branch. Maar hiermee kunnen mogelijk ook andere bestanden meekomen welke niet bedoeld waren als er toch meer bestanden in de commit gewijzigd zijn.

Het is met deze manier niet mogelijk om bestanden uit een andere repo over te nemen. Wel is het mogelijk om met `git log` een patch te maken van een specifieke file in een andere repo en deze patch met `git am`. Hiervoor zijn specifieke flags nodig om te laten werken:

```shell
cd projectA/repository
git log --pretty-email --patch-with-stat --reverse --full-index --binary -- path/to/file_or_folder > tmp/patch
cd ../../projectB/respository
git am < tmp/patch
```

### 2. Commando Overlap

3 Gelijknamige (sub)commando's tussen docker en git:

1. Pull
    - Git haalt met pull de huidige staat van de remote repository op naar een lokale repository. Git download dan alle bestanden die veranderd zijn sinds de laatste keer dat er een pull is uitgevoerd
    - Pull binnen Docker download images van de Docker hub. Dit kunnen zowel externe images zijn als images die je zelf hebt gemaakt. Deze images moeten wel in de Docker hub staan. De images worden lokaal opgeslagen, tenzij de images al een keer gepulled zijn. Vaak worden images automatisch gepulled als er een Docker container wordt gerund die afhankelijk is van een bepaalde image.
2. Push
   - Pushen binnen git betekent dat alle veranderingen in de repository area naar de remote repository worden gebracht, zodat andere ontwikkelaars deze veranderingen naar hun eigen lokale repository kunnen brengen. Bij eerste gebruik van Git moet de ontwikkelaar zijn credentials instellen bij Git config. Git maakt hierbij gebruik van een SSH of een HTTPS verbinding.
   - Met push wordt er een lokale Docker image naar een remote repository gezet. In Docker heet dit de Docker registry. De image moet van tevoren aangemaakt zijn met ```Docker build```. Hiervoor moet de gebruiker met een Docker account ingelogd zijn met een Docker account.
3. Commit
   - Binnen git worden alle lokale veranderingen van de index area naar de repository area klaargezet. Git maakt dan een nieuwe commit hash aan om de veranderingen op te slaan in de history van de repository. Daarna is het optioneel om de veranderingen te pushen naar de remote repository. Het is mogelijk om meerdere commits aan te maken voordat alles gepushed wordt.
   - Docker commit maakt vanuit een container een nieuwe image aan. Als een ontwikkelaar een verandering wilt uitvoeren aan een bestaande image, moet deze ook nog getagged worden. Zo kan de Docker hub onderscheid maken tussen de verschillende images. De image zelf hoeft niet per sé gepushed te worden.

## 3. Eigen servers of cloud servers?

Het verschil tussen cloud en on premise is in de basis niets meer in minder of dat je software hebt draaien op servers die worden beheerd door een derde partij, of door jezelf.

In het geval van GitHub en GitLab is de vergelijking vrij eenvoudig. 

| Git Platform | Cloud | On Premise |
|--------------|-------|------------|
| GitHub       | ✔️    | ✔️         |
| GitLab       | ✔️    | ✔️         |

Het verschil zit hem vooral in de mate waarin ondersteuning beschikbaar is voor de On Premise variant van deze producten. GitHub heeft een [Enterprise variant](https://resources.github.com/getting-started/enterprise/) waarmee ook On Premise hosting mogelijk wordt gemaakt. Bij GitLab zijn er twee verschillende versie beschikbaar, namelijk een [Enterprise Edition of een Community Edition](https://about.gitlab.com/install/ce-or-ee/). Het verschil hierin zit hem vooral in de level van support of subscription features die in het product zitten. Hierbij wordt echter geen onderscheid gemaakt tussen On Premise of Cloud.

Het kiezen voor cloud heeft enkele voordelen zoals:

- Geen overhead kosten voor beheer van hardware systemen en personeel om deze te beheren.
- Geen noodzaak voor redundancy tijdens het ontwerp van de service. Vaak wordt dit door cloud-providers al gegarandeerd met minimale downtime.

Echter zijn er ook nadelen voor de cloud namelijk:

- Compliance structuren moeten via licentie gewaarborgd worden in plaats van dat je hier zelf op kan aansturen. Denk hierbij bijvoorbeeld aan AVG en/of EU-wetgevingen omtrent datagebruik van rechtsgeldige personen.
- In sommige gevallen is een strictere greep nodig op welke hardware beschikbaar is voor het uitvoeren van de taken. Denk bijvoorbeeld aan render-farms voor blockbuster-films.

## 6. Opzet Docker Container
Ga naar de folder unit-testing-using-dotnet-test.

Bouw met Docker de image met:
```
docker build -t <image-name> .
```
Draai een Docker container met:
```
docker run -it --rm -p 5051:80 --name <name-container> <image-name>
```
Gebruik de image-name die bij de vorige stap is gedefinieerd. 

De API is nu te benaderen op [http://localhost:5051/prime/<getal>](http://localhost:5051/prime/1)

## 8. Toevoeging database
Voor de database maken wij gebruik van [Postgres](https://hub.docker.com/_/postgres/)

Ga naar de folder unit-testing-using-dotnet-test.
Bouw en draai de volgende twee containers met: 
```
docker compose up -d
```
Ga daarna in de bash van de Postgres container met:
```
docker exec -it <container-id> bash
```
Als gebruiker postgres is het nu mogelijk om een database aan te maken met
```
createdb mydb
```
## 10. Security
### Aanmaken en toepassen van het ENV bestand
Ga naar de folder unit-testing-using-dotnet-test.
Maak een .env bestand aan

Alle hard-coded waarden moeten vervangen worden met de conventie "${ENV_VELD}".
In het geval van een Postgres gebruiker staat er:
```
user: "${POSTGRES_USER}"
```

Zo zijn de gevoelige gegevens afgeschermd van de git repository.
Het is wel handig om een .env.example erin te hebben zodat andere developers weten wat voor soort gegevens er ingevuld moeten worden.

### Veiligheid Docker Container
De volgende commando's zijn uitgevoerd om de security te verbeteren.
```
- docker scan
- docker scout
- docker scout quickview
- docker scout cves <container-name>:latest
- docker scout recommendations <container-name>:latest

Tussendoor Docker scout updaten
```
Het blijkt dat docker scan is vervangen door docker scout, dat zijn eigen commando's heeft. Met docker scout quickview en cves is het mogelijk om de security vulnerabilities te zien, en met docker scout recommendations voor de container kunnen we zien wat we aan die security vulnerabilities kunnen doen. De recommendations geeft aan om simpelweg de base-image te updaten die de Linux container draait.

```
Recommended fixes for image  webapi-dockerized-app:latest

  Base image is  debian:11-slim

  Name            │  11-slim
  Digest          │  sha256:0cdee24a156b67861e0ec34a0784be80f3c6ec00f0d1708434395aa58d427d44
  Vulnerabilities │    0C     0H     0M    25L
  Pushed          │ 17 hours ago
  Size            │ 31 MB
  Packages        │ 139
  Flavor          │ debian
  OS              │ 11
  Slim            │ v
```
```
   12-slim                                 │ Benefits:                                                          │ 17 hours ago │    0C     0H     0M    17L
  Major OS version update                  │ • Same OS detected                                                 │              │
          -8
  Also known as:                           │ • Image is smaller by 2.2 MB                                       │              │

  • 12.1-slim                              │ • Image contains 13 fewer packages                                 │              │

  • bookworm-slim                          │ • Image introduces no new vulnerability but removes 8              │              │

  • bookworm-20230904-slim                 │ • Major OS version update                                          │              │

                                           │ • Tag is using slim variant                                        │              │

                                           │                                                                    │              │

                                           │ Image details:                                                     │              │

                                           │ • Size: 29 MB                                                      │              │

                                           │ • Flavor: debian                                                   │              │

                                           │ • OS: 12                                                           │              │

                                           │ • Slim: v                                                          │              │

                                           │                                                                    │              │

                                           │                                                                    │              │

                                           │                                                                    │              │

```

Dotnet heeft een aantal tags die op een [bepaalde OS](https://hub.docker.com/_/microsoft-dotnet-sdk/) versie draaien. Hiervoor maakte de Dockerfile gebruik van dotnet versie 7.0, dat op Debian 11 draait. Als we nu van die lijst de tag ```7.0-bookworm-slim``` pakken, wordt de image gedraaid op Debian 12. Daarna was er iets met de cache aan de hand, waardoor de verandering niet meteen te zien was. De containers moesten volledig opnieuw gebouwd worden met:
```
docker compose up --build --remove-orphans --force-recreate -d
```
Door ```docker scout recommendations``` nog een keer uit te voeren, is er te zien dat een aantal security vulnerabilities zijn weggehaald.
```
Recommended fixes for image  webapi-dockerized-app:latest

  Base image is  debian:12-slim

  Name            │  12-slim
  Digest          │  sha256:6f9377128fde3e69e251d0b3c5a7f85f6a20b92ecb81708742c1e153a5c9ce3f
  Vulnerabilities │    0C     0H     0M    17L
  Pushed          │ 18 hours ago
  Size            │ 29 MB
  Packages        │ 126
  Flavor          │ debian
  OS              │ 12
  Slim            │ v
```

### Dev-opdracht

Het gebruik van EF Core raakt meerdere punten van de FURPS+ attributen, namelijk:

- Functionality, doordat EF Core een rijke featureset heeft zoals het maken van migraties & het beheren van de state van de database o.b.v. applicatiecode.
- Reliability, EF Core is als product in een open-source omgeving beter doorgetest en gebugfixed dan een eigenbouw ORM in veel gevallen zal zijn. Dit zal dus ook leiden tot minder fouten in database transacties.
- Supportability, er is veel documentatie en community-posts beschikbaar voor EF Core waardoor veelvoorkomende fouten snel opgespoord kunnen worden. Ook ondersteund EF Core veel verschillende databases om mee te communiceren middels driver-packages zoals Npgsql voor PostgreSQL.

Het gebruik van (Docker) Containers raakt ook enkele FURPS+ attributen, zoals:

- Functionality, gemaakte Images zijn makkelijk herbruikbaar en te delen over meerdere verschillende systemen zolang de CPU architectuur overeenkomt en de Enginer geïnstalleerd is. Ook kunnen Containers compleet afgescheiden worden van andere processen die er niet bij horen te kunnen op hetzelfde hardware systeem.
- Performance, Containers kunnen vrij strict gelimiteerd worden wat betreft de resources die ze gebruiken. Hierdoor blijven meer beschikbare resources vrij voor andere containers of processen die deze mogelijk harder nodig hebben. Hierdoor kan het gehele systeem geoptimaliseerd worden om zo efficiënt mogelijk beschikbare resources te delen over meerdere processen.
- Supportability, met containers worden de applicaties veel makkelijker overdraagbaar tussen verschillende systemen. Ook vergroot dat compatabiliteit doordat alle containers op verschillende locaties zich gelijk gedragen mits de locatie een Docker Engine kan runnen.

## 11. Upgrade naar gebruik applicatieserver

**Wat is het verschil tussen een webserver en een applicatie-server (zou je ook webserver kunnen gebruiken in deze opdracht)?**

In de basis kan een webserver enkel statische gegevens serveren aan een gebruiker met behulp van simpele HTTP verzoeken. Een applicatieserver kan daarentegen veel meer meer, zoals bijvoorbeeld het communiceren met andere apparaten met andere protocollen of het uitvoeren van businesslogica om dynamische responses te genereren.

Vanaf het moment dat de database gekoppeld wordt om nieuw berekende priemgetallen te vinden en op te slaan is dit niet meer mogelijk met een web-applicatie. Er wordt namelijk dynamisch gezocht naar nieuwe waardes.
In de oude situatie waarin alleen voor vaste waardes altijd hetzelfde respons terug zou komen zou dit dus wel met een webserver gedaan kunnen worden.

**Wat zijn voordelen/mogelijkheden van een webserver als nginx boven een kale/self hosting applicatie?**

Nginx is een platform voor het opzetten van web-server waarin standaard al veel features in verwerkt zitten als load balancing, reverse proxying en caching. 
Allerlei van dit soort features zouden met een rechtstreekse deployment zelf moeten worden geïmplementeerd in de applicatie met alle complexiteit van dien.

**Wat is het nadeel/nadelen?**

Nginx heeft de mogelijkheid om modules toe te voegen aan de core functionaliteit. Maar deze modules moeten meegeïnstalleerd worden bij de eerste installatie. Het toevoegen van nieuwe modules aan een bestaande omgeving betekend dus dat deze moet worden hercompileerd.
Andere tools zoals Apache hebben bijvoorbeeld ondersteunding voor dynamisch modules inladen, waardoor herinstallatie niet nodig is.

Een ander punt is dat de configuratie voor Nginx centraal staat. Dit houdt dus in dat vanaf één punt alle instellingen voor alle subdirectories en daarme mogelijke ook subdomeinen inzichtelijk zijn.
Bij Apache wordt dit per folder gedaan waarbij de parent folder vaak meer toelaat dan de child-directories waardoor er een inheritence structuur ontstaat van instellingen. Management van opties in een dergelijk systeem wordt wel moeilijker hierdoor.

**ADR**

| Server Platform |  |
|-----------------|--|
| Status          | Proposed |
| Context         | Er moet een C# applicatie via het internet bereikbaar worden. Hiervoor moet een Server Platform gekozen worden om de applicatie op te hosten. |
| Decision        | Nginx |
| Consequences    | Met Nginx kan een reverse-proxy opgezet worden om toegang te bieden tot de applicatie. Daarbij biedt Nginx ook mogelijkheid tot load-balancing waarmee ook de mogelijkheid er is om meerdere containers met dezelfde features te gebruiken om hoge hoeveelheid requests af te handelen. |
| Positions       | **Windows Service** <br/> **Apache** <br/> **Internet Information Services** |

## 13. Container extern op VPS server
### Image naar docker registry
- [API container](https://hub.docker.com/repository/docker/kafune/unit-testing-using-dotnet-test-app/general)
- [Postgres DB container](https://hub.docker.com/repository/docker/kafune/unit-testing-using-dotnet-test-postgres/general)

We maken voor de VPS server geen gebruik van de images van de docker registry. In plaats daarvan gebruiken we Docker context om via SSH de lokale containers te draaien op de VPS server.
Op dit moment werkt de database niet, maar de aanvraag gaat wel door. Dit is te zien met Docker compose, en dan de logs binnen de containers te bekijken. De website is [hier](http://145.74.104.91/primes/1) te vinden
 

## Reflectie

In de opdracht van deze week zijn enkele punten uit het [CDMM](https://hanaim-devops.github.io/devops/beoordelingsmodel.html#cdmm-basis-tot-gemiddeld) voorbijgekomen, in de kopjes hieronder staat uitgelegd hoe wij deze punten uit het CDMM hebben toegepast in de opdracht.

**OA-103 Dependency Management**

Voor het refactoren van het C# project is gebruik gemaakt van de NuGet package manager. Hiermee zijn verschillende packages toegevoegd aan verschillende projecten zoals:

- EF Core + Npgsql, voor ORM doeleinden met een Postgres Database.
- ASP.NET Core, voor het maken van een API-endpoint om de applicatie te gebruiken.

Ook zijn er gedurende de week aanpassingen gemaakt aan Dockerfile's. Met name de dependency van het platform waarop de applicatie runt is hierin veranderd.
Waar in een eerste iteratie een Debian image gebruikt was, is er gedurende de week overgestapt naar een Alpine image om het formaat te beperken.

**BD-203 Build once deploy anywhere**

De Docker Image die gebouwd is van de applicatie is beschikbaar gemaakt om op meerdere omgevingen te kunnen draaien. Gedurende de week is de image ingezet op de systemen van onszelf. Maar ook is deze op een remote VPS gezet waar deze zich gelijk gedraagd als op de lokale machines.

**BD-204 Automatiseer meeste DB wijzigingen**

In het C# project is gebruik gemaakt van Migrations met behulp van EF Core. Deze migrations zorgen ervoor dat met een simpel commando de database up to date gebracht kan worden met de beoogde state.

Hoewel er wel gebruik wordt gemaakt van Migrations is het nog niet verwerkt in de runtime checks van de applicatie. Hierdoor moeten deze nog handmatig of gescript uitgevoerd worden, dit is wel een volgende stap die ondernomen kan worden zodat hiervoor ook geen handmatig werk nodig is.

Om deze laatste rede werkt de remote VPS nog niet geheel naar behoren zodra de API benaderd wordt.

**BD-206 Gescripte config wijzigingen**

Om zoveel mogelijk hardcoded waardes te vermijden in de codebase en in de docker files is er gebruik gemaakt van een .env bestand waarin dit soort waardes vastgelegd kunnen worden.
Dit bestand wordt niet meegecommit naar de repo behalve de voorbeelden die erbij horen.

In de docker-compose wordt deze gebruikt, maar nog niet in de codebase. Hiervoor moet met name in de DAL nog het e.e.a. aangepast worden om dit juist te laten werken.

**BD-401 Build Bakery**

Op InfoQ was het moeilijk om een definitie te vinden voor de Build Bakery. Echter in dit [artikel](https://thenewstack.io/bakery-foundation-container-images-microservices/) onder het hoofdstuk "Defining the Model of a Bakery" beschrijven dit concept als:

*Het proces van het verkrijgen, bouwen en releasen van machine images om herhaaldelijk deployen van werkende code mogelijk te maken.*

Hoewel hier in deze week nog geen ge-automatiseerd proces voor is opgezet is er wel een eerste stap gemaakt door te beginnen met het maken van eigen images voor zelfgemaakte code. Nadat deze gebuild waren zijn ze op Docker Hub geplaatst, waardoor de images verkrijgbaar waren op de VPS om deze daar te deployen.
