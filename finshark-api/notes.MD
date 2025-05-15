Program.cs = point d'entrée ou l'on configure la façon dont le programme va run
et on ajoute des services

CREATE => POST
READ => GET
UPDATE => PUT (change toute la ressource) ou PATCH (change partiellement la ressouce)

DELETE => DELETE

ORM (entity framework ici) = Object Relational Mapper => ça transforme les colonnes des tables de BDD en Objet

ApplicationDBCOntext va nous permettre de spécifier à quelle table nous voulons accéder

