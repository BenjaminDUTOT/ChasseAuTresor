# Carte aux trésors

## Lancer le projet
 - Pour lancer le projet, cloner ou télécharger l'archive de ce repo.
 - Se rendre dans le dossier du projet `ChasseAuTresor\ChasseAuTresor.Service` et lancer la commande `dotnet build`
 - Aller dans le dossier target (`ChasseAuTresor\ChasseAuTresor.Service\bin\Debug\net8.0` par défaut) et lancer le `ChasseAuTresor.Serivce.exe`.
    - Par défaut, le fichier avec les paramètres d'entrées est `ChasseAuTresor\ChasseAuTresor.Service\data\DefaultParameter\parameter_Default.txt`. Il est possible de préciser le fichier voulu avec la commande :
		- `ChasseAuTresor.exe --input chemin\vers\input.txt`
    - Par défaut, le fichier avec les paramètres de sortie est `ChasseAuTresor\ChasseAuTresor.Service\data\result.txt` Si je souhaite spécifier un chemin où le fichier doit être écrit : 
        - `ChasseAuTresor.exe --output chemin\vers\output.txt`
 - Il est aussi possible de passer par un IDE.

## Architecture du projet
Le projet est constitué de 3 grandes parties :
 - **ChasseAuTresor.Service** : Classe principale qui permet de lancer le programme
 - **ChasseAuTresor.Model** : contient tout le code fonctionnel du projet
 - **ChasseAuTresor.Test** : contient tous les tests du projet

## Environnement technique
 - C#
 - .Net 8 
 - NUnit
 - CommandLineParser
