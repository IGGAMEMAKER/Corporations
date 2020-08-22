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

        var isPlayerRelated = Companies.IsRelatedToPlayer(Q, company);

        Progress.gameObject.SetActive(isPlayerRelated);
    }

    float getProgress(GameEntity company)
    {
        return 0;
    }
}
