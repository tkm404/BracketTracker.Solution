# IMPORTANT TEMPLATE USAGE STUFF IN THE README
Follow along below, replace with your own work as needed:

# TITLE
#### Brief Description/Overview  

#### By (your name here)  

## Technologies Used:
* C#
* .NET 6.0
* MSTest (if needed)
* MySql

## Description:
Long Description / Mission Statement / What the app do.  

## Setup/Installation Req's:

### Install Tools

Install the tools that are introduced in [this series of lessons on LearnHowToProgram.com](https://www.learnhowtoprogram.com/c-and-net/getting-started-with-c).

### Set up the Databases

Follow the instructions in the LearnHowToProgram.com lesson ["Creating a Test Database: Exporting and Importing Databases with MySQL Workbench"](https://www.learnhowtoprogram.com/lessons/creating-a-test-database-exporting-and-importing-databases-with-mysql-workbench). use the example of `todolist_with_ef_core_dump.sql` to create a new database in MySQL Workbench with the name of your project `project_name`.

### Set Up and Run Project
**a. Click "Use This Template" button on the main page. [Read more here](https://docs.github.com/en/repositories/creating-and-managing-repositories/creating-a-repository-from-a-template) delete this before pushing your real project.**
1. Clone this repo.
2. Open the terminal and navigate to this project's production directory called "CsharpTemplate". //Rename this with your project name and delete all this text between the forward-slashes. Also look carefully throughout and replace all placeholder [things in bracks] with your project's name and model names.//
3. Within the production directory "CsharpTemplate", create a new file called `appsettings.json`.
4. Within `appsettings.json`, put in the following code, replacing the `uid` and `pwd` values with your own username and password for MySQL. For the LearnHowToProgram.com lessons, we always assume the `uid` is `root` and the `pwd` is `epicodus`.

```json
{
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=project_name;uid=root;pwd=epicodus;"
  }
}
```

(Additional Instructions about how the app do.)

## Known Bugs:
Any known bugs here

## License:
Git your license, yo!

## One More Thing:
Don't forget to get a picture of your database relations layout with [SQL Designer](https://ondras.zarovi.cz/sql/demo/). Review as needed with [this epicodus lesson](https://www.learnhowtoprogram.com/c-and-net/database-basics/using-sql-designer)
