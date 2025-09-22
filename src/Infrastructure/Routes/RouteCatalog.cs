// apps/XIVSubmarinesRewrite/src/Infrastructure/Routes/RouteCatalog.cs
// 潜水艦航路 ID を読みやすい表記へ変換するためのカタログを提供します
// UI や通知で航路を直感的に理解できるようにするため存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/src/Presentation/Rendering/OverviewWindowRenderer.cs, apps/XIVSubmarinesRewrite/src/Application/Services/NotificationCoordinator.cs

namespace XIVSubmarinesRewrite.Infrastructure.Routes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dalamud.Plugin.Services;
using Lumina.Excel.Sheets;
using XIVSubmarinesRewrite.Infrastructure.Logging;

/// <summary>Provides helpers for formatting submarine route identifiers.</summary>
public sealed class RouteCatalog
{
    private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private readonly Dictionary<uint, SubmarineExploration> sectorLookup = new ();
    private uint[] startingSectors = Array.Empty<uint>();
    private readonly ILogSink log;

    public RouteCatalog(IDataManager? dataManager, ILogSink log)
    {
        this.log = log;
        if (dataManager is null)
        {
            return;
        }

        try
        {
            var sheet = dataManager.GetExcelSheet<SubmarineExploration>();
            if (sheet is null)
            {
                return;
            }

            var starts = new List<uint>();
            foreach (var row in sheet)
            {
                this.sectorLookup[row.RowId] = row;
                if (row.StartingPoint)
                {
                    starts.Add(row.RowId);
                }
            }

            this.startingSectors = starts
                .Distinct()
                .OrderByDescending(v => v)
                .ToArray();
        }
        catch (Exception ex)
        {
            this.log.Log(LogLevel.Warning, "[Routes] Failed to initialize route catalog.", ex);
        }
    }

    /// <summary>Formats a dash-delimited route identifier into an alphabetic representation.</summary>
    public string FormatRoute(string? routeId)
    {
        if (string.IsNullOrWhiteSpace(routeId))
        {
            return string.Empty;
        }

        if (this.sectorLookup.Count == 0 || this.startingSectors.Length == 0)
        {
            return routeId;
        }

        var segments = routeId.Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (segments.Length == 0)
        {
            return routeId;
        }

        var formatted = new List<string>(segments.Length);
        foreach (var segment in segments)
        {
            if (!uint.TryParse(segment, out var sectorId))
            {
                formatted.Add(segment);
                continue;
            }

            formatted.Add(this.ConvertSectorToLetter(sectorId));
        }

        return formatted.Count == 0 ? routeId : string.Join('-', formatted);
    }

    private string ConvertSectorToLetter(uint sectorId)
    {
        if (!this.sectorLookup.ContainsKey(sectorId))
        {
            return sectorId.ToString();
        }

        var start = this.FindVoyageStart(sectorId);
        var offset = sectorId > start ? sectorId - start : 1u;
        return this.ToAlphabetic(offset);
    }

    private uint FindVoyageStart(uint sectorId)
    {
        foreach (var start in this.startingSectors)
        {
            if (sectorId >= start)
            {
                return start;
            }
        }

        return this.startingSectors[^1];
    }

    private string ToAlphabetic(uint value)
    {
        if (value == 0)
        {
            return "A";
        }

        var index = (int)(value - 1);
        var builder = new StringBuilder();
        while (index >= 0)
        {
            builder.Insert(0, Letters[index % Letters.Length]);
            index = (index / Letters.Length) - 1;
        }

        return builder.ToString();
    }
}
