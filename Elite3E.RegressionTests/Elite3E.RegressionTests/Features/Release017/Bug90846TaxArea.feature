Feature: To Tax Area do not validate Payor tax area if field left blank
 
Bug: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/90846

Scenario Outline: 010 Prepare required data
	Given I create a user with details
		| UserName | DataRoleAlias | UserRoleList                                                                               |
		| <User>   | Admin         | 0:AD:G:Common Authorisations,0:WC:P:Proforma Processor,0:WC:P:Proforma Processor - Finance |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	Then I add a workflow user to a FeeEarner
		| User   | Name            |
		| <User> | <FeeEarnerName> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	Then I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName   | FeeEarnerFullName |
		| <Client> | Fully Open | {Today}-20 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Standard | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> | <FeeEarnerName>   |
	Then I create a new Payee with the Api
		| PayeeName            |
		| Payee atVoucherMaint |

@ft @qa @training @staging @canada @europe @uk @singapore
Examples:
	| User           | FeeEarnerName  | Currency            | Client               | Office  | OperationUnit                  | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| TestMonday bug | TestMonday bug | GBP - British Pound | TestMondayBug Client | Default | Dentons UK and Middle East LLP | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |
	
@ft @qa @training @staging @canada @europe @uk @singapore
Scenario: 020 Navigate to matter maintenance
	Given I navigate to the matter maintenance process
	When I quick search by matter number
	And Verify the given fields are present
		| ToTaxArea     | FieldName   |
		| United States | To Tax Area |
	And I add a Matter Payer
		| Start Date |
		| {Today}+1  |
	Then I update it
	And I submit it
	
Scenario Outline: 030 Create a Time Modify
	Given I submit a time modify
		| Time Type  | Hours   | Narrative   | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | Tax Code  |
		| <TimeType> | <Hours> | <Narrative> | <FeeEarnerName> | 7500     | 7500       | <Currency>   | <TaxCode> |
	When I submit the disbursement modify
		| Work Date | DisbursementType   | Work Currency | Work Amount | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <Currency>    | 5000        | {Auto}+36 | <TaxCode> |

@ft @qa @training @staging @canada @europe @uk @singapore
Examples:
	| TimeType | FeeEarnerName  | TaxCode                    | Currency            | Hours | Narrative | DisbursementType    |
	| FEES     | TestMonday bug | UK Output Domestic Zero 0% | GBP - British Pound | 1.00  | {Auto}+8  | Advertising Charges |

Scenario Outline: 040 Generate a Proforma
	Given I can generate the proforma
		| Description | Include Other Proformas | Invoice Date | Proforma Status |
		| {Auto}+36   | No                      | {Today}-1    | Draft           |
	And I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	Then Verify the given fields are present
		| ToTaxArea     | FromTaxArea   | FieldName   |
		| United States | From Tax Area | To Tax Area |
	And I update it
	And I click the 'Recalc' button.
	And I verify value in the given input field is not present
		| ToTaxArea     | FieldName   |
		| United States | To Tax Area |
	And I click the 'Recalc' button.
	And I want to open the 'Charge Details' child form
	And I submit it
	And I cancel proxy
	
@ft @qa @training @staging @canada @europe @uk @singapore
Examples:
	| User           |
	| TestMonday bug |
