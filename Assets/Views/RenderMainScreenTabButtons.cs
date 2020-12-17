using Assets.Core;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RenderMainScreenTabButtons : View
{
    public GameObject DevTab;
    public GameObject TeamTab;
    public GameObject UpgradesTab;
    public GameObject GroupTab;
    public GameObject ExpansionTab;

    public GameObject CorporateCulture;
    public GameObject Investments;
    public GameObject Messages;
    public GameObject CompetitorsTab;

    //public TopPanelManager TopPanelManager;


    public override void ViewRender()
    {
        base.ViewRender();


        var hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);

        var playerCanExploreAdvancedTabs = hasReleasedProducts;

        var daughters = Companies.GetDaughterProducts(Q, MyCompany);
        var numberOfDaughters = daughters.Length;

        var operatingMarkets = GetOperatingMarkets(daughters);

        bool bankruptcyLooming = TutorialUtils.IsOpenedFunctionality(Q, TutorialFunctionality.BankruptcyWarning);

        bool godMode = TutorialUtils.IsGodMode(Q);

        bool showGroupTab       = godMode || numberOfDaughters > 1 && operatingMarkets.Count > 1;
        bool showCultureTab     = godMode || numberOfDaughters > 1 || Flagship.team.Managers.Count > 1;
        bool showInvestmentsTab = godMode || playerCanExploreAdvancedTabs || bankruptcyLooming;
        bool showTeamTab        = godMode || playerCanExploreAdvancedTabs;
        bool showExpansionTab   = godMode || playerCanExploreAdvancedTabs;
        bool showCompetitors = false; // && godMode || playerCanExploreAdvancedTabs;
        
        Draw(DevTab, true);
        Draw(Messages, false);
        Draw(TeamTab, false && showTeamTab);
        Draw(UpgradesTab, showTeamTab);
        //TeamTab.GetComponentInChildren<TextMeshProUGUI>().text = GetTeamTabLabel();

        Draw(GroupTab, showGroupTab);
        Draw(ExpansionTab, false && showExpansionTab);
        Draw(CompetitorsTab, showCompetitors);

        Draw(CorporateCulture, showCultureTab && false);
        CorporateCulture.GetComponentInChildren<TextMeshProUGUI>().text = GetCorporateCultureLabel();

        Draw(Investments, false && showInvestmentsTab);
        Investments.GetComponentInChildren<TextMeshProUGUI>().text = GetInvestmentRoundLabel();
    }

    string GetTeamTabLabel()
    {
        var text = $"TEAM (+{Flagship.employee.Managers.Count})";

        return text;
    }

    string GetCorporateCultureLabel()
    {
        var text = $"CORPORATE CULTURE";

        var task = Cooldowns.GetCorporateCultureCooldown(MyCompany, Q);

        if (task != null)
        {
            var days = task.EndTime - CurrentIntDate;
            text += $" ({days}d)";
        }

        return text;
    }

    string GetInvestmentRoundLabel()
    {
        var text = "INVESTMENTS";

        if (!Companies.IsReadyToStartInvestmentRound(MyCompany))
        {
            var days = MyCompany.acceptsInvestments.DaysLeft;
            text += $" ({days}d)";
        }

        return text;
    }

    List<NicheType> GetOperatingMarkets(GameEntity[] products)
    {
        var markets = new List<NicheType>();

        foreach (var p in products)
        {
            if (!markets.Contains(p.product.Niche))
                markets.Add(p.product.Niche);
        }

        return markets;
    }
}
