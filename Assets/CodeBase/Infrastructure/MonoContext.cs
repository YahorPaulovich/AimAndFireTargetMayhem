using System.Collections.Generic;
using UnityEngine;

public interface IEnableComponent
{
    void OnEnable();
}

public interface IDisableComponent
{
    void OnDisable();
}

public class MonoContext : MonoBehaviour
{
    private List<IEnableComponent> _enableComponents = new();
    private List<IDisableComponent> _disableComponents = new();

    private void Awake()
    {
        AddComponents(ProvideComponents());
    }

    private void AddComponents(IEnumerable<object> provideComponents)
    {
        foreach (var component in provideComponents) 
        {
            if (component is IEnableComponent enableComponent)
            {
                _enableComponents.Add(enableComponent);
            }

            if (component is IDisableComponent disableComponent)
            {
                _disableComponents.Add(disableComponent);
            }
        }
    }

    protected virtual IEnumerable<object> ProvideComponents()
    {
        yield break;
    }

    private void OnEnable()
    {
        foreach (var component in _enableComponents)
        {
            component.OnEnable();
        }
    }

    private void OnDisable()
    {
        foreach (var component in _disableComponents)
        {
            component.OnDisable();
        }
    }
}
