@ignore
#The test is failing at the moment as GL posting details are not available for the charge modify item
Feature: PostingFinancialData_ChargeModify
R-050 - R2R: Posting Financial Data - Charge Modify
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/53172

	@CancelProcess
	Scenario Outline: 010 Create a new Matter
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | InputAmount   | PayorName   | BillingOffice | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Auto_IncludeALL     | Auto_IncludeALL   | <InputAmount> | <PayorName> | Milan         | <PayorName> |

	@e2eft
Examples:
	| Client       | FeeEarnerName     | Office         | OperationUnit | DisbursementType | InputTaxCode                   | Currency            | TransactionType | VoucherStatus | GLAccount                              | VoucherGLTaxCode               | InputAmount | PayorName   |
	| Edward Trust | Billing FeeEarner | London (UKIME) | Firm          | Court Fees       | UK Input Domestic Standard 20% | GBP - British Pound | Direct Debit    | Approved      | 3000-101010-1000-0000-0000-0000-000000 | UA Input Domestic Standard 20% | 100.00      | James Mayor |

@CancelProcess
Scenario Outline: 020 Create a Charge entry and Submit
	When I add a charge entry
		| Charge Type | Amount   | Tax Code  |
		| <Charge>    | <Amount> | <TaxCode> |
	When I validate the post all was successful
	And I locate the submitted entry in 'Charge Modify' process
	And I get the charge index of the charge card
	And  I validate the gl postings for operating unit '<OperatingUnit>'

@e2eft
Examples:
	| Charge        | Amount | TaxCode                        |
	| Sundry Income | 300.00 | UK ICB Output Domestic Zero 0% |

@e2eft @CancelProcess
Scenario Outline: 030 Verify the Post Queue
	Given I navigate to the post queue process
	When I verify the cost/time/charge/receipt card is not present in the post queue

@e2eft @CancelProcess
Scenario: 040 Verify in Posting Results
	Given  I search for the entry in posting results process 
	Then verify the posting status is posted

@e2eft
Scenario: 050 Verify in Journal Register
	Given I search for the entry in journal register
	Then verify the posting details in journal register
            | Search Column   | Search Operator |
            | Journal Manager | Equals          |