Feature: MergeDuplicateVendor

https://dev.azure.com/dentonsglobal/GFT%203E/_workitems/edit/64274

Scenario Outline: 010 Create Vendor in Vendor/Payee Maintenance
	Given I navigate to the vendor/payee maintenance and I add a new record
	Then I add new entity
		| EntityType         | Organisation Name | Site Type  | Street   | Country   | Language   | Message             |
		| EntityOrganisation | {Auto}+36         | <SiteType> | <Street> | <Country> | <Language> | No duplicates found |
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
	| SiteType | Street          | Country        | Vendor                       | Status   | PaymentTerms | Office   | Message             | Language |
	| Billing  | 1 Entity Street | CAYMAN ISLANDS | Dentons Client Global Vendor | Approved | Immediate    | Aberdeen | No duplicates found | English    |


@e2eft
Scenario: 020 Clone a Vendor from the vendor which is created above
	Given I open vendor record
	When I clone and update vendor
	Then I submit the vendor/payee maintenance

@e2eft
Scenario: 030 Merge Vendor
	Given I navigate to vendor/payee merge and I add a new record
	When I add vendor/payee merge details
		| Comments  | VendorMerge |
		| {Auto}+10 | true        |
	Then I submit it
	And I validate submit was successful
