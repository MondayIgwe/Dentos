@us
Feature: Office Configuration

Scenario Outline: 001_Payee Maintenance
	Given I create a new Payee with the Api
		| PayeeName   |
		| <PayeeName> |

Examples:
	| PayeeName |
	| Amsterdam |

Scenario Outline: 002_Office Configuration
	Given I create the Office Configuraton Api
		| Office   | DisbursementTypeValue   |
		| <Office> | <TrustDisbursementType> |
	When I open the office configuration process
	And I try to add a duplicate office
		| Office   | Trust Disbursement Type | Days To Dispatch | TimeKeeperLeaver   | ClientAccountReceiptType   | ClientAccountDefault   |
		| <Office> | <TrustDisbursementType> | <DaysToDispatch> | <TimeKeeperLeaver> | <ClientAccountReceiptType> | <ClientAccountDefault> |
	Then a error message "<ErrorMessage>" is displayed

Examples:
	| Office            | TrustDisbursementType    | DaysToDispatch | Payee                | ErrorMessage           | TimeKeeperLeaver             | ClientAccountReceiptType | ClientAccountDefault                                      |
	| US Administration | Trust to Office Transfer | 5              | Automation Payee - 1 | Enter a unique Office. | 0:AD:G:Common Authorisations | Cash                     | Dentons US LLP (IOLTA) Interest on Lawyer Trust-204968895 |
	



