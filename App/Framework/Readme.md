# WorkplaceX Framework
Framework to create database web apps based on .NET Core, Angular, SQL Server and UI (Angular Material or Bootstrap or Bulma).  Runs on Windows and Linux. Provides CI/CD pipeline.

Project page: [WorkplaceX.org](https://www.workplacex.org)

# Getting Started
Prerequisites for Linux and Windows
* [Node.js](https://nodejs.org/en/) (LTS Version)
* [.NET Core SDK](https://dotnet.microsoft.com/download) (LTS Version)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Free Express)

# Version Check
```
dotnet --version # 6.0.101
node --version # v16.13.1
npm --version # 8.1.2
ng --version # Angular CLI: 13.1.2
git --version # git version 2.34.1.windows.1
```

# Install Framework.Cli (wplx)
Before you can use the wplx command you need to install the tool. In the folder Framework/ run:
```
dotnet pack ./WorkplaceX.Cli/ # Compile source code and create tool package
dotnet tool install --global --add-source ./WorkplaceX.Cli/bin/Debug/ workplacex.cli # Install tool package
dotnet tool list --global # Show all installed tools
dotnet tool uninstall --global workplacex.cli # Uninstall it again (if needed)
```

# Project and Folder Structure
* "App/" (App with custom business logic in C#)
* "App.Cli/" (Command line interface to build and deploy in C#)
* "App.Cli/DeployDb/" (SQL scripts to deploy to SQL server)
* "App.Db/" (From database generated C# database dto objects like tables and views)
* "App.Server/" (ASP.NET Core server to start app)
* "App.Web/" (Angular app)
* "Framework/Framework/" (Framework kernel doing all the heavy work)
* "Framework/Framework.Cli/" (C# Command line interface to create new apps)
* "Framework/Framework.Template/" (Template code to create a new app. Used by Framework.Cli)
* "Framework/Framework.Test/" (Internal C# unit tests)
* "ConfigCli.json" (Configuration file used by App.Cli command line interface)
* "ConfigServer.json" (From ConfigCli.json generated configuration used by App.Server project) 