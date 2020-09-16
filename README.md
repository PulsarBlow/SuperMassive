[![GitHub version](https://badge.fury.io/gh/PulsarBlow%2Fsupermassive.svg)](https://github.com/PulsarBlow/SuperMassive/releases/latest) [![NuGet Downloads](https://img.shields.io/nuget/dt/SuperMassive.svg)](https://www.nuget.org/packages/supermassive) ![Continuous Integration](https://github.com/PulsarBlow/SuperMassive/workflows/Continuous%20Integration/badge.svg)

![SuperMassive Logo](https://github.com/PulsarBlow/SuperMassive/blob/master/supermassive.png)

# SuperMassive

SuperMassive is a small condensed set of reusable .NET components and utility classes.

> This README is for SuperMassive v2.x (the current version). For SuperMassive v1.x, see [here](docs/v1/README.md).

> Starting with v2.x, some library are now :small_orange_diamond:*deprecated*:small_orange_diamond:
> They won't receive new features anymore and will be removed in a future version.

## Libraries

*  **SuperMassive** : Your super massive swiss army knife!
*  **SuperMassive.ExceptionHandling** : Exception handling as it should be. :small_orange_diamond:*deprecated*:small_orange_diamond:
*  **SuperMassive.ExceptionHandling.Logging** : Exception handling with logging features. :small_orange_diamond:*deprecated*:small_orange_diamond:
*  **SuperMassive.Fakers** : A suit of data fakers to feed your hungry unit tests with "almost" real data.
*  **SuperMassive.Logging** : A set of abstract and base logging components. :small_orange_diamond:*deprecated*:small_orange_diamond:
*  **SuperMassive.Logging.AzureTable** : A concrete implementation of the logging facade for Azure Table. :small_orange_diamond:*deprecated*:small_orange_diamond:

## Getting Started
hi

These library are published as nuget packages on [nuget.org](https://www.nuget.org/packages?q=supermassive)

Pick what you want, eg. :

```bash
$ dotnet add SuperMassive
```

## Build & Test

You need to install [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1).

```bash
# clone
$ git clone https://github.com/PulsarBlow/SuperMassive

# build
$ dotnet build

# test
$ dotnet test

# test with coverage
$ dotnet test --collect:"XPlat Code Coverage" --settings ./build/coverlet.runsettings -r .build/
```

Test coverge results are collected using [Coverlet](https://github.com/tonerdo/coverlet) and written in the `.build` directory using *Cobertura* format.

## Contributing

All contributions are welcome! Please read our [Contributing guideline](CONTRIBUTING.md) before submiting your work. By contributing you agree to follow our [Code of Conduct](CODE_OF_CONDUCT.md).

## Licence

<http://opensource.org/licenses/MIT>
