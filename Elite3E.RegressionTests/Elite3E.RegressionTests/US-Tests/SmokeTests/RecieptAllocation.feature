@us
Feature: RecieptAllocation

Scenario Outline: 010 Create Matter
	Given I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | SeasonsEntity |
	When I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-31 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |

Examples:
	| Client                       | Currency        | CurrencyMethod | Office  | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup |PayorName |
	| Client_Automation at_HTOvMOn | USD - US Dollar | Bill           | Chicago | Default    | Default | Standard | Desc_at_3e8glw7 | Auto__18Xeq   |Dave Peter |

Scenario Outline: 020 Allocate a reciept to payment
	Given I create a receipt type with details:
		| Code      | Description | Bank Account Display Name | Currency Type Description | Tolerance Amount | Tolerance Percentage |
		| {Auto}+12 | {Auto}+21   | <BankAccountDisplayName>  | <CurrencyTypeDescription> | 50.00            | 50.00                |
	When I navigate to the Receipts Apply/Reverse Payments process
	And allocate the new reciept
		| Unallocated Type  | Receipt Amount | Operating Unit  | Narrative |
		| <UnallocatedType> | 5000           | <OperatingUnit> | {Auto}+10 |
	And add a new reciept
		| Receipt Amount | Document Number | Payer   | Narrative |
		| 5000           | {Auto}+10       | <Payer> | {Auto}+10 |
	Then I can submit the reciept allocation
	And I validate submit was successful


Examples:
	| Payer           | UnallocatedType              | OperatingUnit   | BankAccountDisplayName                           | CurrencyTypeDescription |
	| Auto_Test_Payer | Earmarked - available to pay | Dentons US, LLP | Dentons US, LLP - Application Off-Set Bank - USD | Daily Rate Xe - US      |


Scenario: 030 Delete the receipt
	When I search for the document number
	Then delete the receipt
