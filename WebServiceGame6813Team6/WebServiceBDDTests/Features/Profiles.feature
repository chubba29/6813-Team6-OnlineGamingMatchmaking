Feature: Profiles

A short summary of the feature

@profiles
Scenario: Get profiles
	Given we want to retrieve all profiles
	When the Profiles endpoint is hit
	Then number of profiles should be >0
