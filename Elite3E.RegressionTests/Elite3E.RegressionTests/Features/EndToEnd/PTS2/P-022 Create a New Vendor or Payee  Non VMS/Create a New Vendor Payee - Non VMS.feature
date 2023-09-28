Feature: Create a New Vendor Payee - Non VMS

A requestor/procurement completes and submits a new supplier form request manually with the relevant attachment. 
The compliance team reviews and approves the form request. 
The new supplier data is then input into 3E by the Master Data Analyst Team. 
The request is then reviewed and approved by the Compliance Team. 
The supplier data is then flowed through to Chrome River/Other Purchasing System.
Azure link: https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/41551

@CancelProcess
Scenario: 010 Create a New  Vendor/Payee Maintenance
	Given I navigate to the vendor/payee maintenance and I add a new record
	Then I add new entity
		| EntityType         | Organisation Name | Site Type  | Street   | Country   | Language   | Message   |
		| EntityOrganisation | {Auto}+36         | <SiteType> | <Street> | <Country> | <Language> | <Message> |
	And I add a new vendor information
		| Vendor   | Status   |
		| <Vendor> | <Status> |
	And I add a new payee
		| Payment Terms  | Office   | Message   |
		| <PaymentTerms> | <Office> | <Message> |
	And I submit the vendor/payee maintenance
	Then I verify the vendor is created

@e2eft
Examples:
	| SiteType | Street          | Country        | Vendor                       | Status     | PaymentTerms | Office   | Message             | Language |
	| Billing  | 1 Entity Street | CAYMAN ISLANDS | Dentons Client Global Vendor | Unapproved | Immediate    | Aberdeen | No duplicates found | English    |


