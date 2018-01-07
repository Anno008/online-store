# Backend

To run the backend after cloning the repository the following steps are necessary:
* Running dotnet restore to download the necessary packages.
* dotnet run to start the application.
* You can also just open the project in Visual Studio and let the IDE do everything for you.

If you have made changes to the models and want to re generate the database with the new tables run:
* dotnet ef migrations add InitialCreate to scaffold a migration and create the initial set of tables for the model.
* dotnet ef database update to apply the new migration to the database. This command creates the database before applying migrations.
