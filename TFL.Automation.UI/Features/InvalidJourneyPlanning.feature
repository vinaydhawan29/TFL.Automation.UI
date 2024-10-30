Feature: InvalidJourneyPlanning

As a user of the journey planning widget
  I want to be notified when I enter invalid locations
  So that I can correct my input and plan a valid journey 

  Background:
    Given the journey planning widget is open

  Scenario: 04_Both "From" and "To" locations are invalid
    When I enter "XYZ123" in the "From" field
    And I enter "ABC456" in the "To" field
    And I submit the journey plan
    Then I should see an error message indicating both locations are invalid
    And no journey results should be displayed

  Scenario: 04_"From" location is invalid
    When I enter "InvalidPlace" in the "From" field
    And I enter "London Bridge" in the "To" field
    And I submit the journey plan
    Then I should see an error message indicating the "From" location is invalid
    And no journey results should be displayed

  Scenario: 05_No locations entered
    Given the journey planning widget is open
    When I leave the "From" field empty
    And I leave the "To" field empty
    And I submit the journey plan
    Then I should see error messages for both empty fields
    And the plan journey button should be disabled
    And no journey results should be displayed

  Scenario: "To" location is invalid
    When I enter "Piccadilly Circus" in the "From" field
    And I enter "NonExistentPlace" in the "To" field
    And I submit the journey plan
    Then I should see an error message indicating the "To" location is invalid
    And no journey results should be displayed

  Scenario: One location is empty and the other is invalid
    When I enter "" in the "From" field
    And I enter "InvalidDestination" in the "To" field
    And I submit the journey plan
    Then I should see an error message for the empty field
    And I should see an error message indicating the "To" location is invalid
    And no journey results should be displayed

  Scenario Outline: Various invalid location combinations
    When I enter "<From>" in the "From" field
    And I enter "<To>" in the "To" field
    And I submit the journey plan
    Then I should see appropriate error messages
    And no journey results should be displayed

    Examples:
      | From           | To             |
      | 123456         | 789012         |
      | !@#$%^         | &*()_+         |
      | London Bridge  | MadeUpPlace    |
      | FictionalTown  | Piccadilly Circus |
      | 　　　　　　　　 | 　　　　　　　　   |

  Scenario: Entering and then clearing locations
    When I enter "London Eye" in the "From" field
    And I enter "Tower Bridge" in the "To" field
    And I clear both "From" and "To" fields
    And I submit the journey plan
    Then I should see error messages for empty fields
    And no journey results should be displayed

  Scenario: Special characters in location names
    When I enter "King's Cross St. Pancras" in the "From" field
    And I enter "Westfield Stratford City!" in the "To" field
    And I submit the journey plan
    Then the journey plan should be processed without errors
    And journey results should be displayed

  Scenario: Very long location names
    When I enter a 100-character long string in the "From" field
    And I enter a 100-character long string in the "To" field
    And I submit the journey plan
    Then I should see an error message indicating the location names are too long
    And no journey results should be displayed
