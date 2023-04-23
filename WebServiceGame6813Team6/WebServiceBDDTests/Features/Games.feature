Feature: Games

A short summary of the feature

@games
Scenario: Get games
	Given we want to retrieve all games
	When the Games endpoint is hit
	Then number of games should be > 0
