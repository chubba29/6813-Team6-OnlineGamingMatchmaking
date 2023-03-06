Feature: Users

BDD tests related to Users interactions

@getusers
Scenario: Get all users
	Given users are requested
	When the "Get List of Users" button is clicked
	Then the list of users is returned
