// src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.Lifecycle.cs
// Dalamud UI スナップショット取得のライフサイクル処理をまとめます
// メイン部分を簡潔に保ち、イベント登録の責務を分離するために存在します
// RELEVANT FILES:src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.cs,src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowExtraction.cs,src/Infrastructure/Acquisition/DalamudUiSubmarineSnapshotSource.RowParsing.cs

namespace XIVSubmarinesRewrite.Infrastructure.Acquisition;

using System;
using System.Reflection;
using Dalamud.Game.Addon.Lifecycle;
using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using Dalamud.Plugin.Services;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>
/// Partial class slice that is responsible for wiring and unwiring Dalamud addon lifecycle events.
/// </summary>
public sealed unsafe partial class DalamudUiSubmarineSnapshotSource
{
    private static readonly AddonEvent[] ObservedEvents =
    {
        AddonEvent.PostSetup,
        AddonEvent.PostRefresh,
        AddonEvent.PreFinalize,
    };

    private static readonly string[] ObservedAddons =
    {
        "SelectString",
    };

    private void RegisterLifecycle()
    {
        if (this.addonLifecycle is null)
        {
            this.log.Log(LogLevel.Warning, "[UI Inspector] AddonLifecycle service unavailable; fallback to polling.");
            return;
        }

        var registered = this.TryRegisterLifecycleInterface();
        if (!registered)
        {
            registered = this.TryRegisterLifecycleReflection();
        }

        if (!registered)
        {
            this.log.Log(LogLevel.Warning, "[UI Inspector] No suitable AddonLifecycle listener registered; fallback to polling.");
        }
    }

    private bool TryRegisterLifecycleInterface()
    {
        if (this.addonLifecycle is null)
        {
            return false;
        }

        var anySuccess = false;
        foreach (var addonName in ObservedAddons)
        {
            foreach (var eventType in ObservedEvents)
            {
                try
                {
                    this.addonLifecycle.RegisterListener(eventType, addonName, this.lifecycleHandler);
                    this.lifecycleRegistrations.Add((eventType, addonName));
                    this.log.Log(LogLevel.Information, "[UI Inspector] Registered " + addonName + " lifecycle listener for " + eventType + ".");
                    anySuccess = true;
                }
                catch (MissingMethodException)
                {
                    return false;
                }
                catch (NotSupportedException)
                {
                    return false;
                }
                catch (Exception ex)
                {
                    this.log.Log(LogLevel.Warning, "[UI Inspector] Failed to register " + addonName + " listener for " + eventType + ".", ex);
                }
            }
        }

        return anySuccess;
    }

    private bool TryRegisterLifecycleReflection()
    {
        if (this.addonLifecycle is null)
        {
            return false;
        }

        try
        {
            var lifecycleType = this.addonLifecycle.GetType();
            var methods = lifecycleType.GetMethods();
            var handlerMethod = typeof(DalamudUiSubmarineSnapshotSource).GetMethod(nameof(this.OnAddonLifecycleInternal), BindingFlags.NonPublic | BindingFlags.Instance);
            if (handlerMethod is null)
            {
                return false;
            }

            foreach (var method in methods)
            {
                if (method.Name != "RegisterListener" || method.GetParameters().Length < 3)
                {
                    continue;
                }

                var parameters = method.GetParameters();
                if (parameters[0].ParameterType != typeof(AddonEvent) || parameters[1].ParameterType != typeof(string))
                {
                    continue;
                }

                var callbackType = parameters[2].ParameterType;
                var callback = Delegate.CreateDelegate(callbackType, this, handlerMethod, false);
                if (callback is null)
                {
                    continue;
                }

                var success = false;
                foreach (var addonName in ObservedAddons)
                {
                    foreach (var eventType in ObservedEvents)
                    {
                        try
                        {
                            var result = method.Invoke(this.addonLifecycle, new object?[] { eventType, addonName, callback });
                            if (result is IDisposable disposable)
                            {
                                this.lifecycleSubscriptions.Add(disposable);
                            }

                            this.log.Log(LogLevel.Information, "[UI Inspector] Registered " + addonName + " lifecycle listener for " + eventType + " via reflection.");
                            success = true;
                        }
                        catch (TargetInvocationException ex)
                        {
                            this.log.Log(LogLevel.Error, "[UI Inspector] Failed to register lifecycle listener via reflection.", ex.InnerException ?? ex);
                        }
                        catch (Exception ex)
                        {
                            this.log.Log(LogLevel.Error, "[UI Inspector] Failed to register lifecycle listener via reflection.", ex);
                        }
                    }
                }

                if (success)
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            this.log.Log(LogLevel.Error, "[UI Inspector] Failed to register lifecycle listener via reflection.", ex);
        }

        return false;
    }

    private void OnAddonLifecycle(AddonEvent eventType, AddonArgs addonArgs)
    {
        this.ProcessLifecycleEvent(eventType, addonArgs, addonArgs);
    }

    private void OnAddonLifecycleInternal(AddonEvent eventType, object addonArgs)
    {
        this.ProcessLifecycleEvent(eventType, addonArgs as AddonArgs, addonArgs);
    }

    private static nint ExtractAddonPointer(object? addonArgs)
    {
        if (addonArgs is null)
        {
            return nint.Zero;
        }

        try
        {
            var type = addonArgs.GetType();
            var property = type.GetProperty("Addon");
            if (property != null)
            {
                var value = property.GetValue(addonArgs);
                if (value is nint directPtr)
                {
                    return directPtr;
                }

                if (value is not null)
                {
                    try
                    {
                        dynamic dyn = value;
                        return (nint)dyn.Value;
                    }
                    catch
                    {
                    }

                    try
                    {
                        dynamic dyn = value;
                        return (nint)dyn.Address;
                    }
                    catch
                    {
                    }
                }

                var valueType = value?.GetType();
                if (valueType != null)
                {
                    var valueProperty = valueType.GetProperty("Value");
                    if (valueProperty != null)
                    {
                        var inner = valueProperty.GetValue(value);
                        if (inner is nint innerPtr)
                        {
                            return innerPtr;
                        }
                    }

                    var field = valueType.GetField("Value");
                    if (field != null)
                    {
                        var inner = field.GetValue(value);
                        if (inner is nint fieldPtr)
                        {
                            return fieldPtr;
                        }
                    }

                    var addressField = valueType.GetField("Address");
                    if (addressField != null)
                    {
                        var inner = addressField.GetValue(value);
                        if (inner is nint addressPtr)
                        {
                            return addressPtr;
                        }
                    }
                }
            }
        }
        catch
        {
            // Ignore extraction failure. Callers will handle null pointers.
        }

        return nint.Zero;
    }

    private void DumpAddonArgs(object addonArgs)
    {
        try
        {
            var type = addonArgs.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                object? value = null;
                try
                {
                    value = property.GetValue(addonArgs);
                }
                catch
                {
                }

                var typeName = property.PropertyType.FullName ?? property.PropertyType.Name;
                var valueType = value?.GetType().FullName ?? "<null>";
                this.log.Log(LogLevel.Debug, "[UI Inspector] AddonArgs." + property.Name + " (" + typeName + ") => " + valueType + ": " + (value?.ToString() ?? "<null>"));
            }
        }
        catch (Exception ex)
        {
            this.log.Log(LogLevel.Debug, "[UI Inspector] Failed to dump AddonArgs.", ex);
        }
    }
}
