# cats-listing-demo-dotnet-core

# Description
A cats listing demo project developed with .Net Core MVC following [SOLID](https://en.wikipedia.org/wiki/SOLID) principles.
It shows a list of pets grouped by their owner's gender.

# Getting Started

This application was developed as a web application using MVC (.net core) to demonstrate the use of best practices patterns known as the SOLID principles to create a separation of concerns between different sections and dependencies of a system and to make it easier to test and extend.

It uses dependency injection to resolve all dependencies.

It reads the data from an externally available source and uses retry policies for resilience with [Polly](https://github.com/App-vNext/Polly)

1. Software dependencies
   - DotNetCore 3.1 SDK for building.
   - VS2019
2. Installation process
   - Make sure you have dotnet core 3.1 SDK installed. Get it [here](https://dotnet.microsoft.com/download).

## Build

1. Build from command line by navigating to the root folder and executing `dotnet build`.
2. 3. To run the UI project, navigate to the WebUI folder `\CatsListingDemo.WebMvc` and execute `dotnet run`.

## Unit Tests
Unit tests for all application layers are included (except for the controllers). Test harness is created using [MS Tests](https://en.wikipedia.org/wiki/Visual_Studio_Unit_Testing_Framework) with a combination of [AutoFixture](https://github.com/AutoFixture/AutoFixture) and [FakeItEasy](https://github.com/FakeItEasy/FakeItEasy) for creating test fixtures and mocking of dependencies respectively and [FluentAssertions](https://github.com/fluentassertions/fluentassertions) for readable, fluent-syntax assertions.

## Limitations
Because of scope limitations for this demo, only basic functionality is included. For a real world scenario improvements should be made to this implementation like data validation, caching, error handling and OO client side code by using a combination of typescript and MVVM javascript frameworks such as Angular, React or Vue.

