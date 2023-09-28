@release4 @frd047 @VoucherMaintenance
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


@ft
Examples:
	| Currency            | Client                       | Office         | OperationUnit                  | DisbursementType                                       | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| GBP - British Pound | Client_Automation at_HTOvMOn | London (UKIME) | Dentons UK and Middle East LLP | Registraton Fees - Issuance of SSCT - Anticipated (NT) | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |
@training
Examples:
	| Currency   | Client                       | Office         | OperationUnit                  | DisbursementType            | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| EUR - Euro | Client_Automation at_HTOvMOn | London (UKIME) | Dentons UK and Middle East LLP | Bankruptcy Certificate (NT) | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |
@staging
Examples:
	| Currency   | Client                       | Office         | OperationUnit                  | DisbursementType            | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| EUR - Euro | Client_Automation at_HTOvMOn | London (UKIME) | Dentons UK and Middle East LLP | Bank & Finance Charges (NT) | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |
@qa
Examples:
	| Currency            | Client                       | Office         | OperationUnit                  | DisbursementType       | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| GBP - British Pound | Client_Automation at_HTOvMOn | London (UKIME) | Dentons UK and Middle East LLP | Bankruptcy Certificate | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |
@canada
Examples:
	| Currency              | Client                       | Office  | OperationUnit      | DisbursementType          | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| CAD - Canadian Dollar | Client_Automation at_HTOvMOn | Calgary | Dentons Canada LLP | CA Output BC Standard PST | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |
@europe
Examples:
	| Currency   | Client                       | Office      | OperationUnit           | DisbursementType            | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| EUR - Euro | Client_Automation at_HTOvMOn | London (EU) | Dentons Europe LLP (UK) | Bankruptcy Certificate (NT) | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |
@uk
Examples:
	| Currency            | Client                       | Office      | OperationUnit                  | DisbursementType           | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| GBP - British Pound | Client_Automation at_HTOvMOn | London (EU) | Dentons UK and Middle East LLP | Dentons UK and Middle East | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |
@singapore
Examples:
	| Currency               | Client                       | Office    | OperationUnit                | DisbursementType  | ChargeTypeGroup | CostTypeGroup | PayorName    |
	| SGD - Singapore Dollar | Client_Automation at_HTOvMOn | Singapore | Dentons Rodyk & Davidson LLP | Registration Fees | Desc_at_3e8glw7 | Auto__18Xeq   | Lucas Wright |


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

@ft
Examples:
	   | OperationUnit                  | DisbursementType                                       | TaxCode                         | InputTaxCode                       | InputAmount |
	   | Dentons UK and Middle East LLP | Registraton Fees - Issuance of SSCT - Anticipated (NT) | UK Output Domestic Standard 20% | AE ICB Input  Domestic Standard 5% | 0           |
@training
Examples:
	 | OperationUnit                  | DisbursementType            | TaxCode                 | InputTaxCode               | InputAmount |
	 | Dentons UK and Middle East LLP | Bankruptcy Certificate (NT) | AE Output Domestic Zero | UK Input Domestic Standard | 20          |
@staging
Examples:
	 | OperationUnit                  | DisbursementType            | TaxCode                     | InputTaxCode               | InputAmount |
	 | Dentons UK and Middle East LLP | Bank & Finance Charges (NT) | UK Output Domestic Standard | UK Input Domestic Standard | 20          |
@qa
Examples:
	 | OperationUnit                  | DisbursementType       | TaxCode                 | InputTaxCode                       | InputAmount |
	 | Dentons UK and Middle East LLP | Bankruptcy Certificate | UK Output Domestic Zero | AE ICB Input  Domestic Standard 5% | 0           |
@canada
Examples:
	 | OperationUnit      | DisbursementType          | TaxCode                         | InputTaxCode                   | InputAmount |
	 | Dentons Canada LLP | CA Output BC Standard PST | CA Output Domestic Standard GST | CA Input Domestic Standard GST | 0           |
@europe
Examples:
	  | OperationUnit           | DisbursementType            | TaxCode                 | InputTaxCode           | InputAmount |
	  | Dentons Europe LLP (UK) | Bankruptcy Certificate (NT) | PL Output Domestic Zero | PL Input Domestic Zero | 0           |
@uk
Examples:
	| OperationUnit                  | DisbursementType           | TaxCode                          | InputTaxCode               | InputAmount |
	| Dentons UK and Middle East LLP | Dentons UK and Middle East | AZ Output Domestic Standard Rate | ES Input Domestic Standard | 0           |
@singapore
Examples:
	| OperationUnit                | DisbursementType                      | TaxCode                 | InputTaxCode                    | InputAmount |
	| Dentons Rodyk & Davidson LLP | Registration Fees | SG Output Domestic Zero | SG Input Domestic Zero | 0          |

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

@ft
Examples:
	| OperationUnit                  | DisbursementType                                       | TaxCode                         | BarristerSeniority | BarristerName   | InputTaxCode                       | InputAmount |
	| Dentons UK and Middle East LLP | Registraton Fees - Issuance of SSCT - Anticipated (NT) | UK Output Domestic Standard 20% | 5 Years            | Tonia Barrister | AE ICB Input  Domestic Standard 5% | 0           |
@training
Examples:
	| OperationUnit                  | DisbursementType            | TaxCode                     | BarristerSeniority | BarristerName   | InputTaxCode               | InputAmount |
	| Dentons UK and Middle East LLP | Bankruptcy Certificate (NT) | UK Output Domestic Standard | 5 Years            | Tonia Barrister | UK Input Domestic Standard | 20          |
@staging
Examples:
	| OperationUnit                  | DisbursementType            | TaxCode                     | BarristerSeniority | BarristerName   | InputTaxCode               | InputAmount |
	| Dentons UK and Middle East LLP | Bank & Finance Charges (NT) | UK Output Domestic Standard | 5 Years            | Tonia Barrister | UK Input Domestic Standard | 20          |
@canada
Examples:
	| OperationUnit      | DisbursementType          | TaxCode                         | BarristerSeniority | BarristerName   | InputTaxCode                   | InputAmount |
	| Dentons Canada LLP | CA Output BC Standard PST | CA Output Domestic Standard GST | 5 Years            | Tonia Barrister | CA Input Domestic Standard GST | 0           |
@europe
Examples:
	| OperationUnit           | DisbursementType            | TaxCode                 | BarristerSeniority | BarristerName   | InputTaxCode           | InputAmount |
	| Dentons Europe LLP (UK) | Bankruptcy Certificate (NT) | PL Output Domestic Zero | 5 Years            | Tonia Barrister | PL Input Domestic Zero | 0           |
@uk
Examples:
	| OperationUnit                  | DisbursementType           | TaxCode                          | BarristerSeniority | BarristerName   | InputTaxCode               | InputAmount |
	| Dentons UK and Middle East LLP | Dentons UK and Middle East | AZ Output Domestic Standard Rate | 5 Years            | Tonia Barrister | ES Input Domestic Standard | 0           |
@singapore
Examples:
	| OperationUnit                | DisbursementType  | TaxCode                 | BarristerSeniority | BarristerName   | InputTaxCode           | InputAmount |
	| Dentons Rodyk & Davidson LLP | Registration Fees | SG Output Domestic Zero | 5 Years            | Tonia Barrister | SG Input Domestic Zero | 0           |
@qa
Examples:
	| OperationUnit                  | DisbursementType       | TaxCode                 | BarristerSeniority | BarristerName   | InputTaxCode                       | InputAmount |
	| Dentons UK and Middle East LLP | Bankruptcy Certificate | UK Output Domestic Zero | 5 Years            | Tonia Barrister | AE ICB Input  Domestic Standard 5% | 0           |

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
@ft @training @staging @canada @europe @uk @singapore @qa
Examples:
	| Payee                |
	| Aberdeen EmployeeEmp |
