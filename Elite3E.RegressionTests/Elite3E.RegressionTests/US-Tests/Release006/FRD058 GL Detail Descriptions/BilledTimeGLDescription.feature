@us
Feature: BilledTimeGLDescription


Scenario Outline: 010 Update Gl description
	Given I search for process 'GL Detail Description'
	And I advanced find and select
		| Search Column  | Search Operator  | Search Value  |
		| <SearchColumn> | <SearchOperator> | <SearchValue> |
	When I update both language '<Description>' and unit override '<Description>'


Examples:
	| SearchColumn        | SearchOperator | SearchValue | Description                                                                                 |
	| Query (Description) | Equals         | Billed Time | <Auto Billed Time> Invoice: @InvNumber@, Matter: @MatterNumber@, TimeKeeperName: @TkprName@ |

Scenario Outline: 020 create matter
	Given I create a user with details						 
		| UserName | DataRoleAlias | UserRoleList                                                                               |
		| <User>   | Admin         | 0:AD:G:Common Authorisations,0:WC:P:Proforma Processor,0:WC:P:Proforma Processor - Finance |
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
		| Client       | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <ClientName> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | <Department> | <Section> | Standard | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
	When I create a charge type with details
		| ChargeCode | Description  | CategoryInput | TransactionTypeAlias | Active |
		| {Auto}+10  | <ChargeType> | Other         | Miscellaneous Income | Yes    |
	When I add a charge entry
		| Charge Type  | Amount | Tax Code  |
		| <ChargeType> | 300.00 | <TaxCode> |

@us
Examples:
	| User                | ClientName          | FeeEarnerName       | ChargeType    | TaxCode                          | Office  | Currency        | Department | Section | PayorName    | DefaultOperatingAlias      |
	| Leena EarnerSurname | Leena ClientSurname | Leena EarnerSurname | Miscellaneous | US Output Domestic Standard Rate | Chicago | USD - US Dollar | Default    | Default | Stacy Rogers | Dentons United States, LLP |

Scenario Outline: 030 Disbursement Entry
	Given I create a hard cost disbursement type with details
		| Code               | Description        |
		| <DisbursementType> | <DisbursementType> |
	When I submit the disbursement modify
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors


Examples:
	| DisbursementType              | WorkCurrency | WorkAmount | TaxCode                          |
	| Dentons United States Funding | USD          | 500        | US Output Domestic Standard Rate |


Scenario Outline: 040 Create a time modify
	When I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName   | WorkRate | WorkCurrency | Tax Code  |
		| <TimeType> | 0.1   | {Auto}+10 | <FeeEarnerName> | 100      | <Currency>   | <TaxCode> |
	And I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | <IncludeOtherProformas> | Draft           |

Examples:
	| TimeType          | Narrative         | TaxCode                          | IncludeOtherProformas | FeeEarnerName       | Currency        |
	| Fixed-Capped Fees | test automation 1 | US Output Domestic Standard Rate | No                    | Leena EarnerSurname | USD - US Dollar |

	   
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
	And remove a fee earner '<User>' from the user

Examples:
	| User                |
	| Leena EarnerSurname |

@CancelProcess
Scenario: 055 Validate Invoice GL has been posted
	Given I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to finance team when dispatch method not set
	When I view the invoices
	And I view the gl postings
	Then I validate gl postings status
	And I close the gl postings

Scenario: 060 verify the GL Detail Subledger enquiry for billing invoice
	Given I search for a process 'GL Detail Subledger enquiry' and select a chart 'GL Detail Subledger enquiry (GLDetailSubledgerInq)'
	When I create GL Detail Subledger 'BillingInvoice' report
	Then I verify the billing invoice report description

