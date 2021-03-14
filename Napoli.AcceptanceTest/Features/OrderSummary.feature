Feature: OrderSummary
	View the order summary output by the Napoli checkout

Scenario Outline: Order a course and view in the summary
	Given the name of the course is <Name>
	And the course type is a <Course Type>
	When the course is added to the order
	And a summary of the order is requested
	Then one course with the name of <Name> and course type of <Course Type> and a course id should be displayed in the summary

	Examples: 
	| Name     | Course Type |
	| Calamari | Starter   |
	| Pizza    | Main      |

Scenario: View the summary for an order without any courses
	Given an order does not have any courses added to it
	When a summary of the order is requested
	Then the summary produced is empty

