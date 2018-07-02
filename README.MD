# Demo
* The application is currently hosted on heroku. The deployment is done automatically through gitlabs' continuous integration.
* The frontend application can be viewed at the following address https://front-star.herokuapp.com/
* Currently supported endpoints can be tested on https://backon.herokuapp.com/swagger/index.html

# Setting up the development environment
## Backend

To run the backend after cloning the repository the following steps are necessary:
* Install the [.NET Core SDK](https://www.microsoft.com/net/download/windows) 
* **dotnet restore** to download the necessary packages.
* **dotnet build** to generate the dlls.
* **dotnet run** to start the application.
* **dotnet test** To run the tests.
* You can also just open the project in Visual Studio and let the IDE do everything for you.

To create our database with the relationships defined in the models, and very important, referenced in DatabaseContext as DbSets we have to run:
* **dotnet ef migrations add InitialCreate** to scaffold a migration and create the initial set of tables for the model.
* **dotnet ef database update** to apply the new migration to the database. This command creates the database before applying migrations.

An easy way to see the exposed endpoints that clients can consume is to start the application and in your favorite browser navigate to http://localhost:8080/swagger/.

## Frontend-React
To run the frontend after cloning the repository the following steps are necessary:
* Install [node](https://nodejs.org/en/download/current/)
* **npm install** to download all the packages defined in package.json.
* **npm start** to start the application.
* **run npm build** to create the dist folder with index.html and bundle.js.
