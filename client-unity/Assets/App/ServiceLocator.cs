using System;
using System.Collections.Generic;

public static class ServiceLocator {
    private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void Register<T>(T implementation) {
        _services[typeof(T)] = implementation;
    }

    public static T Resolve<T>() {
        if (_services.TryGetValue(typeof(T), out var impl)) {
            return (T)impl;
        }
        throw new Exception($"Service not registered: {typeof(T).FullName}");
    }
}
