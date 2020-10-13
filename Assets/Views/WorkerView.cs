using Assets.Core;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WorkerView : View
{
    public HumanPreview HumanPreview;
    public Image LoyaltyChange;

    public Sprite Growth;
    public Sprite Decay;
    public Sprite Stall;

    public GameObject TeamLead;
    public GameObject CompanyLead;

    public GameObject IsRecruitmentTarget;


    public void SetEntity(int humanId, WorkerRole workerRole)
    {
        var link = GetComponent<LinkToHuman>();

        var human = Humans.Get(Q, humanId);

        link.SetHumanId(humanId);
        HumanPreview.SetEntity(humanId);

        if (!Humans.IsEmployed(human)) return;

        var team = Teams.GetTeamOf(human, Q);

        bool isMainManager = human.worker.WorkerRole == Teams.GetMainManagerForTheTeam(team);
        bool isCompanyLead = isMainManager && human.worker.WorkerRole == WorkerRole.CEO;

        Draw(TeamLead, isMainManager);
        Draw(CompanyLead, isCompanyLead);

        var company = Companies.Get(Q, human.worker.companyId);
        var loyaltyGrowth = Teams.GetLoyaltyChangeForManager(human, team, company);

        if (loyaltyGrowth > 0)
            LoyaltyChange.sprite = Growth;

        if (loyaltyGrowth == 0)
            LoyaltyChange.sprite = Stall;

        if (loyaltyGrowth < 0)
            LoyaltyChange.sprite = Decay;

        bool hasOffers = Humans.HasCompetingOffers(human);

        var yourOffer = Teams.GetOpinionAboutOffer(human, Humans.GetCurrentOffer(human)).Sum();
        var bestOffer = human.workerOffers.Offers.Max(o => Teams.GetOpinionAboutOffer(human, o).Sum());

        bool yourOfferIsBestOffer = hasOffers && bestOffer == yourOffer;


        Draw(IsRecruitmentTarget, hasOffers);
        GetComponent<Blinker>().enabled = hasOffers && !yourOfferIsBestOffer;
    }
}
