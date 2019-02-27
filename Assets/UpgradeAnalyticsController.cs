using Entitas;
using UnityEngine;

public class UpgradeAnalyticsController : ButtonController
{
    void OnUpgradeAnalytics()
    {
        GameEntity[] controlled = Contexts.sharedInstance.game
            .GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.ControlledByPlayer));

        Contexts.sharedInstance.game.CreateEntity().AddEventUpgradeAnalytics(controlled[0].product.Id);
        Debug.Log("OnUpgradeAnalytics");
    }

    public override void Execute() => OnUpgradeAnalytics();
}
