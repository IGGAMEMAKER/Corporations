using System;
using Assets.Core;

public partial class ProductDevelopmentSystem : OnPeriodChange
{
    void HandleTeam(GameEntity product)
    {
        foreach (var t in product.team.Teams)
        {
            var time = DateTime.Now;

            MeasureTag("Team Upgrades", time);

            time = DateTime.Now;
            Teams.FillTeam(product, gameContext, t);

            MeasureTag("Teams Fill", time);
        }
    }
}
