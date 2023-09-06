# Opdracht week 2: Containerization

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
