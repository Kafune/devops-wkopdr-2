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

De API is nu te benaderen op [http://localhost:5051/weatherforecast](http://localhost:5051/weatherforecast)

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
