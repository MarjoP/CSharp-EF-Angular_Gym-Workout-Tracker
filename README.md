# Introduction 
Web app utilizing Entity Framework to record and review Gym workout results.

# Getting Started

Actions and using the app:
1. Add new results: 
    a) Select user from the dropdown list. Select "new" if you want to add a new user and write the user's name to the opening dialog box. 
    b) Select exercise from the dropdown list. Select "new" if you want to add a new exercise and write the exercise's name to the opening dialog box. 
    c) Add the amount of repeats and the used weight and hit Add result -button. 

2. Review results: 
    Table of results
    a) Select all users or one specific user from the dropdown list
    b) Select all exercises or one specific exercise from the dropdown list
    c) Limit the amount of results if needed/wanted
    d) Hit the "Get results" -button.

    Graph of max-results
    a) Select one specific user
    b) Select one specific exercise
    c) Limit the amount of results if needed/wanted
    d) Hit the "Get max results to graph" -button.

3. Delete data:
    a) delete user by selecting the user from the dropdown list and hitting Delete user -button. NOTE! This deletes all the user's results as well.
    b) delete exercise by selecting the exercise from the dropdown list and hitting Delete exercise -button. NOTE! Can only be deleted if no one has recorded any results for this exercise.
    c) delete result by selecting the result from the dropdown list and hitting Delete result -button

# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)