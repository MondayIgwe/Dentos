Feature: AddNewFeeEarnerNewofficerate

O-113 - O2C: New Fee Earner :New office rate (Dimensional Rate).
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/37689

Scenario Outline: 001 Create a new Entity Person
	Given the entity person process is opened
	When I enter the  person entity details
		| Entity Type | First Name | Last Name | Format Code | Relationship |
		| Employee    | {Auto}+7   | {Auto}+6  | Default     | Self         |
	And enter site details
		| Description | Site Type  | Country   | Street   | Language                 |
		| {Auto}+10   | <SiteType> | <Country> | <Street> | English (United Kingdom) |
	Then I can submit the new entity details
	And I validate submit was successful

@e2eft
Examples:
	| SiteType | Country        | Street                         |
	| Billing  | UNITED KINGDOM | 10 Umhlanga rocks drive,Durban |

Scenario Outline: 002 Create a new Fee Earner Maintenance
	When I view the view the fee earner maintenance
		| Local Language Name |
		| English             |
	And I add effective dated information
		| Office   | Title   |
		| <Office> | <Title> |
	And I add fee earner entry
		| Default Rates | DefaultCurrency |
		| 120           | GBP             |
	And I add Rate Details
	Then I submit the fee earner
	And I validate submit was successful

@e2eft
Examples:
	| Office      | Title    |
	| London (EU) | Director |


Scenario Outline: 003 Create Matter
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a new matter
	When I update the matter
		| Client                           | Status     | Open Date | Matter Name | Matter Currency Method | Statement Site | MatterType | MatterAttribute | Language |
		| Client_Automation Matter_HTOvMOn | Fully Open | {Today}   | {Auto}+10   | Bill                   | London UK site | Rodyk IP   | Rodyk IP Matter | English  |
	And I add a Matter Payer
		| Start Date |
		| {Today}+1  |
	And I update the effective dated information
		| Child Form                  | Office      |
		| Effective Dated Information | London (EU) |
	And I update the matter rates
		| Child Form   | Rate     |
		| Matter Rates | Standard |
	And I add a new cost type group
		| Cost Type Group |
		| Desc_at_VowqCrR |
	And I add new charge type group
		| Charge Type Group |
		| Desc_at_3e8glw7   |
	And I submit it
	Then verify the matter number is generated

@e2eft
Examples: 
	| PayorName   |
	| James Mayor |

@e2eft @CancelProcess
Scenario Outline: 004 Fee Earner Detail
	Given I navigate to Fee Earner Detail page
	And I input the FE number
	And I run the report
	Then I can verify that all the information is correct

Scenario Outline: 005 Update Fee Earner Display Name
	Given I navigate to Fee Earner page and I select an existing Fee Earner
	And I amend the applicable name and office
		| Office   | DisplayName   |
		| <Office> | <DisplayName> |
	And I submit it
	And I validate submit was successful

@e2eft
Examples:
	| Office   | DisplayName       |
	| Aberdeen | NewFeeEarner Name |

@e2eft
Scenario Outline: 006 Matter Global Change
	Given I navigate to the Matter Global Change process
	When I populate the required fields
	And I click the Calculate button
	Then I click the Preview button
	And I submit it

@e2eft @CancelProcess
Scenario Outline: 007 Matter DrillDown
	Given I navigate to the Matter DrillDown process
	Then I populate the Matter Number
	And I can run the report
	And I can verify that the original office and the updated office are displayed