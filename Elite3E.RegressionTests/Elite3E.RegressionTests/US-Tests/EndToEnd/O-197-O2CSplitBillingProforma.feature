@us
Feature: O-197-O2CSplitBillingProforma

Scenario Outline:010 Create user
Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
And I create a user with details
		| UserName | DataRoleAlias | UserRoleList                                                                               |
		| <User>   | Admin         | 0:AD:G:Common Authorisations,0:WC:P:Proforma Processor,0:WC:P:Proforma Processor - Finance |
And I add a workflow user to a FeeEarner
	| User   | Name            |
	| <User> | <FeeEarnerName> |
Examples:
	| FeeEarnerName | User         |
	| Johnson Pete  | Johnson Pete |


Scenario: 020 Sub matters creation and time cards setup
	Given I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | FrontPage Ltd |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | Rate     | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Proforma             | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | Standard | <PayorName> |
	And I create a submatter 1 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | Rate     | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Proforma             | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | Standard | <PayorName> |
	And I create a submatter 2 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | Rate     | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Proforma             | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | Standard | <PayorName> |
	When I add submatters
		| Split Description  | Split Type  | Split Percentage  |
		| <SplitDescription> | <SplitType> | <SplitPercentage> |
	And I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | TaxCode   |
		| <TimeType> | 1.00  | {Auto}+10 | <FeeEarnerName> | 3000     | 3000       | <Currency>   | <TaxCode> |
	And I submit a time modify
		| Time Type  | Hours    | Narrative | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | TaxCode   |
		| <TimeType> | <Hours1> | {Auto}+10 | <FeeEarnerName> | 3000     | 3000       | <Currency>   | <TaxCode> |
	And I submit a time modify
		| Time Type  | Hours    | Narrative | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | TaxCode   |
		| <TimeType> | <Hours2> | {Auto}+10 | <FeeEarnerName> | 4000     | 4000       | <Currency>   | <TaxCode> |
	

Examples:
  | FeeEarnerName | User         | Client             | SplitDescription | SplitType              | SplitPercentage | TimeType | Hours1 | Hours2 | TaxCode                          | ToTaxArea     | Currency        | Office  | PayorName    | Language                 |
  | Johnson Pete  | Johnson Pete | JohnsonPete Client | 50/50            | Split in Proforma Edit | 50              | FEES     | 1.00   | 1.00   | US Output Domestic Standard Rate | United States | USD - US Dollar | Chicago | Emilio Price | English (United Kingdom) |


  	Scenario Outline: 030 Create a Disbursement card
	When I post the disbursement
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	

Examples:
	| DisbursementType              | WorkCurrency | WorkAmount | TaxCode                          |
	| Dentons United States Funding | USD          | 5000       | US Output Domestic Standard Rate |


Scenario Outline: 040 Create a new Entity Person
	Given the entity person process is opened
	When I enter the  person entity details
		| Entity Type | First Name | Last Name | Format Code | Relationship |
		| Employee    | {Auto}+7   | {Auto}+6  | Default     | Self         |
	And enter site details
		| Description | Site Type  | Country   | Street   | Language |
		| {Auto}+10   | <SiteType> | <Country> | <Street> | Xhosa    |
	Then I can submit the new entity details
	And I validate submit was successful


Examples:
	| SiteType | Country       | Street           |
	| Billing  | UNITED STATES | 10 United States |


Scenario Outline: 050 Create a new Vendor Maintenance
	Given the vendor maintenace process is opened
	When I select the new entity
	And I can verify that there are no duplicates
	And set the global vendor "<GlobalVendor>"
	And I set the status as '<Status>'
	Then I can submit the new vendor details
	And I validate submit was successful


Examples:
	| GlobalVendor  | Status   |
	| Amex Supplier | Approved |

Scenario Outline: 060 Create a new Payee and Voucher
	Given I add the payee from payee maintenance
		| Payment Terms  | Office   | Site   | PayeeType   | PayeeStatus   |
		| <PaymentTerms> | <Office> | <Site> | <PayeeType> | <PayeeStatus> |
	And I submit it
	Then I add a payee bank
		| Description | AccountNumber |
		| {Auto}+8    | {Auto}+5      |
	And I submit it
	And I validate submit was successful
	Given I add a new voucher with voucher default information
		| Operation Unit  | Invoice Number | Invoice Date |
		| <OperationUnit> | v_             | {Today}      |
	And I add disbursement card details for voucher
		| Disbursement Type   | Narrative          | TaxCode   | InputTaxCode   | Voucher Amount |
		| <DisbursementType1> | Automation Testing | <TaxCode> | <InputTaxCode> | 100            |
	And I add disbursement card details for voucher
		| Disbursement Type   | Narrative          | TaxCode   | InputTaxCode   | Voucher Amount |
		| <DisbursementType2> | Automation Testing | <TaxCode> | <InputTaxCode> | 100            |
	And I update it
	And I add input amount "<InputAmount>" in voucher tax card
	When I submit the voucher
	Then I verify the voucher is created
	
Examples:
	| PaymentTerms | Office  | PayeeType | PayeeStatus | OperationUnit              | DisbursementType1             | DisbursementType2 | TaxCode                          | InputTaxCode                    | InputAmount |
	| Immediate    | Chicago | Counsel   | Approved    | Dentons United States, LLP | Dentons United States Funding | Official Fee      | US Output Domestic Standard Rate | US Input Domestic Standard Rate | 0           |


	Scenario Outline: 070 Amend the time and cost cards 
	When I locate the submitted time modify
	And I amend the work amount in time modify to '500'
	And I submit it
	Given I search for process 'Voucher Maintenance' without add button
	When I quick search by voucher number
	And I edit the disbursement card in the voucher 
	| Disbursement Type   | WorkAmount |
	| <DisbursementType1> | 100        |
	And I edit the disbursement card in the voucher 
	| Disbursement Type   | WorkAmount |
	| <DisbursementType2> | 100        |
	When I submit the voucher

Examples: 
| DisbursementType1             | DisbursementType2 |
| Dentons United States Funding | Official Fee      |

Scenario Outline: 080 Create a Client Account Receipt
	Given I search for process 'Client Account Receipt'
	And I add a new client account receipt
		| TransactionDate | ClientAccountReceiptType  | ClientAccountAcct             | DocumentNumber |
		| {Today}-1       | Cheque - Local Bank - USA | GENERAL RENTAL, INC.-37490958 | {Auto}+10      |
	When I add client account receipt detail child form data
		| Amount | IntendedUse | Reason                 |
		| 100    | General     | Client Account Receipt |
	Then I update it
	And submit it

	Scenario: 090 Client account transfer
	Then I proxy as user '<FeeEarnerName>'
	Given I make a client account transfer request
		| TransferType              | FromAccount   | ToAccount   | IntendedUse |
		| Matter to Matter Transfer | <FromAccount> | <ToAccount> | General     |
	And I submit it
	And I validate submit was successful

@CancelProcess
Examples:
	| FeeEarnerName | FromAccount                                               | ToAccount                                |
	| Johnson Pete  | Dentons US LLP (IOLTA) Interest on Lawyer Trust-204968895 | American Savings Bank - IOLTA-8103649398 |

Scenario Outline: 100 Split proforma workflow
	Given I can generate the proforma
		| Description | Change Status To | ProformaStatus | IncludeOtherProformas   |
		| {Auto}+10   | <ChangeStatusTo> | Draft          | <IncludeOtherProformas> |
	And I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
	And I submit it
	And I cancel proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	Then I open the split proforma
	And I split the proforma

Examples:
	| User         | ChangeStatusTo | IncludeOtherProformas |
	| Johnson Pete | Terminated     | Yes                   |

	Scenario Outline: 110 Sub matter proforma bill generation 
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma linked to submatter1 for submission
	And I submit it
	And I cancel the proxy
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma linked to submatter1 for billing
	And I bill it without printing
	And the invoice number is generated
	When I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to finance team when dispatch method not set
	And I view the invoices
	And I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma linked to submatter2 for submission
	And I submit it
	And I cancel proxy
	Then I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	Then I open the proforma linked to submatter2 for billing
	And I bill it without printing
	And the invoice number is generated
	And I search for 'Workflow Dashboard'
	And I open the Invoice Dispatch workflow task
	And I send the invoice routed to finance team when dispatch method not set
	Then I view the invoices

	Examples:
	| FeeEarnerName | User         |
	| Johnson Pete  | Johnson Pete |
