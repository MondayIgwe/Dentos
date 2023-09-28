Feature: AddNewFeeEarner

O-112 - O2C: Add new Fee Earner
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/36978

Scenario Outline: 001 Create a new Entity Person
	Given the entity person process is opened
	When I enter the  person entity details
		| Entity Type | First Name | Last Name | Format Code | Relationship |
		| Employee    | {Auto}+7   | {Auto}+6  | Default     | Self         |
	And enter site details
		| Description | Site Type  | Country   | Street   | Language |
		| {Auto}+10   | <SiteType> | <Country> | <Street> | Xhosa    |
	Then I can submit the new entity details
	And I validate submit was successful

@e2eft
Examples:
	| SiteType | Country      | Street                         |
	| Billing  | SOUTH AFRICA | 10 Umhlanga rocks drive,Durban |

Scenario Outline: 002 Create a new Fee Earner Maintenance
	When I view the view the fee earner maintenance
		| Local Language Name |
		| English             |
	And I add effective dated information
		| Office   | Title   |
		| <Office> | <Title> |
	And I add fee earner entry
		| Default Rates |
		| 100.0000      |
	Then I submit the fee earner
	And I validate submit was successful

@e2eft
Examples:
	| Office   | Title    |
	| Aberdeen | Director |

Scenario Outline: 003 Create a new Vendor Maintenance
	Given the vendor maintenace process is opened
	When I select the new entity
	And I can verify that there are no duplicates
	And set the global vendor "<GlobalVendor>"
	Then I can submit the new vendor details
	And I validate submit was successful

@e2eft
Examples:
	| GlobalVendor       |
	| Amex Global Vendor |

Scenario Outline: 004 Create a new Payee Maintenance
	Given I add the payee from payee maintenance
		| Payment Terms  | Office   | Site   | PayeeType   |
		| <PaymentTerms> | <Office> | <Site> | <PayeeType> |
	And I update it
	Then I submit it
	And I validate submit was successful

@e2eft
Examples:
	| PaymentTerms | Office   | Site                                           | PayeeType |
	| Immediate    | Aberdeen | London - Domestic - Non Vatable Local Currency | Vendors   |

@CancelProcess @e2eft
Scenario Outline: 005 Create a Change Request to HR
	Given I view the fee earner
	And I verify that the timekeeper is assigned to the correct entity
	And I amend the applicable name and office
		| DisplayName     | Office |
		| FeeEarner First |        |
	And I submit it
	And I validate submit was successful

Scenario Outline: 006 Generate a GetRate Test Message log
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	Given I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | Rate          | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-30 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Standard Rate | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
	When I navigate to the GetRate Test process
	And I fill in the GetRate Test required fields
		| TransactionType | WorkDate   |
		| Fees            | {Today}-30 |
	And I click Get Rate button
	And I verify that all information is correct following the HR change request
		| Department | Section | RateClass | Work Date  |
		| Default    | Default | Default   | {Today}-30 |

@CancelProcess @e2eft
Examples:
	| Client       | FeeEarnerName        | Office         | Currency | TimeType       | PayorName   |
	| Notes Client | ClosingMat FeeEarner | London (UKIME) | GBP      | FEES (Default) | James Mayer |

