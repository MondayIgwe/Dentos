@ignore @us
Feature: BillingRule_OAC_Proforma

Billing Rule - OAC - warning message on submitting proforma (defect)
UDF records to be verified in Proforma Edit (defect)
#Defect - https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/67276

Scenario Outline: 005 Verify that a new  flag to the UDF List has been added
	Given I navigate to the UDF List process
	When I add a new UDF List and fill all the mandatory fields
		| Code   | Description   | UDF   |
		| <Code> | <Description> | <UDF> |
	And I add a new Attributes record providing the '<UDF>'
	And I submit it

Examples:
	| Code     | Description | UDF         |
	| {Auto}+6 | {Auto}+10   | Claim Value | 



Scenario Outline:010  Invoice Distribution Method setup
	Given I search or create invoice distribution method
	| Code      | Description        | DispatchOption |
	| Test_Auto | Test Auto Dispatch | Auto Dispatch  |


	Scenario Outline: 020 Create a new Matter
	Given I create a user with details
		| UserName | DataRoleAlias | UserRoleList                                                                               |
		| <User>   | Admin         | 0:AD:G:Common Authorisations,0:WC:P:Proforma Processor,0:WC:P:Proforma Processor - Finance |
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
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |
	And I view an existing matter
	When I edit the existing matter
		| InvoiceDistributionMethod | UDFValue |
		| Test Auto Dispatch        | 100.00   |
	Then I can submit the matter

Examples:
  | FeeEarnerName | User       | Client       | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount    |
  | FRD36 Pete    | FRD36 Pete | FRD36 Client | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Montreal      | James Matt | LAMMERS BARREL GROUP |

Scenario Outline: 030 Post Disbursement and Generate Proforma
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

@CancelProcess
Scenario Outline: 040 Verification on invoice generated
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	When I update the invoice type '<InvoiceType>'
	Then I submit it
	#And I verify the warning '<Message>' in proforma edit

Examples:
	| User       | InvoiceType | Message                                                                  |
	| FRD36 Pete | OAC         | Warning: Proformas with Invoice Type OAC cannot have Time or Cost cards. |

	
