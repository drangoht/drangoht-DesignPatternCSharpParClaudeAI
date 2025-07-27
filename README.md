# Design Patterns en C# 🎨

Ce projet est une implémentation complète des 23 design patterns du Gang of Four (GoF) en C#. Chaque pattern est implémenté de manière claire et documentée, avec des exemples concrets d'utilisation.

## Structure du Projet 📁

```
DesignPatternsProjects/
├── DesignPatternsMenu/            # Application console principale
├── Patterns.Common/              # Interfaces et classes communes
├── Patterns.Creational/         # Patterns de création
│   ├── AbstractFactory/
│   ├── Builder/
│   ├── FactoryMethod/
│   ├── Prototype/
│   └── Singleton/
├── Patterns.Structural/         # Patterns structurels
│   ├── Adapter/
│   ├── Bridge/
│   ├── Composite/
│   ├── Decorator/
│   ├── Facade/
│   ├── Flyweight/
│   └── Proxy/
└── Patterns.Behavioral/         # Patterns comportementaux
    ├── ChainOfResponsibility/
    ├── Command/
    ├── Interpreter/
    ├── Iterator/
    ├── Mediator/
    ├── Memento/
    ├── Observer/
    ├── State/
    ├── Strategy/
    ├── TemplateMethod/
    └── Visitor/
```

## Comment utiliser ce projet 🚀

1. Clonez le repository
2. Ouvrez la solution dans Visual Studio 2022 ou plus récent
3. Compilez le projet
4. Exécutez l'application DesignPatternsMenu

L'application propose un menu interactif permettant de :
- Choisir une catégorie de patterns (Création, Structure, Comportement)
- Sélectionner un pattern spécifique à exécuter
- Voir la démonstration concrète du pattern en action

## Patterns implémentés ✨

### Patterns de Création
- **Abstract Factory** : Création de familles d'objets liés
- **Builder** : Construction d'objets complexes étape par étape
- **Factory Method** : Création d'objets via une interface commune
- **Prototype** : Création d'objets par clonage
- **Singleton** : Instance unique partagée

### Patterns Structurels
- **Adapter** : Compatibilité entre interfaces incompatibles
- **Bridge** : Découplage abstraction/implémentation
- **Composite** : Structure arborescente d'objets
- **Decorator** : Ajout dynamique de fonctionnalités
- **Facade** : Interface simplifiée pour un sous-système
- **Flyweight** : Partage efficace d'états similaires
- **Proxy** : Contrôle d'accès aux objets

### Patterns Comportementaux
- **Chain of Responsibility** : Chaîne de traitement
- **Command** : Encapsulation de requêtes
- **Interpreter** : Interprétation de langages simples
- **Iterator** : Accès séquentiel aux éléments
- **Mediator** : Communication centralisée
- **Memento** : Capture/restauration d'états
- **Observer** : Notification de changements d'état
- **State** : Comportement basé sur l'état
- **Strategy** : Algorithmes interchangeables
- **Template Method** : Structure d'algorithme commune
- **Visitor** : Opérations sur une structure d'objets

## Caractéristiques 🌟

- Code entièrement documenté en français
- Tests et démonstrations pour chaque pattern
- Interface utilisateur console interactive
- Gestion des erreurs robuste
- Support complet de la nullabilité (C# 9.0+)

## Prérequis 🛠️

- .NET 9.0 ou supérieur
- Visual Studio 2022 ou plus récent
- Système d'exploitation : Windows/Linux/MacOS

## Comment contribuer 🤝

1. Forkez le projet
2. Créez une branche pour votre fonctionnalité
3. Soumettez une pull request

## Licence 📄

Ce projet est sous licence MIT. Voir le fichier LICENSE pour plus de détails.

---

*Note: Ce projet a été entièrement généré avec l'assistance de l'Intelligence Artificielle Claude Sonnet (Anthropic). Le code, la structure, et la documentation ont été créés à travers une collaboration interactive avec l'IA, démontrant les capacités de co-création entre l'humain et l'intelligence artificielle.*
