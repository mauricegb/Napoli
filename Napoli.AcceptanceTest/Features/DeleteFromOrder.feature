Feature: DeleteFromOrder
	Delete existing courses from an order


Scenario: Delete a course from an order and check it has been removed from the order cost
	Given the following courses are added to the order
	| Name     | Course Type |
	| Calamari | Starter   |
	| Pizza    | Main      |
	| Pasta    | Main      |
	When the total cost is calculated
	Then the result should be £18.40
	When course 1 is removed from the order
	And the total cost is calculated
	Then the result should be £14.00

Scenario: Delete a course from an order and check it has been removed from the order summary
	Given the following courses are added to the order
	| Name     | Course Type |
	| Calamari | Starter   |
	| Pasta    | Main      |
	When course 2 is removed from the order
	And a summary of the order is requested
	Then one course with the name of Calamari and course type of Starter and a course id should be displayed in the summary

