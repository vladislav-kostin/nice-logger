# Nice Dependency Injection Package

## WARNING

This is a very early version tested only on a single scene setup with MonoBehaviors.  

## Description

This package provides minimal setup dependency injection for MonoBehavior scripts. All you need is `[Inject]` attribute on a class or a field. No installation, no interfaces.  

## Usage

Add `[Inject]` attribute to a MonoBehavior-based class declaration for it to be injected into any field of that type in other MonoBehavior scripts with `[Inject]` attributes. The attribute is inherited.  

The injection is initialized right after scene load, after all `Awake` methods and before all `Start` methods. The classes that are being injected are currently expected to be in the scene when the injection happens.  

If the object is spawned dynamically, it needs a `DependencyInjection` attribute.  

## Example

```csharp
using UnityEngine;

[Inject]
public class PlayerService : MonoBehaviour
{
    public void Initialize()
    {
        Debug.Log("PlayerService initialized!");
    }
}
```

```csharp
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Inject]
    private PlayerService _playerService;

    private void Start()
    {
        if (_playerService != null)
        {
            _playerService.Initialize();
            Debug.Log("PlayerService injected successfully!");
        }
        else
        {
            Debug.LogError("PlayerService was not injected!");
        }
    }
}
```

## Limitations
- Not optimized for large scenes with a lot of preexisting prefab instances with a lot of scripts.  
- Only injects into fields. I can't come up with a use case for properties, and you can inject auto-properties by applying the attribute to the backing field `[field: Inject]`.  
