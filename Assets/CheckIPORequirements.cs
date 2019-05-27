using Assets.Utils;
using UnityEngine.UI;

public class CheckIPORequirements : View
    , IAnyDateListener
{
    public Button IPOButton;
    public Hint Hint;

    void OnEnable()
    {
        ListenDateChanges(this);

        Render();
    }

    void Render()
    {
        int companyId = SelectedCompany.company.Id;

        Hint.SetHint($"Requirements" +
            Visuals.Colorize($"\nCompany Cost more than ${ValueFormatter.Shorten(Constants.IPO_REQUIREMENTS_COMPANY_COST)}", CompanyUtils.IsMeetsIPOCompanyCostRequirement(GameContext, companyId))  +
            Visuals.Colorize($"\nMore than 3 shareholders", CompanyUtils.IsMeetsIPOShareholderRequirement(GameContext, companyId)) + 
            Visuals.Colorize($"\nProfit bigger than ${ValueFormatter.Shorten(Constants.IPO_REQUIREMENTS_COMPANY_PROFIT)}", CompanyUtils.IsMeetsIPOProfitRequirement(GameContext, companyId))
            );

        IPOButton.interactable = CompanyUtils.IsCanGoPublic(GameContext, SelectedCompany.company.Id);
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}
