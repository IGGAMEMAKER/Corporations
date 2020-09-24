using Assets.Core;
using System.Linq;
using UnityEngine;

public class FillInterruptList : View
{
    public GameObject CanReleaseProduct;
    public GameObject CanUpgradeCorporateCulture;
    public GameObject CanCompleteGoal;
    public GameObject CanSellCompany;
    public GameObject CanBuyCompany;
    public GameObject CanRaiseInvestments;
    public GameObject CanCheckAnnualReport;
    public GameObject CanCheckTrends;

    public GameObject InvestorLoyaltyWarning;
    public GameObject TeamLoyaltyWarning;
    public GameObject NeedToCompleteGoal;
    public GameObject NeedToManageCompanies;

    public GameObject InvestorLoyaltyThreat;
    public GameObject TeamLoyaltyThreat;
    public GameObject OutdatedProducts;

    public override void ViewRender()
    {
        base.ViewRender();

        if (!HasCompany)
            return;

        bool isCanCompleteGoal = CheckGoal;

        CanRaiseInvestments         .SetActive(false);
        CanUpgradeCorporateCulture  .SetActive(IsCanUpgradeCorporateCulture);
        CanSellCompany              .SetActive(HasAcquisitionOffers);
        CanCheckTrends              .SetActive(false);

        CanReleaseProduct           .SetActive(false);
        CanCheckAnnualReport        .SetActive(false);
        CanBuyCompany               .SetActive(false);

        TeamLoyaltyThreat           .SetActive(HasUnhappyManagers);
        NeedToCompleteGoal          .SetActive(false);
        OutdatedProducts            .SetActive(false);


        NeedToManageCompanies       .SetActive(false);

        InvestorLoyaltyWarning      .SetActive(false);
        InvestorLoyaltyThreat       .SetActive(false);
        TeamLoyaltyWarning          .SetActive(HasRebelliousManagers);
    }

    bool hasReleasedProduct => Companies.IsHasReleasedProducts(Q, MyCompany);

    bool IsCanUpgradeCorporateCulture
    {
        get
        {
            return hasReleasedProduct && !Cooldowns.HasCorporateCultureUpgradeCooldown(Q, MyCompany);
        }
    }

    bool HasReleaseableProducts
    {
        get
        {
            return false;
            //var upgradableCompanies = Companies.GetDaughterReleaseableCompanies(Q, MyCompany.company.Id);
            //var count = upgradableCompanies.Count();

            //bool isAlreadyOnReleasableMarket = CurrentScreen == ScreenMode.DevelopmentScreen && count == 1 && SelectedCompany.company.Id == upgradableCompanies.First().company.Id;

            //return count > 0 && !isAlreadyOnReleasableMarket;
        }
    }

    //bool HasOutdatedProducts => Companies.GetDaughterOutdatedCompanies(Q, MyCompany.company.Id).Length > 0;

    bool IsCanRaiseInvestments => hasReleasedProduct && Companies.IsReadyToStartInvestmentRound(MyCompany) && Companies.IsHasDaughters(Q, MyCompany);

    bool HasUnhappyManagersInCompany (GameEntity company)
    {
        return false;
        //if (company == null || !company.hasTeam)
        //    return false;

        //foreach (var m in company.team.Managers)
        //{
        //    var human = Humans.GetHuman(Q, m.Key);

        //    var morale = human.humanCompanyRelationship.Morale;
        //    var change = Teams.GetLoyaltyChangeForManager(human, Q);

        //    if (morale < 30 && change < 0)
        //        return true;
        //}

        //return false;
    }

    bool HasRebelliousManagersInCompany (GameEntity company)
    {
        return false;
        //if (company == null || !company.hasTeam)
        //    return false;

        //foreach (var m in company.team.Managers)
        //{
        //    var human = Humans.GetHuman(Q, m.Key);

        //    var morale = human.humanCompanyRelationship.Morale;
        //    var change = Teams.GetLoyaltyChangeForManager(human, Q);

        //    if (change < 0)
        //        return true;
        //}

        //return false;
    }

    bool HasUnhappyManagers => HasUnhappyManagersInCompany(Flagship) || HasUnhappyManagersInCompany(MyCompany);//return Companies.GetDaughterUnhappyCompanies(Q, MyCompany.company.Id).Length > 0;
    bool HasRebelliousManagers => HasRebelliousManagersInCompany(Flagship) || HasRebelliousManagersInCompany(MyCompany);//return Companies.GetDaughterUnhappyCompanies(Q, MyCompany.company.Id).Length > 0;

    bool HasAcquisitionOffers => Companies.GetAcquisitionOffersToPlayer(Q).Count() > 0;

    bool CheckAcquisitionCandidates => Markets.GetProductsAvailableForSaleInSphereOfInfluence(MyCompany, Q).Count > 0;

    bool CheckGoal => Investments.IsGoalCompleted(MyCompany, Q);

    bool CheckAnnualReport => CurrentIntDate > 360;
}
