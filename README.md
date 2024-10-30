﻿# Journey Planner Test Automation Framework

## Overview
This framework is designed to automate testing of a TFL website - journey planning.
It uses SpecFlow for Behavior-Driven Development (BDD) and Selenium WebDriver for browser automation.

## Table of Contents
1. [Prerequisites](#prerequisites)
2. [Setup](#setup)
3. [Project Structure](#project-structure)
4. [Writing Tests](#writing-tests)
5. [Running Tests](#running-tests)
6. [Reporting](#reporting)
7. [Best Practices](#best-practices)
8. [Troubleshooting](#troubleshooting)

## Prerequisites
- .NET Core SDK (version X.X or higher)
- Visual Studio 2019 or later (for Windows users)
- Visual Studio Code (for cross-platform development)
- Chrome browser

## Setup
1. Clone the repository: https://github.com/vinaydhawan29/TFL.Automation.UI

## Project Structure

journey-planner-tests/
│
├── Features/
│ └── JourneyPlanning.feature
│ └── EditPreferences.feature	
│
├── StepDefinitions/
│ └── JourneyPlanningSteps.cs
│ └── EditPreferencesSteps.cs
│
├── Pages/
│ └── HomePage.cs
│ └── SearchResultsPage.cs
│
├── Factory/
│ └── BrowserFactory.cs
│
├── Helpers/
│ ├── WebDriverExtensions.cs
│ └── ConfigReader.cs
│
├── TestResults/
│
└── Hooks.cs
│
└── README.md



## Test Results
Test results are generated in the [TestResults/]() directory after each test run. 
HTML reports provide detailed information about test execution.


## Decisions
Use SpecFlow for Behavior-Driven Development. - Allows for writing tests in Gherkin syntax, making them readable by non-technical stakeholders and promoting collaboration between developers, testers, and business analysts.

## Selenium WebDriver for Browser Automation
Use Selenium WebDriver for interacting with web elements.
Rationale: Provides a robust, widely-used solution for browser automation across different browsers and platforms.

## Page Object Model (POM):
Implement the Page Object Model design pattern.
Rationale: Enhances test maintenance and reduces code duplication by separating page-specific code from test code.

## Centralized Configuration:
Use a ConfigReader class to manage test configurations.
Rationale: Allows for easy management of environment-specific settings and test parameters without changing the test code.

## Custom Extension Methods:
Create WebDriverExtensions for common Selenium actions.
Rationale: Improves code readability and reduces duplication by encapsulating common operations like finding elements and waiting for conditions.

## Scenario Context Injection:
Use SpecFlow's built-in dependency injection for sharing context between steps.
Rationale: Promotes cleaner code and easier state management across different step definition files.

## Explicit Waits:
Implement explicit waits instead of implicit waits or Thread.Sleep().
Rationale: Improves test reliability and speed by waiting only as long as necessary for specific conditions.

## Detailed Reporting:
Implement ExtentReports for comprehensive test reporting.
Rationale: Provides detailed, visually appealing reports that are easy to understand and share with stakeholders.

## Flexible Locator Strategy:
Use a mix of locator strategies (ID, XPath) based on the most stable and efficient option for each element.
Rationale: Improves test reliability and maintenance by using the most appropriate locator for each situation.

## Error Handling and Logging:
Implement robust error handling and logging throughout the framework.
Rationale: Aids in debugging and provides clear information about test failures.

## Cross-Browser Testing Support: Provision is provided via the Factory class and can be extended easily and quickly to run the smae tests across multiple browsers
Design the framework to easily switch between different browsers.
Rationale: Ensures the application works correctly across different browser environments.

## Environment-Based Execution via loading environment variables using local.settings.local file
Implement the ability to run tests against different environments (e.g., dev, staging, production).
Rationale: Allows for testing in various environments without code changes.

## Data-Driven Testing:
Utilize SpecFlow's Scenario Outline feature for data-driven tests.
Rationale: Enables testing multiple data sets without duplicating test code.

## CI/CD Integration: Not Implemented currently but this framework is extendable and CI-CD can be implmented easily and quickly with minimal changes
Design the framework to be easily integrated with CI/CD pipelines.
Rationale: Facilitates automated testing as part of the development and deployment process.

## Modular and Reusable Step Definitions:
Create modular, reusable step definitions.
Rationale: Promotes code reuse and makes it easier to create new scenarios using existing steps.

These development decisions were made to create a robust, maintainable, and efficient test automation framework. They address common challenges in test automation such as reliability, maintainability, speed, and ease of use. The framework is designed to be flexible enough to accommodate future changes and expansions to the testing scope.