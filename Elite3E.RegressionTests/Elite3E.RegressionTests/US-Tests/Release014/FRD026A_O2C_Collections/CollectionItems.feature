@us
Feature: CollectionItems

Defect: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/69247

Scenario Outline: 010 Create a new Matter
	Given there exists a fiscal invoice setup
		| Unit   | Unit Description  |
		| <Unit> | <UnitDescription> |
	And I create a user with details
		| UserName | DataRoleAlias | 
		| <User>   | Admin         | 
	Then I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I add a workflow user to a FeeEarner
    | User   | Name            |
    | <User> | <FeeEarnerName> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | LimeLight Ltd |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |

Examples:
	| DefaultOperatingAlias        | FeeEarnerName | User      | Client       | Unit | UnitDescription                         | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName |
	| Dentons Rodyk & Davidson LLP | Leo Peter     | Leo Peter | LeoPeter Ltd | 3511 | Dentons Europe Studio Legale Tributario | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | Auto_IncludeALL | Auto_IncludeALL | Milan         | James May |


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
	| FeeEarnerName | TimeType          | Hours | Narrative         | TaxCode                    | Currency            |
	| Dave Peter    | Fixed-Capped Fees | 1     | test automation 1 | UK Output Domestic Zero 0% | GBP - British Pound |


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
	| User       |
	|  Leo Peter |

@CancelProcess
Scenario: 035 Validate Invoice GL has been posted
	Given I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to finance team when dispatch method not set
	When I view the invoices



Scenario: 040 Validate if Include Invoice Attachment check exists in Collection items
	Given I search for process 'Collection Items' without add button
	When search for a payer
	And I select payer
	Then I validate 'Include Invoice Attachment' checkbox is available
	And validate if Include Invoice Attachment checkbox check box is editable
	And I cancel it


Scenario: 050 Validate if Include Invoice Attachment check exists in Collection items using matter
	Given I search for process 'Collection Items' without add button
	When I search for the matter
	Then I validate 'Include Invoice Attachment' checkbox is available
	And validate if Include Invoice Attachment checkbox check box is editable
	And I cancel it


Scenario: 060 Validate if Include Invoice Attachment check exists in Collection items using invoice
	Given I search for process 'Collection Items' without add button
	When I quick search by invoice number
	Then I validate 'Include Invoice Attachment' checkbox is available
	And validate if Include Invoice Attachment checkbox check box is editable
	And I cancel it