using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDb.Models;
using System.Collections.Generic;
using WebServiceGame6813Team6.Controllers;
using ServiceDb.Data;
using ServiceDb.Models;
using Microsoft.Extensions.Options;
using SQLitePCL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using System.IO;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;

namespace WebServiceUnitTests
{
    [TestClass]
    public class GamesTests
    {
        private SqliteConnection _connection;
        private GamesController _gamesController;

        // Set up connection to our ServiceDb before each test
        [TestInitialize]
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

        [TestCleanup]
        public void Dispose()
        {
            _connection.Close();
        }


        // Genereate the Game object for the next record to insert
        // Assumes auto-incrementing DB field "Id"
        private Game GenerateNextGameObject(long id = -1)
        {

            //id = -1 indicates no id was provided, so generate the next available ID from the database
            //id = -1 indicates no id was provided, so generate the next available ID from the database
            if (id == -1)
            {

                var highestGameId = _gamesController.GetGames().Result.Value.Max(game => game.GameId);
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


        [TestMethod]
        public void GetAllGames()
        {
            var GamesCount = _gamesController.GetGames().Result.Value.Count();

            GamesCount.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void GetExistingGame()
        {
            // Get the first existing game ID
            var existingGameID = _gamesController.GetGames().Result.Value.ElementAt(0).GameId;

            var game = _gamesController.GetGame(existingGameID);

            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void GetNonExistingGame()
        {
            var existingGameID = -1;

            var game = _gamesController.GetGame(existingGameID);

            Assert.IsNull(game.Result.Value);
        }

        [TestMethod]
        public void ExistingGameStatusCode()
        {
            // Get the first existing Game ID
            var existingGameID = _gamesController.GetGames().Result.Value.ElementAt(0).GameId;

            var game = _gamesController.GetGame(existingGameID);
            var statusCode = game.Result.Result;

            Assert.AreEqual(statusCode, null);
        }

        [TestMethod]
        public void NonExistingGameStatusCode()
        {
            var notFoundStatus = typeof(NotFoundResult).Name;

            var existingGameID = -1;
            var game = _gamesController.GetGame(existingGameID);

            var statusCode = game.Result.Result.GetType().Name;

            Assert.AreEqual(statusCode, notFoundStatus);
        }

        [TestMethod]
        public void PutAddedGameFailure()
        {
            var putSuccessStatusCode = typeof(NotFoundResult).Name;

            var newGame = GenerateNextGameObject();

            var putResponse = _gamesController.PutGame(newGame.GameId, newGame).Result.GetType().Name;

            Assert.AreEqual(putResponse, putSuccessStatusCode);
        }

        [TestMethod]
        public void PutModifiedPostSuccess()
        {
            var putSuccessStatusCode = typeof(NoContentResult).Name;

            var firstGameId = 1;

            var modifiedGame = GenerateNextGameObject(firstGameId);

            var putResponse = _gamesController.PutGame(firstGameId, modifiedGame).Result.GetType().Name;

            Assert.AreEqual(putResponse, putSuccessStatusCode);
        }

        [TestMethod]
        public void PutGameBadRequest()
        {
            var putBadRequestStatusCode = typeof(BadRequestResult).Name;

            var game = GenerateNextGameObject();
            var badGameId = game.GameId + 1;

            var putResponse = _gamesController.PutGame(badGameId, game).Result.GetType().Name;

            Assert.AreEqual(putResponse, putBadRequestStatusCode);
        }

        [TestMethod]
        public void GetGameSuccess()
        {
            var GamesCount = _gamesController.GetGames().Result.Value.Count();

            GamesCount.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void PostGameConflict()
        {
            var postConflictStatusCode = typeof(ConflictResult).Name;

            var firstGameId = 1;

            var game = GenerateNextGameObject(firstGameId);

            var postResponse = _gamesController.PostGame(game).Result.Result.GetType().Name;

            Assert.AreEqual(postResponse, postConflictStatusCode);
        }
    }
}