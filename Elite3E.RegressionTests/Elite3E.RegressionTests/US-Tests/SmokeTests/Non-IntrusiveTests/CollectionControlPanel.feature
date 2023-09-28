@us
Feature: CollectionControlPanel

@CancelProcess
Scenario:Verify Collection Control Panel
	Given  I search for process 'Collection Control Panel' without add button
	Then I verify the sections in collection control panel