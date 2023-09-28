@release2 @frd062 @VerifyRemittanceAccountOnMatter
Feature: VerifyRemittanceAccountOnMatter
	Verify the Remittance Account on Matter


Scenario Outline: 010 Create Matter
	Given I create the Payer with Api
		| PayerName   | Entity                |
		| <PayorName> | BottomLine Associates |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod   | Office   | Department   | Section   | ChargeTypeGroupName   | CostTypeGroupName   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <MatterCurrencyMethod> | <Office> | <Department> | <Section> | <ChargeTypeGroupName> | <CostTypeGroupName> | <PayorName> |

@ft @qa @training @staging @europe @uk
Examples:
	| Client                       | Currency            | MatterCurrencyMethod | Office      | Department | Section | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill                 | London (EU) | Default    | Default | Desc_at_3e8glw7     | Desc_at_VowqCrR   | Kenton Ford |
@canada
Examples:
	| Client                       | Currency              | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName |PayorName |
	| Client_Automation at_HTOvMOn | CAD - Canadian Dollar | Bill                 | Montreal | Default    | Default | Desc_at_3e8glw7     | Desc_at_VowqCrR   |Kenton Ford |
@singapore
Examples:
	| Client                       | Currency               | MatterCurrencyMethod | Office    | Department            | Section | ChargeTypeGroupName | CostTypeGroupName |PayorName |
	| Client_Automation at_HTOvMOn | SGD - Singapore Dollar | Bill                 | Singapore | Corporate - Singapore | Default | Desc_at_3e8glw7     | Desc_at_VowqCrR   |Kenton Ford |

Scenario Outline: 020 Remmittance Account is available on Matter
	When I save the Remittance Account "<RemittanceAccount>"
	Then the remittance account is saved

@ft @qa
Examples:
	| RemittanceAccount                |
	| Perth - ANZ Bank Trust Acc - AUD |

@training @staging
Examples:
	| RemittanceAccount                                |
	| Dentons Europe UK - HSBC - Operating Acc 1 - GBP |
@uk
Examples:
	| RemittanceAccount                   |
	| Abu Dummy Gbp Bank Acc Forex ADUGBP |
@europe
Examples:
	| RemittanceAccount                                                |
	| Dentons Europe (AZ) - Bank of Azerbaijan - Operating Acc 1 - AZN |
@canada
Examples:
	| RemittanceAccount                                                |
	| Dentons Canada Asset Holdings L - Application Off-Set Bank - CAD |
@singapore
Examples:
	| RemittanceAccount                       |
	| IVB - 1201 - Singapore - IVB Bank - SGD |
	