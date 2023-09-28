@release4 @frd047 @VoucherMaintenance
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
@ft
Examples:
	| Currency            | Client                       | Office         | OperationUnit                  | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| GBP - British Pound | Client_Automation at_HTOvMOn | London (UKIME) | Dentons UK and Middle East LLP | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |
@training
Examples:
	| Currency   | Client                       | Office         | OperationUnit                  | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| EUR - Euro | Client_Automation at_HTOvMOn | London (UKIME) | Dentons UK and Middle East LLP | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |
@staging
Examples:
	| Currency   | Client                       | Office         | OperationUnit                  | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| EUR - Euro | Client_Automation at_HTOvMOn | London (UKIME) | Dentons UK and Middle East LLP | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |
@qa
Examples:
	| Currency            | Client                       | Office         | OperationUnit                  | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| GBP - British Pound | Client_Automation at_HTOvMOn | London (UKIME) | Dentons UK and Middle East LLP | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |
@canada
Examples:
	| Currency              | Client                       | Office  | OperationUnit      | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| CAD - Canadian Dollar | Client_Automation at_HTOvMOn | Calgary | Dentons Canada LLP | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |
@europe
Examples:
	| Currency   | Client                       | Office      | OperationUnit           | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| EUR - Euro | Client_Automation at_HTOvMOn | London (EU) | Dentons Europe LLP (UK) | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |
@uk
Examples:
	| Currency            | Client                       | Office      | OperationUnit                  | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| GBP - British Pound | Client_Automation at_HTOvMOn | London (EU) | Dentons UK and Middle East LLP | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |
@singapore
Examples:
	| Currency               | Client                       | Office    | OperationUnit                | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| SGD - Singapore Dollar | Client_Automation at_HTOvMOn | Singapore | Dentons Rodyk & Davidson LLP | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |

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

@ft
Examples:
	| OperationUnit                  | DisbursementType | DisbursementCode | TaxCode                         | InputTaxCode                       | InputAmount |
	| Dentons UK and Middle East LLP | Barrister Fees   | HBarr            | UK Output Domestic Standard 20% | AE ICB Input  Domestic Standard 5% | 0           |
@training
Examples:
	| OperationUnit                  | DisbursementType | DisbursementCode | TaxCode                     | InputTaxCode               | InputAmount |
	| Dentons UK and Middle East LLP | Barrister Fees   | HBARISTER        | UK Output Domestic Standard | UK Input Domestic Standard | 20          |
@staging
Examples:
	| OperationUnit                  | DisbursementType | DisbursementCode | TaxCode                     | InputTaxCode               | InputAmount |
	| Dentons UK and Middle East LLP | Barrister Fees   | HBARISTER        | UK Output Domestic Standard | UK Input Domestic Standard | 20          |
@qa
Examples:
	| OperationUnit                  | DisbursementType | DisbursementCode | TaxCode                 | InputTaxCode                       | InputAmount |
	| Dentons UK and Middle East LLP | Barrister Fees   | HBARISTER        | UK Output Domestic Zero | AE ICB Input  Domestic Standard 5% | 0           |
@canada
Examples:
	| OperationUnit      | DisbursementType          | DisbursementCode | TaxCode                 | InputTaxCode           | InputAmount |
	| Dentons Canada LLP | CA Output BC Standard PST | HBARISTER        | CA Output Domestic Zero | CA Input Domestic Zero | 0           |
@europe
Examples:
	| OperationUnit           | DisbursementType | DisbursementCode | TaxCode                 | InputTaxCode           | InputAmount |
	| Dentons Europe LLP (UK) | Barrister Fees   | s HBARISTER      | PL Output Domestic Zero | PL Input Domestic Zero | 0           |
@singapore
Examples:
	| OperationUnit                | DisbursementType | DisbursementCode | TaxCode                 | InputTaxCode                    | InputAmount |
	| Dentons Rodyk & Davidson LLP | Barrister Fees   | BARISTER10       | SG Output Domestic Zero | MM Input Domestic Standard Rate | 20          |
@uk
Examples:
	| OperationUnit                  | DisbursementType | DisbursementCode | TaxCode                          | InputTaxCode               | InputAmount |
	| Dentons UK and Middle East LLP | Barrister Fees   | HBARISTER        | AZ Output Domestic Standard Rate | ES Input Domestic Standard | 0           |


