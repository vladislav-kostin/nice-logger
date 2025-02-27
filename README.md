# Nice Dependency Injection Package

## Description

When working on team project people always leave behind no longer needed logs. This Logger has explicit methods for either `Temp` or `Important` logs emphasizing that only Important logs should be commited. And gives them clear prefixes and calling clas method, making them more informatie and readable at a glance. I haven't tried it, but the goal is to disallow Temp log commits via git hooks.

(In this package there is also an implementation of the logger with custom log handler that overrides Debug but I decided against that approach so it is not completed. But you can check out LogHandler to see how to fully customize all Debug logs and how to enable code via project settings.) 

## Usage

- You can use following methods:
  - `Logger.LogDebug("message")`
  - `Logger.LogWarning("message")`
  - `Logger.LogError("message")`
  - `Logger.Assert("message")`
  - `Logger.LogImportant("message")`
- Colors are customisable via **ProjectSettings>LoggerSettings**
- Use square brackets to highlight elements: `Logger.LogDebug($"Loaded [{5}] files")`, `Logger.LogDebug("Status: [Initialized]")`

## Roadmap

- It should be pretty easy to make a runtime UI for the logs, so I might do it at some point
