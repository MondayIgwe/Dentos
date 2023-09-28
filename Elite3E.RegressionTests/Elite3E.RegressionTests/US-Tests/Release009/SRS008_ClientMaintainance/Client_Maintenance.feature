@us
Feature: Client_Maintenance

A short summary of the feature

@CancelProcess
Scenario Outline: 010 Create a Client
	Given I search or create a client with fee earner
		| Entity Name  | FeeEarnerFullName |
		| <ClientName> | <FeeEarnerName>   |

Examples:
	| ClientName                 | BillingRules                                            | FeeEarnerName           |
	| FRD008Client ClientSurname | Proforma Time - Check the negative and positive amounts | FRD008Fee EarnerSurname |

@CancelProcess
Scenario Outline: 020 I want to check the "Global Client Reference" field is available and editable in Client Maintenance.
	When I search for the client
	And edit the client details
		| Global Client Number |
		| Ab 123 $. 0.00       |
	And I submit it
	Then I search for the client
	And I verify that the 'Global Client Number' field is correctly edited

@CancelProcess
Scenario Outline: 030 I want to check the field is available and editable in Client Maintenance.
	When I search for the client
	Then I update the 'Client and eBilling Additional Information' child forms with the details:
	And I submit it
	Then I search for the client
	And I verify that the 'Billing Accrual Update Required?' field is correctly edited

@CancelProcess
Scenario Outline: 040 I want to check the "Credit Details" fields are available and editable in Client Maintenance.
	When I search for the client
	Then I update the 'Credit Details' child forms with the details:
		| Risk Score | Credit Score Rating | Credit Limit | Currency   | AML Risk | Finance Risk Score |
		| 45.30      | 23.80               | 3,456.89     | <Currency> | 34.56    | 123.45             |
	And I submit it
	Then I search for the client
	And I verify that the 'Credit Details' field is correctly edited
	And I cancel it


Examples:
	| Currency        |
	| USD - US Dollar |


@CancelProcess
Scenario Outline: 050 I want to check the Client Defaults fields are available in Client Maintenance.
	When I search for the client
	Then I update the 'Client Defaults' child forms with the details:
		| Office   | Department   | PTA Fees 1      | PTA Cost 1      | PTA Charge 1      | PTA Fees 2      | PTA Cost 2      | PTA Charge 2      |
		| <Office> | <Department> | <PTAGroupFees1> | <PTAGroupDisb1> | <PTAGroupCharge1> | <PTAGroupFees2> | <PTAGroupDisb2> | <PTAGroupCharge2> |
	And I add another 'Client Defaults' child forms with the details:
		| Office          | Department          | PTA Fees 1      | PTA Cost 1      | PTA Charge 1      | PTA Fees 2      | PTA Cost 2      | PTA Charge 2      |
		| <UpdatedOffice> | <UpdatedDepartment> | <PTAGroupFees1> | <PTAGroupDisb1> | <PTAGroupCharge1> | <PTAGroupFees2> | <PTAGroupDisb2> | <PTAGroupCharge2> |
	And I submit it
	Then I search for the client
	And I verify that the 'Client Defaults' field is correctly edited


Examples:
	| UpdatedOffice | Office  | Department | PTAGroupFees1                     | PTAGroupDisb1                     | PTAGroupCharge1                   | PTAGroupFees2                    | PTAGroupDisb2                     | PTAGroupCharge2                   | UpdatedDepartment |
	| Oakland       | Chicago | Default    | BANKT Code Set (Task is required) | BANKR Code Set (Task is required) | BAYER Code Set (Task is required) | AQUA Code Set (Task is required) | AQUAT Code Set (Task is required) | ALERI Code Set (Task is required) | Corporate         |

@CancelProcess
Scenario Outline: 060 I want to create a duplicate in Client Maintenance.
	When I search for the client
	Then I update the 'Client Defaults' child forms with the details:
		| Office   | Department   | PTA Fees 1      | PTA Cost 1      | PTA Charge 1      | PTA Fees 2      | PTA Cost 2      | PTA Charge 2      |
		| <Office> | <Department> | <PTAGroupFees1> | <PTAGroupDisb1> | <PTAGroupCharge1> | <PTAGroupFees2> | <PTAGroupDisb2> | <PTAGroupCharge2> |
	And I add another 'Client Defaults' child forms with the details:
		| Office          | Department   | PTA Fees 1      | PTA Cost 1      | PTA Charge 1      | PTA Fees 2      | PTA Cost 2      | PTA Charge 2      |
		| <UpdatedOffice> | <Department> | <PTAGroupFees1> | <PTAGroupDisb1> | <PTAGroupCharge1> | <PTAGroupFees2> | <PTAGroupDisb2> | <PTAGroupCharge2> |
	And I submit it
	Then I verify that an error message 'Cannot have duplicate Office and Department combination' is generated
	Then I search for the client
	Then delete the client
	And I submit it


Examples:
	| UpdatedOffice | Office  | Department | PTAGroupFees1                     | PTAGroupDisb1                     | PTAGroupCharge1                   | PTAGroupFees2                    | PTAGroupDisb2                     | PTAGroupCharge2                   |
	| Dallas        | Chicago | Default    | BANKT Code Set (Task is required) | BANKR Code Set (Task is required) | BAYER Code Set (Task is required) | AQUA Code Set (Task is required) | AQUAT Code Set (Task is required) | ALERI Code Set (Task is required) |