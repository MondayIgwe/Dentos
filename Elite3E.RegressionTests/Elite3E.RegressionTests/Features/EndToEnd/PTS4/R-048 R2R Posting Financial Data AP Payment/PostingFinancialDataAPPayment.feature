Feature: PostingFinancialDataAPPayment

A short summary of the feature
R-048 - R2R: Posting Financial Data - AP Payment
https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/53169

@e2eft
Scenario Outline: 010 Perform Pre-requisite
	Given I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | CentralCoast |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |	
	And I create a matter with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | InputAmount   | PayorName   | BillingOffice |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Auto_IncludeALL     | Auto_IncludeALL   | <InputAmount> | <PayorName> | Milan         |
	And I create a new Payee with the Api
		| PayeeName            |
		| Payee atVoucherMaint |
Examples:
	| Client       | FeeEarnerName     | Office         | OperationUnit | DisbursementType | InputTaxCode                   | Currency            | TransactionType | VoucherStatus | GLAccount                              | VoucherGLTaxCode               | InputAmount | PayorName   |
	| Edward Trust | Billing FeeEarner | London (UKIME) | Firm          | Court Fees       | UK Input Domestic Standard 20% | GBP - British Pound | Direct Debit    | Approved      | 3000-101010-1000-0000-0000-0000-000000 | UA Input Domestic Standard 20% | 100.00      | James Mayor |

@e2eft
Scenario Outline: 020 Create a new entity
	Given the entity person process is opened
	When I enter the  person entity details
		| Entity Type | First Name | Last Name | Format Code | Relationship |
		| Employee    | {Auto}+10  | {Auto}+10 | Default     | Self         |
	And enter site details
		| Description | Site Type  | Country   | Street   | Language |
		| {Auto}+36   | <SiteType> | <Country> | <Street> | English  |
	Then I can submit the new entity details
	And I validate submit was successful

@e2eft
Examples:
	| SiteType | Country      | Street                         |
	| Billing  | SOUTH AFRICA | 10 Umhlanga rocks drive,Durban |

@CancelProcess
Scenario Outline: 030 Create a new vendor
	Given the vendor maintenace process is opened
	When I select the new entity
	And set the global vendor "<GlobalVendor>"
	Then I enter more vendor details
		| Vendor Type   | Vendor Category     |
		| Global Vendor | Chrome River Vendor |
	And I can verify that there are no duplicates
	And I want to add a new site
		| Description | SiteType | Street                         |
		| {Auto}+15   | Billing  | 10 Umhlanga rocks drive,Durban |
	Then I can submit the new vendor details
	#Then I verify the global vendor was created
	And I validate submit was successful

@e2eft
Examples:
	| GlobalVendor                 |
	| Dentons Client Global Vendor |

Scenario Outline: 040 Create a new Payee Maintenance
	Given I add the payee from payee maintenance
		| Payment Terms  | Office   | Site   | PayeeType   | Unit                           | Currency            | Is1099 | IRSNameControl |
		| <PaymentTerms> | <Office> | <Site> | <PayeeType> | Dentons UK and Middle East LLP | GBP - British Pound | false  |                |
	Then add the payee bank
		| DefaultBank   | Description           | Account Number  | Beneficiary Name  | Bank Code  | Address 1  | Address 2  | Address 3  | Clearing Code Type | Clearing Code  | Payment Reference  |
		| <DefaultBank> | Automation payee bank | <AccountNumber> | <BeneficiaryName> | <BankCode> | <Address1> | <Address2> | <Address3> | <ClearingCodeType> | <ClearingCode> | <PaymentReference> |
	And I update it
	Then I submit it
	And I validate submit was successful

@e2eft
Examples:
	| PaymentTerms | Office   | Site                                           | PayeeType | DefaultBank | AccountNumber | BeneficiaryName        | BankCode | Address1   | Address2 | Address3 | ClearingCodeType | ClearingCode | PaymentReference |
	| Immediate    | Aberdeen | London - Domestic - Non Vatable Local Currency | Employee  | Yes         | 8759686       | Automation Beneficiary | 366569   | 21 Bank St | Adelaide | SA 5000  | CTC Cash         | Cash         | PR-987654        |

Scenario Outline: 005 Create Vendor in Vendor/Payee Maintenance
	Given I navigate to the vendor/payee maintenance and I add a new record
	Then I enter the voucher created previously
	Then I set status to approved
	And I update it
	Then I submit it
	And I validate submit was successful
		

@e2eft
Scenario: 060 Create a Payment Selection Generation
	Given I navigate to the payment selection generation process
	Then I add a new payment selection generation record
	And I complete mandatory fields
		| Description | BankAccount                        | PaymentDate |
		| {Auto}+10   | London UKME - HSBC Off 1 Acc - GBP | {Today}     |
	When I add a new selection criteria
		| PaymentDate |
		| {Today}     |
	And I search for the voucher number
	And I test the selection
	And I verify the test result
	And I update it
	Then I submit it
	And I validate submit was successful