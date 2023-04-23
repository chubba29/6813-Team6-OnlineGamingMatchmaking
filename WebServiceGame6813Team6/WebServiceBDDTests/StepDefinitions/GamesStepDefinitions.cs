using System;
using TechTalk.SpecFlow;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDb.Models;
using Microsoft.Data.Sqlite;
using WebServiceGame6813Team6.Controllers;
using ServiceDb.Data;

namespace WebServiceBDDTests.StepDefinitions
{
    [Binding]
    public class GamesStepDefinitions
    {
        private SqliteConnection _connection;
        private GamesController _gamesController;

        public Task<ActionResult<IEnumerable<Game>>> games;

        // Set up connection to our ServiceDb before each test
        public void Initalize()
        {
            // Get the path to the ServiceDB.db file
            var projectBaseDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            var serviceDBPath = $"Data Source = {projectBaseDirectory}/WebServiceGame6813Team6/ServiceDb.db";

            // Initialize ServiceDB connection
            _connection = new SqliteConnection(serviceDBPath);
            _connection.Open();

            // Set up DB Context options
            var contextOptions = new DbContextOptionsBuilder<ServiceDbContext>()
                .UseSqlite(_connection)
                .Options;

            // Get the DB Context
            var context = new ServiceDbContext(contextOptions);

            _gamesController = new GamesController(context);
        }

        // Generate the game object for the next record to insert
        // Assumes auto-incrementing DB field "Id"

        private Game GenerateNextGameObject(long id = -1)
        {

            //id = -1 indicates no id was provided, so generate the next available ID from the database
            //id = -1 indicates no id was provided, so generate the next available ID from the database
            if (id == -1)
            {

                var highestGameId = _gamesController.GetGames().Result.Value.Max(user => user.GameId);
                id = highestGameId + 1;
            }


            var game = new Game()
            {
                GameId = id,
                Name = $"Automated Test {id.ToString()}",
                Type = "Not a real game. Just for testing."
            };


            return game;
        }




        [Given(@"we want to retrieve all games")]
        public void GivenWeWantToRetrieveAllGames()
        {
            Initalize();
        }

        [When(@"the Games endpoint is hit")]
        public void WhenTheGamesEndpointIsHit()
        {
            games = _gamesController.GetGames();
        }

        [Then(@"number of games should be >(.*)")]
        public void ThenNumberOfGamesShouldBe(int p0)
        {
            var gamesCount = games.Result.Value.Count();

            gamesCount.Should().BeGreaterThan(0);
        }
    }
}
