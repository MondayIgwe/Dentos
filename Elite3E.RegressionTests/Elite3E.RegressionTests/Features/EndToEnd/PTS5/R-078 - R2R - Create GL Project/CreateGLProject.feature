Feature: CreateGLProject

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/57669

@e2eft @CancelProcess
Scenario: 010 Create a gl project
	Given I search for process 'GL Project'
	And I click add
	When I want to add a gl project
		| GLProjectCode | Description | Project Number |
		| {Auto}+8      | {Auto}+15   | {Auto}+6       |
	Then I submit it
	And I validate submit was successful
	And I verify the gl project is created
