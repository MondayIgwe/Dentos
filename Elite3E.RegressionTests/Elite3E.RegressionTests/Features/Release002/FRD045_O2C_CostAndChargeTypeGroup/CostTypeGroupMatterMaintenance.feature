@release2 @frd045 @CostTypeGroupMatterMaintenance
Feature: CostTypeGroupMatterMaintenance
	Test 2: Cost Type Group being added to matter from Matter Maintenance
	Test 4: New Cost Type Group creation on Matter Maintenance
	Test 6: Cost Type Group being removed from Matter


Scenario Outline: 010 Create Disbursement Type Group & Cost Type
	Given the cost type group exists
		| Code       | Description    | CostTypeGroupExcludeOrIncludeList |
		| at_CE_CTG3 | Auto_CE_Group3 | IsExcludeList                     |
		| at_CE_CTG4 | Auto_CE_Group4 | IsExcludeList                     |
		Given I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | EspiritEntity |
	And I create a matter with details:
		| Client   | Status   | OpenDate  | MatterName | Currency   | MatterCurrencyMethod   | Office   | Department   | Section   | ChargeTypeGroupName   | CostTypeGroupName   | BillingOffice   | PayorName   |
		| <Client> | <Status> | {Today}-1 | {Auto}+36  | <Currency> | <MatterCurrencyMethod> | <Office> | <Department> | <Section> | <ChargeTypeGroupName> | <CostTypeGroupName> | <BillingOffice> | <PayorName> |
@ft @qa @training @staging @uk
Examples:
| Client                       | Status     | Currency            | MatterCurrencyMethod | Office         | Department | Section | ChargeTypeGroupName | CostTypeGroupName | BillingOffice | PayorName |
| Client_Automation at_HTOvMOn | Fully Open | GBP - British Pound | Bill                 | London (UKIME) | Default    | Default | Auto_CE_Group1      | Auto_CE_Group3    | London (EU)   | Elba Beck |
@europe
Examples:
| Client                       | Status     | Currency            | MatterCurrencyMethod | Office      | Department | Section | ChargeTypeGroupName | CostTypeGroupName | BillingOffice |PayorName |
| Client_Automation at_HTOvMOn | Fully Open | GBP - British Pound | Bill                 | London (EU) | Default    | Default | Auto_CE_Group1      | Auto_CE_Group3    | London (EU)   |Elba Beck |
@canada
Examples:
| Client                       | Status     | Currency              | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | BillingOffice |PayorName |
| Client_Automation at_HTOvMOn | Fully Open | CAD - Canadian Dollar | Bill                 | Montreal | Default    | Default | Auto_CE_Group1      | Auto_CE_Group3    | Montreal      |Elba Beck |
@singapore
Examples:
| Client                       | Status     | Currency               | MatterCurrencyMethod | Office    | Department            | Section | ChargeTypeGroupName | CostTypeGroupName | BillingOffice |PayorName |
| Client_Automation at_HTOvMOn | Fully Open | SGD - Singapore Dollar | Bill                 | Singapore | Corporate - Singapore | Default | Auto_CE_Group1      | Auto_CE_Group3    | Singapore     |Elba Beck |

Scenario Outline: 020 Cost Type Group Added to Matter From Matter Maintenance
	When I add new cost type group to matter
		| Cost Type Group |
		| <CostTypeGroup> |
	Then I verify the cost type group is linked to the matter

@ft @qa @training @staging  @canada @europe @uk @singapore  
Examples:
	| CostTypeGroup  |
	| Auto_CE_Group4 |

@CancelProcess
Scenario Outline: 030 New Cost Type Group Creation on Matter Maintenance
	Given I create a new cost type group via matter maintenance
		| Code   | Description   |
		| <Code> | <Description> |
	Then I verify the cost type group is linked

@ft @qa @training @staging  @canada @europe @uk @singapore  
Examples:
	| Code | Description                |
	| 045  | automation_cost_type_group |

@ft @training @staging  @canada @europe @uk @singapore   @qa @canada
Scenario: 040 Cost Type Group Removed From Matter
	Given I remove a cost type group from a matter
		| Cost Type Group |
		| Auto_CE_Group4  |
	Then I verify the cost type group is removed from the matter

