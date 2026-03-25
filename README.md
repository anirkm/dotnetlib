# Donde esta la biblioteca

## Sujet 

Vous allez réaliser une petite application qui vous permettra de gérer une liste de bibliothèque ayant des livres à louer.
Une petite BDD sera jointe au projet qu'on va essayer d'enrichir au maximum.
L'idée est d'être en autonomie par Groupe de 2.

Le but du projet est de voir un maximum des choses que .NET peut vous apporter donc n'hésitez pas à demander au prof.

Pensez aussi à regarder les raccourcis utiles en bas de page.

Créer un repository sur GitHub en privé que vous mettrez à jour pour garder vos sources.

- *Solution*
  - La racine de votre application .NET qui permet d'avoir la vision globale des projets
  - Manifestée sous la forme d'un fichier **.sln**, c'est ce fichier qu'il faut ouvrir pour ouvrir votre espace de travail
  - Va contenir un ou plusieurs projets
  - **Analogie** : Pensez à la solution comme un "classeur" qui organise plusieurs "cahiers" (projets)
  - **Exemple** : Une solution `LibraryManager.sln` peut contenir plusieurs projets : `LibraryManager.App`, `LibraryManager.Services`, etc.
  
- *Projet* :
  - Bloc de construction de l'application
  - Va générer une dll (Dynamic Link Library) par projet - c'est un fichier compilé qui contient du code réutilisable
  - Facilement extractable d'une solution à une autre
  - **Types de projets** :
    - *Application Console* : Programme qui s'exécute dans une fenêtre de commande
    - *Bibliothèque de classes* : Ensemble de code réutilisable (ne s'exécute pas seul)
    - *Web API* : Service web qui expose des endpoints HTTP
  - **Avantage** : Permet de séparer les responsabilités et de réutiliser le code dans différentes solutions
  
- *Nuget* :
  - Gestionnaire de package pour .NET (équivalent à npm pour JavaScript, pip pour Python)
  - Permet d'intégrer des bibliothèques externes à votre projet pour étendre les fonctionnalités de votre code (AutoMapper, ORM...)
  - On peut l'ouvrir en faisant `Clic droit > Gestionnaire Nuget` en cliquant sur un projet ou la solution
  - **Exemples de packages populaires** :
    - `Microsoft.EntityFrameworkCore` : ORM pour accéder aux bases de données
    - `Newtonsoft.Json` : Manipulation de JSON
    - `AutoMapper` : Mapping automatique entre objets
  - **Fichier de configuration** : Les packages sont listés dans le fichier `.csproj` de votre projet 

## Critères de qualité

- Commits réguliers, au moins à chaque étape
- Indentation
- En Anglais
- Privilégier les interfaces aux classes concrètes (normalement en suivant bien le projet ça devrait être facile)
- Nommage :
  - "Singulier" pour un objet simple et "Pluriel" pour une liste 
  - **PascalCase** : Pour les noms de classes, interfaces et propriétés
  - **camelCase** : Pour les variables
  - **_camelCase** : Pour les variables déclarées au niveau de la classe
  - Nom d'interface commençant par un **I**
  - Nom de classe abstraite par un **A**
  - Pensez à compiler régulièrement : si ça ne compile pas, c'est un 0

N'hésitez pas à me consulter à chaque étape pour vous assurer que vous avez bien réalisé chaque action.

## TODO

### Etape 1 : Créer sa solution .NET
----

Créez un nouveau projet via Visual Studio 2022, puis sélectionnez une *Application Console*.

Vous appellerez votre projet `LibraryManager.App` et votre solution `LibraryManager`.

Si vous n'avez pas d'explorateur de solution sur votre IDE, vous pouvez aussi l'ajouter via le menu `Affichage > Explorateur de Solution`.

Ajoutez via le *gestionnaire de package Nuget* sur votre projet : Microsoft.Extensions.Hosting

Veillez à bien être en vue Solution et d'avoir votre projet chargé.

⚠️ Testez votre code et pensez à commiter.

### Etape 2 : Commencer son programme
---

Créez une méthode Main dans le `Program.cs` grâce aux recommandations VS `Alt + Entrée` à l'intérieur du fichier.

**Explication : Qu'est-ce que la méthode Main ?**
- C'est le **point d'entrée** de votre application console
- Le programme commence toujours son exécution par cette méthode
- Signature typique : `static void Main(string[] args)` où `args` contient les arguments passés en ligne de commande

Créez une classe `Book` qui contiendra uniquement un `string Name` et un `string Type`. 

**Explication : Les classes et les propriétés**
- Une **classe** est un modèle/blueprint qui définit la structure d'un objet
- Les **propriétés** sont des attributs de la classe qui stockent des données
- En C#, on utilise des propriétés auto-implémentées :

```cs
public class Item
{
    // Propriété auto-implémentée avec getter et setter
    public string Title { get; set; }
    
    // Raccourci : tapez "prop" puis Tab pour générer une propriété
    public string Category { get; set; }
}
```

Dans cette méthode, vous allez créer une liste de livres que vous allez alimenter avec quelques éléments dont au moins un livre de type `Aventure`. Libre à vous de mettre ce que vous souhaitez dans celle-ci.

**Exemple de code :**
```cs
public class Program {
    static void Main(string[] args)
    {
        // List<T> est une collection générique qui peut contenir plusieurs éléments du type T
        List<Item> items = new List<Item>
        {
            new Item { Title = "Item 1", Category = "CategoryA" },
            new Item { Title = "Item 2", Category = "CategoryA" },
            new Item { Title = "Item 3", Category = "CategoryB" }
        };
        
        // Alternative : Initialisation puis ajout
        // List<Item> items = new List<Item>();
        // items.Add(new Item { Title = "...", Category = "..." });
    }
}
```

Vous allez ensuite boucler sur celle-ci et afficher dans la console les différents noms.

⚠️ Testez votre code et pensez à commiter.

### Etape 3 : LINQ
---

**Qu'est-ce que LINQ ?**

LINQ (prononcé "link", pour **L**anguage **IN**tegrated **Q**uery) est un langage d'interrogation pour vos collections en .NET.

**Pourquoi utiliser LINQ ?**
- Simplifie les opérations sur les collections (filtrage, tri, projection...)
- Code plus lisible et concis
- Moins de risque d'erreurs qu'avec des boucles manuelles
- Fonctionne avec de nombreuses sources de données (listes, bases de données, XML...)

**Les deux syntaxes LINQ :**

1. **Syntaxe par méthodes** (recommandée pour ce projet) :
```cs
var categoryAItems = items.Where(item => item.Category == "CategoryA");
```

2. **Syntaxe par requête** (ressemble au SQL) :
```cs
var categoryAItems = from item in items
                     where item.Category == "CategoryA"
                     select item;
```

**Principales méthodes LINQ :**

```cs
// Where : Filtre les éléments selon une condition
var categoryAItems = items.Where(i => i.Category == "CategoryA");

// Select : Transforme/projette les éléments
var itemTitles = items.Select(i => i.Title);

// FirstOrDefault : Récupère le premier élément (ou null si vide)
var firstItem = items.FirstOrDefault();

// OrderBy : Trie par ordre croissant
var sortedItems = items.OrderBy(i => i.Title);

// Count : Compte le nombre d'éléments
int count = items.Count(i => i.Category == "CategoryA");

// Any : Vérifie si au moins un élément correspond
bool hasCategoryA = items.Any(i => i.Category == "CategoryA");

// ToList : Convertit le résultat en List<T>
List<Item> categoryAList = items.Where(i => i.Category == "CategoryA").ToList();
```

**Exercice : Filtrez votre liste pour n'afficher que les livres de type `Aventure`.**

```cs
// Méthode 1 : Filtrer puis afficher
var categoryAItems = items.Where(item => item.Category == "CategoryA");
foreach (var item in categoryAItems)
{
    Console.WriteLine($"Item de CategoryA : {item.Title}");
}

// Méthode 2 : Tout en une ligne (chaînage de méthodes)
items.Where(item => item.Category == "CategoryA")
     .ToList()
     .ForEach(item => Console.WriteLine(item.Title));
```

**Note importante :** 
- `=>` est appelé un **lambda** ou **expression lambda** : c'est une fonction anonyme
- `item => item.Category == "CategoryA"` signifie : "pour chaque item, vérifie si sa catégorie est CategoryA"
- C'est équivalent à écrire :
```cs
bool IsCategoryA(Item item)
{
    return item.Category == "CategoryA";
}
```

Pour plus d'informations : [LINQ - Microsoft](https://learn.microsoft.com/fr-fr/dotnet/csharp/linq/) 

⚠️ Testez votre code et pensez à commiter.

### Etape 4.1 : Préparer son architecture
---

**Qu'est-ce qu'une architecture en couches ?**

Une architecture en couches organise le code en différents projets ayant chacun une responsabilité spécifique. Cela permet :
- **Séparation des préoccupations** : Chaque couche a un rôle bien défini
- **Réutilisabilité** : Les couches peuvent être utilisées dans différentes applications
- **Testabilité** : Plus facile de tester chaque couche indépendamment
- **Maintenabilité** : Modifications localisées, pas d'impact sur tout le code

**Les couches de notre application :**

```
LibraryManager (Solution)
│
├── LibraryManager.App (Application Console)
│   └── Point d'entrée, interface utilisateur
│
├── BusinessObjects (Bibliothèque de classes)
│   ├── Entity/ : Objets métier (Book, Author, Library...)
│   ├── Enum/ : Énumérations (TypeBook...)
│   └── DataTransferObject/ : DTOs (objets pour l'API)
│
├── DataAccessLayer (Bibliothèque de classes)
│   ├── Repository/ : Accès aux données (BookRepository...)
│   └── Contexts/ : Configuration base de données (LibraryContext)
│
└── Services (Bibliothèque de classes)
    └── Services/ : Logique métier (CatalogManager...)
```

 Mettez en place votre architecture de projets en ajoutant via Visual Studio des projets de type **Bibliothèque de classes** :
- `BusinessObjects` : Couche contenant vos objets métier (objets de base de données ou de travail)
- `DataAccessLayer` : Couche permettant l'accès aux données; on y retrouvera notamment les repository

Maintenant passons à l'implémentation de notre architecture.

**1. Créer les entités (BusinessObjects)**

Dans votre projet `BusinessObjects`, créez un dossier `Entity` et `Enum`, puis dans ce dossier `Entity`, créez les objets correspondants aux tables du fichier `LibraryInit.sql`.

**Explication : Les entités vs les tables**
- Une **entité** est une classe C# qui représente une table de base de données
- Chaque **propriété** de la classe correspond à une **colonne** de la table
- Cette correspondance s'appelle **ORM** (Object-Relational Mapping)

Déplacez-y votre classe `Book` que vous compléterez et changez votre `string Type` en enum `TypeBook Type`. Pas besoin de créer un fichier pour la table de `Stock`.

**Exemple d'énumération :**
```cs
public enum EnumName
{
    Value1,
    Value2
}
```

**Exemple d'entité complète :**
```cs
public class Entity
{
    public int Id { get; set; }
    public EnumName Type { get; set; }
    public int Rate { get; set; }
    
    // Relation OneToMany : 1..1 en base de données
    public int ExternalId { get; set; }
    public ExternalEntity ExternalEntity { get; set; }  // Propriété de navigation
    
    // Relation ManyToMany : 1..* en base de données
    public IEnumerable<ExternalEntity> ExternalEntities { get; set; }
}
```

Faites en sorte que toutes les entités héritent d'une interface `IEntity` qui contiendra une propriété `int Id`.

**Explication : Pourquoi une interface IEntity ?**
```cs
// Avantages :
// - Permet de créer des méthodes génériques qui fonctionnent avec toute entité
// - Garantit que toutes les entités ont un Id
// - Facilite la création de Repository génériques (étape suivante)
```

**Schéma des relations :**
```
Author (1) ────── (N) Book (N) ────── (N) Library
      └─ Un auteur a plusieurs livres
                    └─ Un livre peut être dans plusieurs bibliothèques
```

**2. Créer les repositories (DataAccessLayer)**

Dans votre projet `DataAccessLayer`, créez un dossier `Repository`, puis dans ce dossier une classe repository pour chaque entité (ex : `BookRepository`, etc...)

**Explication : Le pattern Repository**
- Un **Repository** est une classe qui gère l'accès aux données
- Il fait le lien entre votre application et la base de données
- Permet d'isoler la logique d'accès aux données
- Facilite les tests (on peut créer un faux repository pour tester)

Vous y créerez les méthodes :
- `IEnumerable<object> GetAll()`
- `object Get(int id)`

`object` étant à remplacer pour chaque entité. 

**Exemple de Repository :**
```cs
public class ItemRepository
{
    private List<Item> _books;

    public BookRepository()
    {
        // Pour l'instant, données en dur
        _books = new List<Item>
        {
            new Item { Id = 1, Title = "" },
            new Item { Id = 2, Title = "" }
        };
    }
}
```

Répétez le même schéma pour chacune de vos entités.

Pour le `BookRepository`, utilisez la liste que vous avez créé dans le `Main` puis implémentez les méthodes `IEnumerable<Book> GetAll()` et `Book Get(int id)`, appelez ces méthodes dans votre `Main` et finalement tentez d'afficher les livres d'aventure.

**PI : Vous aurez besoin d'ajouter des références d'un projet à un autre pour permettre d'utiliser vos entités à l'extérieur de leur projet respectif.**

**Comment ajouter une référence entre projets :**
1. Clic droit sur le projet qui a besoin de la référence (ex: DataAccessLayer)
2. `Ajouter > Référence de projet`
3. Cocher le projet nécessaire (ex: BusinessObjects)
4. Cela permet d'utiliser les classes du projet référencé

**Dépendances entre projets :**
```
App → Services → DataAccessLayer → BusinessObjects
      └─────────→ BusinessObjects
```

⚠️ Testez votre code et pensez à commiter.


### Etape 4.2 : Préparer son architecture
---

 Mettez en place votre architecture de projets en ajoutant via Visual Studio des projets de type **Bibliothèque de classes** :
- `Services` : Couche services intermédiaire; va permettre d'orchestrer les besoins et de relier d'autres couches entre elles

**Explication : La couche Service**
- Elle orchestre les appels aux repositories
- Elle peut combiner les données de plusieurs repositories

**Exemple de différence Repository vs Service :**
```cs
// Repository : Méthodes techniques d'accès aux données
ItemRepository.GetAll() → Retourne tous les livres de la BD
ItemRepository.Get(id)  → Retourne un livre par son ID

// Service : Méthodes orientées métier
ItemManager.GetCatalog()           → Retourne le catalogue complet
ItemManager.GetCatalog(Type type)  → Filtre par type (logique métier)
ItemManager.FindBook(int id)       → Recherche intelligente
```

4. Dans votre projet `Services`, créez un dossier `Services`, puis dans ce dossier une classe `CatalogManager` qui contiendra les méthodes :
- `IEnumerable<Book> GetCatalog()`
- `IEnumerable<Book> GetCatalog(Type type)` 
- `Book FindBook(int id)` 

Elles utiliseront le `BookRepository` pour accéder et retourner des données.

**Exemple d'implémentation du CatalogManager :**
```cs
// Fichier: Services/CatalogManager.cs
public class ItemManager
{
    private readonly ItemRepository _itemRepository;

    // Le repository est passé dans le constructeur
    public ItemManager()
    {
        _repository = new ItemRepository();
    }
}
```

Ces méthodes vont utiliser les Repositories que vous avez créés. Remplacez les méthodes de votre `Main` par les méthodes nouvellement créées dans votre `CatalogManager`.

**Exemple d'utilisation dans le Main :**
```cs
static void Main(string[] args)
{
    // Création manuelle des dépendances (sera remplacé par l'injection plus tard)
    ItemManager itemManager = new ItemManager();
    
    // Utilisation du service
    var allItems = itemManager.GetItems();
    var filteredItems = itemManager.GetItems(EnumName.Value1);
    var itemFounded = itemManager.FindItem(1);
    
    // Affichage
    Console.WriteLine("All Items :");
    foreach (var item in allItems)
    {
        Console.WriteLine($"- {item.Title}");
    }
    
    Console.WriteLine("All Items :");
    foreach (var item in filteredItems)
    {
        Console.WriteLine($"- {item.Title}");
    }
    
    Console.WriteLine($"All Items : {itemFounded.Title}");
}
```

⚠️ Testez votre code et pensez à commiter.


### Etape 5 : Injection de dépendance
---

**Qu'est-ce que l'injection de dépendance ?**

L'injection de dépendance (DI - Dependency Injection) est un concept fondamental du développement moderne.

**Le problème sans injection de dépendance :**
```cs
public class ItemManager
{
    private ItemRepository _repository;
    
    public ItemManager()
    {
        _repository = new ItemRepository();  // ❌ Couplage fort !
    }
}

// Problèmes :
// - Difficile de tester (impossible de remplacer le repository par un mock)
// - Difficile de changer l'implémentation
// - La classe gère elle-même ses dépendances
```

**La solution avec injection de dépendance :**
```cs
public class ItemManager
{
    private readonly ItemRepository _repository;
    
    public ItemManager(ItemRepository repository)  // ✅ Injection via constructeur
    {
        _repository = repository;
    }
}

// Avantages :
// - Testable (on peut injecter un mock)
// - Flexible (on peut changer l'implémentation facilement)
// - La classe ne gère plus ses dépendances
```

Après avoir fait évoluer votre `CatalogManager` en suivant l'exemple ci-dessus, ajoutez dans le `Program.cs` la méthode suivante :

```cs
    private static IHost CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
              services.AddSingleton<ItemRepository>();
              services.AddScoped<ItemManager>();
            })
            .Build();
    }
```

Ajouter la méthode à votre méthode Main() :

```cs
static void Main(string[] args) 
{
    var host = CreateHostBuilder();
    using var serviceScope = host.Services.CreateScope();
    var services = serviceScope.ServiceProvider; 
    // Récupération du service configuré
    ItemManager itemManager = services.GetRequiredService<ItemManager>(); 
    // ...
}
```

En réalisant toutes ces manipulations, nous avons délégué l'instanciation de nos différentes classes au Builder intégré à votre application .NET et centralisé le tout au sein de la configuration des services.

On peut aller encore plus loin.

1. **Extrayez une interface de vos classes concrètes** ayant de la logique et instanciées ailleurs dans votre code (Ex : Services...)

```cs
// Exemple pour CatalogManager :
// Clic droit sur le nom de la classe > Actions rapides > Extraire l'interface

public interface IItemManager
{
    IEnumerable<Item> GetItems();
    // etc...
}

public class ItemManager : IItemManager
{
    // Implémentation...
}
```

2. **Pour vos repository**, on fera un peu différemment. Vous allez créer une seule interface `IGenericRepository<T>` qui prendra en paramètre un type générique `T` qui sera une `IEntity` et qui vous servira pour vos types de retours

**Explication : Les Génériques (Generics)**
```cs
// Interface générique : fonctionne avec n'importe quel type d'entité
public interface IGenericRepository<T> where T : IEntity
{
    IEnumerable<T> GetAll();
    T Get(int id);
}

// Utilisation :
// IGenericRepository<Book>   → GetAll() retourne IEnumerable<Book>
// IGenericRepository<Author> → GetAll() retourne IEnumerable<Author>

// Implémentation pour Book :
public class ItemRepository : IGenericRepository<Item>
{
    public IEnumerable<Book> GetAll() { /* ... */ }
    public Book Get(int id) { /* ... */ }
}
```

3. **Injectez vos dépendances** dans la configuration de vos services de votre `Program.cs`

```cs
private static IHost CreateHostBuilder()
{
    return Host.CreateDefaultBuilder()
        .ConfigureServices(services =>
        {
            // Enregistrement des repositories
            services.AddTransient<IGenericRepository<Item>, ItemRepository>();
            
            // Enregistrement des services
            services.AddTransient<IItemManager, ItemManager>();
        })
        .Build();
}
```

4. **Utilisez ces classes injectées** en retirant les appels inutiles à vos classes concrètes 

Récupérez votre Service dans le `Main` et testez en reprenant l'exemple du `IApiCaller` :

```cs
static void Main(string[] args)
{
    // 1. Créer le host avec la configuration des services
    var host = CreateHostBuilder();
    
    // 2. Récupérer le service depuis le conteneur DI
    ICatalogManager catalogManager = host.Services.GetRequiredService<ICatalogManager>();
    
    // 3. Utiliser le service (les dépendances sont automatiquement injectées !)
    var adventureBooks = catalogManager.GetCatalog(TypeBook.Aventure);
    
    foreach (var book in adventureBooks)
    {
        Console.WriteLine(book.Name);
    }
}
```

**Pourquoi avoir remplacé les classes concrètes par des interfaces ?**

Nous avons cassé le lien entre l'implémentation (l'action) et la définition (la possibilité).

Concrètement, l'interface permet à la classe appelante de connaître l'existence d'une méthode et son retour, mais pas la manière dont elle est implémentée.

**Les avantages principaux :**

1. **Testabilité** : On peut facilement créer des "faux" services (mocks) pour tester notre code
   ```cs
   // En test, on peut remplacer le vrai repository par un faux
   services.AddTransient<IGenericRepository<Item>, FakeItemRepository>();
   ```

2. **Flexibilité** : On peut changer l'implémentation sans modifier le code qui l'utilise
   ```cs
   // Passage d'une implémentation en mémoire...
   services.AddTransient<IGenericRepository<Item>, InMemoryItemRepository>();
   
   // ...à une implémentation avec base de données (aucun changement dans ItemManager !)
   services.AddTransient<IGenericRepository<Item>, DatabaseItemRepository>();
   ```

3. **Découplage** : Les classes ne dépendent plus d'implémentations concrètes mais d'abstractions
   - `ItemManager` ne sait pas s'il utilise un repository en mémoire ou en base de données
   - Il connaît juste les méthodes disponibles via `IGenericRepository<Item>`
   - C'est le **Principe d'Inversion de Dépendance** (SOLID)

4. **Maintenabilité** : Facilite l'ajout de nouvelles implémentations sans casser le code existant
   ```cs
   // On peut ajouter une nouvelle implémentation sans toucher à ItemManager
   public class CachedItemRepository : IGenericRepository<Item>
   {
       // Nouvelle implémentation avec cache
   }
   ```

**Exemple concret de la différence :**

```cs
// ❌ AVANT : Dépendance forte (couplage fort)
public class ItemManager
{
    private ItemRepository _repository;
    
    public ItemManager()
    {
        _repository = new ItemRepository(); // Création directe = couplage fort
    }
    // Impossible de changer ItemRepository sans modifier ItemManager
}

// ✅ APRÈS : Dépendance faible (couplage faible)
public class ItemManager
{
    private IGenericRepository<Item> _repository;
    
    public ItemManager(IGenericRepository<Item> repository)
    {
        _repository = repository; // Reçu de l'extérieur = couplage faible
    }
    // On peut injecter n'importe quelle implémentation de IGenericRepository<Item>
}
```

**Résumé du flux d'injection :**
```
1. Configuration (Program.cs) :
   services.AddTransient<IItemManager, ItemManager>()
   services.AddTransient<IGenericRepository<Item>, ItemRepository>()

2. Le conteneur DI sait maintenant que :
   - IItemManager → ItemManager
   - IGenericRepository<Item> → ItemRepository

3. Quand on demande IItemManager :
   - Le conteneur crée automatiquement ItemRepository
   - Puis crée ItemManager en lui injectant ItemRepository
   - Retourne l'instance de ItemManager

4. Tout est automatique, pas de "new" manuel !
```

Pour plus d'informations : 
- [Injection de dépendance - Microsoft](https://learn.microsoft.com/fr-fr/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-8.0)
- [Classes et méthodes générique - Microsoft](https://learn.microsoft.com/fr-fr/dotnet/csharp/fundamentals/types/generics)

⚠️ Testez votre code et pensez à commiter.

### Etape 6 : EntityFramework
---

**Qu'est-ce qu'un ORM ?**

ORM signifie **Object-Relational Mapping** (Mappage Objet-Relationnel). C'est un outil qui fait le pont entre votre code orienté objet (C#) et votre base de données relationnelle (SQL).

**Avantages d'EntityFramework :**
- Pas besoin d'écrire du SQL manuellement
- Type-safe : erreurs détectées à la compilation
- Gère automatiquement les relations entre entités
- Simplifie les opérations CRUD (Create, Read, Update, Delete)

**Exemple de transformation :**
```cs
// Sans ORM : SQL manuel ❌
string sql = "SELECT * FROM book WHERE type = 'Aventure'";
// Risque d'erreur SQL, pas de vérification de type...

// Avec ORM : Code C# ✅
var books = context.Books.Where(b => b.Type == TypeBook.Aventure);
// Type-safe, IntelliSense, facile à maintenir
```

Avec l'aide de la base de données SQLite fournie en annexe, vous allez implémenter l'ORM **EntityFramework**.

**Packages NuGet nécessaires :**
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Sqlite` (pour SQLite)
- `Microsoft.EntityFrameworkCore.Design` (pour les outils EF)

Vous aurez besoin d'un package SQLite pour poursuivre, je vous laisse chercher.

Le but ici est de renvoyer les informations stockées en base de données au lieu de renvoyer des listes vides au niveau de vos `Repositories`.

Dans votre `DataAccessLayer`, créez un dossier `Contexts` et un fichier `LibraryContext` qui implémentera la classe `DbContext`, servez-vous de la documentation pour remplir votre DbContext.

**Exemple de DbContext :**
```cs
public class ItemContext : DbContext
{
    public ItemContext(DbContextOptions<ItemContext> options) 
        : base(options)
    {
    }

    // DbSet : Représente une table de la base de données
    public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuration des relations Many-to-Many
        modelBuilder.Entity<Item>()
            .HasMany(b => b.Items)
            .WithMany(l => l.ExternalItems)
            .UsingEntity(j => j.ToTable("external_items"));
            
        // Configuration des relations One-to-Many
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Item)
            .WithMany(a => a.ExternalItems)
            .HasForeignKey(b => b.ExternalId);
    }
}
```

Pensez à l'injecter, pour une fois on utilisera une classe concrète. Vous aurez sûrement un autre Nuget à récupérer sur vos deux projets.

```cs
// Dans la configuration du service
services.AddDbContext<ItemContext>(options =>
{
    // Chemin vers la base de données
    string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "library.db");
    options.UseSqlite($"Data Source={dbPath}");
});
```

**Copier le fichier .db à la compilation :**
```xml
<!-- Dans votre fichier .csproj -->
<ItemGroup>
    <None Update="library.db">
        <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
</ItemGroup>
```

Vous pouvez construire un chemin absolu avec l'aide de la classe `Path`. **Faites en sorte que le fichier `.db` soit copié à la compilation.**

Dans vos repositories, utilisez le `LibraryContext` injecté pour récupérer le contenu de la base.

**Exemple de Repository avec EntityFramework :**
```cs
public class ItemRepository : IGenericRepository<Item>
{
    private readonly ItemContext _context;

    public ItemRepository(ItemContext context)
    {
        _context = context;
    }
}
```

Ajoutez une méthode `IEntity Add(IEntity)` dans `IGenericRepository`. Compilez et déboguez.

Vous réalisez à quel point c'est fastidieux de tout changer à la fois.

Ajouter dans votre `IGenericRepository` la méthode `IEnumerable<T> GetMultiple(Func<T, bool>? filter = null, params string[] includes)`.

Maintenant créez une classe concrète `GenericRepository<T>` qui doit remplacer tous vos repositories existants et remplacez les injections.

**Exemple de GenericRepository :**
```cs
public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
{
    private readonly LibraryContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(LibraryContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();  // Set<T> : Accès générique à la table
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T Get(int id)
    {
        return _dbSet.FirstOrDefault(e => e.Id == id);
    }
    
    public T Add(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
        return entity;
    }


    public IEnumerable<T> GetMultiple(Func<T, bool>? filter = null, params string[] includes)
    {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            if (filter != null)
            {
                return query.AsEnumerable().Where(filter);
            }

            return query.ToList();
     }
```

**Configuration dans Program.cs :**
```cs
// Plus besoin de créer un repository par entité !
services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
```

Supprimez vos anciens repositories.

**Concepts EntityFramework importants :**
- **DbSet<T>** : Représente une table
- **Include()** : Charge les relations (évite N+1 queries)
- **SaveChanges()** : Persiste les modifications en base
- **Tracking** : EF suit automatiquement les modifications des entités

Pour plus d'informations : 
- [EntityFramework - Microsoft](https://learn.microsoft.com/fr-fr/ef/core/)
- [DBeaver](https://dbeaver.io/) qui est vraiment utile quand vous avez plusieurs SGBD différents à gérer.

⚠️ Testez votre code et pensez à commiter.

### Etape 7 : TU (Tests Unitaires)
---

**Qu'est-ce qu'un test unitaire ?**

Un **test unitaire** vérifie qu'une petite unité de code (généralement une méthode) fonctionne correctement de manière isolée.

**Principes des tests unitaires :**
- **Isolation** : Teste une méthode indépendamment des autres
- **Rapide** : S'exécute en quelques millisecondes
- **Répétable** : Donne toujours le même résultat
- **Automatisé** : Peut être exécuté automatiquement

**Pattern AAA (Arrange-Act-Assert) :**
```cs
[Fact]
public void GetCatalog_WithType_ReturnsFilteredItems()
{
    // Arrange : Prépare les données et mocks
    var mockBooks = new List<Item> { /* ... */ };
    
    // Act : Exécute la méthode à tester
    var result = itemManager.GetItems(TypeBook.Aventure);
    
    // Assert : Vérifie le résultat
    Assert.Equal(2, result.Count());
}
```

Créez un dossier `Tests` à la racine de votre solution et un nouveau projet `Services.Test` dans ce dossier.
Créez une classe `CatalogManagerTest`.

**Packages NuGet nécessaires pour les tests :**
- `xUnit` ou `NUnit` : Framework de tests
- `Moq` : Bibliothèque de mocking
- `Microsoft.NET.Test.Sdk` : SDK pour exécuter les tests

Implémentez un test unitaire pour chaque méthode de votre `CatalogManager`. 
Créez un Mock de chaque `Repository` pour bien tester unitairement votre méthode.

**Qu'est-ce qu'un Mock ?**

Un **mock** est un faux objet qui simule le comportement d'une dépendance. Cela permet de tester une classe sans dépendre de ses dépendances réelles.

**Méthodes Assert courantes :**
```cs
Assert.Equal(expected, actual);        // Vérifie l'égalité
Assert.NotEqual(expected, actual);     // Vérifie la différence
Assert.True(condition);                // Vérifie qu'une condition est vraie
Assert.False(condition);               // Vérifie qu'une condition est fausse
Assert.Null(object);                   // Vérifie qu'un objet est null
Assert.NotNull(object);                // Vérifie qu'un objet n'est pas null
Assert.Contains(item, collection);     // Vérifie qu'un élément est dans une collection
Assert.Throws<Exception>(() => {});    // Vérifie qu'une exception est levée
```

**Exécuter les tests :**
```bash
# Dans Visual Studio : Test > Exécuter tous les tests
# En ligne de commande :
dotnet test
```

Pour plus d'informations : [TU avec C# - Microsoft](https://learn.microsoft.com/fr-fr/dotnet/core/testing/unit-testing-with-dotnet-test)

⚠️ Testez votre code et pensez à commiter.

### Etape 8 : API
---

**Qu'est-ce qu'une API REST ?**

Une **API** (Application Programming Interface) est une interface logicielle qui permet à des applications de communiquer entre elles.

**REST** (Representational State Transfer) est un style d'architecture pour les APIs web basé sur HTTP.

**Principes REST :**
- Utilise les méthodes HTTP : GET (lire), POST (créer), PUT (modifier), DELETE (supprimer)
- Les ressources sont identifiées par des URLs : `/books`, `/books/1`
- Communication sans état (stateless) : chaque requête est indépendante
- Réponses au format JSON ou XML

**Exemple de requêtes REST :**
```
GET    /api/books          → Récupère tous les livres
GET    /api/books/1        → Récupère le livre avec ID 1
POST   /api/books          → Crée un nouveau livre
PUT    /api/books/1        → Modifie le livre avec ID 1
DELETE /api/books/1        → Supprime le livre avec ID 1
```

On va maintenant mettre en place une API. Pour rappel, une API est une interface logicielle sur laquelle vous pourrez vous connecter et récupérer des informations via requête HTTP. Elle vous renverra un résultat sous la forme d'un JSON.

Pour cela, vous allez créer un nouveau projet de type `ASP.NET Core WebAPI` sans authentification, que vous allez appeler `LibraryManager.Hosting`.

Une fois créé, vous allez mettre ce projet en tant que projet de démarrage. 

Votre nouveau `Program.cs` va ressembler à ça :

```cs
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
Les Middleware ajoutés avant le builder seront récupérés par l'application
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

Transformez votre fichier grâce aux recommandations VS `Alt + Entrée`.

Ajoutez à votre builder les services de votre précédent `Program.cs`.

**Qu'est-ce que Swagger ?**
- **Swagger** génère automatiquement une documentation interactive de votre API
- Permet de tester les endpoints directement depuis le navigateur
- Accessible à : `https://localhost:5001/swagger`

Observez la classe créée :

```cs
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
```

**Explication des attributs :**

Les éléments entre crochets sont appelés des **attributs**. Ils définissent des métadonnées qui sont lisibles durant l'exécution.

```cs
[ApiController]              // Marque cette classe comme contrôleur API
[Route("[controller]")]      // Définit la route : /WeatherForecast
[HttpGet]                    // Méthode HTTP GET
[HttpPost]                   // Méthode HTTP POST
[HttpPut]                    // Méthode HTTP PUT
[HttpDelete]                 // Méthode HTTP DELETE
```

Donc pour accéder à cette API, nous utiliserons `GET localhost:5000/WeatherForecast`. Il existe même des attributs pour l'authentification.

Après l'explication, place à la pratique.

Créez un fichier `BookController` qui va comprendre les méthodes suivantes :

**Endpoints à implémenter :**
- `GET /books` : Récupère tous les livres
- `GET /books/{id}` : Récupère un livre par son ID
- `GET /books/type/{type}` : Récupère les livres d'un type donné
- `POST /books` : Ajoute un nouveau livre
- `GET /books/top-rated` : Récupère le livre le mieux noté
- `DELETE /books/{id}` : Supprime un livre

Modifier l'appel à `AddController()` dans `Program.cs` de votre WebAPI :
```cs
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
```

Pour pouvoir tester ces routes plus simplememnt, vous pouvez ajouter ces lignes dans votre `Program.cs`
```cs
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
         Title = "La Cocina",
         Version = "v1"
    }
});
app.UseSwagger();
app.UseSwaggerUI();
```


**Codes de statut HTTP courants :**
```cs
return Ok(data);              // 200 : Succès
return Created();             // 201 : Créé avec succès
return NoContent();           // 204 : Succès sans contenu
return BadRequest();          // 400 : Requête invalide
return NotFound();            // 404 : Ressource non trouvée
return Unauthorized();        // 401 : Non authentifié
return Forbid();              // 403 : Accès interdit
return StatusCode(500);       // 500 : Erreur serveur
```

**Paramètres dans les contrôleurs :**
```cs
[HttpGet("{id}")]                    // Paramètre dans l'URL
public IActionResult Get(int id);     // Automatiquement lié

[HttpPost]
public IActionResult Create([FromBody] <Item> item);  // Depuis le corps JSON

[HttpGet]
public IActionResult Search([FromQuery] string name);  // Depuis query string ?name=...
```

Implémentez les méthodes manquantes.

Faites en sorte d'afficher l'`Author` correspondant à votre `Book`.

**Astuce : Include() pour charger les relations**
```cs
// Dans votre repository ou service
var books = _context.Books
    .Include(b => b.Author)  // Charge l'auteur en même temps
    .ToList();
```

Pour tester votre API, installez [Postman](https://www.postman.com/).

**Comment tester avec Postman :**
1. Ouvrir Postman
2. Créer une nouvelle requête
3. Sélectionner la méthode HTTP (GET, POST, etc.)
4. Entrer l'URL : `https://localhost:5001/api/books`
5. Pour POST : ajouter le JSON dans l'onglet "Body" > "raw" > "JSON"
6. Cliquer sur "Send"

**Alternative : Tester avec Swagger**
- Lancer l'application
- Naviguer vers `https://localhost:5001/swagger`
- Tester directement les endpoints depuis l'interface

Pour plus d'informations : [Tutoriel ASP.NET Core Web API- Microsoft](https://learn.microsoft.com/fr-fr/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio)

⚠️ Testez votre code et pensez à commiter.

### Etape 9 : Limiter l'accès aux données (DTO)
---

**Qu'est-ce qu'un DTO ?**

DTO signifie **Data Transfer Object** (Objet de Transfert de Données).

**Pourquoi utiliser des DTOs ?**
- **Sécurité** : Cache des données sensibles (mots de passe, informations internes...)
- **Contrôle** : Expose uniquement les données nécessaires au client
- **Performance** : Réduit la taille des réponses
- **Découplage** : L'API n'est pas liée à la structure interne de la base de données
- **Versionning** : Facilite l'évolution de l'API sans casser les clients existants

**Problème sans DTO :**
```cs
// Entité Book complète
public class Book : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Pages { get; set; }
    public TypeBook Type { get; set; }
    public int Rate { get; set; }          // ⚠️ Information interne
    public int IdAuthor { get; set; }      // ⚠️ Clé étrangère exposée
    public Author Author { get; set; }
}

// Problème : Tout est exposé dans l'API, même les données internes !
```

Dans votre projet `BusinessObjects`, créez un dossier `DataTransfertObject`.
Dans celui-ci, vous pouvez créer un `BookDto`. 

Un DTO est une version de l'objet destinée à l'extérieur de votre application. Cela peut être utile pour limiter les données accessibles aux clients de votre API.

Dans votre `BookDto`, reprenez les éléments de votre `Book` qui posent problèmes.

Faites en sorte que les `Book`s fournis par votre `CatalogManager` soient convertis en `BookDto` au niveau de vos `Controller`s.


Chacun de vos `Controller`s doivent utiliser et renvoyer des `BookDto`.

**Exemple de réponse JSON avec DTO :**
```json
{
  "id": 1,
  "name": "Le Comte de Monte Cristo",
  "pages": 900,
  "type": "Aventure",
  "author": {
    "firstName": "Alexandre",
    "lastName": "Dumas",
    "fullName": "Alexandre Dumas"
  }
}
```

⚠️ Testez votre code et pensez à commiter.

### Etape 10 : Recherche flexible 
---

En vous servant de vos connaissances acquises, ajouter un endpoint `/filteredBooks` qui retournera un Dto `BookAuthorDto` qui contiendra les mêmes infos que `BookDto` en y ajoutant les données d'`Author` et de `Library` sauf les `Id`s.

Ce endpoint doit être capable de filtrer sur le `Type` ou le `Nom` / `Prénom` de l'auteur.

Il faudra respecter notre architecture actuelle en passant par le `CatalogManager`.

⚠️ Testez votre code et pensez à commiter.

## Glossaire des termes techniques
---

### Architecture et Design Patterns

**Architecture en couches (Layered Architecture)**
- Organisation du code en différentes couches avec des responsabilités distinctes
- Exemple : Présentation → Services → DataAccess → BusinessObjects

**Pattern Repository**
- Abstraction de l'accès aux données
- Centralise la logique d'accès à la base de données

**Design Pattern**
- Solution réutilisable à un problème récurrent
- Exemples : Singleton, Factory, Repository, Observer

### Concepts .NET et C#

**Solution (.sln)**
- Conteneur de projets Visual Studio
- Permet de gérer plusieurs projets ensemble

**Projet (.csproj)**
- Unité de compilation
- Génère une DLL ou un EXE

**DLL (Dynamic Link Library)**
- Bibliothèque de code compilé réutilisable
- Ne peut pas s'exécuter seule

**NuGet**
- Gestionnaire de packages pour .NET
- Équivalent de npm (JavaScript) ou pip (Python)

**Namespace**
- Espace de noms pour organiser le code
- Évite les conflits de noms : `System.Collections.Generic`

**Assembly**
- Unité de déploiement .NET (DLL ou EXE)
- Contient le code compilé (IL - Intermediate Language)

### Programmation Orientée Objet (POO)

**Classe**
- Modèle/Blueprint pour créer des objets
- Définit la structure (propriétés) et le comportement (méthodes)

**Interface**
- Contrat qui définit des méthodes sans implémentation
- Commence par `I` : `IRepository`, `IService`

**Héritage**
- Mécanisme permettant à une classe d'hériter d'une autre
- `class Book : IEntity`

**Propriété (Property)**
- Membre de classe avec getter/setter
- `public string Name { get; set; }`

**Méthode**
- Fonction membre d'une classe
- Définit un comportement

**Constructeur**
- Méthode spéciale appelée lors de la création d'un objet
- Même nom que la classe : `public Book() { }`

### Concepts avancés C#

**Générique (Generic)**
- Type paramétré qui fonctionne avec n'importe quel type
- `List<T>`, `IGenericRepository<T>`

**Lambda (Expression Lambda)**
- Fonction anonyme concise
- `book => book.Type == "Aventure"`

**LINQ (Language Integrated Query)**
- Langage de requête intégré pour manipuler des collections
- `books.Where(b => b.Type == "Aventure")`

**Extension Methods**
- Méthodes ajoutées à un type existant sans modification
- Utilisées par LINQ : `.Where()`, `.Select()`

**Nullable**
- Type pouvant être null
- `int?` peut contenir un nombre ou null

**var**
- Mot-clé pour inférence de type
- Le compilateur déduit le type : `var books = new List<Book>();`

### Base de données et ORM

**ORM (Object-Relational Mapping)**
- Outil pour mapper objets C# ↔ tables SQL
- EntityFramework est un ORM

**EntityFramework (EF Core)**
- ORM officiel de Microsoft pour .NET
- Permet d'accéder aux bases de données via du code C#

**DbContext**
- Classe centrale d'EntityFramework
- Représente une session avec la base de données

**DbSet<T>**
- Représente une table dans le DbContext
- `DbSet<Book>` → table `books`

**Migration**
- Fichier décrivant les modifications de schéma de base de données
- Permet de versionner la structure de la BD

**Eager Loading**
- Chargement anticipé des relations
- `.Include(b => b.Author)` charge l'auteur immédiatement

**Lazy Loading**
- Chargement différé des relations
- Les données sont chargées uniquement quand on y accède

**CRUD**
- Create, Read, Update, Delete
- Opérations de base sur les données

### Injection de dépendances

**Injection de dépendance (DI)**
- Pattern où les dépendances sont fournies de l'extérieur
- Améliore la testabilité et la maintenabilité

**Conteneur DI (IoC Container)**
- Gestionnaire qui crée et injecte les dépendances
- `IServiceProvider` en .NET

**Scope / Cycle de vie**
- Durée de vie d'un objet injecté :
  - **Transient** : Nouvelle instance à chaque injection
  - **Scoped** : Une instance par requête/scope
  - **Singleton** : Une seule instance pour toute l'application

**Couplage**
- Degré de dépendance entre classes
- **Couplage faible** (loose coupling) : Préférable, facilite les modifications

### Tests

**Test Unitaire (Unit Test)**
- Teste une unité de code isolée (une méthode)
- Rapide, automatisé, répétable

**Mock**
- Faux objet qui simule une dépendance
- Utilisé dans les tests pour isoler le code testé

**AAA Pattern**
- **Arrange** : Préparer les données
- **Act** : Exécuter le code
- **Assert** : Vérifier le résultat

**Framework de test**
- Outil pour écrire et exécuter des tests
- xUnit, NUnit, MSTest

### API et Web

**API (Application Programming Interface)**
- Interface permettant à des applications de communiquer
- Définit les méthodes/endpoints disponibles

**REST (REpresentational State Transfer)**
- Style d'architecture pour les APIs web
- Utilise HTTP : GET, POST, PUT, DELETE

**Endpoint**
- Point d'accès d'une API
- Exemple : `GET /api/books`

**JSON (JavaScript Object Notation)**
- Format de données textuelles
- Utilisé pour échanger des données entre client/serveur

**HTTP Status Codes**
- Codes indiquant le résultat d'une requête :
  - 2xx : Succès (200 OK, 201 Created)
  - 4xx : Erreur client (400 Bad Request, 404 Not Found)
  - 5xx : Erreur serveur (500 Internal Server Error)

**Controller**
- Classe gérant les requêtes HTTP dans une API
- Contient les endpoints

**Middleware**
- Composant dans le pipeline de traitement des requêtes
- Exemple : Authentication, Logging, CORS

**Swagger / OpenAPI**
- Outil de documentation d'API
- Génère une interface interactive pour tester l'API

### Transfert de données

**DTO (Data Transfer Object)**
- Objet pour transférer des données entre couches
- Limite les données exposées par l'API

**Mapping**
- Conversion d'un objet vers un autre
- Exemple : `Book` → `BookDto`

**AutoMapper**
- Bibliothèque pour automatiser le mapping
- Évite le code répétitif de conversion

### Divers

**Attribut (Attribute)**
- Métadonnées ajoutées au code
- `[HttpGet]`, `[Required]`, `[ApiController]`

**Async/Await**
- Programmation asynchrone
- Permet d'exécuter du code sans bloquer

**Exception**
- Erreur durant l'exécution
- `try-catch-finally` pour gérer les erreurs

**Debugging**
- Processus de recherche et correction de bugs
- Utilise des breakpoints, step-by-step

**Compilation**
- Transformation du code source en code exécutable
- Détecte les erreurs de syntaxe et de type

**IntelliSense**
- Autocomplétion de Visual Studio
- Suggestions de code en temps réel

## Problèmes courants et solutions
---

### Erreurs de compilation

**"The type or namespace name could not be found"**
```
Solution :
1. Vérifier que le using est présent en haut du fichier
2. Vérifier que la référence au projet est ajoutée
3. Rebuild la solution (Ctrl + Shift + B)
```

**"Cannot implicitly convert type 'X' to 'Y'"**
```
Solution :
- Vérifier les types des variables
- Faire un cast explicite si nécessaire : (TypeY)variable
- Utiliser des méthodes de conversion : .ToString(), .ToList()
```

**"Object reference not set to an instance of an object" (NullReferenceException)**
```
Solution :
- Vérifier qu'un objet est bien instancié avant de l'utiliser
- Utiliser l'opérateur null-conditional : book?.Name
- Vérifier les retours de méthodes qui peuvent être null
```

### Erreurs EntityFramework

**"Unable to create an object of type 'LibraryContext'"**
```
Solution :
- Vérifier que DbContext est correctement configuré dans Program.cs
- Vérifier que le constructeur DbContext accepte DbContextOptions
- S'assurer que la chaîne de connexion est correcte
```

**"A referential integrity constraint violation occurred"**
```
Solution :
- Respecter les contraintes de clés étrangères
- Vérifier que l'entité liée existe avant de créer la relation
- Vérifier les propriétés de navigation
```

**"Sequence contains no elements" (InvalidOperationException)**
```
Solution :
- Utiliser FirstOrDefault() au lieu de First()
- Vérifier que la collection n'est pas vide avant d'y accéder
- Gérer le cas où aucun élément ne correspond
```

### Erreurs d'injection de dépendances

**"Unable to resolve service for type 'IService'"**
```
Solution :
- Vérifier que le service est enregistré dans Program.cs
- Vérifier l'ordre d'enregistrement des services
- S'assurer que l'interface et la classe sont correctes
```

**"Cannot consume scoped service from singleton"**
```
Solution :
- Un singleton ne peut pas dépendre d'un scoped
- Changer le cycle de vie du service
- Utiliser IServiceProvider pour créer un scope
```

### Erreurs API

**"405 Method Not Allowed"**
```
Solution :
- Vérifier que le bon verbe HTTP est utilisé (GET, POST, etc.)
- Vérifier les attributs [HttpGet], [HttpPost] sur les méthodes
```

**"404 Not Found"**
```
Solution :
- Vérifier la route : /api/controller/action
- Vérifier l'attribut [Route] sur le contrôleur
- S'assurer que l'application est lancée
```

**"415 Unsupported Media Type"**
```
Solution :
- Ajouter Content-Type: application/json dans les headers
- Utiliser [FromBody] pour les paramètres POST
```

**"Circular reference detected" (JSON)"**
```
Solution :
- Utiliser des DTOs au lieu d'entités directement
- Configurer JSON pour ignorer les références circulaires :
  builder.Services.AddControllers()
    .AddJsonOptions(options => {
      options.JsonSerializerOptions.ReferenceHandler = 
        ReferenceHandler.IgnoreCycles;
    });
```

### Erreurs de tests

**"Mock object is not set up correctly"**
```
Solution :
- Vérifier que Setup() est appelé avant Act
- S'assurer que les paramètres du Setup correspondent à ceux de l'appel
- Utiliser It.IsAny<T>() pour des paramètres génériques
```

**"Test passes locally but fails in CI"**
```
Solution :
- Vérifier les dépendances de chemin (utiliser des chemins relatifs)
- S'assurer que les tests sont isolés (pas d'état partagé)
- Vérifier les configurations d'environnement
```

### Conseils de débogage

**Utiliser les breakpoints**
```
- Cliquer dans la marge gauche pour placer un breakpoint
- F5 pour lancer en mode debug
- F10 pour exécuter ligne par ligne
- F11 pour entrer dans une méthode
```

**Inspecter les variables**
```
- Survoler une variable pour voir sa valeur
- Utiliser la fenêtre "Variables locales" (Debug > Fenêtres > Variables locales)
- Ajouter une "Espion" pour suivre une expression
```

**Console.WriteLine() pour tracer**
```cs
Console.WriteLine($"Valeur de book: {book}");
Console.WriteLine($"Nombre d'éléments: {books.Count()}");
```

## Raccourcis utiles 
---

- Recommandation VS : `Alt + Entrée` => Hyper utile, n'hésitez pas à en abuser
- Renommer un élément et ses références : `(Hold) CTRL + R + R`
- Créer une propriété : `(Write) prop + Tab`
- Créer un constructeur: `(Write) ctor + Tab`
- Redirection sur la classe concernée : `F12`
- Redirection sur la classe concrète concernée : `Ctrl + F12`
- Afficher le contenu de l'élément dans un encart : `Alt + F12`
- Lancer en Debug : `F5`
- Faire continuer le programme : `F5`
- Instruction suivante : `F10`
- Instruction suivante dans la méthode : `F11`
- Ajouter une référence à un projet : `Clic droit sur un Projet > Ajouter > Ajouter une référence à un projet`

### Relecture 📝

- Colin Prokopowicz 
