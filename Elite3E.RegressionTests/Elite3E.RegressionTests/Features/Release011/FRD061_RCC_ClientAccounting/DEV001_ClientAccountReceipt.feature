@ignore
#Bug: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/50520
#Bug: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/50521
Feature: DEV001_ClientAccountReceipt


DEV001 - Client Account Receipt Self-Service form & WF (ID: 43667)
Client Account Receipt Request:
In order to be able to get the Approved checkbox to be ticked or true must have the following:
A billing fee earner must be created which is used on the Client Maintenance.
The Client with the billing fee earner must be applied to the Matter created.
A Billing Timekeeper(Fee Earner User) user must be created with no roles (to be used for proxying later on)
The Billing Timekeeper/Fee Earner must be added a Billing Timekeeper user as a workflow user


@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa
Scenario: 010 New fields are available within the Office Configuration process
	Given I open the office configuration process
	Then the new required fields are displayed
	When I try to update the form with no details the <ErrorMessage> is displayed for required fields
		| ErrorMessage                                                                      |
		| Client Account Acct Default: Assign required attribute before continuing.         |
		| Client Account Receipt Type Default: Assign required attribute before continuing. |
	And the client account receipt approval required checkbox is displayed
	And the client account receipt approval required checkbox can be set to false or true

@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa
Scenario: 020 Client Account Receipt Request process
#This process is currently named 'Trust Receipt Request' should be updated once naming is corrected
	Given I open the client account receipt request process
	Then the client account receipt request process should exist
	And all the client account receipt request fields should exist

#Defect-https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/50520
@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa @ignore
Scenario: 030 Client Account Receipt Approval process
#This process is currently named 'Trust Receipt Approval' should be updated once naming is corrected test fails
	Given I open the client account receipt approval process
	Then the client account receipt approval process should exist

#Defect -https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/50560
@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa @ignore
Scenario: 040 Client Account Receipt Finance process
#This process is currently named 'Trust Receipt Finance' should be updated once naming is corrected test fails
	Given I open the client account receipt finance process
	Then the client account receipt finance process should exist

@CancelProcess
Scenario Outline: 050 Prepare Data for Workflow User for Fee Earner
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
	Then I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName   | CostTypeGroupName   | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroupName> | <CostTypeGroupName> | <BillingOffice> | <PayorName> |
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser   |
		| Workflow User.Name | Equals          | null         | <FeeEarnerName> |

@training @staging
Examples:
	| FeeEarnerName     | Client       | Currency   | CurrencyMethod | Office              | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias          | PayorName     |
	| Billing FeeEarner | Sam McPosner | EUR - Euro | Bill           | London Billing (EU) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Milan         | Dentons UK and Middle East LLP | Lisa Marvelle |
@qa
Examples:
	| FeeEarnerName     | Client       | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias          | PayorName     |
	| Billing FeeEarner | Sam McPosner | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Milan         | Dentons UK and Middle East LLP | Lisa Marvelle |
@europe
Examples:
	| FeeEarnerName     | Client       | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias   | PayorName     |
	| Billing FeeEarner | Sam McPosner | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Milan         | Dentons Europe LLP (UK) | Lisa Marvelle |
@uk
Examples:
	| FeeEarnerName     | Client       | Currency   | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias          | PayorName     |
	| Billing FeeEarner | Sam McPosner | EUR - Euro | Bill           | London (UKIME) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Milan         | Dentons UK and Middle East LLP | Lisa Marvelle |
@canada
Examples:
	| FeeEarnerName     | Client       | Currency             | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias | PayorName     |
	| Billing FeeEarner | Sam McPosner | CD - Canadian Dollar | Bill           | London (UKIME) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Milan         | Dentons Canada LLP    | Lisa Marvelle |
@singapore
Examples:
	| FeeEarnerName     | Client       | Currency               | CurrencyMethod | Office    | Department            | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias        | PayorName     |
	| Billing FeeEarner | Sam McPosner | SGD - Singapore Dollar | Bill           | Singapore | Corporate - Singapore | Default | Auto_IncludeALL | Auto_IncludeALL | Singapore     | Dentons Rodyk & Davidson LLP | Lisa Marvelle |
@ft
Examples:
	| FeeEarnerName     | Client       | Currency                | CurrencyMethod | Office | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias          | PayorName     |
	| Billing FeeEarner | Sam McPosner | AUD - Australian Dollar | Bill           | Sydney | Default    | Default | Desc_at_3e8glw7 | Desc_at_VowqCrR | Sydney        | Dentons UK and Middle East LLP | Lisa Marvelle |
	

Scenario: 060 Verify that user is able to Submit Client Account Receipt Request
	Given I 'add' a workflow user '<FeeEarnerName>' to fee earner '<FeeEarnerName>'
	Then I proxy as user '<FeeEarnerName>'
	And I open the client account receipt request process
	And I set the aml checks complete checkbox to true
	And I complete all the client account receipt required fields
		| Narrative | TransactionDate | Amount | Reason                         |
		| {Auto}+10 | {Today}-1       | 100    | Client Account Receipt Request |
	When the approval checkbox is checked
	And I add an attachment
		| File           |
		| Attachment.txt |
	And I submit it
	And I validate submit was successful
	And I cancel proxy
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser   |
		| Workflow User.Name | Equals          | null         | <FeeEarnerName> |

@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa
Examples:
	| FeeEarnerName     |
	| Billing FeeEarner |
	
@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa
Scenario: 070 Verify that user is able to Terminate Client Account Receipt request WF
	Given I open the client account receipt request process
	And I complete all the client account receipt required fields
		| Narrative | TransactionDate | Amount | Reason                         |
		| {Auto}+10 | {Today}-1       | 100    | Client Account Receipt Request |
	Then I terminate the process
	And I validate terminate was successfull
	Then I search for 'Workflow Dashboard'
	When I navigate to the client account receipt section
	And the client account receipt record does not exist

@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa
Scenario: 080 Verify that user is able to Close Client Account Receipt request WF
	Given I open the client account receipt request process
	And I complete all the client account receipt required fields
		| Narrative | TransactionDate | Amount | Reason                         |
		| {Auto}+10 | {Today}-1       | 100    | Client Account Receipt Request |
	Then I close the client receipt process
	And I validate close was successfull
	When I search for 'Workflow Dashboard'
	And I navigate to the client account receipt section
	And the client account receipt record does exist


Scenario: 090 Verify that user is able to progress Approval task to the next step
	Given I 'add' a workflow user '<FeeEarnerName>' to fee earner '<FeeEarnerName>'
	And I open the client account receipt request process
	And I complete all the client account receipt required fields
		| Narrative | TransactionDate | Amount | Reason                         |
		| {Auto}+10 | {Today}-1       | 100    | Client Account Receipt Request |
	And I submit it
	And I validate submit was successful
	When I proxy as user '<FeeEarnerName>'
	And I search for 'Workflow Dashboard'
	And I navigate to the client account receipt section
	And I open the client account receipt task
	And I approve the task by clicking submit
	Then I validate submit was successful
	And I cancel proxy
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser   |
		| Workflow User.Name | Equals          | null         | <FeeEarnerName> |

@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa
Examples:
	| FeeEarnerName     |
	| Billing FeeEarner |
 

Scenario: 100 Verify that user is able to close Client Account Receipt Finance task
	Given I 'add' a workflow user '<FeeEarnerName>' to fee earner '<FeeEarnerName>'
	And I open the client account receipt request process
	And I complete all the client account receipt required fields
		| Narrative | TransactionDate | Amount | Reason                         |
		| {Auto}+10 | {Today}-1       | 100    | Client Account Receipt Request |
	And I submit it
	And I validate submit was successful
	When I proxy as user '<FeeEarnerName>'
	And I search for 'Workflow Dashboard'
	And I navigate to the client account receipt section
	And I open the client account receipt task
	And I approve the task by clicking submit
	Then I validate submit was successful
	And I cancel proxy
	And I open the client account receipt record as a finance reviewer
	And I close the client receipt process
	And I validate close was successfull
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser   |
		| Workflow User.Name | Equals          | null         | <FeeEarnerName> |

@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa
Examples:
	| FeeEarnerName     |
	| Billing FeeEarner |


Scenario: 110 Verify that user is able to Terminate Account Receipt Finance task
	Given I 'add' a workflow user '<FeeEarnerName>' to fee earner '<FeeEarnerName>'
	And I open the client account receipt request process
	And I complete all the client account receipt required fields
		| Narrative | TransactionDate | Amount | Reason                         |
		| {Auto}+10 | {Today}-1       | 100    | Client Account Receipt Request |
	And I submit it
	And I validate submit was successful
	When I proxy as user '<FeeEarnerName>'
	And I search for 'Workflow Dashboard'
	And I navigate to the client account receipt section
	And I open the client account receipt task
	Then I approve the task by clicking submit
	And I validate submit was successful
	And I cancel proxy
	And I open the client account receipt record as a finance reviewer
	And I terminate the process
	And I validate terminate was successfull
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser   |
		| Workflow User.Name | Equals          | null         | <FeeEarnerName> |

@CancelProcess @training @staging @canada @europe @uk @singapore @ft @qa
Examples:
	| FeeEarnerName     |
	| Billing FeeEarner |
	

