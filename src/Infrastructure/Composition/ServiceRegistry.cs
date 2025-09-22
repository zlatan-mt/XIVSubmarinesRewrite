namespace XIVSubmarinesRewrite.Infrastructure.Composition;

using System;
using System.Collections.Generic;

/// <summary>Lightweight service container used until a DI library is adopted.</summary>
public sealed class ServiceRegistry : IDisposable
{
    private readonly Dictionary<Type, object> singletons = new ();
    private readonly List<IDisposable> disposables = new ();

    private ServiceRegistry()
    {
    }

    public static ServiceRegistry CreateDefault() => new ();

    public void RegisterSingleton<TService>(TService instance) where TService : class
    {
        this.singletons[typeof(TService)] = instance;
        if (instance is IDisposable disposable && !this.disposables.Contains(disposable))
        {
            this.disposables.Add(disposable);
        }
    }

    public TService Resolve<TService>() where TService : class
    {
        if (this.singletons.TryGetValue(typeof(TService), out var value) && value is TService typed)
        {
            return typed;
        }

        throw new InvalidOperationException($"Service not registered: {typeof(TService).FullName}");
    }

    public void Dispose()
    {
        for (var i = this.disposables.Count - 1; i >= 0; i--)
        {
            this.disposables[i].Dispose();
        }

        this.disposables.Clear();
        this.singletons.Clear();
    }
}
