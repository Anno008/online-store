# Backend

To run the backend after cloning the repository the following steps are necessary:
* Running dotnet restore to download the necessary packages.
* dotnet run to start the application.
* You can also just open the project in Visual Studio and let the IDE do everything for you.

To create our database with the relationships defined in the models, and very important, referenced in DatabaseContext as DbSets we have to run:
* **dotnet ef migrations add InitialCreate** to scaffold a migration and create the initial set of tables for the model.
* **dotnet ef database update** to apply the new migration to the database. This command creates the database before applying migrations.

An easy way to see our exposed endpoints that clients can consume is to start the application and in your favorite browser navigate to http://localhost:8080/swagger/.