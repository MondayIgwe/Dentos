@smoke @RecieptAllocation
Feature: RecieptAllocation

Scenario Outline: 010 Create Matter
	Given I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | SeasonsEntity |
	When I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | Rate   | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-31 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <Rate> | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |

@ft
Examples:
	| Client                        | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | PayorName  |
	| Client_Automation NumberThree | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | TestGroup3      | Desc_at_3e8glw7 | Dave Peter |
@training @staging
Examples:
	| Client        | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   | PayorName  |
	| Adam Smithone | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | TestGroup3      | Desc_at_3e8glw7 | Dave Peter |
@qa
Examples:
	| Client                 | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup   |PayorName |
	| Automation_John Client | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | TestGroup3      | Desc_at_3e8glw7 |Dave Peter |
@europe
Examples:
	| Client                       | Currency   | CurrencyMethod | Office      | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup |PayorName |
	| Client_Automation at_HTOvMOn | EUR - Euro | Bill           | London (EU) | Default    | Default | Standard | Desc_at_3e8glw7 | Auto__18Xeq   |Dave Peter |
@canada
Examples:
	| Client                       | Currency              | CurrencyMethod | Office    | Department          | Section           | Rate     | ChargeTypeGroup | CostTypeGroup |PayorName |
	| Client_Automation at_HTOvMOn | CAD - Canadian Dollar | Bill           | Vancouver | Banking and Finance | Banking & Finance | Standard | Desc_at_3e8glw7 | Auto__18Xeq   |Dave Peter |
@uk
Examples:
	| Client                       | Currency            | CurrencyMethod | Office         | Department | Section | Rate     | ChargeTypeGroup | CostTypeGroup |PayorName |
	| Client_Automation at_HTOvMOn | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Standard | Desc_at_3e8glw7 | Auto__18Xeq   |Dave Peter |
@singapore
Examples:
	| Client                       | Currency               | CurrencyMethod | Office    | Department            | Section | Rate     | ChargeTypeGroup | CostTypeGroup |PayorName |
	| Client_Automation at_HTOvMOn | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Standard | Desc_at_3e8glw7 | Auto__18Xeq   |Dave Peter |

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

@ft
Examples:
	| Payer                                  | UnallocatedType | OperatingUnit                  | BankAccountDisplayName             | CurrencyTypeDescription         |
	| Sydney - Domestic Client - Head office | Unidentified    | Dentons UK and Middle East LLP | London UKME - HSBC Off 1 Acc - GBP | Daily - Azerbaijan Central Bank |
@uk
Examples:
	| Payer           | UnallocatedType              | OperatingUnit            | BankAccountDisplayName                   | CurrencyTypeDescription         |
	| Auto_Test_Payer | Earmarked - available to pay | Dentons & Co - Abu Dhabi | Barclays Office GBP - (LOB) GBP BBLOBGBP | Daily - Azerbaijan Central Bank |
@training @staging
Examples:
	| Payer                                  | UnallocatedType | OperatingUnit           | BankAccountDisplayName                   | CurrencyTypeDescription         |
	| Sydney - Domestic Client - Head office | Unidentified    | Dentons Europe LLP (UK) | Barclays Office GBP - (LOB) GBP BBLOBGBP | Daily - Azerbaijan Central Bank |
@qa
Examples:
	| Payer                                  | UnallocatedType | OperatingUnit                  | BankAccountDisplayName             | CurrencyTypeDescription         |
	| Sydney - Domestic Client - Head office | Unidentified    | Dentons UK and Middle East LLP | IGB - 3000 - UKME - IGB Bank - GBP | Daily - Azerbaijan Central Bank |
@europe
Examples:
	| Payer           | UnallocatedType | OperatingUnit                       | BankAccountDisplayName                                           | CurrencyTypeDescription |
	| AIG EUROPE S.A. | Unidentified    | Advocacy Bureau of Moscow "Dentons" | Advocacy Bureau of Moscow "Dent - Application Off-Set Bank - EUR | Daily Rate Xe           |
@canada
Examples:
	| Payer                                     | UnallocatedType | OperatingUnit      | BankAccountDisplayName                              | CurrencyTypeDescription |
	| (CIBC) Canadian Imperial Bank of Commerce | Unallocated     | Dentons Canada LLP | Dentons Canada LLP - Application Off-Set Bank - CAD | Daily Rate Xe - Firm    |
@singapore
Examples:
	| Payer                                  | UnallocatedType | OperatingUnit                       | BankAccountDisplayName                  | CurrencyTypeDescription         |
	| Sydney - Domestic Client - Head office | Unidentified    | 1201 - Dentons Rodyk & Davidson LLP | ICB - 1201 - Singapore - ICB Bank - SGD | Daily - Azerbaijan Central Bank |

@training @staging @canada @europe @uk @singapore @ft @qa
Scenario: 030 Delete the receipt
	When I search for the document number
	Then delete the receipt
