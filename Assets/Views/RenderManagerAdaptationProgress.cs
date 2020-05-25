using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderManagerAdaptationProgress : View
{
    public Image AdaptationProgress;

    GameEntity worker;

    public void SetEntity(GameEntity worker)
    {
        this.worker = worker;

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (worker == null)
            return;

        var adaptation = worker.humanCompanyRelationship.Adapted;
        bool isAdapted = adaptation == 100;

        AdaptationProgress.fillAmount = adaptation / 100f;
        AdaptationProgress.enabled = !isAdapted;
    }
}
