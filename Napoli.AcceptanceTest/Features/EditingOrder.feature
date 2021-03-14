Feature: EditingOrder
	Edit existing courses on an order

Scenario: Order a starter then edit it to be a main
	Given the name of the course is Calamari
	And the course type is a starter
	When the course is added to the order
	And the course is updated so that the course name is pizza and the course type is a Main
	And a summary of the order is requested
	Then one course with the name of pizza and course type of Main and a course id should be displayed in the summary


