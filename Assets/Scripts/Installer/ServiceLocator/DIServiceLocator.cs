using System;
using System.Collections.Generic;
using UnityEngine;

public class DIServiceLocator : MonoBehaviour
{
    private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

    /// <summary>
    /// T 타입 서비스 등록
    /// </summary>
    public void Register<T>(T instance)
    {
        Type type = typeof(T);

        if (services.ContainsKey(type))
        {
            Debug.LogWarning($"[ServiceLocator] {type.Name} 이미 등록되어 덮어씁니다.");
        }

        services[type] = instance;
    }

    /// <summary>
    /// T 타입 서비스 반환
    /// </summary>
    public T Resolve<T>()
    {
        Type type = typeof(T);

        if (services.TryGetValue(type, out object service))
        {
            return (T)service;
        }

        Debug.LogError($"[ServiceLocator] {type.Name} 타입 서비스가 등록되지 않았습니다.");
        return default;
    }

    public bool IsRegistered<T>()
    {
        return services.ContainsKey(typeof(T));
    }
}
