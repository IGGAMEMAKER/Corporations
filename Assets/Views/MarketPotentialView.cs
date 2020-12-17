 using Assets.Core;
using System.Linq;
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
    public Text IncomePerUser;
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
        NicheType nicheType = SelectedNiche;
        var niche = Markets.Get(Q, nicheType);

        var profile = niche.nicheBaseProfile.Profile;

        var nicheCosts = Markets.GetNicheCosts(Q, nicheType);

        var rating = Markets.GetMarketRating(niche);
        SetAmountOfStars.SetStars(rating);

        ChangeSpeed.text = "xxx days";

        PotentialMarketSize.text = Format.Money(Markets.GetMarketPotential(niche));
        PotentialAudienceSize.text = Format.MinifyToInteger(Markets.GetMarketAudiencePotential(niche)) + " users";

        var risk = Markets.GetMarketDemandRisk(Q, nicheType);
        string riskText = Markets.ShowRiskStatus(risk).ToString();

        RiskLabel.text = $"{risk}% ({riskText})";

        var demand = Marketing.GetClientFlow(Q, nicheType); // * MarketingUtils.GetMarketingFinancingAudienceReachModifier(MarketingFinancing.High) * 30;
        Demand.text = Format.MinifyToInteger(demand) + " monthly";

        var baseMaintenance = Markets.GetBaseProductMaintenance(Q, niche);
        //Maintenance.text = Format.MinifyMoney(baseMaintenance) + " / month";
        Maintenance.text = $"{nicheCosts.AcquisitionCost.ToString("0.0")} / user"; // Format.MinifyMoney(baseMaintenance) + " / month";
        IncomePerUser.text = $"{nicheCosts.BaseIncome.ToString("0.0")} / user"; // Format.MinifyMoney(baseMaintenance) + " / month";

        //var teamMaintenance = NicheUtils.GetTeamMaintenanceCost(niche);
        //TeamMaintenance.text = Format.MoneyToInteger(teamMaintenance) + " / month";

        TeamMaintenance.text = profile.AppComplexity.ToString();

        long maxIncome = 0;
        if (BiggestIncome != null)
        {
            var players = Markets.GetProductsOnMarket(Q, niche.niche.NicheType);


            if (players.Count() == 0)
                BiggestIncome.text = "???";
            else
            {
                maxIncome = players.Max(p => Economy.GetIncome(Q, p));
                BiggestIncome.text = Format.Money(maxIncome) + " / month";
            }
        }

        var start = Markets.GetStartCapital(niche, Q);
        if (StartCapital != null)
        {
            StartCapital.text = Format.Money(start);

            var showStartCapital = !Companies.HasCompanyOnMarket(MyCompany, nicheType, Q);

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
