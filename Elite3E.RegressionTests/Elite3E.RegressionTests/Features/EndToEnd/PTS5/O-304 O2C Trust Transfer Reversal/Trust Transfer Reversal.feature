Feature: Trust Transfer Reversal

Fee Earner/Secretary request for the transfer to be reversed.
Azure Link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/56819

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
	Then I create a submatter 1 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Auto_IncludeALL     | Auto_IncludeALL   | <BillingOffice> | <PayorName> |

@e2eft
Examples:
	| FeeEarnerName          | Client                 | Currency                | CurrencyMethod | Office | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias          | PayorName     |
	| Transfer EarnerSurname | Transfer ClientSurname | AUD - Australian Dollar | Bill           | Sydney | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Sydney        | Dentons UK and Middle East LLP | Lisa Marvelle |

@CancelProcess
Scenario Outline: 020 Create a receipt request to load balance for matter 1 account
	Given I 'add' a workflow user '<FeeEarnerName>' to fee earner '<FeeEarnerName>'
	Then I proxy as user '<FeeEarnerName>'
	Then I open the client account receipt request process
	And I set the aml checks complete checkbox to true
	And I complete all the client account receipt required fields
		| Narrative | TransactionDate | Amount | Reason                         |
		| {Auto}+10 | {Today}-1       | 100    | Client Account Receipt Request |
	When the approval checkbox is checked
	And I submit it
	And I validate submit was successful
	And I cancel proxy
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser   |
		| Workflow User.Name | Equals          | null         | <FeeEarnerName> |
@e2eft
Examples:
	| FeeEarnerName          |
	| Transfer EarnerSurname |

@CancelProcess
Scenario Outline: 030 Prepare Data for matter 2
	Given I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	And I search or create a client
		| Entity Name |
		| <Client>    |
	When I create the Payer with Api
		| PayerName   | Entity       |
		| <PayorName> | BlackBox Ltd |
	Then I create a submatter 2 with details:
		| Client   | FeeEarnerFullName | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department | Section | ChargeTypeGroupName | CostTypeGroupName | BillingOffice   | PayorName   |
		| <Client> | <FeeEarnerName>   | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | Bill                 | <Office> | Default    | Default | Auto_IncludeALL     | Auto_IncludeALL   | <BillingOffice> | <PayorName> |
	
@e2eft
Examples:
	| FeeEarnerName    | Client           | Currency                | CurrencyMethod | Office | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias          | PayorName     |
	| Transfer NewSub2 | Transfer Client2 | AUD - Australian Dollar | Bill           | Sydney | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Sydney        | Dentons UK and Middle East LLP | Lisa Marvelle |

@CancelProcess
Scenario Outline: 040 Create a Client Account Transfer
	Given I navigate to the client account transfer process
	And I add a new client account transfer request
		| TransferType   | FromAccount   | ToAccount   | IntendedUse | Amount | TransferNumber |
		| <TransferType> | <FromAccount> | <ToAccount> | General     | 100    | {Auto}+4       |
	And I update it
	Then I submit it

@e2eft
Examples:
	| TransferType              | FeeEarnerName          | FromAccount                       | ToAccount                         |
	| Matter to Matter Transfer | Transfer EarnerSurname | Sydney - WBC Bank Trust Acc - AUD | Sydney - ANZ Bank Trust Acc - AUD |

@CancelProcess @e2eft
Scenario Outline: 050 Reverse a Client Account Transfer
	Given I navigate to the client account transfer process
	And I quick search by transfer number
	When I reverse the client account transfer
		| Reason     |
		| Correction |
	And I update it
	And I can submit and stay
	And I quick search by transfer number
	Then I verify the transfer does not exist


@CancelProcess
Scenario Outline: 060 Add new Client Account Transfer
	Given I add a new client account transfer request
		| TransferType   | FromAccount   | ToAccount   | IntendedUse | Amount | TransferNumber |
		| <TransferType> | <FromAccount> | <ToAccount> | General     | 100    | {Auto}+4       |
	And I update it
	Then I submit it

@e2eft
Examples:
	| TransferType              | FeeEarnerName          | FromAccount                       | ToAccount                         |
	| Matter to Matter Transfer | Transfer EarnerSurname | Sydney - WBC Bank Trust Acc - AUD | Sydney - ANZ Bank Trust Acc - AUD |
