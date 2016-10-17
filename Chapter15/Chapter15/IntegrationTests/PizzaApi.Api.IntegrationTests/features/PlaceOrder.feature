Feature: PlaceOrders
	In order to inform kitchen of the customer orders
	As a pizza shop operator
	I want to be able to place customer orders

@normal
Scenario: Placing order
	Given I have an order for a mixture of pizzas
		And it is for a particular customer
	When I place the order
		And retrive the order
	Then system must have priced the order
		And system must have saved the order
		And saved order must contain same pizzas
		And saved order must have the name of the customer
