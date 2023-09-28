@ignore
Feature: ClientAccountDisbursementRequest

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/57383 - Open defect : Request not going to the workflow inbox 

We still have to update Examples for other enviroments other than Ft currently data applied has not been tested 

Scenario: 005 Create Finance approval user
	Given I create a user with details
		| UserName | DataRoleAlias   | DefaultOperatingAlias   | UserRoleList   |
		| <User1>  | <DataRoleAlias> | <DefaultOperatingAlias> | <UserRoleList> |
	Then I submit it
@training @staging @canada @europe @uk @singapore @ft @qa
Examples:
	| User1       | DataRoleAlias | DefaultOperatingAlias     | UserRoleList                                                                                                                                                                                                                |
	| FRD061_User | Admin         | Dentons Australia Limited | 0:WC:R:Client Account Processor: 1001: 8001,0:WC:R:Client Account Processor: 1001: 8006,0:WC:R:Client Account Processor: 1001: 8007,0:WC:R:Client Account Processor: 1001: 8008,0:WC:R:Client Account Processor: 1001: 8009 |


Scenario Outline: 010 Prepare Data for Workflow User for Fee Earner 1
	Given I create a user with details
		| UserName        | DataRoleAlias | DefaultOperatingAlias   |
		| <FeeEarnerName> | Admin         | <DefaultOperatingAlias> |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I add a workflow user to a FeeEarner
	| User            | Name            |
	| <FeeEarnerName> | <FeeEarnerName> |
	When I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | RedBlocks Ltd |
	And I create a submatter 1 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Auto_IncludeALL     | Auto_IncludeALL   | <BillingOffice> | <PayorName> |

@training @staging @ft @qa
Examples:
	| FeeEarnerName          | Client                 | Currency                | CurrencyMethod | Office | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias          | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | AUD - Australian Dollar | Bill           | Sydney | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Sydney        | Dentons UK and Middle East LLP | Lisa Marvelle |

@canada
Examples:
	| FeeEarnerName          | Client                 | Currency              | CurrencyMethod | Office    | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | CAD - Canadian Dollar | Bill           | Vancouver | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Sydney        | Dentons Canada LLP    | Lisa Marvelle |
@uk
Examples:
	| FeeEarnerName          | Client                 | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias          | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Sydney        | Dentons UK and Middle East LLP | Lisa Marvelle |
@europe
Examples:
	| FeeEarnerName          | Client                 | Currency | CurrencyMethod | Office      | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias   | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | EUR      | Bill           | London (EU) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Sydney        | Dentons Europe LLP (UK) | Lisa Marvelle |
@singapore
Examples:
	| FeeEarnerName          | Client                 | Currency               | CurrencyMethod | Office    | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias      | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | SGD - Singapore Dollar | Bill           | Singapore | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Sydney        | Dentons United States, LLP | Lisa Marvelle |

Scenario Outline: 013 receipt type with details:
	Given I create a receipt type with details:
		| Code   | Description   | Tolerance Amount | Tolerance Percentage |
		| <Code> | <Description> | 50.00            | 50.00                |
	And I add a new receipt
		| Receipt Date |
		| {Today}      |
	And change the operating unit "<OperatingUnit>"
	And I update the receipt
		| Narrative |
		| {Auto}+12 |
	When I add an unallocated child form for submatter 1
		| Unallocated Type  | Receipt Amount | Child Form  |
		| <UnallocatedType> | 5000           | Unallocated |
	And I receipt the total amount
	And I update the receipt
		| Document Number |
		| {Auto}+36       |
	Then the payer is auto populated
	And I can submit the receipt
	And I validate submit was successful

@training @staging @ft @qa
Examples:
	| Client                 | OperatingUnit                  | FeeEarnerName          | UnallocatedType       | Currency            | CurrencyMethod | Office         | Department | Section | Code | Description | BillingOffice |
	| Transfer ClientSurname | Dentons UK and Middle East LLP | Transfer EarnerSurname | Duplicate/Overpayment | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | CASH | CASH        | Sydney        |
@canada
Examples:
	| Client                 | OperatingUnit                  | FeeEarnerName          | UnallocatedType       | Currency              | CurrencyMethod | Office    | Department | Section | Code | Description | BillingOffice |
	| Transfer ClientSurname | Dentons UK and Middle East LLP | Transfer EarnerSurname | Duplicate/Overpayment | CAD - Canadian Dollar | Bill           | Vancouver | Default    | Default | CASH | CASH        | Sydney        |
@europe
Examples:
	| Client                 | OperatingUnit                  | FeeEarnerName          | UnallocatedType       | Currency | CurrencyMethod | Office      | Department | Section | Code | Description | BillingOffice |
	| Transfer ClientSurname | Dentons UK and Middle East LLP | Transfer EarnerSurname | Duplicate/Overpayment | EUR      | Bill           | London (EU) | Default    | Default | CASH | CASH        | Sydney        |
@uk
Examples:
	| Client                 | OperatingUnit                  | FeeEarnerName          | UnallocatedType       | Currency            | CurrencyMethod | Office         | Department | Section | Code | Description | BillingOffice |
	| Transfer ClientSurname | Dentons UK and Middle East LLP | Transfer EarnerSurname | Duplicate/Overpayment | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | CASH | CASH        | Sydney        |
@singapore
Examples:
	| Client                 | OperatingUnit                  | FeeEarnerName          | UnallocatedType       | Currency               | CurrencyMethod | Office    | Department | Section | Code | Description | BillingOffice |
	| Transfer ClientSurname | Dentons UK and Middle East LLP | Transfer EarnerSurname | Duplicate/Overpayment | SGD - Singapore Dollar | Bill           | Singapore | Default    | Default | CASH | CASH        | Sydney        |


Scenario: 020 I want to create a Client Account Disbursement Request
	Given I search for process with option
		| search                              | option                            |
		| Client Account Disbursement Request | (WF_TrustDisbursementRequest_ccc) |
	When I create the client account disbursement request
		| DisbursementType    | Matter   | ClientAccountAcct     | IntendedUse    | Amount   | PaymentName    |
		| <Disbursement Type> | <Matter> | <Client Account Acct> | <Intended Use> | <Amount> | <Payment Name> |
	Then I want to submit and get an error message
		| message                                             |
		| Client approval must be obtained before continuing. |
	Then I confirm IsClientApprovalObtained
		| IsClientApprovalObtained   |
		| <IsClientApprovalObtained> |
	And I want to submit and get an error message
		| message                                                 |
		| Payment information must be verified before continuing. |
	Then I confirm IsPaymentInformationVerified
		| IsPaymentInformationVerified   |
		| <IsPaymentInformationVerified> |
	Then I submit it
	And I validate submit was successful

@training @staging @ft @qa
Examples:
	| Disbursement Type | Matter    | Client Account Acct               | Intended Use | Amount   | IsPaymentInformationVerified | IsClientApprovalObtained | Payment Name    |
	| Completion BACS   | 100050014 | Sydney - ANZ Bank Trust Acc - AUD | General      | 1,234.00 | true                         | true                     | Automation Test |
@canada
Examples:
	| Disbursement Type          | Matter    | Client Account Acct               | Intended Use | Amount   | IsPaymentInformationVerified | IsClientApprovalObtained | Payment Name    |
	| Bank of Canada Certificate | 100050014 | Sydney - ANZ Bank Trust Acc - AUD | General      | 1,234.00 | true                         | true                     | Automation Test |
@uk
Examples:
	| Disbursement Type                   | Matter    | Client Account Acct               | Intended Use | Amount   | IsPaymentInformationVerified | IsClientApprovalObtained | Payment Name    |
	| DENUK-Dentons Cross Region Invoices | 100050014 | Sydney - ANZ Bank Trust Acc - AUD | General      | 1,234.00 | true                         | true                     | Automation Test |
@singapore
Examples:
	| Disbursement Type                   | Matter    | Client Account Acct               | Intended Use | Amount   | IsPaymentInformationVerified | IsClientApprovalObtained | Payment Name    |
	| DENSG-Dentons Cross Region Invoices | 100050014 | Sydney - ANZ Bank Trust Acc - AUD | General      | 1,234.00 | true                         | true                     | Automation Test |
@europe
Examples:
	| Disbursement Type                   | Matter    | Client Account Acct               | Intended Use | Amount   | IsPaymentInformationVerified | IsClientApprovalObtained | Payment Name    |
	| DENEU-Dentons Cross Region Invoices | 100050014 | Sydney - ANZ Bank Trust Acc - AUD | General      | 1,234.00 | true                         | true                     | Automation Test |

@training @staging @canada @europe @uk @singapore @ft @qa
Scenario: 030 I want to approve the Client Account Disbursement Request
	Given I proxy as user 'FRD061_User'
	When I search for 'Workflow Dashboard'
	Then I open the request
	Then I validate feilds are available
	Then I validate feilds are not available
	When I add an attachment
		| File           |
		| Attachment.txt |
	Then the attachment number is shown
		| Number of Attachments |
		| 1                     |
	And I want to click approve
	Then I submit it
	And I validate submit was successful
	
@training @staging @canada @europe @uk @singapore @ft @qa
Scenario: 040 I want to proxy as billing time keeper and approve the Client Account Disbursement Request
	Given I proxy as user 'Sydney - Associate Std'
	When I search for 'Workflow Dashboard'
	Then I open the request
	Then I validate feilds are available
	Then I validate feilds are not available
	And I cancel the process


@training @staging @canada @europe @uk @singapore @ft @qa
Scenario: 050 I want to proxy as billing time keeper and approve the Client Account Disbursement Request
	When I open the request
	And I want to approve it
	Then I submit it
	And I validate submit was successful