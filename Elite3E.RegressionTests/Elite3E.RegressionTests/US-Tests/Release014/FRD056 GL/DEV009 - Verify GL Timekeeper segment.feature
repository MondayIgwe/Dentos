@us
Feature: DEV009 - Verify GL Timekeeper segment

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/61400

@CancelProcess
Scenario Outline: 010 Create a voucher with disbursement card
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity            |
		| <PayorName> | WhiteWater Entity |
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroup>   | <CostTypeGroup>   | <BillingOffice> | <PayorName> |
	And I create a new Payee with the Api
		| PayeeName            |
		| Payee atVoucherMaint |
	And I add a new voucher with voucher default information
		| Operation Unit  | Invoice Number | Invoice Date | Transaction Type  | Voucher Status  |
		| <OperationUnit> | v_             | {Today}      | <TransactionType> | <VoucherStatus> |
	And I add disbursement card details for voucher
		| Disbursement Type  | Narrative          | InputTaxCode   | Voucher Amount |
		| <DisbursementType> | Automation Testing | <InputTaxCode> | 100.00         |
	And I update it
	And I add input amount "<InputAmount>" in voucher tax card
	When I submit the voucher
	Then I verify the voucher is created

Examples:
  | FeeEarnerName | User           | Client     | Currency        | CurrencyMethod | Office  | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | PayorName  | RemittanceAccount    | OperationUnit | TransactionType | VoucherStatus | DisbursementType | InputTaxCode                     | InputAmount |
  | Matt Thomas   | Matthew Thomas | Bella Matt | USD - US Dollar | Bill           | Chicago | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Montreal      | James Matt | LAMMERS BARREL GROUP | Firm          | Direct Debit    | Direct Debit  | Court Fees       | US Output Domestic Standard Rate | 100         |


Scenario Outline: 020 Verify GL Postings timekeeper
	Given I search for process 'Voucher Maintenance' without add button
	When I quick search by voucher number
	And I view the gl postings
	Then I validate the gl timekeeper is '<Timekeeper>'


Examples:
	| Timekeeper |
	| 000000     |
