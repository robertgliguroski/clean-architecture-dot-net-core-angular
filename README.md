## LinkitAir - Clean architecture with ASP.NET Core and Angular

A modular, loosely-coupled web application, following the SOLID principles, built using ASP.NET Core 2.1 & Angular 5. It demonstrates the "Clean architecture".

The motivation for choosing the Clean architecture is that I believe it is one of the best approaches to designing web applications today. It directly addresses and solves the main problem with the traditional "N-layer" architecture which was so dominant in the past years(most of the .NET projects I've witnessed have been built using this approach). The problem with the N-layer approach is that compile-time dependencies run vertically from the top layer to the bottom layer. That means that the UI layer directly depends on the BLL(Business Logic Layer), which in turn directly depends on the DAL(Data Access Layer).

This means that the main application logic(which is contained in the BLL) will always depend on the existence of a database, which makes testing extremely hard. One approach often used to approach this problem is using Dependency Injection together with some Mocking functionality(they often go together with a Unit Testing Framework). 

This approach is perfectly fine for dealing with this issue, because using Dependency Injection actually inverts the responsibility of handling dependency - instead of having an object construct its needed dependencies itself, we are shifting that responsibility to another "object"(most often a DI framework) and we're providing those dependencies to the object at run time, instead of compile time. 

Clean architecture takes this approach one step further - instead of fixing a broken architecture with DI, why don't we have a completely new architecture which relies on the same principles DI relies on? 

So now, instead of having the Business logic layer depend on the Data access layer, let us have the dependency inverted and have the business logic in the center of the application(Core) and have the Infrastructure depend on it! 

This means that we will be creating a lot of Interfaces and put them in the Core(center of the application), together with the Entities and Domain Services(which will contain most of the business logic).

This provides us with two major benefits:

* The Core does not depend on Infrastructure, so we can easily write unit tests(and automated unit tests) for this layer(and test the bulk of the business logic)
* the UI layer does not depend on Infrastructure, so it is ery easy to swap implementations(e.g. in the Controllers) for testing purposes

The following provides a simple layout of a web application organized by the principles defined in the Clean architecture:

* Core project: Holds the Interfaces(for both Services and Repositories), Entities and the actual Services(which hold the business logic but rely on Interfaces and do not depend on types defined in Infrastructure), Specifications, Exceptions. Additional services which require infrastructure-related classes should also have their Interfaces here, but they will be implemented in the Infrastructure layer
* Infrastructure project: Contains implementations related to data access, such as: Entity Framework DbCOntext, Data Migrations and data access code, most often classes following the Repository pattern(i.e. Repositories). Interfaces for services that require classes related to Infrastructure(files, logging etc.) should be implemented here, by the appropriate class implementations
* UI project - Contains the Controllers, Views, ViewModels etc., all of which must not interact with Infrastructure directly, but strictly through interfaces defined in the Core layer. In practice this means that we will not have any instantiation of types defined in Infrastructure
