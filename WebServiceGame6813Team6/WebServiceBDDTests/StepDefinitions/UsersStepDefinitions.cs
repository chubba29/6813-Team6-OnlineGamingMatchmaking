using System;
using TechTalk.SpecFlow;

namespace WebServiceBDDTests.StepDefinitions
{
    [Binding]
    public class UsersStepDefinitions
    {
        [Given(@"users exist")]
        public void GivenUsersExist()
        {
            throw new PendingStepException();
        }

        [When(@"the Users endpoint is hit")]
        public void WhenTheUsersEndpointIsHit()
        {
            throw new PendingStepException();
        }

        [Then(@"number of users should be > 0")]
        public void ThenNumberOfUsersShouldBe(int p0)
        {
            throw new PendingStepException();
        }
    }
}
