@us
Feature: CollectionControlPanel


@CancelProcess
Scenario:Verify Collection Control Panel
	Given  I search for process 'Collection Control Panel' without add button
	When I enter the value '12' in payor and billing office field 
	Then I verify the fields in payor office collection group link child form 
