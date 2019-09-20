@game @gaming @guessing
Feature: Guess the word

# The first example has two steps
@maker
Scenario: Maker starts a game
When the Maker starts a game
Then the Maker waits for a Breaker to join

# The second example has three steps
@breaker @join
Scenario: Breaker joins a game
Given the Maker has started a game with the word "silky"
When the Breaker joins the Maker's game
Then the Breaker must guess a word with 5 characters