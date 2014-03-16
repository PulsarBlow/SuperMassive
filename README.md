# SuperMassive

SuperMassive is a small condensed framework of reusable .NET components and utility classes.

### Version

Current : 1.1.0-alpha

### Projects descriptions

* **SuperMassive** : Core features - SuperMassive swiss army knife!
* **SuperMassive.Fakers** : A suit of data fakers to feed your hungry unit tests with "almost" real data.
* **SuperMassive.Storage.TableStorage** : Abstrations and base types for using Azure table storage.
* **SuperMassive.Identity** : Abstractions for all SuperMassive "identity" libraries
* **SuperMassive.Identity.TableStorage** : Azure table storage implementation of ASP.NET Identity 2.0-beta. Authentication and users store in the cloud!

### Installation

Pick the features you want by installing the corresponding NuGet package :


```Ìnstall-Package SuperMassive -Pre```
```Ìnstall-Package SuperMassive.Fakers -Pre```
etc..

If you want the prerelease nugets, dont forget to select *Include prerelease* in the nuget manager window or add ```-Pre``` to your Install Package command.

### Build & Tests

To run the unit test suits you need to install and run the [Windows Azure Storage Emulator 2.2.1](http://blogs.msdn.com/b/windowsazurestorage/archive/2014/01/27/windows-azure-storage-emulator-2-2-1-preview-release-with-support-for-2013-08-15-version.aspx) preview which support the "2013-08-15" version.


#### Licence

http://opensource.org/licenses/MIT