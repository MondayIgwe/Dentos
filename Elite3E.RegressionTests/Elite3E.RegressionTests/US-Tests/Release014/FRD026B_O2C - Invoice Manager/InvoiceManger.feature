@us
Feature: InvoiceManger

A short summary of the feature
Bug https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/63371
and
Bug https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/63367/

Scenario Outline: 010 Create a new Matter
	Given I create a user with details
		| UserName | DataRoleAlias | DefaultOperatingAlias   |
		| <User>   | Admin         | <DefaultOperatingAlias> |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I add a workflow user to a FeeEarner
		| User   | Name            |
		| <User> | <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | LimeLight Ltd |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |


Examples:
	| User         | Client    | FeeEarnerName | Office         | ChargeType    | TaxCode                    | ChargeAmount | ChargeTypeGroup | Currency            | PayorName | DefaultOperatingAlias          |
	| Invoice User | Ann Smith | Ann Smith     | London (UKIME) | Miscellaneous | UK Output Domestic Zero 0% | 500.00       | at_BOA_Group    | GBP - British Pound | Lara Moon | Dentons UK and Middle East LLP |


Scenario Outline: 020 Create a proforma Edit
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |
	And I cancel the process

Examples:
	| DisbursementType    | WorkCurrency | WorkAmount | TaxCode                         | IncludeOtherProformas |
	| Advertising Charges | GBP          | 5000       | UK Output Domestic Standard 20% | No                    |



Scenario Outline: 030 Create a time entry/modify
	Given I submit a time modify
		| Time Type  | Hours   | Narrative   | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | Tax Code  |
		| <TimeType> | <Hours> | <Narrative> | <FeeEarnerName> | 1        | 1          | <Currency>   | <TaxCode> |


Examples:
	| FeeEarnerName     | TimeType          | Hours | Narrative         | TaxCode                    | Currency            |
	| Doctor FeeEarner1 | Fixed-Capped Fees | 1     | test automation 1 | UK Output Domestic Zero 0% | GBP - British Pound |


Scenario: 031 Proforma Edit
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	Then I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for billing
	And I bill it without printing
	And the invoice number is generated
	And remove a fee earner '<User>' from the user

Examples:
	| User         |
	| Invoice User |

@CancelProcess
Scenario: 032 Validate Invoice GL has been posted
	Given I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to finance team when dispatch method not set
	When I view the invoices
	Then I cancel it

@CancelProcess
Scenario: 035 Start Notification task
	Given I search for 'Notification Start Task'
	And I start a notification task '<NotificationTask>'


Examples:
	| NotificationTask |
	| 2_Collections    |


@CancelProcess
Scenario: 036 Validate Invoice has been successfully added
	Given I search for process 'Invoices' without add button
	When I quick search by invoice number
	Then I cancel it



Scenario: 040 I want to search results without entering information and see pop up message
	Given I search for process 'Invoice Manager' without add button
	Then I want to search for results
	And I want to see results with warning message
	Then I close the process

		 
@CancelProcess
Scenario: 060 Confirm how many Invoices client has
	Given I search for process 'Invoices' without add button
	Then I get total number of invoices using client
		| Index Number | Search Criteria |
		| 12           | Equals          |

@CancelProcess
Scenario: 070 I want to view Client and Matter notes using Invoice Manager Client
	Given I search for process 'Invoice Manager' without add button
	When I search the results using 'Client' details
	Then I want to see all outstanding invoices for the client
	And I close the process