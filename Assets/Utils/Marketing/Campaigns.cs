using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static void StartBrandingCampaign(GameContext gameContext, GameEntity company)
        {
            CompanyUtils.AddCooldown(gameContext, company, CooldownType.BrandingCampaign, 180);

            Debug.Log("Spend Branding campaign resources");
        }
    }
}
