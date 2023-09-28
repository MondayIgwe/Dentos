@release6 @frd022 @PurgeTypeWorkflow
Feature: PurgeTypeWorkflow
	Test 4: If a purge type is entered, the Purge Reason Type field must be populated.
	NOTES: 
	User running Test must have the following roles:
		DefaultWFTimeModifyApprovalRole_ccc
		WFTimeModifyLondonOfficeApprover_ccc

Scenario Outline: 010 Create entity and fee earner
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

@ft @training @staging @uk @qa
Examples:
	| ClientName        | FeeEarnerName     | TimeType | Currency            | Office         | PayorName |
	| Sandy PurgeClient | Sandy PurgeEarner | FEES     | GBP - British Pound | London (UKIME) | Max Hart  |
@singapore
Examples:
	| ClientName        | FeeEarnerName     | TimeType | Currency            | Office    | PayorName |
	| Sandy PurgeClient | Sandy PurgeEarner | FEES     | GBP - British Pound | Singapore | Max Hart  |
@europe
Examples:
	| ClientName        | FeeEarnerName     | TimeType | Currency | Office      | PayorName |
	| Sandy PurgeClient | Sandy PurgeEarner | FEES     | AED      | London (EU) | Max Hart  |
@canada
Examples:
	| ClientName        | FeeEarnerName     | TimeType | Currency              | Office    | PayorName |
	| Sandy PurgeClient | Sandy PurgeEarner | FEES     | CAD - Canadian Dollar | Vancouver | Max Hart  |


@CancelProcess 
Scenario: 020 Internal comments is populated
	Given I enter an internal comment
		| Internal Comment  |
		| <InternalComment> |
	Then I confirm the workflow is created for approval

@ft @qa @training @staging @canada @europe @uk @singapore
Examples:
	| InternalComment                                            |
	| This time should be wtitten off before proforma generation |

@CancelProcess 
Scenario: 030 Purge type field is populated
	Given I select the open workflow
	When I select the purge type
		| Purge Type  | Purge Type Reason |
		| <PurgeType> | <PurgeTypeReason> |
	Then I submit the workflow
	And remove a user fee earner map

@ft @qa @training @staging @canada @europe @uk @singapore
Examples:
	| PurgeType      | PurgeTypeReason |
	| Billable Purge | Correction      |
