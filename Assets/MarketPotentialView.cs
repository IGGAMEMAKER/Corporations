using Assets.Utils;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MarketPotentialView : View
{
    public Text PotentialMarketSize;
    public Text PotentialAudienceSize;

    public SetAmountOfStars SetAmountOfStars;

    public Text RiskLabel;
    public Hint RiskHint;

    public Text Demand;
    public Text Maintenance;
    public Text TeamMaintenance;

    public Text ChangeSpeed;

    public Text BiggestIncome;

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    public void Render()
    {
        NicheType nicheType = ScreenUtils.GetSelectedNiche(GameContext);
        var niche = NicheUtils.GetNicheEntity(GameContext, nicheType);

        var rating = NicheUtils.GetMarketRating(niche);
        SetAmountOfStars.SetStars(rating);

        var speed = niche.nicheLifecycle.NicheChangeSpeed;
        //var iteration = (int)speed;
        //ChangeSpeed.text = $"{iteration} months";

        var iteration = ProductUtils.GetBaseConceptTime(niche);
        //var iteration = speed.ToString();
        ChangeSpeed.text = iteration + " days";

        PotentialMarketSize.text = Format.Money(NicheUtils.GetMarketPotential(niche));
        PotentialAudienceSize.text = Format.MinifyToInteger(NicheUtils.GetMarketAudiencePotential(niche)) + " users";

        var risk = NicheUtils.GetMarketDemandRisk(GameContext, nicheType);
        string riskText = NicheUtils.ShowRiskStatus(risk).ToString();

        RiskLabel.text = $"{risk}% ({riskText})";

        var demand = MarketingUtils.GetCurrentClientFlow(GameContext, nicheType); // * MarketingUtils.GetMarketingFinancingAudienceReachModifier(MarketingFinancing.High) * 30;
        Demand.text = Format.MinifyToInteger(demand) + " monthly";

        var maintenance = NicheUtils.GetBaseMarketingMaintenance(niche).money;
        Maintenance.text = Format.MoneyToInteger(maintenance) + " / month";

        var teamMaintenance = NicheUtils.GetTeamMaintenanceCost(niche);
        TeamMaintenance.text = Format.MoneyToInteger(teamMaintenance) + " / month";


        if (BiggestIncome != null)
        {
            var players = NicheUtils.GetPlayersOnMarket(GameContext, niche.niche.NicheType);

            if (players.Count() == 0)
                BiggestIncome.text = "???";
            else
            {
                var max = players.Max(p => CompanyEconomyUtils.GetCompanyIncome(p, GameContext));
                BiggestIncome.text = Format.Money(max) + " / month";
            }
        }
    }
}
