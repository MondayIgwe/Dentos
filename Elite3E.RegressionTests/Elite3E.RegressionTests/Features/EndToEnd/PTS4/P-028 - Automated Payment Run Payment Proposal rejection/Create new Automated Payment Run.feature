Feature: Create new Automated Payment Run

Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/56015

@CancelProcess
Scenario: 001 Prepare Data for the Payment Run
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-31 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |
	And I create a new Payee with the Api
		| PayeeName     |
		| Payee_ PayRun |
	Then I add a payee bank
		| Description | AccountNumber |
		| {Auto}+8    | {Auto}+5      |
	And I submit it

@e2eft
Examples:
	| Client               | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | PayorName |
	| Client_Automation PR | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | TestGroup3      | Desc_at_3e8glw7 | James May |

@CancelProcess
Scenario Outline: 010 Create a New Voucher
	Given I add a new voucher with voucher default information
		| Operation Unit  | Invoice Number | Invoice Date |
		| <OperationUnit> | v_             | {Today}      |
	And I update the status to "Approved"
	And I add disbursement card details for voucher
		| Disbursement Type  | Narrative          | TaxCode   | InputTaxCode   | Voucher Amount |
		| <DisbursementType> | Automation Testing | <TaxCode> | <InputTaxCode> | 100            |
	And I validate the barrister fields are not mandatory
	And I update it
	And I add input amount "<InputAmount>" in voucher tax card
	When I submit the voucher

@e2eft
Examples:
	| OperationUnit                  | DisbursementType                                       | TaxCode                         | InputTaxCode                       | InputAmount |
	| Dentons UK and Middle East LLP | Registraton Fees - Issuance of SSCT - Anticipated (NT) | UK Output Domestic Standard 20% | AE ICB Input  Domestic Standard 5% | 0           |

@CancelProcess @e2eft
Scenario: 020 Create a Payment Selection Generation
	Given I navigate to the payment selection generation process
	Then I add a new payment selection generation record
	And I complete mandatory fields
		| Description | BankAccount                          | PaymentDate |
		| {Auto}+10   | 3-London UKME - HSBC Off 1 Acc - GBP | {Today}     |
	And I tick the pay electronically checkbox
	When I add a new selection criteria
		| PaymentDate |
		| {Today}     |
	And I search for a payee
	And I search for the voucher number
	And I test the selection
	And I verify the test result
	And I click process payment
	And I verify the page is process payments
	And I update the cheque printer and template
		| ChequeTemplate | ChequePrinter |
		| TE_AP Check    | NO_PRINTER    |
	And I process payment

@CancelProcess @e2eft
Scenario: 030 Run a Payment Preview with Voucher Information Report
	Given I navigate to the payment preview with voucher information report
	Then I search by the payment selection index
		| SearchType                          | Operator |
		| Payment Selection (Selection Index) | Equals   |
	And I can run the report
	When I verify that the report information is correct

#Currently fails because the report is not generated 
@CancelProcess @e2eft
Scenario: 040 Verify Proposed Payments
	Given I navigate to the payment selection generation process
	And I select an existing payment selection
	Then I generate it
	And I verify that the proposed payment childform is displayed
	When I click detail listing
	And I verify the payment preview

@CancelProcess @e2eft
Scenario: 050 Delete a Payment Selection Generation Record
	Given I navigate to the payment selection generation process
	And I select an existing payment selection
	Then I delete the payment selection
	When I submit it
	And I validate submit was successful
	And I verify the delete was successful

@CancelProcess
Scenario: 060 Prepare another set of data for Approval
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-31 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |
	And I create a new Payee with the Api
		| PayeeName   |
		| Payee_ Prop |
	Then I add a payee bank
		| Description | AccountNumber |
		| {Auto}+8    | {Auto}+5      |
	And I submit it

@e2eft
Examples:
	| Client                 | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | PayorName |
	| Client_Automation Prop | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | TestGroup3      | Desc_at_3e8glw7 | James May |

@CancelProcess
Scenario Outline: 070 Create a new Voucher For Approval
	Given I add a new voucher with voucher default information
		| Operation Unit  | Invoice Number | Invoice Date |
		| <OperationUnit> | v_             | {Today}      |
	And I update the status to "Approved"
	And I add disbursement card details for voucher
		| Disbursement Type  | Narrative          | TaxCode   | InputTaxCode   | Voucher Amount |
		| <DisbursementType> | Automation Testing | <TaxCode> | <InputTaxCode> | 100            |
	And I validate the barrister fields are not mandatory
	And I update it
	And I add input amount "<InputAmount>" in voucher tax card
	When I submit the voucher

@e2eft
Examples:
	| OperationUnit                  | DisbursementType                                       | TaxCode                         | InputTaxCode                       | InputAmount |
	| Dentons UK and Middle East LLP | Registraton Fees - Issuance of SSCT - Anticipated (NT) | UK Output Domestic Standard 20% | AE ICB Input  Domestic Standard 5% | 0           |

@CancelProcess @e2eft
Scenario: 080 Create new Payment Selection Generation for Approval
	Given I navigate to the payment selection generation process
	Then I add a new payment selection generation record
	And I complete mandatory fields
		| Description | BankAccount                          | PaymentDate |
		| {Auto}+10   | 4-London UKME - HSBC Off 1 Acc - GBP | {Today}     |
	And I tick the pay electronically checkbox
	When I add a new selection criteria
		| PaymentDate |
		| {Today}     |
	And I search for a payee
	And I search for the voucher number
	And I test the selection
	And I verify the test result
	And I generate it
	And I set the status to "Approved"
	And I click process payment

