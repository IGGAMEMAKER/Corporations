using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class CheckIPORequirements : View
{
    public Button IPOButton;
    public Hint Hint;

    void Start()
    {
        Hint.SetHint($"Requirements" +
            $"\nCompany Cost more than ${ValueFormatter.Shorten(Constants.IPO_REQUIREMENTS_COMPANY_COST)}" +
            $"\nMore than 3 shareholders" +
            $"\nProfit bigger than ${ValueFormatter.Shorten(Constants.IPO_REQUIREMENTS_COMPANY_PROFIT)}");
    }

    void Update()
    {
        Render();
    }

    public static bool IsCanGoPublic(GameContext gameContext, int companyId)
    {
        return 
            !CompanyUtils.GetCompanyById(gameContext, companyId).isPublicCompany &&
            CompanyUtils.IsMeetsIPORequirements(gameContext, companyId);
    }

    void Render()
    {
        IPOButton.interactable = IsCanGoPublic(GameContext, SelectedCompany.company.Id);
    }
}
