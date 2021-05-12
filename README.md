# Tamagotchi
API for playing a simplified version of Tamagotchi

**Running the solution locally**

Make sure you have [installed](https://docs.docker.com/docker-for-windows/install/) docker in your environment. Once you've downloaded or cloned the code in your local computer, from the root directory, execute the following commands from a terminal/command prompt:

`docker-compose build`

`docker-compose up`

This should build and run the containers, and you should be able to access the API specifications from the following URL :

http://localhost:5100/swagger/index.html

To view the generated logs by the API got to the following url

http://localhost:5340/ (Seq logs)

## High level architecture 

This application was written using .NET 5 which can run both on windows/mac/linux on a docker container host. The services were built following DDD and clean architecture, and using Akka.Net for handling the command messages. The GET/Queries were built using Dapper library. The API uses sql server for persisting the data, and is also running in a docker container.

The API has a background service that implements BackgroundService abstract class, this service runs scheduled tasks every 60 seconds to update the existing dragons Age and their metrics.

**Note on the use of Akka.Net:
Since this is the first time that I've used this library, it's probably not the best implementation, nor is it likely using it's full potential for handling messages. This is work in progress.
