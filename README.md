# Bracket Tracker
#### This is a self-directed Two-Day C# project that tracks wins and losses over multiple rounds.

#### By Thomas McDowell and Emma Gerigscott 

## Technologies Used:
* C#
* .NET 6.0
* MySql

## Description:
Bracket Tracker tracks wins and losses over multiple rounds. Currently, it will track wins and losses between Heads and Tails, but as we progress, we hope to include multiple teams and a Wins/Loss bracket that handles double-elimination, and determines a final winner. Rounds are advanced by navigating through action-links.  

## Setup/Installation Req's:



### Set Up and Run Project
1. Clone this repo.
2. Open the terminal and navigate to this project's production directory called "BracketTracker". 
3. Within the production directory "BracketTracker", create a new file called `appsettings.json`.
4. Within `appsettings.json`, put in the following code, replacing the `uid` and `pwd` values with your own username and password for MySQL. For the LearnHowToProgram.com lessons, we always assume the `uid` is `root` and the `pwd` is `epicodus`.

```json
{
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=bracket_tracker;uid=[YOUR SQL USERNAME];pwd=[YOUR SQL PASSWORD];"
  }
}
```

5. Run ```dotnet watch run``` to view the project in your web browser. Enter your computer password when prompted.

### Set up the Databases

In your terminal in the project directory (BracketTracker.Solution/BracketTracker), run ```dotnet ef database update```

## Known Bugs:
Any known bugs here

## License:
Git your license, yo!

