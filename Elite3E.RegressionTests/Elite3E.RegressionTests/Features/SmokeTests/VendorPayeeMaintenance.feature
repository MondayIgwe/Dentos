@smoke @VendorPayeeMaintenance
Feature: VendorPayeeMaintenance
	Create Vendor via payee maintenance

Scenario Outline: 010 Create Vendor
	Given I create a person entity '<EntityName>'
	Given I create a new vendor via payee maintenance
	When I create the vendor
		| Entity       | Status   | Global Vendor Value |
		| <EntityName> | <Status> | <GlobalVendor>      |
	Then I can submit the vendor
	And a vendor number is assigned
	
@training @staging @europe
Examples:
	| EntityName             | Status   | GlobalVendor    |
	| TestEntity TestEntityL | Approved | GlobalVendor_UK |
@ft
Examples:
	| EntityName             | Status   | GlobalVendor                 |
	| TestEntity TestEntityL | Approved | Dentons Client Global Vendor |
@uk
Examples:
	| EntityName             | Status   | GlobalVendor                 |
	| TestEntity TestEntityL | Approved | Dentons Client Global Vendor |
@qa
Examples:
	| EntityName             | Status   | GlobalVendor                 |
	| TestEntity TestEntityL | Approved | Dentons Client Global Vendor |
@canada
Examples:
	| EntityName             | Status   | GlobalVendor       |
	| TestEntity TestEntityL | Approved | Amex Global Vendor |
@singapore
Examples:
	| EntityName                                              | Status   | GlobalVendor    |
	| (BRS - RMB)Richard Koh, 2 Tractor Road Singapore 627966 | Approved | GlobalVendor_UK |


Scenario Outline: 020 Create Vendor in Vendor/Payee Maintenance
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

@ft
Examples:
	| SiteType | Street          | Country        | Vendor                       | Status   | PaymentTerms | Office   | Message             | Language |
	| Billing  | 1 Entity Street | CAYMAN ISLANDS | Dentons Client Global Vendor | Approved | Immediate    | Aberdeen | No duplicates found | English    |
@qa
Examples:
	| SiteType | Street          | Country        | Vendor                       | Status   | PaymentTerms | Office   | Message             | Language |
	| Billing  | 1 Entity Street | CAYMAN ISLANDS | Dentons Client Global Vendor | Approved | Immediate    | Aberdeen | No duplicates found | Xhosa    |
@uk
Examples:
	| SiteType | Street          | Country        | Vendor                       | Status   | PaymentTerms | Office   | Message             | Language |
	| Billing  | 1 Entity Street | CAYMAN ISLANDS | Dentons Client global vendor | Approved | Immediate    | Aberdeen | No duplicates found | Xhosa    |
@training @staging @europe
Examples:
	| SiteType | Street          | Country        | Vendor          | Status   | PaymentTerms | Office  | Message             | Language |
	| Billing  | 1 Entity Street | CAYMAN ISLANDS | GlobalVendor_UK | Approved | Immediate    | Default | No duplicates found | Xhosa    |
@canada
Examples:
	| SiteType | Street          | Country        | Vendor             | Status   | PaymentTerms | Office  | Message             | Language |
	| Billing  | 1 Entity Street | CAYMAN ISLANDS | Amex Global Vendor | Approved | Immediate    | Toronto | No duplicates found | Xhosa    |
@singapore
Examples:
	| SiteType | Street          | Country        | Vendor          | Status   | PaymentTerms | Office    | Message             | Language |
	| Billing  | 1 Entity Street | CAYMAN ISLANDS | GlobalVendor_UK | Approved | Immediate    | Singapore | No duplicates found | Xhosa    |