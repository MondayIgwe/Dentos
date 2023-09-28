@release6 @frd032 @SuperBill_TimeCostCards
Feature: SuperBill_TimeCostCards
	Test 1: Superbill includes pre-split details of time cards.
	Test 2: Superbill includes pre-split details of cost cards.

Scenario: 010 Includes pre-split details of time cards
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | FrontPage Ltd |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | Rate     | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | Standard | <PayorName> |
	And I create a submatter 1 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | Rate     | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | Standard | <PayorName> |
	And I create a submatter 2 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | Rate     | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Desc_at_3e8glw7     | Desc_at_3e8glw7   | Standard | <PayorName> |
	When I add submatters
		| Split Description  | Split Type  | Split Percentage  |
		| <SplitDescription> | <SplitType> | <SplitPercentage> |
	And I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | TaxCode   |
		| <TimeType> | 0.1   | {Auto}+10 | <FeeEarnerName> | 1        | 0.1        | <Currency>   | <TaxCode> |
	And I submit a time modify
		| Time Type  | Hours | Narrative | FeeEarnerName   | WorkRate | WorkAmount | WorkCurrency | TaxCode   |
		| <TimeType> | 0.1   | {Auto}+10 | <FeeEarnerName> | 1        | 0.1        | <Currency>   | <TaxCode> |
	And can generate edit the proforma
		| Description | Change Status To | ProformaStatus |
		| {Auto}+10   | <ChangeStatusTo> | Draft          |
	Then I can bill the submatters
		| To Tax Area |
		| <ToTaxArea> |
@europe
Examples:
	| Client                      | FeeEarnerName                  | SplitDescription | SplitType              | SplitPercentage | TimeType | Hours1 | Hours2 | TaxCode                   | ChangeStatusTo | ToTaxArea      | Currency | Office      | PayorName    |
	| SplitBillClient NumberThree | SplitBillFeeEarner NumberThree | 50/50            | Split in Proforma Edit | 50              | FEES     | 0.10   | 0.20   | EU Output Conversion Code | Terminated     | United Kingdom | EUR      | London (EU) | Emilio Price |

@ft @qa
Examples:
	| Client                      | FeeEarnerName                  | SplitDescription | SplitType              | SplitPercentage | TimeType       | Hours1 | Hours2 | TaxCode                    | ChangeStatusTo | ToTaxArea      | Currency            | Office         | PayorName    |
	| SplitBillClient NumberThree | SplitBillFeeEarner NumberThree | 50/50            | Split in Proforma Edit | 50              | FEES (Default) | 0.01   | 0.20   | UK Output Domestic Zero 0% | Terminated     | United Kingdom | GBP - British Pound | London (UKIME) | Emilio Price |
@training @staging
Examples:
	| Client                      | FeeEarnerName                  | SplitDescription | SplitType              | SplitPercentage | TimeType | Hours1 | Hours2 | TaxCode                 | ChangeStatusTo | ToTaxArea      | Currency            | Office      | PayorName    |
	| SplitBillClient NumberThree | SplitBillFeeEarner NumberThree | 50/50            | Split in Proforma Edit | 50              | FEES     | 0.10   | 0.20   | AE Output Domestic Zero | Terminated     | United Kingdom | GBP - British Pound | London (EU) | Emilio Price |
@uk
Examples:
	| Client                      | FeeEarnerName                  | SplitDescription | SplitType              | SplitPercentage | TimeType       | Hours1 | Hours2 | TaxCode                 | ChangeStatusTo | ToTaxArea      | Currency            | Office         | PayorName    |
	| SplitBillClient NumberThree | SplitBillFeeEarner NumberThree | 50/50            | Split in Proforma Edit | 50              | FEES (Default) | 0.10   | 0.20   | UK Output Domestic Zero | Terminated     | United Kingdom | GBP - British Pound | London (UKIME) | Emilio Price |
@canada
Examples:
	| Client                      | FeeEarnerName                  | SplitDescription | SplitType              | SplitPercentage | TimeType | Hours1 | Hours2 | TaxCode                         | ChangeStatusTo | ToTaxArea | Currency              | Office    | PayorName    |
	| SplitBillClient NumberThree | SplitBillFeeEarner NumberThree | 50/50            | Split in Proforma Edit | 50              | FEES     | 0.10   | 0.20   | CA Output Domestic Standard GST | Terminated     | Canada    | CAD - Canadian Dollar | Vancouver | Emilio Price |
@singapore
Examples:
	| Client                      | FeeEarnerName                  | SplitDescription | SplitType              | SplitPercentage | TimeType | Hours1 | Hours2 | TaxCode                     | ChangeStatusTo | ToTaxArea | Currency               | Office    | PayorName    |
	| SplitBillClient NumberThree | SplitBillFeeEarner NumberThree | 50/50            | Split in Proforma Edit | 50              | FEES     | 0.10   | 0.20   | SG Output Domestic Standard | Terminated     | Singapore | SGD - Singapore Dollar | Singapore | Emilio Price |


Scenario: 020 Includes pre-split details of cost cards
	Given I create a hard cost disbursement type with details
		| Code           | Description    |
		| <Disbursement> | <Disbursement> |
	And I add a disbursement entry
		| Disbursement Type | Work Currency | Work Amount | Narrative | Tax Code  |
		| <Disbursement>    | <Currency>    | <Amount>    | {Auto}+10 | <TaxCode> |
	When can generate edit the proforma
		| Description | ChangeStatusTo   | ProformaStatus |
		| {Auto}+10   | <ChangeStatusTo> | Draft          |
	Then I can bill the submatters
		| To Tax Area |
		| <ToTaxArea> |

@europe
Examples:
	| Disbursement | Currency | Amount | TaxCode                   | ChangeStatusTo | ToTaxArea      |
	| Cards        | EUR      | 300    | EU Output Conversion Code | Terminated     | United Kingdom |
@ft
Examples:
	| Disbursement | Currency | Amount | TaxCode                    | ChangeStatusTo | ToTaxArea      |
	| Cards        | GBP      | 300    | UK Output Domestic Zero 0% | Terminated     | United Kingdom |
@qa
Examples:
	| Disbursement     | Currency | Amount | TaxCode                    | ChangeStatusTo | ToTaxArea      |
	| Agents Fees (NT) | GBP      | 300    | UK Output Domestic Zero 0% | Terminated     | United Kingdom |
@training
Examples:
	| Disbursement | Currency | Amount | TaxCode                 | ChangeStatusTo | ToTaxArea      |
	| Cards        | GBP      | 300    | UK Output Domestic Zero | Terminated     | United Kingdom |
@staging
Examples:
	| Disbursement | Currency | Amount | TaxCode                 | ChangeStatusTo | ToTaxArea      |
	| Accomodation | GBP      | 300    | UK Output Domestic Zero | Terminated     | United Kingdom |
@uk
Examples:
	| Disbursement        | Currency | Amount | TaxCode                 | ChangeStatusTo | ToTaxArea      |
	| Dentons Taiwan Cost | GBP      | 300    | UK Output Domestic Zero | Terminated     | United Kingdom |
@canada
Examples:
	| Disbursement                          | Currency | Amount | TaxCode                         | ChangeStatusTo | ToTaxArea |
	| Court & Stamp Fees - Anticipated (NT) | CAD      | 300    | CA Output Domestic Standard GST | Terminated     | Canada    |
@singapore
Examples:
	| Disbursement          | Currency               | Amount | TaxCode                     | ChangeStatusTo | ToTaxArea |
	| Application Fees (NT) | SGD - Singapore Dollar | 300    | SG Output Domestic Standard | Terminated     | Singapore |