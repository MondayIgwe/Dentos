@us
Feature: VoucherMaintenance_BarristerFlagSetToTrue

The Barrister Boolean is available and can be edited in Cost Type/disbursement type
	Data can be entered into the 3 custom Barrister fields when creating vouchers.
	When the cost type has IsBarrister_ccc set to True, the 3 custom Barrister fields become Required.
	Attachments can be added to payees.

Scenario Outline: 010 Search or create disbursement type with barrister flag true
	Given I create a new Payee with the Api
		| PayeeName            |
		| Payee atVoucherMaint |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | EntityDigital |
	And I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-20 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Standard | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |

Examples:
	| Currency        | Client                       | Office  | OperationUnit              | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| USD - US Dollar | Client_Automation at_HTOvMOn | Chicago | Dentons United States, LLP | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |

Scenario Outline: 020 New Voucher When Barrister is True
	Given I search and create disbursement type with barrister flag
		| Code               | Description        | DisbursementCode   | IsBarristerFlag | HardOrSoftDisbursement | TransactionTypeAlias |
		| <DisbursementType> | <DisbursementType> | <DisbursementCode> | true            | IsHardCost             | Hard Cost            |
	Given I add a new voucher with voucher default information
		| Operation Unit  | Invoice Number | Invoice Date |
		| <OperationUnit> | v_             | {Today}      |
	And I add disbursement card details for voucher
		| Disbursement Type  | Narrative          | TaxCode   | InputTaxCode   | Voucher Amount |  |
		| <DisbursementType> | Automation Testing | <TaxCode> | <InputTaxCode> | 100            |  |
	And I validate the barrister fields are mandatory
	And I enter the barrister fields in voucher maintenance
		| Barrister Gender | Barrister Seniority | Barrister Name  |
		| Female           | 5 Years             | Tonia Barrister |
	And I update it
	And I add input amount "<InputAmount>" in voucher tax card
	When I submit the voucher
	Then I verify the voucher is created

Examples:
	| OperationUnit              | DisbursementType                        | DisbursementCode | TaxCode                          | InputTaxCode                    | InputAmount |
	| Dentons United States, LLP | (Gain)/Loss on Disposal of Fixed Assets | I960001          | US Output Domestic Standard Rate | US Input Domestic Standard Rate | 0           |
