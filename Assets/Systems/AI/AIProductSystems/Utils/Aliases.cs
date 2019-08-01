using Assets.Classes;
using Assets.Utils;
using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;

// utils, logs and aliases
public partial class AIProductSystems
{
    static T PickMostImportantValue<T>(Dictionary<T, long> values)
    {
        long value = 0;
        T goal = default;

        foreach (var pair in values)
        {
            if (pair.Value > value)
            {
                value = pair.Value;
                goal = pair.Key;
            }
        }

        return goal;
    }

    int GetDate()
    {
        return ScheduleUtils.GetCurrentDate(gameContext);
    }

    // logging
    TestComponent GetLogs()
    {
        return gameContext.GetEntities(GameMatcher.Test)[0].test;
    }

    bool GetLog(LogTypes logTypes)
    {
        return GetLogs().logs[logTypes];
    }

    void Print(string action, GameEntity company)
    {
        bool isMyCompany = company.isControlledByPlayer || CompanyUtils.IsCompanyRelatedToPlayer(gameContext, company);
        bool isMyCompetitor = false; // player != null && company.product.Niche == player.product.Niche;

        bool canRenderMyCompany = GetLog(LogTypes.MyProductCompany) && isMyCompany;
        bool canRenderMyCompetitors = GetLog(LogTypes.MyProductCompanyCompetitors) && isMyCompetitor;

        string companyName = company.company.Name;
        if (isMyCompany)
            companyName = Visuals.Colorize(company.company.Name, VisualConstants.COLOR_COMPANY_WHERE_I_AM_CEO);

        if (canRenderMyCompany || canRenderMyCompetitors)
            Debug.Log($"Goal: {ChooseGoal(company)}. {action} : {companyName}");
    }

    TeamResource GetResourceChange(GameEntity company)
    {
        return CompanyEconomyUtils.GetResourceChange(company, gameContext);
    }

    TeamResource GetConceptCost(GameEntity company)
    {
        return ProductUtils.GetDevelopmentCost(company, gameContext);
    }

    TeamResource GetProductUpgradeCost(GameEntity company)
    {
        return ProductUtils.GetProductUpgradeCost(company, gameContext);
    }
}
