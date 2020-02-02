Feature: RoadStatusTest

Scenario: Valid Road ID is specified
	Given a valid road ID A2 is specified
	When the client is run
	Then Road status found value is true
	And the following status of the given road should be present
	| Property		 | Value    |
	| DisplayName    | A2       |
	| Url			 | /Road/a2 |

Scenario: InValid Road ID is specified
	Given a valid road ID XABCD is specified
	When the client is run
	Then Road status found value is false


	
