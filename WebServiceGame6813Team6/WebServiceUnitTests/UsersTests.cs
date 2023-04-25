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
using WebServiceGame6813Team6.Services;

namespace WebServiceUnitTests
{
    [TestClass]
    public class UsersTests
    {
        private SqliteConnection _connection;
        private UsersController _usersController;

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
            var service = new UserService(context);


            _usersController = new UsersController(context, service);
        }

        [TestCleanup]
        public void Dispose()
        {
            _connection.Close();
        }


        // Genereate the user object for the next record to insert
        // Assumes auto-incrementing DB field "Id"
        private User GenerateNextUserObject(long id = -1)
        {

            // id = -1 indicates no id was provided, so generate the next available ID from the database
            if (id == -1)
            {
                var highestId = _usersController.GetUsers().Result.Value.Max(user => user.Id);
                id = highestId + 1;
            }


            var user = new User()
            {
                Id = id,
                Password = "password",
                Username = $"Test-PutUserSuccess-{id}"
            };

            return user;
        }


        [TestMethod]
        public void GetAllUsers()
        {
            var usersCount = _usersController.GetUsers().Result.Value.Count();

            usersCount.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void GetExistingUser()
        {
            // Get the first existing user ID
            var existingUserID = _usersController.GetUsers().Result.Value.ElementAt(0).Id;

            var user = _usersController.GetUser(existingUserID);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void GetNonExistingUser()
        {
            var existingUserID = -1;

            var user = _usersController.GetUser(existingUserID);

            Assert.IsNull(user.Result.Value);
        }

        [TestMethod]
        public void ExistingUserStatusCode()
        {
            // Get the first existing user ID
            var existingUserID = _usersController.GetUsers().Result.Value.ElementAt(0).Id;

            var user = _usersController.GetUser(existingUserID);
            var statusCode = user.Result.Result;

            Assert.AreEqual(statusCode, null);
        }

        [TestMethod]
        public void NonExistingUserStatusCode()
        {
            var notFoundStatus = typeof(NotFoundResult).Name;

            var existingUserID = -1;
            var user = _usersController.GetUser(existingUserID);

            var statusCode = user.Result.Result.GetType().Name;

            Assert.AreEqual(statusCode, notFoundStatus);
        }

        [TestMethod]
        public void GetAddedUserSuccess()
        {
            var usersCount = _usersController.GetUsers().Result.Value.Count();

            usersCount.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void PutModifiedUserSuccess()
        {
            var putSuccessStatusCode = typeof(NoContentResult).Name;

            var firstUserId = 1;

            var modifiedUser = GenerateNextUserObject(firstUserId);

            var putResponse = _usersController.PutUser(firstUserId, modifiedUser).Result.GetType().Name;

            Assert.AreEqual(putResponse, putSuccessStatusCode);
        }

        [TestMethod]
        public void PutUserBadRequest()
        {
            var putBadRequestStatusCode = typeof(BadRequestResult).Name;

            var user = GenerateNextUserObject();
            var badUserId = user.Id + 1;

            var putResponse = _usersController.PutUser(badUserId, user).Result.GetType().Name;

            Assert.AreEqual(putResponse, putBadRequestStatusCode);
        }

        [TestMethod]
        public void GetUserSuccess()
        {
            var usersCount = _usersController.GetUsers().Result.Value.Count();

            usersCount.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void PostUserConflict()
        {
            var postConflictStatusCode = typeof(ConflictResult).Name;

            var firstUserId = 1;

            var user = GenerateNextUserObject(firstUserId);

            var postResponse = _usersController.PostUser(user).Result.Result.GetType().Name;

            Assert.AreEqual(postResponse, postConflictStatusCode);
        }
    }
}