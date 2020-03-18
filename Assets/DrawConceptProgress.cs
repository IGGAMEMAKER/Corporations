using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawConceptProgress : View
{
    public Image Progress;

    internal void SetEntity(GameEntity company)
    {
        Progress.fillAmount = getProgress(company) / 100;
    }

    float getProgress(GameEntity company)
    {
        var c = Cooldowns.GetTask(Q, new CompanyTaskUpgradeConcept(company.company.Id));

        if (c == null)
            return 0;

        return (CurrentIntDate - c.StartTime) * 100f / (c.EndTime - c.StartTime);
    }
}
