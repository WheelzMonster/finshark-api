Program.cs = point d'entrée ou l'on configure la façon dont le programme va run
et on ajoute des services

CREATE => POST
READ => GET
UPDATE => PUT (change toute la ressource) ou PATCH (change partiellement la ressouce)

DELETE => DELETE

ORM (entity framework ici) = Object Relational Mapper => ça transforme les colonnes des tables de BDD en Objet

ApplicationDBCOntext va nous permettre de spécifier à quelle table nous voulons accéder

un DTO va permettre de transporter les données entre différents couches de l'app,
en choisissant les données qu'on veut transférer ou nom

par exemple si on récupère les infos d'un model User qui contient username et pw, on va utiliser
un dto pour ne renvoyer que le username, en gros je controle ce que j'expose

![img.png](img.png)

ATTENTION il existe des DTO de response (celui dont je viens de parler) et des DTO de request
ce qui fait globalement le même taff mais pour la création d'info (exemple on veut pouvoir controler les données avec lesquelles le user créer un truc,
on va pas lui demander de renseigner un Id)

Mapping = transformation d'un objet vers un autre

Question posée à chatGPT : 

Si le DTO est déjà censé ne contenir que les propriétés utiles, à quoi bon refaire un filtre dans un mapper ? Pourquoi ne pas juste créer le DTO directement ?

✅ Réponse : Le DTO filtre par sa structure, le mapper filtre par les données et le contexte.
✳️ 1. Le DTO filtre les champs exposés
Il détermine ce que l’extérieur peut voir.

Ex : PasswordHash n’est pas dans le DTO, donc impossible de l’exposer même par erreur.

✳️ 2. Le mapper filtre les valeurs concrètes
Même si le champ existe dans le DTO, tu peux :

Modifier, tronquer ou anonymiser une valeur.

Adapter le format (ex : convertir une date, un enum, etc.).

Filtrer en fonction de la logique métier (ex : ne pas mettre l’email si l’utilisateur est anonyme).

🔍 Exemples pour clarifier
🚫 Si tu fais ça sans mapper :
csharp
Copier
Modifier
var dto = new StockDto {
Id = stock.Id,
Symbol = stock.Symbol,
CompanyName = stock.CompanyName,
Industry = stock.Industry
};
Tu le fais dans chaque contrôleur, à la main. Peu réutilisable.

✅ Avec mapper :
csharp
Copier
Modifier
var dto = stock.ToStockDto();
Plus propre.

Centralisé (si un jour tu veux changer comment une propriété est construite, tu le fais en un seul endroit).

Tu peux faire des conditions, des calculs, de la logique métier.

🧠 En résumé
DTO	Mapper
Déclare les champs visibles	Définit comment les remplir
Structure statique	Logique dynamique
Ne contient pas de logique métier	Peut en contenir un peu (filtrage, formatage)
Ne change presque jamais	Change plus facilement si besoin

.find VS .firstOrDefault

Find est Uniquement utilisable avec la clé primaire.
Tu ne peux pas faire de conditions comme Find(x => x.Symbol == "AAPL") → ça n’existe pas.

avec firstOrDefault on peut faire des choses plus complexes
_dbContext.Stocks.FirstOrDefault(s => s.Symbol == "AAPL" && s.Price > 100);

Pour Async / Await
une fonction avec async, renverra toujours une Task<UnType> genre Task<ActionResult>
parce que sur une fonction async il faut etre sur que quelque chose est return
si le traitement n'est pas fini (et donc que le actionResult est pas encore la)
alors rien ne serait renvoyé, c'est pour cela qu'on wrap le tout dans une task, pour avoir quelque chose en attendant
et des que la fonction a fini alors on renvoi le actionResult


🧠 .Include() sert à quoi ?
.Include() permet de charger explicitement des données liées (relations) à une entité principale, en une seule requête SQL.

C’est ce qu’on appelle le "eager loading" (chargement anticipé).

🎯 Sans .Include() :
Tu récupères uniquement l’objet principal.

Exemple :
var stock = await _dbContext.Stocks
.FirstOrDefaultAsync(s => s.Id == 1);
Tu obtiens un Stock, mais sa liste de Comments est vide (non chargée), même si elle existe en base.

✅ Avec .Include() :

var stock = await _dbContext.Stocks
.Include(s => s.Comments)
.FirstOrDefaultAsync(s => s.Id == 1);
Tu obtiens le Stock, et EF Core inclut aussi les Comments liés.

➡ Cela génère une requête SQL avec jointure pour ramener tous les commentaires associés dès le départ.

