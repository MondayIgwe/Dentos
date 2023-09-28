Feature: VendorTaxCertificateValidity


@CancelProcess
Scenario: 010 Create a New  Vendor/Payee Maintenance with TaxCertificateDate
	Given I navigate to the vendor/payee maintenance and I add a new record
	Then I add new entity
		| EntityType         | Organisation Name | Site Type  | Street   | Country   | Language   | Message   |
		| EntityOrganisation | {Auto}+36         | <SiteType> | <Street> | <Country> | <Language> | <Message> |
	And I add a new vendor information
		| Vendor   | Status   |
		| <Vendor> | <Status> |
	And I add a new payee
		| Payment Terms  | Office   | Message   | TaxCertificateDate |
		| <PaymentTerms> | <Office> | <Message> | {Today}            |
	And I submit the vendor/payee maintenance
	Then I verify the vendor is created

@ft @training @staging  @canada @europe @uk @singapore @us @qa
Examples:
	| SiteType | Street          | Country        | Vendor                       | Status     | PaymentTerms | Office   | Message             | Language |
	| Billing  | 1 Entity Street | CAYMAN ISLANDS | Dentons Client Global Vendor | Unapproved | Immediate    | Aberdeen | No duplicates found | English  |


