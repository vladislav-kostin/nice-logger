# Nice Dependency Injection Package

## Description

This logger displayes a clear prefix in front of a message, line number and file name, and then the message with highlights. The log message is clickable and will take you to the correct line.

One of the reasons to have prefixes is to disallow team members to commit temporary debug logs, so that logs remain clean and clear for everyone. Currently `Logger.LogDebug("message")` is intended as that temp log type. You can setup git commit hooks to block commits with those.

## Usage

- You can use following methods: `Logger.LogDebug("message")`,  `Logger.LogWarning("message")`,  `Logger.LogError("message")`, `Logger.Assert("message")`, `Logger.LogImportant("message")`
- Colors are customisable via ProjectSetting>LoggerSettings
- Use square brackets to highlight elements: `Logger.LogDebug($"Loaded [{5}] files")`, `Logger.LogDebug("Status: [Initialized]")`