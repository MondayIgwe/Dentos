﻿@us
Feature: CostTypeGroup


Scenario:Verify Cost Type Group
	Given I search for process 'Cost Type Group'
	When I select an existing record if present
	Then I verify the sections in cost type group

