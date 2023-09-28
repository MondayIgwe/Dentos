@release8 @frd028 @AddBillingRulesValidationToAClient
Feature: AddBillingRulesValidationToAClient
 Create a Billing Rules Validation List Using API
 Craete Client Using API
 Add the billing rules validation list to The Clent And verify 
 And delete the validations from client

Scenario Outline: 010 Create a billing rule validation list
	Given I search or create a client
		| Entity Name  |
		| <ClientName> |
	And I search or create proforma time billing rule validation list
		| Code                                                    | Description                                                                    |
		| Proforma Time - Check the negative and positive amounts | Proforma Time - Check the corresponding negative and positive amounts of GWTMU |

@training @staging @canada @europe @uk @singapore @ft @qa
Examples:
	| ClientName                        | BillingRules                                            |
	| ClientWith BillingRulesValidation | Proforma Time - Check the negative and positive amounts |

@training @staging @canada @europe @uk @singapore @ft @qa
Scenario: 020 Add billing rules validation list to a client
	When I search for the client
	And Add billing rules validation list
		| BillingRulesValidationList                              |
		| Proforma Time - Check the negative and positive amounts |
	And I submit it

@training @staging @canada @europe @uk @singapore @ft @qa
Scenario: 030 Verify the client maintenance
	When I search for the client
	Then I verify the added billing rules validatiton list
	And I delete billing rules validation list
	And I submit it
