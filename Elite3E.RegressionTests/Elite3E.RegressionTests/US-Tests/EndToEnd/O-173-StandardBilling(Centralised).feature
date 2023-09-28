@us
Feature: O-173-StandardBilling(Centralised)

A short summary of the feature

Scenario Outline: 001 Create a new entity

	Given the entity person process is opened
	When I enter the  person entity details
		| Entity Type | First Name | Last Name | Format Code | Relationship |
		| Employee    | {Auto}+6   | {Auto}+6  | Default     | Self         |
	And enter site details
		| Description | Site Type  | Country   | Street   | Language |
		| {Auto}+10   | <SiteType> | <Country> | <Street> | Xhosa    |
	Then I can submit the new entity details
Examples:
	| SiteType | Country      | Street                          |
	| Billing  | SOUTH AFRICA | 10 Umhlanga rocks drive, Durban |


Scenario Outline: 002 Create a new activated client
	Given I create a user with details
		| UserName | DataRoleAlias | UserRoleList                                                                                                                                                  |
		| <User>   | Admin         | DEFAULT_WORKFLOW_ROLE,0:AD:G:Common Authorisations,0:WC:P:Proforma Processor,0:WC:P:Proforma Processor - Finance,0:WC:W:Proforma Billing Team[: Unit: Office] |
	And I create a fee earner with details
		| EntityName  |
		| <FeeEarner> |
	Then I add a workflow user to a FeeEarner
		| User   | Name        |
		| <User> | <FeeEarner> |
	When the client maintenace process is opened
	When I select the new entity
	And enter the client details
		| Opening Fee Earner | Date Opened | Status     | Status Date |
		| <FeeEarner>        | {Today}     | Fully Open | {Today}     |
	Then I can enter the effective dates information an save
		| Billing Fee Earner | Responsible Fee Earner | Supervisor Fee Earner | Office   |
		| <FeeEarner>        | <FeeEarner>            | <FeeEarner>           | <Office> |

Examples:
	| FeeEarner    | Office  | User         |
	| The Original | Chicago | The Original |



Scenario Outline: 003 Matter Maintenance
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a new matter
	When I update the matter
		| OpeningFeeEarner   | Status     | Open Date | Matter Name | Matter Currency Method | Statement Site                  | MatterType                          | MatterAttribute | Language |
		| <OpeningFeeEarner> | Fully Open | {Today}   | {Auto}+10   | Bill                   | 10 Umhlanga rocks drive, Durban | Non Taxable Real Estate Transaction | Client Billable | English  |
	And I add a Matter Payer
		| Start Date |
		| {Today}+1  |
	And I update the effective dated information
		| Child Form                  | Office  |
		| Effective Dated Information | Chicago |
	And I update the matter rates
		| Child Form   | Rate     |
		| Matter Rates | Standard |
	And I add a new cost type group
		| Cost Type Group |
		| Desc_at_VowqCrR |
	And I add new charge type group
		| Charge Type Group |
		| Desc_at_3e8glw7   |
	Then verify the matter number is generated
	And I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName      | WorkRate | WorkAmount | WorkCurrency   | TaxCode   |
		| <TimeType> | 0.1   | {Auto}+10 | <OpeningFeeEarner> | 1        | 0.1        | <WorkCurrency> | <TaxCode> |
Examples:
	| TimeType | OpeningFeeEarner | PayorName   | WorkCurrency | WorkAmount | TaxCode                          |
	| FEES     | The Original     | James Mayor | USD          | 5000       | US Output Domestic Standard Rate |


Scenario Outline: 004 Create a Disbursement Modify and Submit
	Given I add a disbursement entry
		| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+10 | <TaxCode> |
	When I validate the disbursement is posted with no errors

Examples:
	| DisbursementType                   | WorkCurrency | WorkAmount | TaxCode                          | OperatingUnit |
	| Data Storage & Costs - Anticipated | USD          | 5000       | US Output Domestic Standard Rate | 3000          |



Scenario Outline: 005 Create a Voucher
	Given I create a new Payee with the Api
		| PayeeName            |
		| Payee atVoucherMaint |
	And I add a new voucher with voucher default information
		| Operation Unit  | Invoice Number | Invoice Date | Transaction Type  | Voucher Status  |
		| <OperationUnit> | v_             | {Today}      | <TransactionType> | <VoucherStatus> |
	And I add disbursement card details for voucher
		| Disbursement Type  | Narrative          | InputTaxCode   | Voucher Amount |
		| <DisbursementType> | Automation Testing | <InputTaxCode> | 100.00         |
	And I click on Tax button in disbursement card and validate tax record is added in voucher taxes
	And I update it
	When I submit the voucher
	Then I verify the voucher is created

Examples:
	| Client         | FeeEarnerName | Office  | OperationUnit | DisbursementType       | InputTaxCode                    | Currency | TransactionType | VoucherStatus | PayorName |
	| TestEquity LLC | The Original  | Chicaco | Firm          | Bank & Finance Charges | US Input Domestic Standard Rate | USD      | Direct Debit    | Direct Debit  | James May |

Scenario: 006 Proforma generate Bill
	And I can generate the proforma
		| Description | Include Other Proformas | Proforma Status |
		| {Auto}+36   | No                      | Draft           |


Scenario: 007 I want Proforma Workflow - Modifications
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	And I open the proforma for submission
    #Then I want to open cost/disbursment details child
	#And I  verify the 'Soft Cost' card is displayed then write down the 'Bill Amount' by 100
	#And Select the 'Hard Cost' card then write down the 'Bill Amount' by 300.
	Then I want to open the 'Fee Details' child form
	And I click the 'Divide' button.
	Then I want to combine the time entries.
	And I click the 'Recalc' button.

@ft
Examples:
	| User      |
	| Gavin Jim |

#
##Scenario: 008 want to Apply Adjustment
#	#Given I open and add 'Apply Adjustment' child form
#	#Then Within the 'Percentage' field, enter '-5'
#	#And I select the appropriate option from the 'Adjustment Type'
#	#And Populate the 'Description' field to specify the reason behind
#	#When I add another 'additional adjustment'
#	#Then Within the 'Percentage' field, enter '-2'
#	#And Within the 'Adjustment Method' dropdown, select 'Proportional by soft entries on the proforma
#	#And I select the appropriate option from the 'Adjustment Type' which represents the discount
#	#And Populate the 'Description' field to specify the reason behind
#
#
#Scenario: 009 want to Transfer Time/Cost cards
#	#When I transfer to transfer Time/Cost cards
#	#And I click the 'OK' button.
#	Then I update it
#	And I can submit the record
#	Then I validate submit was successful
#	And I cancel proxy
#
Scenario: 010 I want Write-down Approval Proxy in as the appropriate Approver
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for billing
	Then I want to click approve
	@ft
Examples:
	| User      |
	| Gavin Jim |

Scenario: 020 I want Process Bill Proxy in as Finance user
	Given I proxy as user '<User>'
	And I search for 'Workflow Dashboard'
	And I open the proforma workflow task
	When I open the proforma for billing
	And I bill it without printing
	And the invoice number is generated
	And remove a fee earner '<User>' from the user
	@ft
Examples:
	| User      |
	| Gavin Jim |

Scenario: 030 Validate Invoice has been successfully added
	Given I search for 'Workflow Dashboard'
	And I open the billing workflow task and send the invoice
	When I search for process 'Invoices' without add button
	When I quick search by invoice number
	Given I search for 'Workflow Dashboard'
	And I open the billing workflow task and send the invoice
	Then I cancel it

Scenario Outline: 040 Create a Receipt with GL
	Given I add a new receipt
		| Receipt Type  | Receipt Date | Document Number | Narrative |
		| <ReceiptType> | {Today}      | {Auto}+36       | {Auto}+36 |
	#And I want to add an invoice child form
	#Then Verify 'Receipt Amount'
	#And I close child form
	#Then I enter 'Receipt Amount' from the 'Receipt Amount' in the Invoices child form
	#Then I update it
	#And I can submit the record
	Then I validate submit was successful