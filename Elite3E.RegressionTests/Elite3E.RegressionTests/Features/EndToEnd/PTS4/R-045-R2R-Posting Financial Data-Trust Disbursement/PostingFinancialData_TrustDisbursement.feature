@ignore
Feature: PostingFinancialData_TrustDisbursement

The approvals are not going to the correct user hence the tests are failing. Please refer to the ADO links for more information. 

This feature needs to be revisited once the defects are fixed 

#Bug: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/52908
#Bug: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/50478

	Scenario: 005 Create Finance approval user
	Given I create a user with details
		| UserName | DataRoleAlias   | DefaultOperatingAlias   | UserRoleList   |
		| <User1>  | <DataRoleAlias> | <DefaultOperatingAlias> | <UserRoleList> |
	Then I submit it
	

@e2eft
Examples:
	| User1       | DataRoleAlias | DefaultOperatingAlias     | UserRoleList                                                                                                                                                                                                                |
	| FRD061_User | Admin         | Dentons Australia Limited | 0:WC:R:Client Account Processor: 1001: 8001,0:WC:R:Client Account Processor: 1001: 8006,0:WC:R:Client Account Processor: 1001: 8007,0:WC:R:Client Account Processor: 1001: 8008,0:WC:R:Client Account Processor: 1001: 8009 |

Scenario Outline: 010 Prepare Data for matter
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
		| PayerName   | Entity   |
		| <PayorName> | <Client> |
Then I create a matter with details:
		| Client   | Status     | OpenDate  | MatterName | Currency   | MatterCurrencyMethod | Office   | Department   | Section   | ChargeTypeGroupName   | CostTypeGroupName   | BillingOffice   | PayorName   |
		| <Client> | Fully Open | {Today}-1 | {Auto}+36  | <Currency> | <CurrencyMethod>     | <Office> | <Department> | <Section> | <ChargeTypeGroupName> | <CostTypeGroupName> | <BillingOffice> | <PayorName> |

@e2eft
Examples:
	| FeeEarnerName          | Client                 | Currency                | CurrencyMethod | Office | Department | Section | ChargeTypeGroup | CostTypeGroup   | BillingOffice | DefaultOperatingAlias          | PayorName  |
	| Transfer EarnerSurname | Transfer ClientSurname | AUD - Australian Dollar | Bill           | Sydney | Default    | Default | Auto_IncludeALL | Auto_IncludeALL | Sydney        | Dentons UK and Middle East LLP | James Matt |
	

@CancelProcess
Scenario Outline: 020 Create new ClientAccount Intended Use
	Given I create a new ClientAccount Intended Use APi
		| Code   | Description   |
		| <Code> | <Description> |

@e2eft
Examples:
	| Code           | Description    |
	| E2EIntendedUse | E2EIntendedUse |


Scenario Outline: 030 Create a receipt request to load balance for matter account 
	Given I 'add' a workflow user '<FeeEarnerName>' to fee earner '<FeeEarnerName>'
	Then I proxy as user '<FeeEarnerName>'
	Then I open the client account receipt request process
	And I set the aml checks complete checkbox to true
	And I complete all the client account receipt required fields
		| Narrative | TransactionDate | Amount | Reason                         |
		| {Auto}+10 | {Today}-1       | 100    | Client Account Receipt Request |
	When the approval checkbox is checked
	When I add an attachment
		| File           |
		| Attachment.txt |
	Then the attachment number is shown
		| Number of Attachments |
		| 1                     |
	And I want to click approve
	And I submit it
	And I validate submit was successful
	Then I cancel proxy

Scenario Outline: 040 Approve the receipt request through workflow
	When I proxy as user 'FRD061_User'
	And I search for 'Workflow Dashboard'
	And I navigate to the client account receipt section
	And I open the client account receipt task
	And I reject the receipt task
	And I cancel proxy
	And I proxy as user '<FeeEarnerName>'
	Given I search for 'Workflow Dashboard'
	And I navigate to the client account receipt section
	And I navigate to the client receipt section and search by '<FeeEarnerName>'
	Then I submit it
	And I cancel proxy
	And I proxy as user 'FRD061_User'
	Given I search for 'Workflow Dashboard'
	And I navigate to the client account receipt section
	And I navigate to the client receipt section and search by 'FRD061_User'
	Then I submit it 

	@CancelProcess @e2eft
	Examples:
	| FeeEarnerName          |
	| Transfer EarnerSurname |

	#Bug: Template dropdown not working
@CancelProcess @e2eft 
Scenario Outline: 050 Print the Client Account Receipt
	Given I search for process 'Client Account Receipt'
	And I select existing receipt
	Then I print the receipt
		| PrintJobName                   | Template |
		| Client Account Receipt PRint01 | Test     |
	And I submit it
	And I validate submit was successful

	# Due to defect 52908 the client receipt approval steps needs to be updated once the defect is fixed 

	@e2eft
Scenario Outline: 060 Create a Client Account Disbursement Request
	Given I search for process with option
		| search                              | option                            |
		| Client Account Disbursement Request | (WF_TrustDisbursementRequest_ccc) |
	When I create the client account disbursement request
		| DisbursementType   | ClientAccountAcct   | IntendedUse   | Amount   | PaymentName   |
		| <DisbursementType> | <ClientAccountAcct> | <IntendedUse> | <Amount> | <PaymentName> |
	Then I confirm IsClientApprovalObtained
		| IsClientApprovalObtained   |
		| <IsClientApprovalObtained> |
	Then I confirm IsPaymentInformationVerified
		| IsPaymentInformationVerified   |
		| <IsPaymentInformationVerified> |
	Then I submit it
	And I validate submit was successful
	
	Examples:
	| DisbursementType    | ClientAccountAcct                 | IntendedUse | IsPaymentInformationVerified | IsClientApprovalObtained | PaymentName    |
	| Completion EFT/WIRE | Sydney - WBC Bank Trust Acc - AUD | General      | true                         | true                     | Automation Test |


	Scenario Outline: 070 I want to approve the Client Account Disbursement Request
	Given I proxy as user 'FRD061_User'
	When I search for 'Workflow Dashboard'
	And I navigate to the client account disbursement section
	And I navigate to the client receipt section and search by '<FeeEarnerName>'
	And I send the request for ApprovalRequired
	Given I proxy as user '<FeeEarnerName>'
	When I search for 'Workflow Dashboard'
	And I navigate to the client account disbursement section
	And I navigate to trust disbursement finance approval section and search by 'FRD061_User'
	Then I submit it
	@CancelProcess @e2eft
	Examples:
	| FeeEarnerName          |
	| Transfer EarnerSurname |

	@CancelProcess @e2eft
	Scenario Outline: 080 Validate GL postings for the client account disbursement
	When I locate the submitted entry in 'Client Account Disbursement' process
	And I get the disbursement index of the client account disbursement
	And  I validate the gl postings for operating unit '1001'

	 #Due to defect 52908 the client disbursement approval steps below step will fail
	@e2eft
	Scenario: 090 I want to approve the Client Account Disbursement Request
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


	@e2eft @CancelProcess
    Scenario Outline: 100 Verify the Post Queue
	Given I navigate to the post queue process
	When I verify the cost/time/charge/receipt card is not present in the post queue

	@e2eft @CancelProcess
	Scenario: 110 Verify in Posting Results
	Given  I search for the entry in posting results process 
	Then verify the posting status is posted

	@e2eft
	Scenario: 120 Verify in Journal Register
	Given I search for the entry in journal register
	Then verify the posting details in journal register
            | Search Column   | Search Operator |
            | Journal Manager | Equals          |