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
    public class ProfileTests
    {
        private SqliteConnection _connection;
        private ProfilesController _profilesController;
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

            _profilesController = new ProfilesController(context);
            _usersController = new UsersController(context);
        }

        [TestCleanup]
        public void Dispose()
        {
            _connection.Close();
        }


        // Genereate the user object for the next record to insert
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
              ProfileId= id,
              UserId= 0,
              Elo= 0,
              BehaviorIndex= 0,
              PrivacyBool= true,
            };


            return profile;
            }


        [TestMethod]
        public void GetAllProfiles()
        {
            var ProfilesCount = _profilesController.GetProfiles().Result.Value.Count();

            ProfilesCount.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void GetExistingProfile()
        {
            // Get the first existing user ID
            var existingProfileID = _profilesController.GetProfiles().Result.Value.ElementAt(0).ProfileId;

            var user = _profilesController.GetProfile(existingProfileID);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void GetNonExistingProfile()
        {
            var existingProfileID = -1;

            var user = _profilesController.GetProfile(existingProfileID);

            Assert.IsNull(user.Result.Value);
        }

        [TestMethod]
        public void ExistingProfileStatusCode()
        {
            // Get the first existing user ID
            var existingProfileID = _profilesController.GetProfiles().Result.Value.ElementAt(0).ProfileId;

            var profile = _profilesController.GetProfile(existingProfileID);
            var statusCode = profile.Result.Result;

            Assert.AreEqual(statusCode, null);
        }

        [TestMethod]
        public void NonExistingProfileStatusCode()
        {
            var notFoundStatus = typeof(NotFoundResult).Name;

            var existingProfileID = -1;
            var profile = _profilesController.GetProfile(existingProfileID);

            var statusCode = profile.Result.Result.GetType().Name;

            Assert.AreEqual(statusCode, notFoundStatus);
        }

        [TestMethod]
        public void PutAddedProfileFailure()
        {
            var putSuccessStatusCode = typeof(NotFoundResult).Name;

            var newProfile = GenerateNextProfileObject();

            var putResponse = _profilesController.PutProfile(newProfile.ProfileId, newProfile).Result.GetType().Name;

            Assert.AreEqual(putResponse, putSuccessStatusCode);
        }

        [TestMethod]
        public void PutModifiedPostSuccess()
        {
            var putSuccessStatusCode = typeof(NoContentResult).Name;

            var firstProfileId = 1;

            var modifiedProfile = GenerateNextProfileObject(firstProfileId);

            var putResponse = _profilesController.PutProfile(firstProfileId, modifiedProfile).Result.GetType().Name;

            Assert.AreEqual(putResponse, putSuccessStatusCode);
        }

        [TestMethod]
        public void PutProfileBadRequest()
        {
            var putBadRequestStatusCode = typeof(BadRequestResult).Name;

            var profile = GenerateNextProfileObject();
            var badProfileId = profile.ProfileId + 1;

            var putResponse = _profilesController.PutProfile(badProfileId, profile).Result.GetType().Name;

            Assert.AreEqual(putResponse, putBadRequestStatusCode);
        }

        [TestMethod]
        public void PostProfileSuccess()
        {
            var postSuccessStatusCode = typeof(CreatedAtActionResult).Name;

            var profile = GenerateNextProfileObject();

            var postResponse = _profilesController.PostProfile(profile).Result.Result.GetType().Name;

            Assert.AreEqual(postResponse, postSuccessStatusCode);
        }

        //[TestMethod]
        //public void PostUserConflict()
        //{
        //    var postConflictStatusCode = typeof(ConflictResult).Name;

        //    var firstProfileId = 1;

        //    var profile = GenerateNextProfileObject(firstProfileId);

        //    var postResponse = _profilesController.PostProfile(profile).Result.Result.GetType().Name;

        //    Assert.AreEqual(postResponse, postConflictStatusCode);
        //}

        [TestMethod]
        public void PostUserConflict()
        {
            var postConflictStatusCode = typeof(ConflictResult).Name;

            var firstProfileId = 1;

            var profile = GenerateNextProfileObject(firstProfileId);

            var postResponse = _profilesController.PostProfile(profile).Result.Result.GetType().Name;

            Assert.AreEqual(postResponse, postConflictStatusCode);
        }
    }
}