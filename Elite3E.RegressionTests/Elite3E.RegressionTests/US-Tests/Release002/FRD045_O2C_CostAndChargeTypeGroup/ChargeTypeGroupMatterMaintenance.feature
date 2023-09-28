@us
Feature: ChargeTypeGroupMatterMaintenance
	Test 1: Charge Type Group being added to matter from Matter Maintenance
	Test 3: New Charge Type Group creation on Matter Maintenance
	Test 5: Charge Type Group being removed from Matter
	Test 7: All must be linked to at least one Charge and one Cost Type

Scenario Outline: 010 Create Charge Type Group & Charge Type
	Given the charge type group exists
		| Code       | Description    | ChargeTypeGroupExcludeOrIncludeList |
		| at_CHT_MM1 | Auto_MM_Group1 | IsIncludeList                       |
		| at_CHT_MM2 | Auto_MM_Group2 | IsIncludeList                       |
	Given I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | EspiritEntity |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | Auto_MM_Group1      | <CostTypeGroup>   | <BillingOffice> | <PayorName> |
Examples:
	| Client                       | Currency        | CurrencyMethod | Office  | Department | Section | CostTypeGroup  | BillingOffice | PayorName |
	| Client_Automation at_HTOvMOn | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_CE_Group1 | Chicago       | Elba Beck |

Scenario Outline: 020 Charge Type Group Added to Matter From Matter Maintenance
	When I add new charge type group to matter
		| Charge Type Group |
		| <ChargeTypeGroup> |
	Then I verify the charge type group is linked to the matter
		| Charge Type Group |
		| <ChargeTypeGroup> |

Examples:
	| ChargeTypeGroup |
	| Auto_MM_Group2  |

Scenario Outline: 030 New Charge Type Group Creation on Matter Maintenance
	Given I create a new charge type group via matter maintenance
		| Code   | Description   |
		| <Code> | <Description> |
	Then I verify the charge type group is linked

Examples:
	| Code | Description       |
	| 045  | charge_type_group |

Scenario Outline: 040 Charge Type Group Removed From Matter
	Given I remove a charge type group from a matter
		| Charge Type Group     |
		| <ChargeTypeGroupCode> |
	Then I verify the charge type group is removed from the matter
		| Charge Type Group     |
		| <ChargeTypeGroupCode> |

Examples:
	| ChargeTypeGroupCode |
	| at_CHT_MM1          |

Scenario Outline: 050 Error Occurs When No Charge or Cost Type is Linked To Matter
	Given I remove all cost and charge types from the matter
	Then an error occurs
		| Mandatory Field  |
		| <MandatoryField> |
	And I cancel the process

Examples:
	| MandatoryField                                                                        |
	| Non ICB matters must have at least one Cost Type Group and Charge Type Group defined. |
