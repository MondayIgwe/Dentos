@us
Feature: PEPurgeAniticipatedCost
	Purge Cost Cards from the Proforma Edit process


Scenario Outline: 020 Create a new Matter
	Given I create a user with details
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
		| PayerName   | Entity          |
		| <PayorName> | SafeHarbour Ltd |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |


Examples:
	| User       | Client         | FeeEarnerName | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName   |
	| Sticks Sam | Sticks Mandela | Sticks Sam    | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_PD_CHGroup | Auto_PD_CTGroup | Chicago       | Melisa Tate |

Scenario Outline: 030 Create a Disbursement and Generate Proforma
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |

Examples:
	| DisbursementType                   | WorkCurrency | WorkAmount | TaxCode                          | IncludeOtherProformas |
	| Data Storage & Costs - Anticipated | USD          | 5000       | US Output Domestic Standard Rate | No                    |

#Defect #https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/69247
#Below scenarios are ignored as well 
Scenario Outline: 040 Create a Proforma Edit, add a disbursement and generate Bill
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	When I open the proforma workflow task
	And I open the proforma for submission
	Then add the disbursement on the proforma edit
		| Work Date | Disbursement Type  | Anticipated   | Reference Currency  | Work Amount  | Tax Code  | Narrative | Reason     |
		| {Today}-1 | <DisbursementType> | <Anticipated> | <ReferenceCurrency> | <WorkAmount> | <TaxCode> | {Auto}+36 | Correction |
	And I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for billing
	And I bill it without printing
	Then the invoice number is generated
	Given I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	When I send the invoice routed to finance team when dispatch method not set

Examples:
	| User       | DisbursementType                   | Anticipated | ReferenceCurrency | WorkAmount | TaxCode                          |
	| Sticks Sam | Data Storage & Costs - Anticipated | Yes         | USD               | 5001       | US Output Domestic Standard Rate |

Scenario Outline: 050 New proforma Generation
	Given I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |
	And I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	When I open the proforma workflow task
	And I open the proforma for submission
	Then a negative entry is generated
	And I cancel it
	And I cancel proxy

Examples:
	| User       | IncludeOtherProformas |
	| Sticks Sam | No                    |


Scenario Outline: 060 Purge disbursement and update without permission
	Given I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	And select and purge the disbursement on proforma edit
	When I update the proforma
	Then an error message "<ErrorMessage>" is displayed

Examples:
	| User          | ErrorMessage                                            |
	| Proforma User | Current user is not allowed to purge anticipated costs. |


Scenario: 070 cancel the process
	Given I cancel the process
	 

Scenario: 080 Update disbursement with permission
	Given I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	And select and purge the disbursement on proforma edit
	When I update the proforma
	Then no errors are displayed


Examples:
	| User       |
	| Sticks Sam |


