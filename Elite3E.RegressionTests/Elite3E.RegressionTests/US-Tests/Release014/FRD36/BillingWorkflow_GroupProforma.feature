@ignore @us
Feature: BillingWorkflow_GroupProforma

#Defect https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/69647

# Need to be revisited. Group Proforma workflow defect


	Scenario Outline: 020 Create a new Matter
	Given I create a user with details
		| UserName | DataRoleAlias | DefaultOperatingAlias   | UserRoleList                                                                               |
		| <User>   | Admin         | <DefaultOperatingAlias> | 0:AD:G:Common Authorisations,0:WC:P:Proforma Processor,0:WC:P:Proforma Processor - Finance |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I add a workflow user to a FeeEarner
	| User   | Name            |
	| <User> | <FeeEarnerName> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   | BillingGroupDescription   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> | <BillingGroupDescription> |
		

Examples:
  | FeeEarnerName | User       | Client       | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount    | PresentationCurrency | PresentationExchangeRate | BillingGroupDescription |
  | FRD36 Pete    | FRD36 Pete | FRD36 Client | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Montreal      | James Matt | LAMMERS BARREL GROUP | GBP                  | 1.00                     | US15800787-007467       |
	


Scenario Outline: 030 Post Disbursement and Generate Proforma
	Given I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	Then I can generate the proforma
		| Description | Include Other Proformas | Proforma Status | BillingGroup   |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           | <BillingGroup> |

Examples:
	| DisbursementType                   | WorkCurrency | WorkAmount | TaxCode                          | IncludeOtherProformas | BillingGroup      |
	| Data Storage & Costs - Anticipated | USD          | 5000       | US Output Domestic Standard Rate | No                    | US15800787-007467 |

@CancelProcess
Scenario Outline: 040 Verification on invoice generated
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	When I submit it
	And I cancel proxy
	And I bill the group proforma
	And the invoice number is generated
	And I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to the timekeeper
	And I cancel proxy
	Then I view the invoices

Examples:
	| User       |
	| FRD36 Pete |