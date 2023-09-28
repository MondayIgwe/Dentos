Feature: PostingFinancialData_CostModify
R-033-R2R: Posting Financial Data 
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/52594

	@CancelProcess
	Scenario Outline: 010 Create a new Matter
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |

	@e2eft
	Examples:
	 | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName | Client      | FeeEarnerName |
	 | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James May | Mike Thanks | Mike Thanks   |

	@CancelProcess
	Scenario Outline: 020 Create a Disbursement Modify and Submit
	When I submit the disbursement modify
		| Work Date | DisbursementType   | Work Currency  | Work Amount  | Narrative | Tax Code  |
		| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+36 | <TaxCode> |
	And I validate the disbursement is posted with no errors
	And I locate the submitted entry in 'Disbursement Modify' process
	And I get the cost index of the disbursement card
	And  I validate the gl postings for operating unit '<OperatingUnit>'

	@e2eft
	Examples:
	| DisbursementType                       | WorkCurrency | WorkAmount | TaxCode                         | OperatingUnit |
	| Bank Charges & Admin Fee - Anticipated | GBP          | 5000       | UK Output Domestic Standard 20% | 3000          |

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