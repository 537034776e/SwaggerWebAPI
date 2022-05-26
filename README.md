# Overview
Wep API integrating Swagger service with Dependency Injection using Ninject and db connection through Entity Framework

This project offers 4 principale modules:
- **Swagger.WebAPI**: host that expose service methods 
- **Swagger.ORM**: database implementation using Entity Framework 5.0
- **Swagger.Interfaces**: definition of interfaces for Ninject
- **Swagger.InterfaceImplementation**: implementation of interfaces methods

To use correctly this repository in your system, you need to:
- Change all Connection String in Web.config and App.config files modifying the db connection
- Create a database that represent the existent mapping

SwaggerWebAPI needs the following package to work correctly:
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Relational
- Microsoft.EntityFrameworkCore.Relational.Design
- Ninject
- Ninject.Web.Common
- Ninject.Web.Common.WebHost
- Ninject.WebApi.DependencyResolver
- Swashbuckle

To avoid error about Ninject implementation, you need to modify the **NinjectWebCommon.cs** file in *Swagger.WebAPI > App_Start* adding the following line under the **CreateKernel** method:

```System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);```

Also you need to bind the interfaces to make it usable in the controllers:

```kernel.Bind<Interface>().To<InterfaceImplementation>();```

