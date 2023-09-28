@us
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

Examples:
	| Client                       | OperationUnit   | DisbursementType | TaxCode                          | GLUnit | GLNatural | GLUnitLocal | GLDepart | GLSection | GLOffice | GLTimekeeper | InputTaxCode                    | Office  | VoucherGLTaxCode         | Currency        | PayorName  |
	| Client_Automation at_HTOvMOn | Dentons US, LLP | Bank Fees        | US Output Domestic Standard Rate | 5000   | 301050    | 4580        | 0000     | 0000      | 8025     | 000000       | US Input Domestic Standard Rate | Chicago | US Input Conversion Code | USD - US Dollar | Dave Peter |
