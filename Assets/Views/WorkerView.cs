using Assets.Core;
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

    public void SetEntity(int humanId, WorkerRole workerRole)
    {
        var link = GetComponent<LinkToHuman>();

        var human = Humans.GetHuman(Q, humanId);

        link.SetHumanId(humanId);
        HumanPreview.SetEntity(humanId);

        if (!Humans.IsEmployed(human)) return;

        var team = Teams.GetTeamOf(human, Q);

        bool isMainManager = human.worker.WorkerRole == Teams.GetMainManagerForTheTeam(team);
        bool isCompanyLead = isMainManager && human.worker.WorkerRole == WorkerRole.CEO;

        Draw(TeamLead, isMainManager);
        Draw(CompanyLead, isCompanyLead);

        var loyaltyGrowth = Teams.GetLoyaltyChangeForManager(human, team, Q);

        if (loyaltyGrowth > 0)
            LoyaltyChange.sprite = Growth;

        if (loyaltyGrowth == 0)
            LoyaltyChange.sprite = Stall;

        if (loyaltyGrowth < 0)
            LoyaltyChange.sprite = Decay;
    }
}
