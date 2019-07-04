using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class NotificationRendererNewCompany : NotificationRenderer<NotificationMessageNewCompany>
{
    public override void Render(NotificationMessageNewCompany message, Text Title, Text Description, GameObject LinkToEvent)
    {
        var product = CompanyUtils.GetCompanyById(GameContext, message.CompanyId);

        Description.text = $"STARTUP on niche {GetNicheName(product.product.Niche)}. Will they change the world?";

        Title.text = GetTitle(message, GameContext);

        LinkToEvent.AddComponent<LinkToProjectView>().CompanyId = message.CompanyId;
    }

    public static string GetTitle(NotificationMessageNewCompany message, GameContext gameContext)
    {
        return $"New Startup! {CompanyUtils.GetCompanyById(gameContext, message.CompanyId).company.Name}";
    }
}
