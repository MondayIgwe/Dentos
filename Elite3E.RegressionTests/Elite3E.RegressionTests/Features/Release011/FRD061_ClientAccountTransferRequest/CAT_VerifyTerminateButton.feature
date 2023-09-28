Feature: VerifyTerminateButton

36777 - Verify that the Terminate button works in the Client Account Transfer Request process

@CancelProcess
Scenario Outline: 010 Prepare Data for matter 1
	Given I create a user with details
		| UserName        | DataRoleAlias | DefaultOperatingAlias   |
		| <FeeEarnerName> | Admin         | <DefaultOperatingAlias> |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	When I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And I create the Payer with Api
		| PayerName   | Entity        |
		| <PayorName> | RedBlocks Ltd |
	And I create a submatter 1 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Auto_IncludeALL     | Auto_IncludeALL   | <BillingOffice> | <PayorName> |
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser   |
		| Workflow User.Name | Equals          | null         | <FeeEarnerName> |

@training @canada @europe @uk @ft @qa
Examples:
	| FeeEarnerName          | Client                 | Currency                | CurrencyMethod | Office | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias          | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | AUD - Australian Dollar | Bill           | Sydney | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Sydney        | Dentons UK and Middle East LLP | Lisa Marvelle |
@singapore
Examples:
	| FeeEarnerName          | Client                 | Currency               | CurrencyMethod | Office    | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias        | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | SGD - Singapore Dollar | Bill           | Singapore | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Sydney        | Dentons Rodyk & Davidson LLP | Lisa Marvelle |
@staging
Examples:
	| FeeEarnerName          | Client                 | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice       | DefaultOperatingAlias          | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | London Billing (EU) | Dentons UK and Middle East LLP | Lisa Marvelle |

@CancelProcess
Scenario Outline: 020 Prepare Data for matter 2
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I search or create a client
		| Entity Name | FeeEarnerFullName | ResponsibleFeeEarnerName   | SupervisingFeeEarnerName   |
		| <Client>    | <FeeEarnerName>   | <ResponsibleFeeEarnerName> | <SupervisingFeeEarnerName> |
	And I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | BlackBox Ltd |
	And I create a submatter 2 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Auto_IncludeALL     | Auto_IncludeALL   | <BillingOffice> | <PayorName> |

@training @canada @europe @uk @ft @qa
Examples:
	| SupervisingFeeEarnerName | ResponsibleFeeEarnerName | Client             | OperatingUnit                  | FeeEarnerName          | UnallocatedType       | Currency                | CurrencyMethod | Office | Department | Section | Code | Description | BillingOffice | PayorName   |
	| Mark EarnerSurname       | Jade EarnerSurname       | Rose ClientSurname | Dentons UK and Middle East LLP | Transfer EarnerSurname | Duplicate/Overpayment | AUD - Australian Dollar | Bill           | Sydney | Default    | Default | CASH | CASH        | Sydney        | James Smith |
@singapore
Examples:
	| SupervisingFeeEarnerName | ResponsibleFeeEarnerName | Client             | OperatingUnit                | FeeEarnerName          | UnallocatedType       | Currency               | CurrencyMethod | Office    | Department | Section | Code | Description | BillingOffice | PayorName   |
	| Audrey Sim Mei Jun       | Carine Teo               | Rose ClientSurname | Dentons Rodyk & Davidson LLP | Transfer EarnerSurname | Duplicate/Overpayment | SGD - Singapore Dollar | Bill           | Singapore | Default    | Default | CASH | CASH        | Sydney        | James Smith |
@staging
Examples:
	| SupervisingFeeEarnerName | ResponsibleFeeEarnerName | Client             | OperatingUnit                | FeeEarnerName          | UnallocatedType       | Currency               | CurrencyMethod | Office    | Department | Section | Code | Description | BillingOffice | PayorName   |
	| Ann Rose                 | Ann Smith                | Rose ClientSurname | Dentons Rodyk & Davidson LLP | Transfer EarnerSurname | Duplicate/Overpayment | SGD - Singapore Dollar | Bill           | Singapore | Default    | Default | CASH | CASH        | Sydney        | James Smith |

@CancelProcess
Scenario Outline: 030 Verify the workflow is terminated and the form is closed
	Given I add a workflow user to a FeeEarner
		| User            | Name            |
		| <FeeEarnerName> | <FeeEarnerName> |
	Then I proxy as user '<FeeEarnerName>'
	When I make a client account transfer request
		| TransferType   | FromAccount   | ToAccount   | IntendedUse | Amount |
		| <TransferType> | <FromAccount> | <ToAccount> | General     | 100    |
	Then I terminate the process
	And I validate terminate was successfull

@ft
Examples:
	| TransferType              | FeeEarnerName          | FromAccount                       | ToAccount                         |
	| Matter to Matter Transfer | Transfer EarnerSurname | Sydney - WBC Bank Trust Acc - AUD | Sydney - ANZ Bank Trust Acc - AUD |
@singapore
Examples:
	| TransferType              | FeeEarnerName          | FromAccount          | ToAccount             |
	| Matter To Matter Transfer | Transfer EarnerSurname | MBB - Clients' S$ FD | HSBC - Clients' S$ FD |
