using System;
using TechTalk.SpecFlow;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDb.Models;
using Microsoft.Data.Sqlite;
using WebServiceGame6813Team6.Controllers;
using ServiceDb.Data;
using WebServiceGame6813Team6.Services;

namespace WebServiceBDDTests.StepDefinitions
{
    [Binding]
    public class UsersStepDefinitions
    {
        private SqliteConnection _connection;
        private UsersController _usersController;

        public Task<ActionResult<IEnumerable<User>>> users;

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
            var service = new UserService(context);



            _usersController = new UsersController(context, service);
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

        [Given(@"we want to count users")]
        public void GivenWeWantToCountUsers()
        {
            Initalize();
        }

        [When(@"the Users endpoint is hit")]
        public void WhenTheUsersEndpointIsHit()
        {
            users = _usersController.GetUsers();
        }

        [Then(@"number of users should be > 0")]
        public void ThenNumberOfUsersShouldBe()
        {
            var usersCount = users.Result.Value.Count();

            usersCount.Should().BeGreaterThan(0);
        }
    }
}
