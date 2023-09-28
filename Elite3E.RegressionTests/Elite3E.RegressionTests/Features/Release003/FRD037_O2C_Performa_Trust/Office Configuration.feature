@release3 @frd037 @OfficeConfiguration
Feature: Office Configuration

Scenario Outline: 001_Payee Maintenance
	Given I create a new Payee with the Api
		| PayeeName   |
		| <PayeeName> |

@ft @qa @training @staging @canada @europe @uk @singapore  
Examples:
	| PayeeName |
	| Amsterdam |
	
@CancelProcess
Scenario Outline: 002_Office Configuration
	Given I create the Office Configuraton Api
		| Office   | DisbursementTypeValue   |
		| <Office> | <TrustDisbursementType> |
	When I open the office configuration process
	And I try to add a duplicate office
		| Office   | Trust Disbursement Type | Days To Dispatch | TimeKeeperLeaver   | ClientAccountReceiptType   | ClientAccountDefault   |
		| <Office> | <TrustDisbursementType> | <DaysToDispatch> | <TimeKeeperLeaver> | <ClientAccountReceiptType> | <ClientAccountDefault> |
	Then a error message "<ErrorMessage>" is displayed

@ft
Examples:
	| Office    | TrustDisbursementType    | DaysToDispatch | Payee                | ErrorMessage           | TimeKeeperLeaver             | ClientAccountReceiptType | ClientAccountDefault                 |
	| Amsterdam | Trust to Office Transfer | 5              | Automation Payee - 1 | Enter a unique Office. | 0:AD:G:Common Authorisations | Cash                     | Singapore - SCB Bank Trust Acc - GBP |
@qa
Examples:
	| Office    | TrustDisbursementType    | DaysToDispatch | Payee                | ErrorMessage           | TimeKeeperLeaver             | ClientAccountReceiptType | ClientAccountDefault                 |
	| Amsterdam | Trust to Office Transfer | 5              | Automation Payee - 1 | Enter a unique Office. | 0:AD:G:Common Authorisations | Cash                     | Singapore - SCB Bank Trust Acc - GBP |
@training @staging
Examples:
	| Office    | TrustDisbursementType    | DaysToDispatch | Payee                | ErrorMessage           | TimeKeeperLeaver             | ClientAccountReceiptType | ClientAccountDefault                 |
	| Amsterdam | Trust to Office Transfer | 5              | Automation Payee - 1 | Enter a unique Office. | 0:AD:G:Common Authorisations | Cash                     | Singapore - SCB Bank Trust Acc - GBP |
@canada
Examples:
	| Office    | TrustDisbursementType    | DaysToDispatch | Payee                | ErrorMessage           | TimeKeeperLeaver             | ClientAccountReceiptType | ClientAccountDefault                 |
	| Vancouver | Trust to Office Transfer | 5              | Automation Payee - 1 | Enter a unique Office. | 0:AD:G:Common Authorisations | Cash                     | Singapore - SCB Bank Trust Acc - GBP |
@europe
Examples:
	| Office    | TrustDisbursementType    | DaysToDispatch | Payee                | ErrorMessage           | TimeKeeperLeaver             | ClientAccountReceiptType | ClientAccountDefault      |
	| Amsterdam | Trust to Office Transfer | 5              | Automation Payee - 1 | Enter a unique Office. | 0:AD:G:Common Authorisations | Cash                     | Barclays Bank PLC (DCEUR) |
@uk
Examples:
	| Office    | TrustDisbursementType    | DaysToDispatch | Payee                | ErrorMessage           | TimeKeeperLeaver             | ClientAccountReceiptType | ClientAccountDefault                 |
	| Amsterdam | Trust to Office Transfer | 5              | Automation Payee - 1 | Enter a unique Office. | 0:AD:G:Common Authorisations | Cash                     | Singapore - SCB Bank Trust Acc - GBP |
@singapore
Examples:
	| Office    | TrustDisbursementType    | DaysToDispatch | Payee                | ErrorMessage           | TimeKeeperLeaver             | ClientAccountReceiptType | ClientAccountDefault                  |
	| Singapore | Trust to Office Transfer | 5              | Automation Payee - 1 | Enter a unique Office. | 0:AD:G:Common Authorisations | Cash                     | L&Y-Client-Citic Ka Wah HKD (Current) |



