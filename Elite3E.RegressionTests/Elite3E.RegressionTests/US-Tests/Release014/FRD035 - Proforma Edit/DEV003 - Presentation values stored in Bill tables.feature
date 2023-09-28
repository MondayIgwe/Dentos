@us
Feature: DEV003 - Presentation values stored in Bill tables

Presentation values stored in Bill tables

@CancelProcess
Scenario Outline: 010 Verify Presentation values stored in Time Bill
	Given I navigate to the 'Time Bill' report process
	Then I verify the presentation fields and their values
		| AttributeField | Name             |
		| Pres Hours     | WTMatterTimeBill |
		| Pres Rate      | WTMatterTimeBill |
		| Pres AMount    | WTMatterTimeBill |

@CancelProcess
Scenario Outline: 020 Verify Presentation values stored in Cost Bill
	Given I navigate to the 'CostBill' report process
	Then I verify the presentation fields and their values
		| AttributeField | Name     |
		| Pres Hours     | CostBill |
		| Pres Rate      | CostBill |
		| Pres AMount    | CostBill |


@CancelProcess
Scenario Outline: 030 Verify Presentation values stored in Charge Bill
	Given I navigate to the 'ChrgBill' report process
	Then I verify the presentation fields and their values
		| AttributeField | Name       |
		| Pres Hours     | ChargeBill |
		| Pres Rate      | ChargeBill |
		| Pres AMount    | ChargeBill |
		

