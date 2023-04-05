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
    public class ProfilesStepDefinitions
    {
        private SqliteConnection _connection;
        private ProfilesController _profilesController;

        public Task<ActionResult<IEnumerable<Profile>>> profiles;

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

            _profilesController = new ProfilesController(context);
        }

        // Generate the profile object for the next record to insert
        // Assumes auto-incrementing DB field "Id"

        private Profile GenerateNextProfileObject(long id = -1)
        {

            //id = -1 indicates no id was provided, so generate the next available ID from the database
            //id = -1 indicates no id was provided, so generate the next available ID from the database
            if (id == -1)
            {

                var highestProfileId = _profilesController.GetProfiles().Result.Value.Max(user => user.ProfileId);
                id = highestProfileId + 1;
            }


            var profile = new Profile()
            {
                ProfileId = id,
                UserId = 0,
                Elo = 0,
                BehaviorIndex = 0,
                PrivacyBool = true,
            };


            return profile;
        }









        [Given(@"we want to retrieve all profiles")]
        public void GivenWeWantToRetrieveAllProfiles()
        {
            Initalize();
        }

        [When(@"the Profiles endpoint is hit")]
        public void WhenTheProfilesEndpointIsHit()
        {
            profiles = _profilesController.GetProfiles();
        }

        [Then(@"number of profiles should be >(.*)")]
        public void ThenNumberOfProfilesShouldBe(int p0)
        {
            var profilesCount = profiles.Result.Value.Count();

            profilesCount.Should().BeGreaterThan(0);
        }
    }
}
