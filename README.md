# Introduction 
Rakenteilla oleva EntityFrameworkia hy�dynt�v� sovellus kuntosalitulosten tallentamiseen. 

# Getting Started
At the current stage the adding and querying data into and from the database is handled through the console. However, an appropriate user interface will be added in near future. 

Actions and using the app:
1. Add new user: This action will create a new user profile with a given name. You may not use an existing user name.

2. List users: You will be able to see the reserved names ie. existing user profiles in the current database (later this should be a drop down list etc.). 

3. Add new exercise: This action will add a new gym exercise with a given name to the database. This needs to be done only once for each exercise type. 

4. List exercises: You will be able to see which exercises have already added to the database (later this shoud be a drop down list etc.).

5. Add new result: This enables the user to add new result for a specific exercise and user to the database. Currently the app will record the day the result was added, however there should be an option to record also past exercises after UI development.

6. List all results: This will list all results of all the users in the database.

7. List results based on selection: This enables to get results for specific user or for specific exercise from the database. You can also choose to limit the amount of retrieved results (useful if the database gets big with lot of results...) There will be further options later on to get e.g. max-results and graphical expression of progress of a specific user. 


# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)