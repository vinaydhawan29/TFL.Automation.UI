Feature: EditJourneyPreferences

  As a user of the journey planning system
  I want to be able to edit my preferences after planning a journey
  So that I can customize my route based on my current needs

  Background:
    Given I have planned a journey from "Leicester Square Underground Station" to "Covent Garden Underground Station"

    @Automation
  Scenario: Edit preferences to select route with least walking
    When I select "Edit preferences" option
    And I choose "Least walking" as my preference
    And I update the journey
    Then the system should recalculate the route
    And the new route should prioritize minimal walking distance
    And the journey details should be updated accordingly

    @ManualScenarios
  Scenario: Verify updated journey reflects least walking preference
    Given I have edited preferences to "Least walking"
    When I view the updated journey details
    Then I should see a route with minimal walking segments
    And the total walking distance should be less than or equal to the original route
    And the journey summary should indicate "Least walking" preference

    @ManualScenarios
  Scenario Outline: Compare walking distances before and after preference change
    Given I have noted the original walking distance
    When I change the preference to "Least walking"
    And I update the journey
    Then the new walking distance should be less than or equal to the original
    And the difference in walking distance should be at least <minimum_reduction> percent

    Examples:
      | minimum_reduction |
      | 10                |
      | 20                |
      | 30                |

