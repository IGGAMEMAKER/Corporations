using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class ShowProductChanges : View
{
    public GameObject ClientChanges;
    GameEntity product;

    public void SetEntity(GameEntity entity)
    {
        product = entity;
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (product == null)
            return;

        var newClients = Marketing.GetAudienceGrowth(product, Q);
        ClientChanges.GetComponent<Text>().text = Visuals.Positive(Format.Minify(newClients)) + " users";

        if (ScheduleUtils.IsPeriodEnd(Q))
        {
            ClientChanges.AddComponent<AnimateResourceChange>().Renewable = true;
            ClientChanges.SetActive(true);
        }
    }
}
