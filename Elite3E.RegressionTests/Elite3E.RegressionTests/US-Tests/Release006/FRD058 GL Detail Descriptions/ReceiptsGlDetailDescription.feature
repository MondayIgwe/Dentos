@us @ignore

#Defect https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/52567
Feature: ReceiptsGlDetailDescription


Scenario Outline: 010 Update Gl description
	Given I search for process 'GL Detail Description'
	And I advanced find and select
		| Search Column  | Search Operator  | Search Value  |
		| <SearchColumn> | <SearchOperator> | <SearchValue> |
	When I update both language '<Description>' and unit override '<Description>'


Examples:
	| SearchColumn        | SearchOperator | SearchValue | Description                                                                                  |
	| Query (Description) | Equals         | Paid Cost   | <Auto Paid Time> Receipt Type: @ReceiptType@, Receipt Date: @RcptDate@, Invoice: @InvNumber@ |

Scenario Outline: 020 Create Matter and Charge Entry
	Given I create a user with details						 
		| UserName | DataRoleAlias | UserRoleList                                                                                                                            |
		| <User>   | Admin         | 0:AD:G:Common Authorisations,0:WC:P:Proforma Processor,0:WC:P:Proforma Processor - Finance,0:WC:W:Proforma Billing Team[: Unit: Office] |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I add a workflow user to a FeeEarner
	| User   | Name            |
	| <User> | <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity            |
		| <PayorName> | WhiteWater Entity |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | <Department> | <Section> | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
	When I add a charge entry
		| Charge Type  | Amount | Tax Code  |
		| <ChargeType> | 300.00 | <TaxCode> |

Examples:
	| User                | Client              | FeeEarnerName       | ChargeType                                              | TaxCode                          | Office  | Currency        | Department | Section | PayorName    | DefaultOperatingAlias      |
	| Leena EarnerSurname | Leena ClientSurname | Leena EarnerSurname | Admin Charges (e.g. On-boarding fee, data storage etc.) | US Output Domestic Standard Rate | Chicago | USD - US Dollar | Default    | Default | Stacy Rogers | Dentons United States, LLP |

Scenario Outline: 030 Disbursement Entry
	When I submit the disbursement modify
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors

Examples:
	| DisbursementType              | WorkCurrency    | WorkAmount | TaxCode                          |
	| Dentons United States Funding | USD - US Dollar | 500        | US Output Domestic Standard Rate |


Scenario Outline: 040 Create a time modify
	When I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName | WorkRate | WorkCurrency | Tax Code  |
		| <TimeType> | 0.1   | {Auto}+10 | <FeeEarner>   | 1        | <Currency>   | <TaxCode> |
	And I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |


Examples:
	| FeeEarner           | TimeType | Hours | Narrative         | TaxCode                          | IncludeOtherProformas | Currency        |
	| Leena EarnerSurname | FEES     | 0.10  | test automation 1 | US Output Domestic Standard Rate | No                    | USD - US Dollar |


@CancelProcess
Scenario: 050 Create a Proforma Edit and generate Bill
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


Examples:
	| User                |
	| Leena EarnerSurname |


@CancelProcess
Scenario: 055 Validate Invoice GL has been posted
	When I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to finance team when dispatch method not set
	Given I search for process 'Invoices' without add button
	When I quick search by invoice number
	And I view the gl postings
	Then I validate gl postings status
	And I close the gl postings

Scenario Outline: 060 Enter receipt information
	When I add a new receipt
		| ReceiptType   | Receipt Date | Document Number | Narrative |
		| <ReceiptType> | {Today}      | {Auto}+36       | {Auto}+36 |
	And change the operating unit "<OperatingUnit>"
	And add the invoice on the receipt
	And I receipt the total amount
	Then I can submit the receipt

Examples:
	| ReceiptType | OperatingUnit   |
	| BANK_CHARGE | Dentons US, LLP |


Scenario: 070 verify the GL Detail Subledger enquiry for receipts
	Given I search for a process 'GL Detail Subledger enquiry' and select a chart 'GL Detail Subledger enquiry (GLDetailSubledgerInq)'
	When I create GL Detail Subledger 'Receipts' report
	Then I verify the receipt report description

