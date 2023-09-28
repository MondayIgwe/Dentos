Feature: ReceiptsApplyReversePayments

A short summary of the feature
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/46625

Scenario Outline: 010 Data Preparation Create Matter and check Reciept Type
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a receipt type with details:
		| Code   | Description   | Tolerance Amount | Tolerance Percentage |
		| <Code> | <Description> | 50.00            | 50.00                |
	And I create a submatter 1 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Auto_IncludeALL     | Auto_IncludeALL   | <BillingOffice> | <PayorName> |
	
@e2eft
Examples:
	| Client                 | FeeEarnerName          | UnallocatedType       | Currency            | CurrencyMethod | Office         | Department | Section | Code    | Description | BillingOffice | PayorName |
	| Transfer ClientSurname | Transfer EarnerSurname | Duplicate/Overpayment | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | EFT1000 | EFT 1000    | Sydney        | James May |


Scenario Outline: 020 Create unallocated receipt for matter 1
	Given I add a new receipt
		| Receipt Date | Narrative | Document Number | ReceiptAmount |
		| {Today}      | {Auto}+12 | {Auto}+36       | 5000          |
	When I add an unallocated child form for submatter 1
		| Unallocated Type  | Receipt Amount | Child Form  |
		| <UnallocatedType> | 5000           | Unallocated |
	And change the operating unit "<OperatingUnit>"
	Then the payer is auto populated
	And I can submit the receipt
	And I validate submit was successful

@e2eft
Examples:
	| UnallocatedType       | OperatingUnit                  |
	| Duplicate/Overpayment | Dentons UK and Middle East LLP |

