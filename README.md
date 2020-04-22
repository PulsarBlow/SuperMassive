[![GitHub version](https://badge.fury.io/gh/PulsarBlow%2Fsupermassive.svg)](https://github.com/PulsarBlow/SuperMassive/releases/latest) [![NuGet Downloads](https://img.shields.io/nuget/dt/SuperMassive.svg)](https://www.nuget.org/packages/supermassive)

# SuperMassive

![SuperMassive Logo](https://github.com/PulsarBlow/SuperMassive/blob/master/supermassive.png)

SuperMassive is a small condensed libraries of reusable .NET components and utility classes.

## Libraries

* **SuperMassive** : Core features - SuperMassive swiss army knife!
* **SuperMassive.ExceptionHandling** : Exception handling as it should be.
* **SuperMassive.ExceptionHandling.Logging** : Exception handling with logging features.
* **SuperMassive.Fakers** : A suit of data fakers to feed your hungry unit tests with "almost" real data.
* **SuperMassive.Logging** : A set of abstract and base logging components
* **SuperMassive.Logging.AzureTable** : A concrete implementation of the logging facade for Azure Table


## Use them

These library are published as nuget packages on [nuget.org](https://www.nuget.org/packages?q=supermassive)

Pick what you want, eg. :


```bash
$ dotnet add SuperMassive
```

## Inspect or contribute

You need to install [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1).

```bash
# clone
$ git clone https://github.com/PulsarBlow/SuperMassive

# build
$ dotnet build

# test
$ dotnet test

# test with coverage
$ dotnet test --collect:"XPlat Code Coverage" --settings ./build/coverlet.runsettings -v m -r .build/
```

Test coverge results are collected using [Coverlet](https://github.com/tonerdo/coverlet) and written in the `.build` directory using *Cobertura* format

## Licence

http://opensource.org/licenses/MIT
