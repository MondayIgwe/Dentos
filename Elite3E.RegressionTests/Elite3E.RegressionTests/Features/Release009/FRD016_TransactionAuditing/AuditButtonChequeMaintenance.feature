@ignore
Feature: AuditButtonChequeMaintenance

[Defect] Cheques without vouchers show an error when clicking the Audit button: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/35513
Borrowing from Release 4, FRD47 Voucher Maintenance for Data creation for this test.

Create Payee, Disbursement Type, Matter > Create Voucher > In Voucher Maintenance, Click Pay Now then you create a cheque with Voucher Data
You can now find Cheques in Cheque Maintenance that have vouchers
#Defect https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/83219
#CR https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/37653

@ft @training @staging @canada @europe @uk @singapore @us @qa
Scenario Outline: 010 SearchOrCreatePayee
	Given I create a new Payee with the Api
		| PayeeName            |
		| Payee atVoucherMaint |

Scenario Outline: 020 New Voucher Without Barrister Fields
	Given I create a hard cost disbursement type with details
		| Code               | Description        |
		| <DisbursementType> | <DisbursementType> |
	And I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | BrightEntity |
	And I create a matter with details:
		| Client   | Status     | OpenDate   | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | PayorName   |
		| <Client> | Fully Open | {Today}-20 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | <ChargeTypeGroup>   | <CostTypeGroup>   | <PayorName> |
    #Watch below step on new enviornments. Possible issues with Input Tax Amount and submitting.
	And I add a new voucher with voucher default information
		| Operation Unit  | Invoice Number | Invoice Date |
		| <OperationUnit> | v_             | {Today}      |
	And I add disbursement card details for voucher
		| Disbursement Type  | Narrative          | TaxCode   | InputTaxCode   | Voucher Amount |
		| <DisbursementType> | Automation Testing | <TaxCode> | <InputTaxCode> | 100            |
	And I update it
	And I add input amount "<InputAmount>" in voucher tax card
	When I submit the voucher
	And I validate submit was successful
	Then I verify the voucher is created

@ft
Examples:
	| Currency            | Client                       | Office         | OperationUnit                  | DisbursementType | TaxCode                         | InputTaxCode                | ChargeTypeGroup | CostTypeGroup | InputAmount | PayorName    |
	| GBP - British Pound | Client_Automation at_HTOvMOn | London (UKIME) | Dentons UK and Middle East LLP | Advertising      | UK Output Domestic Standard 20% | UK Input Domestic Exempt 0% | Desc_at_3e8glw7 | Auto__18Xeq   | 0           | Susan Wilson |
@staging
Examples:
	| Currency            | Client                       | Office         | OperationUnit                  | DisbursementType        | TaxCode                 | InputTaxCode                       | ChargeTypeGroup | CostTypeGroup | InputAmount | PayorName    |
	| GBP - British Pound | Client_Automation at_HTOvMOn | London (UKIME) | Dentons UK and Middle East LLP | Agency Fees - Attorneys | UK Output Domestic Zero | UK ICB Input  Domestic Standard 5% | Desc_at_3e8glw7 | Auto__18Xeq   | 20          | Susan Wilson |
@qa
Examples:
	| Currency            | Client                       | Office         | OperationUnit                  | DisbursementType                | TaxCode                 | InputTaxCode                       | ChargeTypeGroup | CostTypeGroup | InputAmount | PayorName    |
	| GBP - British Pound | Client_Automation at_HTOvMOn | London (UKIME) | Dentons UK and Middle East LLP | Building and  Zoning Compliance | UK Output Domestic Zero | AE ICB Input  Domestic Standard 5% | Desc_at_3e8glw7 | Auto__18Xeq   | 0           | Susan Wilson |
@uk
Examples:
	| Currency            | Client                       | Office         | OperationUnit                  | DisbursementType           | TaxCode                          | InputTaxCode               | ChargeTypeGroup | CostTypeGroup | InputAmount | PayorName    |
	| GBP - British Pound | Client_Automation at_HTOvMOn | London (UKIME) | Dentons UK and Middle East LLP | Dentons UK and Middle East | AZ Output Domestic Standard Rate | ES Input Domestic Standard | Desc_at_3e8glw7 | Auto__18Xeq   | 0           | Susan Wilson |
@europe
Examples:
	| Currency            | Client                       | Office      | OperationUnit           | DisbursementType | TaxCode                   | InputTaxCode             | ChargeTypeGroup | CostTypeGroup | InputAmount | PayorName    |
	| GBP - British Pound | Client_Automation at_HTOvMOn | London (EU) | Dentons Europe LLP (UK) | Dentons Europe   | EU Output Conversion Code | EU Input Conversion Code | Desc_at_3e8glw7 | Auto__18Xeq   | 0           | Susan Wilson |
@canada
Examples:
	| Currency              | Client                       | Office  | OperationUnit      | DisbursementType     | TaxCode                 | InputTaxCode           | ChargeTypeGroup | CostTypeGroup | InputAmount | PayorName    |
	| CAD - Canadian Dollar | Client_Automation at_HTOvMOn | Calgary | Dentons Canada LLP | Rent - Accommodation | CA Output Domestic Zero | CA Input Domestic Zero | Desc_at_3e8glw7 | Auto__18Xeq   | 0           | Susan Wilson |
@us
Examples:
	| Currency        | Client                       | Office  | OperationUnit              | DisbursementType        | TaxCode                          | InputTaxCode             | ChargeTypeGroup | CostTypeGroup | InputAmount | PayorName     |
	| USD - US Dollar | Client_Automation at_HTOvMOn | Chicago | Dentons United States, LLP | Agency Fees - Attorneys | US Output Domestic Standard Rate | US Input Conversion Code | Desc_at_3e8glw7 | Auto__18Xeq   | 0           | Susan William |
@singapore
Examples:
	| Currency               | Client                       | Office    | OperationUnit                | DisbursementType                      | TaxCode                 | InputTaxCode           | ChargeTypeGroup | CostTypeGroup | InputAmount | PayorName    |
	| SGD - Singapore Dollar | Client_Automation at_HTOvMOn | Singapore | Dentons Rodyk & Davidson LLP | DENSGHC-Dentons Cross Region Invoices | SG Output Domestic Zero | SG Input Domestic Zero | Desc_at_3e8glw7 | Auto__18Xeq   | 0           | Susan Wilson |
@training
Examples:
	| Currency            | Client                       | Office         | OperationUnit                  | DisbursementType | TaxCode                 | InputTaxCode               | ChargeTypeGroup | CostTypeGroup | InputAmount | PayorName    |
	| GBP - British Pound | Client_Automation at_HTOvMOn | London (UKIME) | Dentons UK and Middle East LLP | Agents Fees (NT) | AE Output Domestic Zero | UK Input Domestic Standard | Desc_at_3e8glw7 | Auto__18Xeq   | 20          | Susan Wilson |


Scenario Outline: 030 Pay Voucher Now
	Given I search for 'Voucher Maintenance'
	When I quick search by voucher number
	Then I create a cheque on the voucher using pay now
		| Bank Account  | ChequeNumber | Cheque Template | Cheque Printer | Voucher Status |
		| <BankAccount> | {Auto}+7     | TE_AP Check     | NO_PRINTER     | Approved       |
	And I submit it
	And I validate submit was successful
	
@ft @qa
Examples:
	| BankAccount                        |
	| London UKME - HSBC Off 1 Acc - GBP |
@training @staging @uk @us
Examples:
	| BankAccount                        |
	| American Savings Bank - Hawaii ASB |
@europe
Examples:
	| BankAccount                                    |
	| Dentons Europe UK HSBC - Operating Acc 1 - EUR |
@canada
Examples:
	| BankAccount                                         |
	| Dentons Canada LLP - Application Off-Set Bank - CAD |
@singapore
Examples:
	| BankAccount                                |
	| Singapore - Application Off-Set Bank - SGD |


@ft @qa @training @staging @canada @europe @uk @singapore @us
Scenario Outline: 040 Cheque Maintenance Audit Validation
	Given I search for 'Cheque Maintenance'
	And I advanced find and select unlocked record
		| Search Column                   | Search Operator | Search Value |
		| Voucher Data.Voucher.Voucher ID | Not Equal To    | null         |
	When I verify audit button in process 'Cheque Maintenance'
	Then I cancel the process