# BangazonWorkForce

Welcome to the BangazonWorkForce 

## Getting Started

These instructions will get you a copy of the project up and running on your local machine

### Installing

First, you'll need to clone down the repo into a directory. Open your terminal and enter

```
git clone git@github.com:the-devatstating-chickens/BangazonWorkForce.git
```

You'll need to create a database and also, add data. We used Azure, we recommend using either Azure or SSMS and to run the script to create the database [Official Banagazon SQL](./bangazon.sql).

The next thing to do is add data to each of the tables. We've provided you with the following script [Bangazon Data SQL](./data.sql).


After that, open up your editor which we prefer to be Visual Studio, through the terminal with the command

```
cd BangazonWorkForce/BangazonWorkForce
```

```
start BangazonWorkForce.csproj
```

Now, you'll be taken to Visual Studio with the project opened up. The next thing you'll want to do it start the application and Run the program. On the upper middle part of the toolbar, you'll see a green arrow and `IIS Express`, click on the dropdown button and choose `BangazonWorkForce`. This will run the program and automatically open up a window to see our API. If for some chance you are not directed, simply go to your browser and enter in 

```
http://localhost:5000
```

###### You are now ready to use our BangazonWorkForce. 


## User instructions
* You are able to few several resources: `Training Programs`, `Employees`, `Departments`, and `Computers`

### We hope you enjoyed our app! :)


## Built With

* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) - The language we used
* [ADO.NET](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ado-net-overview) - SQL access
* [Azure](https://gruntjs.com/) - Cloud for database
* [ASP.NET](https://dotnet.microsoft.com/apps/aspnet) - Framework for Web app 




## Authors

* **Billy Mathison** 
* **Jonathan Schaffer** 
* **Alex Thacker** 
* **Anne Vick** 



## Acknowledgments

* [PurpleBooth](https://gist.githubusercontent.com/PurpleBooth/109311bb0361f32d87a2/raw/8254b53ab8dcb18afc64287aaddd9e5b6059f880/README-Template.md) - For their template
