Feature: R-044 - R2R: Posting Financial Data - Trust Receipt

Check the sub-ledger and general ledger posting for trust receipt.
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/52601

@CancelProcess
Scenario Outline: 010 Prepare Data for Workflow User for Fee Earner
	Given I create a user with details
		| UserName        | DataRoleAlias | DefaultOperatingAlias   |
		| <FeeEarnerName> | Admin         | <DefaultOperatingAlias> |
	And I create a fee earner with details
		| EntityName      |
		| <FeeEarnerName> |
	When I search or create a client
		| Entity Name | FeeEarnerFullName |
		| <Client>    | <FeeEarnerName>   |
	And add a user '<User>' fee earner
	And I create the Payer with Api
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
	Then I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName   | CostTypeGroupName   | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroupName> | <CostTypeGroupName> | <BillingOffice> | <PayorName> |
	And I remove workflow user from fee earner
		| Search Column      | Search Operator | Search Value | FeeEarnerUser   |
		| Workflow User.Name | Equals          | null         | <FeeEarnerName> |

@e2eft
Examples:
	| FeeEarnerName     | Client       | Currency            | CurrencyMethod | Office         | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias          | PayorName  | User     |
	| Billing FeeEarner | Sam McPosner | GBP - British Pound | Bill           | London (UKIME) | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Milan         | Dentons UK and Middle East LLP | James Matt | Ann Rose |

@CancelProcess
Scenario Outline: 020 Create new ClientAccount Intended Use
	Given I create a new ClientAccount Intended Use APi
		| Code   | Description   |
		| <Code> | <Description> |

@e2eft
Examples:
	| Code           | Description    |
	| E2EIntendedUse | E2EIntendedUse |

#Bug: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/52908
@CancelProcess @ignore
Scenario Outline: 030 Create a receipt request to load balance for matter account
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

@e2eft
Examples:
	| FeeEarnerName     |
	| Billing FeeEarner |

#Failing bcause of the previous scenario unable to approve the receipt
@CancelProcess @e2eft @ignore
Scenario Outline: 040 Create Client Account Disbursement
	Given I navigate to the client account disbursement process
	And I add a new client account disbursement record
		| TransactionDate | DisbursementType    | ClientAccountAcct                    | Amount | PaymentName | DocumentNumber | UseDetails |
		| {Today}-1       | Completion EFT/WIRE | Singapore - SCB Bank Trust Acc - GBP | 100    | {Auto}+10   | {Auto}+11      | {Auto}+10  |
	Then I update it
	When I submit it
	And I validate submit was successful

@CancelProcess @e2eft
Scenario Outline: 050 Create a Client Account Receipt
	Given I search for process 'Client Account Receipt'
	And I add a new client account receipt
		| TransactionDate | ClientAccountReceiptType | ClientAccountAcct                    | DocumentNumber |
		| {Today}-1       | Cash                     | Singapore - SCB Bank Trust Acc - GBP | {Auto}+10      |
	When I add client account receipt detail child form data
		| Amount | IntendedUse | Reason                 |
		| 100    | General     | Client Account Receipt |
	Then I update it
	And submit it
	And I validate submit was successful


#Bug: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/52908
@CancelProcess @e2eft @ignore
Scenario Outline: 060 Approve the Client Account Receipt
	Given I search for 'Workflow Dashboard'
	And I navigate to the client account receipt section
	Then I open the client account receipt record
	And I verify that the details of the receipt are correct
	When I tick the aml checks checkbox and approve
	And I submit it
	And I validate submit was successful
	
#Bug: Template dropdown not working
@CancelProcess @e2eft
Scenario Outline: 070 Print the Client Account Receipt
	Given I search for process 'Client Account Receipt'
	And I select existing receipt
	Then I print the receipt
		| PrintJobName                   | Template |
		| Client Account Receipt PRint01 | Test     |
	And I submit it
	And I validate submit was successful

Scenario Outline: 080 Check Gl Posting Status
	Given I search for process 'Client Account Receipt'
	And I select existing receipt
	When I click the gl postings buttons
	And I save the journal manager number
	Then I validate the status is posted
	And I validate the gl postings for unit '<OperatingUnit>'
	And I close the dialog
	And I cancel the process

@e2eft
Examples:
	| OperatingUnit |
	| 1201          |

@e2eft
Scenario Outline: 090 Verify the Post Queue
	Given I navigate to the post queue process
	When I click the show button
	And I verify the client account receipt is not present with a failed status
		| PostSource |
		| RECEIPT    |
	And I close the dialog
	Then I cancel the process

@e2eft
Scenario Outline: 100 Verify the Posting Results
	Given I search for the entry in posting results process
	Then verify the posting status is posted
	And I cancel the process

@CancelProcess @e2eft
Scenario Outline: 110 Verify the Journal Register Report
	Given I search for the entry in journal register
	Then verify the posting details in journal register
		| Search Column             | Search Operator |
		| Journal Manager.JM Number | Equals          |


