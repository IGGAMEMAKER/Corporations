 using Assets.Utils;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MarketPotentialView : View
{
    public bool FullView;

    public Text PotentialMarketSize;
    public Text PotentialAudienceSize;

    public SetAmountOfStars SetAmountOfStars;

    public Text RiskLabel;
    public Hint RiskHint;

    public Text Demand;
    public Text Maintenance;
    public Text TeamMaintenance;

    public Text ChangeSpeed;
    public Text ChangeSpeedLabel;

    public Text BiggestIncome;

    public Text StartCapital;
    public Text StartCapitalLabel;

    public Text MonthlyMaintenanceLabel;
    public Text MonthlyMaintenance;

    public Text ROILabel;
    public Text ROI;

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    public void Render()
    {
        NicheType nicheType = ScreenUtils.GetSelectedNiche(GameContext);
        var niche = NicheUtils.GetNiche(GameContext, nicheType);

        var profile = niche.nicheBaseProfile.Profile;

        var rating = NicheUtils.GetMarketRating(niche);
        SetAmountOfStars.SetStars(rating);

        var speed = profile.NicheSpeed;
        //var iteration = (int)speed;
        //ChangeSpeed.text = $"{iteration} months";

        var iteration = ProductUtils.GetBaseIterationTime(niche);
        //var iteration = speed.ToString();
        ChangeSpeed.text = iteration + " days";

        PotentialMarketSize.text = Format.Money(NicheUtils.GetMarketPotential(niche));
        PotentialAudienceSize.text = Format.MinifyToInteger(NicheUtils.GetMarketAudiencePotential(niche)) + " users";

        var risk = NicheUtils.GetMarketDemandRisk(GameContext, nicheType);
        string riskText = NicheUtils.ShowRiskStatus(risk).ToString();

        RiskLabel.text = $"{risk}% ({riskText})";

        var demand = MarketingUtils.GetClientFlow(GameContext, nicheType); // * MarketingUtils.GetMarketingFinancingAudienceReachModifier(MarketingFinancing.High) * 30;
        Demand.text = Format.MinifyToInteger(demand) + " monthly";

        var baseMaintenance = NicheUtils.GetBaseProductMaintenance(niche);
        Maintenance.text = Format.MinifyMoney(baseMaintenance) + " / month";

        //var teamMaintenance = NicheUtils.GetTeamMaintenanceCost(niche);
        //TeamMaintenance.text = Format.MoneyToInteger(teamMaintenance) + " / month";

        TeamMaintenance.text = profile.AppComplexity.ToString();

        long maxIncome = 0;
        if (BiggestIncome != null)
        {
            var players = NicheUtils.GetProductsOnMarket(GameContext, niche.niche.NicheType);


            if (players.Count() == 0)
                BiggestIncome.text = "???";
            else
            {
                maxIncome = players.Max(p => EconomyUtils.GetCompanyIncome(p, GameContext));
                BiggestIncome.text = Format.Money(maxIncome) + " / month";
            }
        }

        var start = NicheUtils.GetStartCapital(niche);
        if (StartCapital != null)
        {
            StartCapital.text = Format.Money(start);

            var showStartCapital = !CompanyUtils.HasCompanyOnMarket(MyCompany, nicheType, GameContext);

            StartCapital.gameObject.SetActive(showStartCapital);
            StartCapitalLabel.gameObject.SetActive(showStartCapital);
        }

        //var monthlyMaintenance = marketingMaintenance + teamMaintenance;
        //if (MonthlyMaintenance != null)
        //    MonthlyMaintenance.text = Format.Money(monthlyMaintenance) + " / month";

        //if (ROI != null)
        //{
        //    var roi = (maxIncome - monthlyMaintenance) * 100 * 12 / (monthlyMaintenance + 1);
        //    ROI.text = $"{roi}% / yearly";
        //}
    }
}
