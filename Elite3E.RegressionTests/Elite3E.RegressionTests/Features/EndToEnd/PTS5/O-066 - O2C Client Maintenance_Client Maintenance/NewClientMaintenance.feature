Feature: NewClientMaintenance

Flat Fee (set at Client and Matter)
Azure Link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/56612


@CancelProcess
Scenario Outline: 010 Prepare Data for Client maintenance
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	When I search or create a client
		| Entity Name | FeeEarnerFullName |
		| {Auto+9}    | <FeeEarnerName>   |
	Then I create a matter with details:
		| Client       | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName   | CostTypeGroupName   | BillingOffice   | PayorName   |
		| Edward Trust | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroupName> | <CostTypeGroupName> | <BillingOffice> | <PayorName> |

@e2eft
Examples:
	| FeeEarnerName     | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName |
	| Billing FeeEarner | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Milan         | James May |

@CancelProcess @e2eft
Scenario Outline: 020 Add Flat Fees for Client maintenance
	Given I view an existing client
	When I add flat fees for the client
		| TimeType       | Currency            |
		| Test Flat Rate | GBP - British Pound |
	And I update it
	And I submit it
	Then I validate submit was successful

@CancelProcess @e2eft
Scenario Outline: 030 Add Flat Fees for Matter maintenance
	Given I view an existing matter
	When I add flat fees for the matter
		| TimeType       | Currency            |
		| Test Flat Rate | GBP - British Pound |
	And I update it
	And I submit it
	Then I validate submit was successful