# Instructions for setting up the environment to run the LinkitAir application

1. Clone the repository using git clone https://gliguroskir@bitbucket.org/linkit-group/robert-gliguroski.git
2. Select the 'dev' branch and checkout that branch using git fetch && git checkout dev
3. Create a new database in Sql Server(or Sql Server Express) with the desired name
4. Create a new Sql Server user with the desired name
5. Grant the 'db_owner' role for your new database to the new user you have created
6. Open the application in Visual Studio(or some other editor) and open the 'appsettings.json' file
7. Edit the connection string accordingly, entering your new database, user id and password and save the file
8. Open the Package Manager Console if using Visual Studio and Windows and run the following command: 
		"Update-Database -Project Infrastructure -StartupProject LinkitAir"
   - If you're using Linux or some other system run:
		"dotnet ef database update --project Infrastructure --startup-project LinkitAir"
9. Open Sql Server to ensure that the Migrations have been applied and the new database is populated with all the tables
10. Open the Startup.cs class and uncomment the following lines to seed the database and insert an admin user and all the appropriate roles and other related data:
		"dbContext.Database.Migrate();
                 DbSeeder.Seed(dbContext, roleManager, userManager);"
 - Run the application after uncommenting those lines in order to execute them. 
11. Open the Users table and verify that a new row has been inserted in the Users table(the AspNetRoles and other similar tables will contain data, as well).
12. Comment out the two lines in Startup.cs again
13. Open a command prompt on Windows or a Linux terminal and navigate to the "ClientApp" folder inside the "LinkitAir" project.
14. run "npm start" to get the Angular client up and running
15. Open Visual Studio and run the application either in Debug mode or without debugging(run the appropriate VS Code commands if on Linux) and notice the local URL and the port
16. In the appsettings.json file replace the Auth "Issuer" and "Audience" properties with the appropriate local URL and pot
17. In the "environment.ts" file, replace the baseApiUrl value with the appropriate local URL(if using localhost just update the port, if using a virtual host enter it there)
17. At this point, the Users table(and related tables) contain data so you can go ahead and login to the admin by using the username "Admin" and password "Pass4Admin", as specified in the DbSeeder class. 
18. Once you log in, you should already be able to see the statistics, since the RequestLog table is being populated with data on each request.
19. On the frontend you do not have any data, no airports, flights etc. So go ahead and import the "linkit_air_data.sql" file which can be found in the Infrastructure project, inside the folder called "SqlScripts". It contains "Insert into" statements, ordered in the correct order so that no FK violation would occur, so you just need to change the Use [database] command and execute the script and your new database will be filled with data
20. Run the application again and you should see the airport data in the dropdowns and you should be able to start using the application.



