@us
Feature: VoucherMaintenanceFeature_BarristerFlagSetToFalse
The Barrister Boolean is available and can be edited in Cost Type/disbursement type
	Data can be entered into the 3 custom Barrister fields when creating vouchers.
	When the cost type has IsBarrister_ccc set to True, the 3 custom Barrister fields become Required.
	Attachments can be added to payees.

Scenario Outline: 010 Search or create disbursement type with barrister flag false
	Given I create a new Payee with the Api
		| PayeeName            |
		| Payee atVoucherMaint |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | EntityDigital |
	And I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-20 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Standard | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |
	Given I search and create disbursement type with barrister flag
		| Code               | Description        | DisbursementCode   | IsBarristerFlag | HardOrSoftDisbursement | TransactionTypeAlias |
		| <DisbursementType> | <DisbursementType> | <DisbursementCode> | false           | IsHardCost             | Hard Cost            |


Examples:
	| Currency        | Client                       | Office  | OperationUnit              | DisbursementType | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| USD - US Dollar | Client_Automation at_HTOvMOn | Chicago | Dentons United States, LLP | Dentons US       | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |  


Scenario Outline: 020 New Voucher Without Barrister Fields
	Given I add a new voucher with voucher default information
		| Operation Unit  | Invoice Number | Invoice Date |
		| <OperationUnit> | v_             | {Today}      |
	And I add disbursement card details for voucher
		| Disbursement Type  | Narrative          | TaxCode   | InputTaxCode   | Voucher Amount |
		| <DisbursementType> | Automation Testing | <TaxCode> | <InputTaxCode> | 100            |
    And I validate the barrister fields are not mandatory
	And I update it
	And I add input amount "<InputAmount>" in voucher tax card
	When I submit the voucher
	Then I verify the voucher is created

Examples:
	| OperationUnit              | DisbursementType | TaxCode                          | InputTaxCode                    | InputAmount |
	| Dentons United States, LLP | Dentons US       | US Output Domestic Standard Rate | US Input Domestic Standard Rate | 0           |

Scenario Outline: 030 New Voucher With Barrister Fields when Barrister Flag is False
	Given I add a new voucher with voucher default information
		| Operation Unit  | Invoice Number | Invoice Date |
		| <OperationUnit> | v_             | {Today}      |
	And I add disbursement card details for voucher
		| Disbursement Type  | Narrative          | TaxCode   | InputTaxCode   | Voucher Amount |  
		| <DisbursementType> | Automation Testing | <TaxCode> | <InputTaxCode> | 100            |  
    And I validate the barrister fields are not mandatory
	And I enter the barrister fields in voucher maintenance
		| Barrister Gender | Barrister Seniority  | Barrister Name  |
		| Female           | <BarristerSeniority> | <BarristerName> |
	And I update it
	And I add input amount "<InputAmount>" in voucher tax card
	When I submit the voucher
	Then I verify the voucher is created

Examples:
	| OperationUnit              | DisbursementType | TaxCode                          | BarristerSeniority | BarristerName   | InputTaxCode                    | InputAmount |
	| Dentons United States, LLP | Dentons US       | US Output Domestic Standard Rate | 5 Years            | Tonia Barrister | US Input Domestic Standard Rate | 0           |

Scenario Outline: 040 Add Attachments to Payee
	Given I create a new Payee with the Api
		| PayeeName |
		| <Payee>   |
	And I select an existing payee
	When I add an attachment
		| File           |
		| Attachment.txt |
	Then the attachment number is shown
		| Number of Attachments |
		| 1                     |

Examples:
	| Payee                |
	| Aberdeen EmployeeEmp |
