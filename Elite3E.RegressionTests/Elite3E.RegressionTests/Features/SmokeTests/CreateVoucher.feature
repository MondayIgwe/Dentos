@smoke @CreateVoucher
Feature: CreateVoucher
	Verify that a Voucher can be created in 3E for a vendor invoice received


@PipelineUser
Scenario Outline: 010 Create Voucher
	Given I create a new Payee with the Api
		| PayeeName            |
		| Payee atVoucherMaint |
	And  I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | SeasonsEntity |
	And I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | Rate     | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Standard | Desc_at_3e8glw7     | Desc_at_VowqCrR   | <PayorName> |
	And I add a new voucher with voucher default information
		| Operation Unit  | Invoice Number | Invoice Date |
		| <OperationUnit> | v_             | {Today}      |
	And I add disbursement card details for voucher
		| Disbursement Type  | Narrative          | TaxCode   | InputTaxCode   | Voucher Amount |
		| <DisbursementType> | Automation Testing | <TaxCode> | <InputTaxCode> | 100            |
	And I update it
	And I add input amount "100" in voucher tax card
	When I add a voucher direct form
		| Voucher Amount | GL Unit  | GL Natural  | GL Unit Local | GL Department | GL Section  | GL Office  | GL Timekeeper  | Disbursement Type  | VoucherGLTaxCode   | Input Amount |
		| 100            | <GLUnit> | <GLNatural> | <GLUnitLocal> | <GLDepart>    | <GLSection> | <GLOffice> | <GLTimekeeper> | <DisbursementType> | <VoucherGLTaxCode> | 100          |
	And I submit the voucher
	Then I verify the voucher is created

@ft
Examples:
	| Client                       | OperationUnit                  | DisbursementType   | TaxCode                         | GLUnit | GLNatural | GLUnitLocal | GLDepart | GLSection | GLOffice | GLTimekeeper | InputTaxCode               | Office         | VoucherGLTaxCode           | Currency            | PayorName  |
	| Client_Automation at_HTOvMOn | Dentons UK and Middle East LLP | Administration Fee | UK Output Domestic Standard 20% | 1001   | 101010    | 1000        | 0000     | 0000      | 8001     | 000000       | ES Input Domestic Standard | London (UKIME) | ES Input Domestic Standard | GBP - British Pound | Dave Peter |
@training
Examples:
	| Client                       | OperationUnit                  | DisbursementType | TaxCode                     | GLUnit | GLNatural | GLUnitLocal | GLDepart | GLSection | GLOffice | GLTimekeeper | InputTaxCode               | Office         | VoucherGLTaxCode           | Currency            | PayorName  |
	| Client_Automation at_HTOvMOn | Dentons UK and Middle East LLP | Accommodation    | UK Output Domestic Standard | 3000   | 208010    |             | 0000     | 0000      | 8053     | 000000       | ES Input Domestic Standard | London (UKIME) | ES Input Domestic Standard | GBP - British Pound | Dave Peter |
@staging
Examples:
	| Client                       | OperationUnit                  | DisbursementType            | TaxCode                     | GLUnit | GLNatural | GLUnitLocal | GLDepart | GLSection | GLOffice | GLTimekeeper | InputTaxCode               | Office         | VoucherGLTaxCode           | Currency            | PayorName  |
	| Client_Automation at_HTOvMOn | Dentons UK and Middle East LLP | Bank & Finance Charges (NT) | UK Output Domestic Standard | 3000   | 308510    | 0000        | 0000     | 0000      | 8054     | 000000       | ES Input Domestic Standard | London (UKIME) | ES Input Domestic Standard | GBP - British Pound | Dave Peter |
@uk
Examples:
	| Client                       | OperationUnit                  | DisbursementType | TaxCode                          | GLUnit | GLNatural | GLUnitLocal | GLDepart | GLSection | GLOffice | GLTimekeeper | InputTaxCode               | Office  | VoucherGLTaxCode           | Currency            | PayorName  |
	| Client_Automation at_HTOvMOn | Dentons UK and Middle East LLP | Agents Fees      | AZ Output Domestic Standard Rate | 1001   | 101010    |             | 0000     | 0000      | 8001     | 000000       | ES Input Domestic Standard | Default | ES Input Domestic Standard | GBP - British Pound | Dave Peter |
@qa
Examples:
	| Client                       | OperationUnit                  | DisbursementType          | TaxCode                         | GLUnit | GLNatural | GLUnitLocal | GLDepart | GLSection | GLOffice | GLTimekeeper | InputTaxCode               | Office         | VoucherGLTaxCode           | Currency            | PayorName  |
	| Client_Automation at_HTOvMOn | Dentons UK and Middle East LLP | Certificate of Conviction | UK Output Domestic Standard 20% | 1001   | 101010    |             | 0000     | 0000      | 8001     | 000000       | ES Input Domestic Standard | London (UKIME) | ES Input Domestic Standard | GBP - British Pound | Dave Peter |
@canada
Examples:
	| Client                       | OperationUnit      | DisbursementType                     | TaxCode                         | GLUnit | GLNatural | GLUnitLocal | GLDepart | GLSection | GLOffice | GLTimekeeper | InputTaxCode             | Office    | VoucherGLTaxCode         | Currency              | PayorName  |
	| Client_Automation at_HTOvMOn | Dentons Canada LLP | Court Filing Fees (NT) - Anticipated | CA Output Domestic Standard GST | 5801   | 103010    |             | 0000     | 0000      | 8019     | 000000       | CA Input Conversion Code | Vancouver | CA Input Conversion Code | CAD - Canadian Dollar | Dave Peter |
@europe
Examples:
	| Client                       | OperationUnit           | DisbursementType                     | TaxCode               | GLUnit | GLNatural | GLUnitLocal | GLDepart | GLSection | GLOffice | GLTimekeeper | InputTaxCode               | Office      | VoucherGLTaxCode           | Currency   | PayorName  |
	| Client_Automation at_HTOvMOn | Dentons Europe LLP (UK) | Court Filing Fees (NT) - Anticipated | ES Output Europe Zero | 3501   | 203350    | 3516        | 0000     | 0000      | 8159     | 000000       | ES Input Domestic Standard | London (EU) | ES Input Domestic Standard | EUR - Euro | Dave Peter |
@singapore
Examples:
	| Client                       | OperationUnit                       | DisbursementType                      | TaxCode                 | GLUnit | GLNatural | GLUnitLocal | GLDepart | GLSection | GLOffice | GLTimekeeper | InputTaxCode                    | Office    | VoucherGLTaxCode                | Currency               | PayorName  |
	| Client_Automation at_HTOvMOn | 1201 - Dentons Rodyk & Davidson LLP | DENSGHC-Dentons Cross Region Invoices | SG Output Domestic Zero | 1201   | 208010    |             | 1000     | 0000      | 8010     | 000000       | MM Input Domestic Standard Rate | Singapore | MM Input Domestic Standard Rate | SGD - Singapore Dollar | Dave Peter |
