@us
Feature: VerifyCustomItemLanguageField


@CancelProcess
Scenario: 010 Verify custom item language field
	Given I search for process 'Collection Items' without add button
	Then I verify the language field in advanced find
		| Search Column                     | Search Operator | Search Value |
		| Collection Language (Description) | Equals          | English      |

@CancelProcess
Scenario: 020 Verify that the custom field Collection Language is available
	Given I search for process 'Collection Items' without add button
	And I select an existing client in collection item
		| Client                         |
		| Dentons UK and Middle East LLP |
	Then I verify the custom field collection language exists on the main form
	And I verify the custom field collection language exists on the child form colelction step
	And I verify that the collection office and the collector inputs are required fields
		| Error Message                                                   |
		| Collection Office: Assign required attribute before continuing. |
	And I verify stock office field is read only
	And I verify the email override body field exists
