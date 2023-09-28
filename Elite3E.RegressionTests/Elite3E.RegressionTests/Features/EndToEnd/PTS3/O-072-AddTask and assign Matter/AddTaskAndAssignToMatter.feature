Feature: AddTaskAndAssignToMatter

A short summary of the feature
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/49121

Scenario Outline: 010 Create Matter
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	When I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |
	And I create a fee earner with details
		| EntityName  |
		| <FeeEarner> |
	Then I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |

@e2eft
Examples:
	| FeeEarner                           | Client                                 | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | PayorName |
	| London - Partner Full Interest Prem | London - Domestic Client - Head office | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | Desc_at_3e8glw7 | Desc_at_VowqCrR | James May |

Scenario Outline: 020 Create a time entry
	Given I search for process 'Time Entry'
	Then I click add
	When add the time entry
		| Fee Earner  | TimeType   | Hours | Narrative | Language   | Office   | Phase1   | Task1   | Activity1   | Matter    |
		| <FeeEarner> | <TimeType> | 0.05  | {Auto}+10 | <Language> | <Office> | <Phase1> | <Task1> | <Activity1> | 100000001 |
	Then I can post all the time entries
	And I validate the post all was successful
	Then I search for process 'Matter Group Enquiry' without add button
	When I search in matter group enquiry
		| SearchPhrase | SearchValue |
		| Matter       | 100000001   |
	And I submit it

@e2eft
Examples:
	| FeeEarner                           | TimeType       | Language | Office         | Narrative                 | Phase1         | Task1               | Activity1 |
	| London - Partner Full Interest Prem | FEES (Default) | English  | London (UKIME) | Automation test narrative | Administration | Case Administration | Research  |