using Assets.Utils;
using UnityEngine.UI;

public class CheckIPORequirements : View
{
    public Button IPOButton;
    public Hint Hint;


    public override void ViewRender()
    {
        base.ViewRender();


        int companyId = SelectedCompany.company.Id;

        Hint.SetHint($"Requirements" +
            Visuals.Colorize($"\nCompany Cost more than ${Format.Minify(Constants.IPO_REQUIREMENTS_COMPANY_COST)}", Companies.IsMeetsIPOCompanyCostRequirement(GameContext, companyId))  +
            Visuals.Colorize($"\nMore than 3 shareholders", Companies.IsMeetsIPOShareholderRequirement(GameContext, companyId)) + 
            Visuals.Colorize($"\nProfit bigger than ${Format.Minify(Constants.IPO_REQUIREMENTS_COMPANY_PROFIT)}", Companies.IsMeetsIPOProfitRequirement(GameContext, companyId))
            );

        IPOButton.interactable = Companies.IsCanGoPublic(GameContext, SelectedCompany.company.Id);
    }
}
