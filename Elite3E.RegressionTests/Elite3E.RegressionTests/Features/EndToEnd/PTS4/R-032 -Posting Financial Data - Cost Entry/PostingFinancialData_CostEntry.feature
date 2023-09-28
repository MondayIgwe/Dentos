Feature: PostingFinancialData_CostEntry
R-032 - R2R: Posting Financial Data - Cost Entry
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/52593

@CancelProcess
	Scenario Outline: 010 Create a new Matter
	Given I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |

	@e2eft
	Examples:
	| Client                       | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London (EU)   | James Matt |

	@CancelProcess
	Scenario Outline: 020 Create a Disbursement Modify and Submit
	Given I add a disbursement entry
	| Work Date | Disbursement Type  | Work Currency  | Work Amount  | Narrative | Tax Code  |
	| {Today}-1 | <DisbursementType> | <WorkCurrency> | <WorkAmount> | {Auto}+10 | <TaxCode> |
	When I validate the disbursement is posted with no errors
	And I locate the submitted entry in 'Disbursement Modify' process
	And I get the cost index of the disbursement card
	And I validate the gl postings for operating unit '<OperatingUnit>'

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
	Given I search for the entry in posting results process 
	Then verify the posting status is posted

	@e2eft
	Scenario: 050 Verify in Journal Register
	Given I search for the entry in journal register
	Then verify the posting details in journal register
            | Search Column   | Search Operator |
            | Journal Manager | Equals          |