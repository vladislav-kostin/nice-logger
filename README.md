# Nice Logger Package

## Description

When working on team project people always leave behind no longer needed logs. This Logger has explicit methods for either `Temp` or `Important` logs emphasizing that only Important logs should be commited. And gives them clear prefixes with calling class and method names, making them more informative and readable at a glance. I haven't tried it, but the goal is to disallow Temp log commits via git hooks.

(In this package there is also an implementation of a custom log handler that overrides Debug but I decided against that approach so it is not completed. But you can check out the LogHandler to see how to fully customize all Debug logs and how to enable code via project settings.) 

## Usage

- You can use following methods:
  - `Logger.LogDebug("message")`
  - `Logger.LogWarning("message")`
  - `Logger.LogError("message")`
  - `Logger.Assert("message")`
  - `Logger.LogImportant("message")`
- Colors are customisable via **ProjectSettings>LoggerSettings**
- Use square brackets to highlight elements: `Logger.LogDebug($"Loaded [{5}] files")`, `Logger.LogDebug("Status: [Initialized]")`

## Screenshot

![34tt](https://github.com/user-attachments/assets/707806ed-0d68-4bf7-8d13-2b47634f5fcf)

## Roadmap

- It should be pretty easy to make a runtime UI for the logs, so I might do it at some point
