@release9 @frd016 @AuditButtonChecking
Feature: AuditButtonChecking

FRD016 - Test1 Part 2 - Process Auditing

Usage: If you're validating the high level audit button (blue) leave the form and archetype fields empty.
Archetype in this test is basically a subform.

@ft @qa @training @staging @canada @europe @uk @singapore @us
Scenario Outline: 010 Audit Validation Use Existing Data
	Given I verify single audit button on a process form
		| Process                             | Form                  | Archetype      |
		| Invoices                            |                       | Master Invoice |
		| Receipts - Apply / Reverse Payments |                       |                |
		| Unit Period Review and Update       |                       |                |
		| Rate Exception Group                | Rate Exception Detail |                |
		| Task List                           |                       |                |
		| Tax Area Matrix                     |                       |                |
		| Bank Account Client Account         |                       |                |

@ft @qa @training @staging @canada @europe @uk @singapore @us
Scenario Outline: 012 Audit Validation Voucher Maintenance
	Given I verify single audit button on a process form
		| Process             | Form | Archetype |
		| Voucher Maintenance |      |           |
		
@ft @qa @training @staging @canada @europe @uk @singapore @us
Scenario Outline: 015 Audit Validation For Vendor Payee Bank
	Given I verify multiple audit buttons on a process form
		| Process                  | Form  | Archetype  |
		| Vendor/Payee Maintenance | Payee |            |
		| Vendor/Payee Maintenance | Payee | Payee Bank |

@ft @qa @training @staging @canada @europe @uk @singapore @us
Scenario Outline: 020 Audit Validation Data Creation Required
	Given I verify single audit button on a process form
		| Process                     |
		| Client Account Cheque       |
		| Client Account Disbursement |
		| Activity List               |
		

@ft @qa @training @staging @canada @europe @uk @singapore @us
Scenario Outline: 030 Currency Type Exchange Audit Validation
	Given I verify multiple audit buttons on a process form
		| Process                      | Form                        | Archetype               |
		| Currency Type Exchange Rates |                             |                         |
		| Currency Type Exchange Rates | Effective Dated Information |                         |
		| Currency Type Exchange Rates | Effective Dated Information | Currency Exchange Rates |

@ft @qa @training @staging @canada @europe @uk @singapore @us
Scenario Outline: 040 Matter Maintenance Audit Validation
	Given I verify multiple audit buttons on a process form
		| Process            | Form                        | Archetype                 |
		| Matter Maintenance | Effective Dated Information | Originating Fee Earners   |
		| Matter Maintenance | Effective Dated Information | Proliferating Fee Earners |
		| Matter Maintenance | Rate Exception              |                           |
		| Matter Maintenance |                             |                           |
		| Matter Maintenance | Effective Dated Information |                           |
		| Matter Maintenance | Flat Fees                   |                           |
		| Matter Maintenance | Matter Budget               |                           |

@ft @qa @training @staging @canada @europe @uk @singapore @us
Scenario Outline: 050 Fee Earner Maintenance Audit Validation
	Given I verify multiple audit buttons on a process form
		| Process                | Form                 | Archetype             |
		| Fee Earner Maintenance |                      |                       |
		| Fee Earner Maintenance | Fee Earner Rates     |                       |
		| Fee Earner Maintenance | Fee Earner Rates     | Effective Dated Rates |
		| Fee Earner Maintenance | Fee Earner Rates     | Rate Details          |
		| Fee Earner Maintenance | Fee Earner Objective |                       |


