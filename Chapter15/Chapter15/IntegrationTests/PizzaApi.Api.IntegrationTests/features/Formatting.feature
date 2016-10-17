Feature: Formatting
	In order to get orders data
	As an API client
	I want to be able to define the format

@normal
Scenario Outline: Get back data with the format requested when asking for orders
	Given I provide format <Format>
	When When I request for all orders
	Then I get back <ContentType> content type
		And content is a set of orders
	Examples:
	| Format | ContentType      |
	| JSON   | application/json |
	| XML    | application/xml  |

@normal
Scenario Outline: Get back data with the format requested when an error is returned
	Given I provide format <Format>
	When When an error is returned
	Then I get back <ContentType> content type
		And message content contains error information
	Examples:
	| Format | ContentType      |
	| JSON   | application/json |
	| XML    | application/xml  |

