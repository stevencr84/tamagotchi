# Tamagotchi
API for playing a simplified version of Tamagotchi

**Running the solution locally**

Make sure you have [installed](https://docs.docker.com/docker-for-windows/install/) docker in your environment. Once you've downloaded or cloned the code in your local computer, from the root directory, execute the following commands from a terminal/command prompt:

`docker-compose build`

`docker-compose up`

This should build and run the containers, and you should be able to access the API specifications from the following URL :

http://localhost:5100/swagger/index.html

This endpoint lists your existing dragons
http://localhost:5100/api/dragon
This endpoint creates a new dragon with the name you specified and will return an endpoint (something like this: http://localhost:5100/api/dragon/{createdDragonId}) to check on the created dragon status and metrics
http://localhost:5100/api/Dragon/create
This endpoint is for petting your draggon, providing the dragon's ID
http://localhost:5100/api/dragon/pet
This endpoint is for feeding your draggon, providing the dragon's ID
http://localhost:5100/api/dragon/feed

To view the generated logs by the API got to the following url
http://localhost:5340/ (Seq logs)

## High level architecture 

This application was written using .NET 5 which can run both on windows/mac/linux on a docker container host. The services were built following DDD and clean architecture, and using Akka.Net for handling the command messages. The GET/Queries were built using Dapper library. The API uses sql server for persisting the data, and is also running in a docker container.

The API has a background service that implements BackgroundService abstract class, this service runs scheduled tasks every 60 seconds to update the existing dragons Age and their metrics.

** **Note on the use of Akka.Net**:
Since this is the first time that I've used this library, it's probably not the best implementation, nor is it likely using it's full potential for handling messages. This is work in progress.

If I hadn't used this library I would normally implement CQRS with [Mediatr](https://github.com/jbogard/MediatR) for in-process message handling.

** **Out of scope for this solution**:
I didn't implement logic for what will happen if the dragon keeps on getting hungry, or his happiness keeps on decreasing, since they're out of scope for this test, but in the real world it would check for this metrics and it would either change the life status to died of hunger or died of saddness or something similar.

I didn't create integration tests, testing the actual API endpoints with something like an in memory database, since it seemed out of scope for what I'm trying to do here, but in a real world scenario I would create this tests using similar practices to [these](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-5.0).
