Feature: JourneyPlanner Verification from Leicester Square to Covent Garden

A short summary of the feature: Scenarios for Plan a journey widget - Journey Planner ont he TFL wesbsite under test

Background: 
Given the journey planner website is up and running

    @Automation
  Scenario: Verify walking journey from Leicester Square to Covent Garden
    When I request a walking route from "Leicester Square Underground Station" to "Covent Garden Underground Station"
    Then I should receive a valid route
    And the walking time should be between 1 and 15 minutes
    And the walking distance should be approximately 0.8 kilometers

    @Automation
  Scenario: Verify cycling journey from Leicester Square to Covent Garden
    When I request a cycling route from "Leicester Square Underground Station" to "Covent Garden Underground Station"
    Then I should receive a valid route
    And the cycling time should be between 3 and 10 minutes
    And the cycling distance should be approximately 0.8 kilometers

     @Automation
  Scenario: Compare walking and cycling times
    When I request both walking and cycling routes from "Leicester Square Underground Station" to "Covent Garden Underground Station"
    Then the cycling time should be less than the walking time
    And both routes should have the same approximate distance


    @ManualScenarios
  Scenario Outline: Verify journey details for different transportation modes
    When I request a <mode> route from "Leicester Square Underground Station" to "Covent Garden Underground Station"
    Then I should receive a valid route
    And the estimated time should be within acceptable range for <mode>
    And the route should use appropriate <mode> paths or roads

    Examples:
      | mode     |
      | walking  |
      | cycling  |