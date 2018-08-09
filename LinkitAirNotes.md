# LinkitAir app development process

A modular, loosely-coupled web application, following the SOLID principles, built using ASP.NET Core & Angular. It demonstrates the "Clean architecture":

"Clean architecture puts the business logic and application model at the center of the application. Instead of having business logic depend on data access or other infrastructure concerns, this dependency is inverted: infrastructure and implementation details depend on the Application Core. This is achieved by defining abstractions, or interfaces, in the Application Core, which are then implemented by types defined in the Infrastructure layer." (Steve Smith - "Architecting modern web applications with ASP.NET Core and Azure", 2017) 

## Clean architecture

The motivation for choosing the Clean architecture is that I believe it is one of the best approaches to designing web application today. It directly addresses and solves the main problem with the traditional "N-layer" architecture which was so dominant in the past years(most of the .NET projects I've witnessed have been built using this approach). The problem with the N-layer approach is that compile-time dependencies run vertically from the top layer to the bottom layer. That means that the UI layer directly depends on the BLL(Business Logic Layer), which in turn directly depends on the DAL(Data Access Layer).

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

## Explanation for decisions and steps taken in Commit #4

As previously mentioned, Entities will live in the Core layer(and Core project) which is in the center of the application and does not depend on anything. I created the Entities as simple POCOs which have only properties and getters and setters, but do not really have any behaviour. According to Martin Fowler, this is an anti-pattern. [In this 2003 blog post titled "AnemicDomainModel](https://martinfowler.com/bliki/AnemicDomainModel.html) Martin Fowler talks about the increasing popularity of Domain models which do not contain any behaviour at all, which is "contrary to the basic idea of object-oriented design; which is to combine data and process together". Martin Fowler explains that although he encourages putting behaviour in the domain objects, that does not mean that we should put persistence logic or presentation logic in our domain models, which would be a violation of the "Single responsibility principle". He also references Eric Evans' book "Domain Driven Design", where Eric Evans explains the difference between the logic which should go into the domain models and the logic which the Service layer(if present) should contain.

However, since I have been working with "Anemic domain models" in my practical .NET experience and have never actually tried to put any significant logic in the domain models, and considering the time limit of this practical test, I decided not to experiment with it now in fear of making a mistake and putting some behaviour in the wrong place(and also just doing it would require a lot of extra thinking). 

Also, even though Evans explains in his DDD book that the "Service layer should be thin", since I am not following DDD strictly in this example(but simply adopting some of the design principles that DDD includes like Persistence Ignorance, Explicit Dependencies etc.) and also since the "Clean architecture" guides by Steve Smith do allow a thicker Service layer which contains most of the business logic(I am guessing he is OK with this because the Service classes in his Clean architecture projects belong in the Core layer, right next to the Domain models(Entities)) - I decided to not follow this recommendation by Martin Fowler for this example and go with the anemic domain models. Hopefully I will have the chance to think about this again and perhaps implement it in the future.

The AppDbContext class - I decided to override the OnModelCreating method and use Entity Framework Core Fluent API to configure my models instead of choosing data attributes(Data Annotations). Fluent API is the most powerful model configuration method in Entity Framework and it also doesn't require modifying the domain classes(Entities). I have not enabled Lazy Loading(which I think is still turned off by default in EF Core) and have not declared virtual navigation properties in my Entities. [This post by Steve Smith explains why we should avoid Lazy Loading in Web applications](https://ardalis.com/avoid-lazy-loading-entities-in-asp-net-applications). It shows a drastic difference in performance when not using Lazy Loading. Namely, in his example - a simple query with Lazy Loading the properties results in 22 database queries! He then runs the same query using EF Core without Lazy Loading, including the needed properties explicitly in his EF query, which results in 3 database queries generated.

One problem I had with the Fluent API is that I have created a "cycle or multiple cascade paths" by having my FlightRoute entity reference the Airport entity twice, with two different properties(for the Origin and Destination). So I took a simple(although not the best) approach of defining the foreign key properties in FlightRoute as nullable. I realize this is incorrect, but again  - since we're talking about a test app I decided not to spend any more time on this and just go with this fix.

I decided to use Sql Server Express with Sql Server authentication(and created a Sql Server user, granded the needed roles to the LinkitAir database). I realize this would require the same on your side in order to test the application, but since Sql Server Express is now also supported on Linux, I thought it wasn't going to be a problem. I thought about using an In-Memory database or Docker, but for the former I now it's mainly used for testing and wasn't sure if it would support all my wishes in terms of database normalization and referential integrity and for the latter - I just do not have any exprience with Docker and did not want to complicate things furthermore.

I also used EF Core Migrations, since I think they go very well with the "Code First" approach that I've taken and are a convenient way of applying the Model changes to the database. There were some setbacks with the CLI commands for adding migration and updating the database, because my Class Library project "Infrastructure" which holds the migrations targets .NET Standard, but I fixed that by specifying the "--project Infrastructure" flag and "--startup-project LinkitAir" flag, as well.

The last thing is the second "@TODO" comment about trying to avoid referencing the Infrastructure project just so I can register the DbContext. Will need to look into that in more details.

## Explanation for decisions and steps taken in Commit #5

Added a generic repository called "EfRepository" in the Infrastructure project(Data folder). This is the central point for working with the DbContext class. It works with any object that extends BaseEntity. This way I do not have to create separate repositories for each entity with a partially repeating logic. 

Added a Specification class(BaseSpecification.cs) - [The Specification design pattern is explained here on DevIq](https://deviq.com/specification-pattern/). It basically encapsulates queries in reusable objects which receive the query details dynamically. It can be tested by itself. But the biggest benefit of using this pattern is that in enables us to refine the query we need in the repository layer(by using IQueryable which builds a tree with all the criteria) and still return an IEnumerable from the Repository! [Returning IQueryable is a topic of debate amongst software engineers, a lot of people feel very strongly against it, and for good reasons, too](https://softwareengineering.stackexchange.com/questions/192044/should-repositories-return-iqueryable). So by having a method in the Repository layer that returns an IEnumerable, but uses IQueryable inside it in order to build dynamic queries with conditions/criteria which can be added depending on the needs, we have eliminated the danger of leaking query trees into the Service layer or even the Controller, and at the same time have eliminated the terrible(but often seen, mostly from juniors) approach of not using IQueryable at all and building a query with dynamic conditions by using IEnumerable, which does not make any sense since that query will be materialized on the first call to a .Where() method, and will result in making a trip to the database way too earlier than we intended and will add the additional conditions only after it has retrieved the data(so it will filter out the rows in memory).

Also added a service - AirportService, which uses the methods provided by the generic Repository and in this case - returns all the airport rows from the database. 

I also added the appropriate interfaces for all of the aforementioned classes and have used the default IoC container shipped with .NET Core in order to register these dependencies in the ConfigureServices method of the Startup.cs class. I have not yet decided whether I am going to replace this container with a custom one.

The ViewModels(which should have really be called ApiModels) - I am using these because I do not want to expose domain models(Entities) directly to the client. Doing this provides me several major benefits: First, I am not exposing all the attributes of the entity to the client(perhaps I do not want the client to be aware of some attributes). With using a ViewModel, I can only expose what I want. Another thing is that I make the data transfer lighter - I'm not sending unnecessary data with each request. One thing to note is that while I was committing the code a while ago, I realized "ViewModel" is an incorrect name for these classes, because this is a Web Api and not MVC, so I'm not using views currently, so I probably should've called them "ApiModels" or maybe even DTOs. But nevertheless, this is not that big of an issue.

Finally, in the Angular 5 app - I have added an "interfaces" folder which currently contains one file called "airport.ts" which exposes an "IAirport" interface(Angular does not encourage the use of the I-prefix for the interfaces, but I find it I am much more comfortable and enjoyable with using it, so I'm going with it). I decided to use interfaces in order to fully utilize the benefits that Typescript offers. In this example - I am working with a type definition instead of an anonymous object. This means I will get type-checking which means errors about properties having the wrong name and/or type will be caught much earlier. Also, an interface is a reusable structure which will eliminate the need for violating the DRY principle by copying/pasting anonymous objects.

I have also added a "services" folder in the Angular app - services offer a way to remove the data retrieval responsibility from the Angular components. It is not the component's job to retrieve its own data. It should be provided to it by a service, which will be injected via constructor injection in the component. In the airport-service we can already see our IAirport interface being used to define the expected result from the get request.

The airport-list-component lists all the airport records retrieved from the server. I included it in the home-component.html template so that it displays the airport records on the homepage(for now).

## Explanation for decisions and steps taken in Commit #6

Added and configured Swagger, it is available at the following URL - <host>:port/swagger/index.html

## Explanation for decisions and steps taken in Commit #7

The new GetDestinationAirportsForOriginAirport() method in the AirportService shows us the power of the Specification pattern. By creating a new Specification class "AirportFilterByBeingADestinationForOrigin" specifically for this scenario, I have encapsulated the linq query inside an object of a class, where it can be reused, it can be tested independetly etc. 

This methods gives me the airport data for those records which are valid destinations(as defined in the FlightRoute table) for the selected origin. I have represented this behaviour with two simple dropdowns, the second of which is not populated until a value is selected in the first one. Only then it is populated with the valid destinations. I did not get to making the dropdowns searchable yet, since I consider it a less significant step and will implement it in a later stage.

In the angular part - I made use of the ngModel directive to bind the select fields to properties of type IAirport in the AirportListComponent. I also used the ngModelChange event to capture the change in the dropdown and call an appropriate function in the component.

Apart from that, I also made some minor changes in the Airpot model and the appropriate Fluent API configuration in DbContext(as mentioned in the commit message). It is important to note here that the navigation properties I added are not declared as virtual(because of Lazy Loading).

## Explanation for decisions and steps taken in Commits #8 and #9

These commits are pretty self-explanatory, I worked on the business logic here, added the needed Specifications, Service layer methods and Controller methods and made the calls from the Angular client(service method, interface and component http call over there, as well). Now I've gotten to the stage where I am successfully showing the flights for a combination of origin airport and a destination airport and will most probably switch to the statistics part, even though the UI needs much, much more work because at the moment it is extremely not-good-looking :)

## Explanation for decisions and steps taken in Commit #10

*Note: The last commit should have been called 'Commit #10', instead it was mistakenly called Commit #9, which already existed

So, at the moment I'm using the RequestActionFilter class where I am overriding the OnActionExecuting and OnActionExecuted methods. In the OnActionExecuting I am getting the request method and path from the HttpContext object, and I'm also starting a Stopwatch instance whose value I am later retrieving as elapsed ticks in the OnActionExecuted method, where I am also getting the response status code, which is the one that was causing me the most problems since here, at this point - it will always be 200 because even if my action methods return 500, that has not yet been told to this HttpContext.Request so it doesn't know about it and returns 200.

So I'm using that for now, and also in the OnActionExecuted method I am calling a service method to save this info to a database table I created. 

I have used this custom filter on a new custom 'BaseController' which I had all my API controllers extend from. That way - the filtering with my custom logic will always be applied to all of the API methods.

At first I tried to write my own custom middleware and add it to the pipeline, following this [Official Microsoft documentation article on ASP.NET Core Middleware](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-2.1&tabs=aspnetcore2x), as well as [some useful blog posts like this one](https://elanderson.net/2017/02/log-requests-and-responses-in-asp-net-core/).

This resulted in the "RequestResponseLoggingMiddleware" which is practically taken from the blog post example, but I decided not to use it in the end because it was supposed to be used via an extension method to the IApplicationBuilder object and called in the Startup class, in the Configure method(app.UseRequestResponseLogging();).

I did not have a way to inject a Service here in order to call my method for saving the data in the DB, and I did not want to instantiate a new repository instance here and use DbContext directly, but if I do not find another solution for the status code problem, I might as well do that.

The IAsyncRepository interface and its implementation in EfRepository were yet another thing I tried, I tried to use an inline anonynous method(in-line middleware) in Startup.cs by using the app.Use() extension method and calling  class called "HttpRequestResponseHelper", but I wasn't able to use my service here either(in the anonymous method). 

I then tried to set the request/response data here in Session variables and retrieve it later, but I did not like this approach and did not proceed with it(I left all this unused code there just for reference).

So as I said - I will go with the ActionFilter for now and will try to think of something better in the meantime.

	
## Explanation for decisions and steps taken in Commit #11

I quickly read the following text before going to work today:

[ASP.NET Core deep dive](https://joonasw.net/view/aspnet-core-di-deep-dive)

and [the following Stackoverflow thread](https://stackoverflow.com/questions/37813721/asp-net-core-dependencyresolver)

so I used IRequestLogService service = (IRequestLogService)context.RequestServices.GetService(typeof(IRequestLogService));

in order to make the service available in my in-line middleware, so now I can comment out the ActionFIlter(I left the code for a reference) and use my HttpRequestResponseHelper and get the correct Response StatusCode! 

## Explanation for decisions and steps taken in Commit #12

I used async/await to make the "CreateRequestLog" method in the "RequestLogService" asynchronous, so that any delay on database side when saving the metrics data would not block the UI in any way. I used an async version of the repository. 

## Explanation for decisions and steps taken in Commit #13

I added Angular material to improve the look & feel of the application, especially the dropdowns. It also helped with the searchable selects, the MatAutocomplete module works fine, needed some custom configuring and modification of the existing code but now it works as expected - gives the correct destination values to choose from for a selected origin.

It lacks searching by code and description, right now it can only search by name.

## A small commit with some new packages

I noticed yesterday that my app's initial load was really slow, so I got to debugging and found out that actually it's the building of all the packages, [as can be seen here](https://i.imgur.com/rufprE4.png). The "building modules" takes way too much time. I investigated further into this and figured out it's because I'm using the ASP.NET Core Angular template, and found some possible solutions to this problem but I'm afraid they required a lot of effort and since this is out of the scope of this project - I decided not to do it. I wouldn't use a template in real life, anyway, so this is a non-issue.

## Explanation for decisions and steps taken in Commit #14

For the methods that will be retrieving the statistics for the request/response data, it was hard to reuse the existing generic Repository because I needed a Count() method and the only way to use the existing repository was to first call the GetAll() method which would have returned an IEnumerable<RequestLog> and only then call Count() on the collection.. But this means in-memory filtering of the data and it means that I have already retrieved all the records(every column and every row), when I just needed the count. I did not want to do a full select * when I only needed a select count(*) from..., which a direct call to Count() would have given me.

That is why I (partially) used another pattern connected to the repository pattern, which is "a per-model repository", that is - I created a dedicated repository for the RequestLog model which extends the EfRepository but contains methods specific for that entity/model.

I like this approach, as well, and I have used it extensively in my experience, and even though I like the generic Repository better, I decided to use it here because it solved my problem at the time and at the same time I thought I would demonstrate a mixture of the two approaches.

I have also created separate methods for geting the average, min and max response times, which I think is superfluous because we will be needing that data on one page(the dashboard) and perhaps it would be unnecessary to execute three separate queries for that when we could get them with one query which combines those results(even though I am not sure if Entity Framework offers that functionality out of the box withot grouping). I will think about this.

I also added a "group by" query which was not requested explicitly, but I think it's nice because it gives number of requests by the exact response code, not just starting with 4 like "4xx" which would group them together, but specifically for all status codes(400, 401, 404 etc.)

I added some styling via Angular 5 Material which improved the UI and the design, I will be doing that tomorrow as well, for the dashboard where the statistics will be shown. 

## Explanation for decisions and steps taken in Commit #15

Added "ASP.NET Core Identity" - a membership system that offers out-of-the box authentication and authorization features. I registered the service in Startup.cs, I created an "ApplicationUser" model which extends the provided "IdentityUser" class. I am mapping this entity to a "Users" table, and after adding a "RoleManager" and a "UserManager"(also provided by .NET Core Identity) - the next migration and database update generates the Users table, as well as six new tables(for the roles, claims, tokens etc).

I also added a "Seeder" class which I used to create an "admin" user so that I can test this functionality.

For authentication of the "admin" part of the application(where the statistics are going to be shown) I decided to use JSON Web Tokens, sinice tokens are the most widely used method of authenticating WEB Apis these days, and are also suitable to this occassion. There's a new "TokenController" which does the work of using the provided "UserManager" class and actually performing authentication and returning a valid token if the user authenticates successfully. 

At this point the Angular client is not yet updated to work with authentication, but I tested this with Postman yesterday and it worked perfectly, so hopefully tonight I will be able to add the support for the Angular client, too

Apart from this Authentication work, I also made the Angular API calls to get all the needed statistics, some modifications to the repository/service layer methods were needed in order to work successfully with the "ticks" stored in the database(since I was using "Stopwatch" to measure the request time and stored "ticks" in the "ElapsedTicks" column, they needed to be converted back to seconds before they are shown to the user. 

One interesting thing to mention here is that since I am using an anonymous object in the "GetStats()" method in the Repository/Service layer to handle the IGrouping returned by the LINQ group by query - I ended up having an "object" whose properties values I needed to get in order to convert them from ticks to seconds. I used reflection for this, to get the object type and then the properties and their values. I had other options(to return something else from the grouping query), but I thought I would leave it like this, since reflection is interesting to work with and it is a small example and wouldn't affect the performance greatly.

Finally, I looked in more details into the problem of the Angular app starting extremely slowly, and as I said before - the ASP.NET Core Angular project template was the culprit - it allows us to not have to run a separate server manually, but the drawback is that [each time we modify our C# code and the app needs to restart, the Angular CLI server restarts](https://docs.microsoft.com/en-us/aspnet/core/client-side/spa/angular?view=aspnetcore-2.1&tabs=visual-studio).

Microsoft says this adds around 10 seconds to the start-up process, which I can confirm. 

So when I started the Angular CLI server independently via cmd, and modified the Startup.cs class accordingly to not use the integrated CLI instance - the Angular app runs separately and startup time after each C# change is significantly reduced.

## Explanation for decisions and steps taken in Commit #16

I updated the context menu on the left to contain links to the Home screen, Admin screen and a Log out option for when a user is logged in. I also added a login component which is going to be used to access the Admin panel. This is where end users are redirected to if they Click the "Admin" link in the context menu. It's a standard form with username and password and of course, it works using the JWT tokens. I added a lot of standard code to support login functionality, but I separated it into an AuthService and a LoginComponent and an AuthInterceptor service which implements Angular's "HttpInterceptor" interface in order to add the token to the headers of the HTTP request.

## Explanation for decisions and steps taken in Commit #19

I wanted to offer users some other alternative flights from other airports in the same city of origin/destination. So that they can choose to fly from Luton to Amsterdam if the flight from Heathrow is too expensive.

## Explanation for decisions and steps taken in Commit #20

I relize now that I have left the configuration handling of the API endpoints for too late, I should have handled it earlier. I have created some basic centralization of the URLs in the environment config file and only there will we have hard-coded strings for the URLs, which are later retrieved in a BaseService from which all the other services extend. But I realized only now that I cannot just do this for URLs which have parameter and would need some kind of string replacement, but since I do not have any time left - I will submit it like this with the note that I am aware of this and am sure that there is an easy way to handle this.

