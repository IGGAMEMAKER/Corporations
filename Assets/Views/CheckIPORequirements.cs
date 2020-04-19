using Assets.Core;
using UnityEngine.UI;

public class CheckIPORequirements : View
{
    public Button IPOButton;
    public Hint Hint;


    public override void ViewRender()
    {
        base.ViewRender();


        int companyId = SelectedCompany.company.Id;

        bool hasShareholders = Companies.IsMeetsIPOShareholderRequirement(Q, companyId);
        bool costsALot = Companies.IsMeetsIPOCompanyCostRequirement(Q, companyId);
        bool isProfitable = Companies.IsMeetsIPOProfitRequirement(Q, companyId);

        Hint.SetHint($"Requirements" +
            Visuals.Colorize($"\nCompany Cost more than ${Format.Minify(C.IPO_REQUIREMENTS_COMPANY_COST)}", costsALot)  +
            Visuals.Colorize($"\nMore than 3 shareholders", hasShareholders) + 
            Visuals.Colorize($"\nProfit bigger than ${Format.Minify(C.IPO_REQUIREMENTS_COMPANY_PROFIT)}", isProfitable)
            );

        IPOButton.interactable = Companies.IsCanGoPublic(Q, SelectedCompany.company.Id);
    }
}
