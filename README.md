# Opdracht week 2: Containerization

Deze week focussen we op containers, het gebruik van Docker, en data binnen DevOps. De hoofdopdracht is het bouwen van een Docker container 'rondom' de (basic) .NET applicatie die je hebt gescaffold bij de opdracht afgelopen week.

Je vormt een nieuw duo. Maak weer een planning van de taken. Kijk welke je parallel kunt doen en welke niet (die kun je samen doen). Je fixed evt. openstaande issues uit de vorige opdracht. Hiervoor upgrade je de gemaakte classlib eerst nog naar een 'web api'. Je kijkt naar Container security scanning en het verbinden van de container met je `API` met een `MSSQL` database door juiste networking in te stellen.

Ook switchen we deze week van GitHub naar GitLab om ervaring op te doen met een 2e 'code management' tool (maar ook een cloud-based git schil).

- [Opdracht week 2: Containerization](#opdracht-week-2-containerization)
  - [Theorie: Van week 1 naar week 2, recap en transfer](#theorie-van-week-1-naar-week-2-recap-en-transfer)
    - [1. git terugblik](#1-git-terugblik)
    - [2. Commando overlap](#2-commando-overlap)
  - [Tools](#tools)
    - [macOS](#macos)
    - [Windows](#windows)
  - [Refactoren](#refactoren)
  - [Bouwen](#bouwen)
  - [Optimaliseren](#optimaliseren)
  - [Networking](#networking)
  - [Opslag](#opslag)
  - [Security](#security)
  - [Upgrade naar gebruik applicatieserver (deels optioneel)](#upgrade-naar-gebruik-applicatieserver-deels-optioneel)
    - [Theorievragen hierbij](#theorievragen-hierbij)
  - [Run container op een eigen VPS extern](#run-container-op-een-eigen-vps-extern)

## Theorie: Van week 1 naar week 2, recap en transfer

Zet in de README een stukje theorie. Zet dit onder een eigen [(markdown) kopje](https://www.markdownguide.org/basic-syntax/#headings) `Van <week-thema-1> naar <week-thema-2>; van docker naar git` (NB de stukjes tussen vishaken invullen).

### 1. Git terugblik

Met welk commando kun je enkele stukjes code uit een commit uit een andere branch van de repo halen? Zoek uit of je hiermee ook code uit een heel andere repository kan halen. Kijk of je deze opdracht tekst als `opdracht-2.md` in je eigen repository kunt setten.

### 2. Commando overlap

Docker en git zijn beiden DevOps tools, maar zijn wel heel verschillende tools. Toch hebben de git cli en Docker cli enkele 'gelijknamige' (sub)commando's. Noem er minstens drie. Leg deze subcommando's uit, beschrijf wat ze doen en wat het verschil is met gelijknamige git commando. Geef hierbij in ieder geval de volgende drie dingen aan voor elk van de commando's:

1. Waaróp de actie werkt
2. Wáar de actie gegevens vandaan haalt
3. Waar de gegevens heengaan/oopgeslagen worden

Beetje abstracte vraag wellicht. Om iets concreter te maken, stel dat zowel git als Docker een `clone` commando zouden hebben (wat niet zo is, want `docker clone` actie/commando bestaat niet), dan zou je dus bijvoorbeeld aangeven:

- Wat het clone commando cloned
- Waarvandaan je cloned
- Waarnaar toe je cloned

## Tools

Deze week heb je Docker en een [Docker hub](https://hub.docker.com/signup) account nodig.

### macOS

```sh
brew install --cask docker
```

Mocht je een nieuwe Mac met M1 chip hebben, waarschijnlijk heb jij dan ook `rosetta` nodig.

### Windows

```ps
choco install docker-desktop
```

## 3. Eigen servers of cloud servers?

Leg in je README kort uit het verschil tussen *cloud* en *on premise*. Google dit voor jezelf zo nodig. Gebruik in je uitleg als  voorbeeld van github en gitlab omgeving die we gebruiken (zie volgende opdracht). Welke is cloud en welke on premise? Geef ook aan, of dit een fundamenteel verschil is, of dat het ook andersom zou kunnen. E.g. bieden beide services zowel een on premis als een cloud variant aan? Noem ook minstens 2 redenen dat een bedrijf (of freelancer?) cloud zou gebruiken, of juist on premise.

## 4. GitHub naar GitLab

Clone de GitHub repo die je vanuit GitHub classroom hebt gekregen naar lokaal. Maak vanuit lokaal een 2e remote aan naar een repo in de [GitLab omgeving](https://gitlab.devops.aimsites.nl) omgeving (docent doet dit voor)

## 5. Refactoren

De code van je opdracht van vorige week kun je nu enkel runnen via het runnen van de unit tests. Upgrade je >NET classlib code (priemgetal checken) met een 'console applicatie' (een `main` entrypoint).
En refactor daarna verder naar een Web API project. De docent doet dit voor/legt verder uit. Pas hierbij 'branch by abstraction' toe door op de goede momenten te committen (altijd op een moment dat je code compileert én werkt).

## 6. Bouwen

Bouw een Docker container voor jouw `API`. Dit kan je doen door deze [guide](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-5.0) te volgen.

- Voeg een `Dockerfile` toe aan jullie repository.

## 7. Optimaliseren

Als het werkt, ga je nu je Docker image nog verder optimaliseren. Dit kan je doen met [deze](https://www.thorsten-hans.com/how-to-build-smaller-and-secure-docker-images-for-net5/) of [deze](https://itnext.io/smaller-docker-images-for-asp-net-core-apps-bee4a8fd1277) guide. 

- Leg de optimalisatie stappen vast door screenshots en voeg een mooie tabel toe aan de `README .md` die laat zien hoeveel kleiner de container is geworden. Deze moet kleiner zijn dan `~209MB`.

## 8. Networking

Tijdens de ORM les deze week voeg je aan jouw `API` een `MSSQL` database toe. Om deze beide in Docker te draaien maken we gebruik van `docker compose`.

- Maak een `docker-compose` bestand met een `api` en `db` service
- Pas de database connectie string aan.

## 9. Opslag

Een container kan makkelijk verwijderd worden. Dan is alle database informatie weg.

- Voeg een volume toe aan de `db` service.

## 10. Security

- Zorg dat er geen private keys, database wachtwoorden of andere secrets in je code/repo staan (maar het wel werkt, documenteer in je README hoe/waar je secrets opslaat/waarom)
- Stel de veiligheid van jouw Docker container vast en rapporteer deze in de `README`.

```sh
docker scan <image-tag>
```

Mogelijk moet eerst ingelogd worden met een docker account

```sh
docker login --username <username>
```

- Los een van de security vulnerability op en rapporteer jouw stappen in de `README`.

Zorg nu dat je gemaakte Docker container in een zelf gekozen registry, ANDERS dan Docker hub. Maar wel private! Je mag evt. wel beginnen met Docker Hub als simpele opstart (je hebt toch al een account vanwege security substap hiervoor). We raden aan gebruik van github [(GitHub packages](https://docs.github.com/en/packages/learn-github-packages/introduction-to-github-packages), ghcri.io) of gitlab, waar je onder de `hanaim-devops` organisatie respectievelijk de self hosted gitlabs op aimsites private packages kunt opslaan.

<https://stackoverflow.com/questions/39359463/entity-framework-sql-injection/39361759>
 
OPS role:

- Geeft het gebruik van Entity Framework nog 'security considerations'?
- Zoek ze op op de [Microsoft site](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ef/security-considerations), en formuleer een opdracht voor de developer voor uitbreiden code. Bedenk hiervoor een dataset. Zet in README (Dev hoeft hier nu niet perse meteen aan de slag)

DEV rol:

- Welke van de non functional requiremtns (de URPS uit FURPS+) kun je bereiken via gebruik van EF?
- Welke via gebruik container/Docker? Geef belangrijkste punten en licht toe met een voorbeeld van extra feature op gemaakte code.

## 11. Upgrade naar gebruik applicatieserver (deels optioneel)

Je kunt je Docker container upgraden door je web applicatie te draaien in een applicatieserver. We raden nginx aan. Hiervoor is een goede tutorial onder docs.microsoft.com. Beantwoord onderstaande vragen en opdrachten, en voer de tutorial eventueel uit (beantwoorden en ADR is verplicht, uitvoeren optioneel).

### 12. Theorievragen bij applicatieserver

- Wat is het verschil tussen een webserver en een applicatie-server (zou je ook webserver kunnen gebruiken in deze opdracht)?
- Wat zijn voordelen/mogelijkheden van een webserver als **nginx** boven een kale/self hosting applicatie?
- Wat is het nadeel/nadelen?

Voeg in je repository een [Architecture Decision Record](https://github.com/joelparkerhenderson/architecture-decision-record) (ARD; voor meer info artikel achter link lezen, evt. vragen aan docent stellen in de les). In de ADR Hierin geef je aan of je nginx of andere het wel doet en beantwoord de gevraagde voordelen en nadelen.
Je mag een van de simpelere formats gebruiken van Michael Nygard, maar dan wel ook met noemen alternatieven/positions uit die van Tyree en Akerman. Zoek hiervoor alternatieven en of nginx in deze minor/voor .NET een goede keus is of dat er beter alternatief is.

## 13. Run container op een eigen VPS extern

Tot slot ga je de container opleveren naar een externe server i.p.v. enkel op localhost.
Deze VPS krijg je van school, maar moet je aanvragen en voor Docker optuigen. Verdeel de taken, maar zorg dat je het allebei snapt en ook daadwerkelijk kunt updaten.

1. Onderteken beiden het anti-misbruik formulier en vraag VPS aan.
1. Maak een SSH key aan en zorg dat je kunt connecten met de VPS (zie handleiding)
1. Installeer Docker op de remote VPS
1. Zorg dat je vanaf lokaal kunt connecten met docker (gebruik `docker context` die je op remote exporteert en lokaal importeert (zorg dat SSH is toegevoegd)
1. Kijk of je applicatie kunt bekijken. Zorg in ieder geval dat de applicatie bij opstart een melding wegschrijft die je kunt checken
