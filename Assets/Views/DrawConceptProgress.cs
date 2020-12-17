using Assets.Core;
using UnityEngine.UI;

public class DrawConceptProgress : View
{
    public Image Progress;

    internal void SetEntity(GameEntity company)
    {
        Progress.fillAmount = getProgress(company) / 100;

        var isPlayerRelated = Companies.IsDirectlyRelatedToPlayer(Q, company);

        Progress.gameObject.SetActive(isPlayerRelated);
    }

    float getProgress(GameEntity company)
    {
        return 0;
    }
}
