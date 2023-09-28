@release6 @frd022 @TimeWrittenOff
Feature: TimeWrittenOff
	Test 1: Users can select timecards based on the settings in their user record in the first workflow process.
	Test 2: If Internal Comments is populated, cards are sent for approval. Routing will be based on the timecard office and the workflow configuration.
	Test 3: At the approval step, after the changes are made and Submit is clicked, the changes are saved, and the workflow ends.

	NOTES: 
	User running Test must have the following roles:
		DefaultWFTimeModifyApprovalRole_ccc
		WFTimeModifyLondonOfficeApprover_ccc
	
Scenario: 010 Create entity and fee earner
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | NorthSide Ltd |
	When I create a matter with details:
		| Client       | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <ClientName> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Standard | Desc_at_3e8glw7     | Desc_at_3e8glw7   | <PayorName> |
	And I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName   | WorkRate | WorkCurrency |
		| <TimeType> | 0.1   | {Auto}+10 | <FeeEarnerName> | 100      | <Currency>   |
	And add a user fee earner map
	Then I select the unbilled time card

@ft @training @staging @qa @uk
Examples:
	| ClientName           | FeeEarnerName        | TimeType | Currency            | Office         | PayorName |
	| Jason WriteOffClient | Jason WriteOffEarner | FEES     | GBP - British Pound | London (UKIME) | Max Hart  |
@singapore
Examples:
	| ClientName           | FeeEarnerName        | TimeType | Currency            | Office    | PayorName |
	| Jason WriteOffClient | Jason WriteOffEarner | FEES     | GBP - British Pound | Singapore | Max Hart  |
@europe
Examples:
	| ClientName           | FeeEarnerName        | TimeType | Currency | Office      | PayorName |
	| Jason WriteOffClient | Jason WriteOffEarner | FEES     | AED      | London (EU) | Max Hart  |
@canada
Examples:
	| ClientName           | FeeEarnerName        | TimeType | Currency              | Office    | PayorName |
	| Jason WriteOffClient | Jason WriteOffEarner | FEES     | CAD - Canadian Dollar | Vancouver | Max Hart  |

@CancelProcess 
Scenario: 020 Internal comments is populated
	Given I enter an internal comment
		| Internal Comment  |
		| <InternalComment> |
	Then I confirm the workflow is created for approval

@ft @qa @training @staging @canada @europe @uk @singapore
Examples:
	| InternalComment                 |
	| This time should be written off |

@CancelProcess 
Scenario: 030 Verify changes are saved and workflow ends
	Given I select the open workflow
	When I submit the workflow
	Then the changes are reflected
		| Internal Comment  |
		| <InternalComment> |
	And remove a user fee earner map

@ft @qa @training @staging @canada @europe @uk @singapore
Examples:
	| InternalComment                 |
	| This time should be written off |
