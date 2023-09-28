@us
Feature: O-170 - O2C Volume discounts tiered or sums

Global volume discount instructions should be indicated in the billing instructions with thresholds and discount % to be applied.
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/82221
Step 73 to 87 are skipped due to not able to access 3E Proforma environment
Step 95 to 98, Need clarification regarding checkboxes to select for perfoming Combine opeartion to recalculate the cost
Fee details section 
Step 139 - The checkbox is set to read-only


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
	Then I create a submatter 1 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | BillingOffice | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | <ChargeTypeGroup>   | <CostTypeGroup>   | Office>       | <PayorName> |
	Then I create a new Payee with the Api
		| PayeeName            |
		| Payee atVoucherMaint |
Examples:
	| User       | FeeEarnerName | Currency        | Client           | Office  | OperationUnit              | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| Monday Joe | Monday Joe    | USD - US Dollar | MondayJoe Client | Chicago | Dentons United States, LLP | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |
	
	
Scenario Outline: 020 Create a Time Modify
	Given I submit a time modify
		| Time Type  | Hours   | Narrative   | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | Tax Code  |
		| <TimeType> | <Hours> | <Narrative> | <FeeEarnerName> | 7500     | 7500       | <Currency>   | <TaxCode> |
	When I submit the disbursement modify
		| Work Date | DisbursementType   | Work Currency | Work Amount | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <Currency>    | 5000        | {Auto}+36 | <TaxCode> |
	Then I validate the disbursement is posted with no errors

Examples:
	| TimeType | FeeEarnerName | TaxCode                          | Currency        | Hours | Narrative | DisbursementType              |
	| FEES     | Monday Joe    | US Output Domestic Standard Rate | USD - US Dollar | 1.00  | {Auto}+8  | Dentons United States Funding |

Scenario Outline: 030 Create Voucher from Voucher Maintenance
	Given I add a new voucher with voucher default information
		| Operation Unit  | Invoice Number | Invoice Date |
		| <OperationUnit> | v_             | {Today}      |
	And I add disbursement card details for voucher
		| Disbursement Type  | Narrative          | TaxCode   | InputTaxCode   | Voucher Amount |
		| <DisbursementType> | Automation Testing | <TaxCode> | <InputTaxCode> | 5000           |
	And I update it
	And I add input amount "<InputAmount>" in voucher tax card
	When I submit the voucher
	Then I verify the voucher is created

Examples:
	| OperationUnit              | DisbursementType              | TaxCode                          | InputTaxCode                    | InputAmount |
	| Dentons United States, LLP | Dentons United States Funding | US Output Domestic Standard Rate | US Input Domestic Standard Rate | 0           |

Scenario Outline: 040 Generate a Proforma
	Given I can generate the proforma
		| Description | Include Other Proformas | Invoice Date | Proforma Status |
		| {Auto}+36   | No                      | {Today}-1    | Draft           |

Scenario Outline: 050 Proxy as Finance User
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	When I add apply adjustment details
		| Percentage | Adjustment Method                                 |
		| -5         | Proportional by soft cost entries on the proforma |
	When I add apply adjustment details
		| Percentage | Adjustment Method                                 |
		| -2         | Proportional by soft cost entries on the proforma |
	Then I transfer the time cards to another matter
	Then I submit it
	And I cancel proxy
	

Examples:
	| User       |
	| Monday Joe |

Scenario Outline: 060 Create Volume Discount
	Given I create a volume discount
		| Code     | Description | Office  | ChargeType     | Increase Chargetype | EffectiveDate | CalculationMethod | Currency | TierAmount | DiscountPercent |
		| {Auto}+6 | {Auto}+12   | Chicago | Address change | Address change      | {Today}       | Billed            | USD      | 1000       | 5               |


Scenario Outline: 070 Add a new Billing Contact
	Given I create the Payer with Api
		| PayerName   | Entity      |
		| <PayerName> | Paragon Ltd |
	And I navigate to the matter maintenance process
	And I reopen a saved Matter
	When I add a Matter Payer
		| Start Date |
		| {Today}+2  |
	And I add a new Billing Contact info
		| ContactType   | FirstName | LastName | ContactName | Email     | NewEmail | Payer Name  |
		| <ContactType> | {Auto}+4  | {Auto}+5 | {Auto}+5    | {Auto}+15 | {Auto}+6 | <PayerName> |
	And I add volume discount group to the matter
	And I update it
	And I submit the form
	
Examples:
	| ContactType     | PayerName   |
	| BILLING_PRIMARY | Carla Bates |

@CancelProcess
Scenario: 080 Invoice Generation
	Given I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for billing
	And I bill it without printing
	And the invoice number is generated
	And I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to finance team when dispatch method not set
	Then I view the invoices

Scenario Outline: 090 Create a Receipt with Invoice
	Given I add a new receipt
		| Receipt Type  | Receipt Date | Document Number | Narrative |
		| <ReceiptType> | {Today}      | {Auto}+36       | {Auto}+36 |
	And change the operating unit "<OperatingUnit>"
	When add the invoice on the receipt
	And I receipt the total amount
	And update the receipt
	Then I can submit the receipt
	And I validate submit was successful

Examples:
	| ReceiptType | OperatingUnit   |
	| DUSHIUSD    | Dentons US, LLP |




