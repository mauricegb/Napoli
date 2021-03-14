Feature: Ordering
	Order combinations of starters and mains in the Napoli checkout system

Scenario: Order a single starter
	Given the name of the course is Calamari
	And the course type is a starter
	When the course is added to the order
	And the total cost is calculated
	Then the result should be £4.40


Scenario: Order a single main
	Given the name of the course is Pizza
	And the course type is a main
	When the course is added to the order
	And the total cost is calculated
	Then the result should be £7.00


Scenario: Order multiple courses
	Given the following courses are added to the order
	| Name     | Course Type |
	| Calamari | Starter   |
	| Pizza    | Main      |
	| Pasta    | Main      |
	When the total cost is calculated
	Then the result should be £18.40