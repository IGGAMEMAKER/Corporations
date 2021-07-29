using Assets.Core;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HireTeamView : View
{
    public TextMeshProUGUI Title;

    public Image HiringProgress;
    public GameObject Padlock;

    public override void ViewRender()
    {
        base.ViewRender();

        var teamType = GetComponent<AddTeamButton>().TeamType;

        bool hasEnoughPointsToHireTeam = true; // Teams.IsCanAddMoreTeams(Flagship, Q);
        var promotionCost = Teams.GetPromotionCost(TeamRank.Solo);
        var managerPoints = Flagship.companyResource.Resources.managerPoints;
        
        var lastHiringTeam = Flagship.team.Teams.FirstOrDefault(t => t.TeamType == teamType && !Teams.IsFullTeam(t) && t.Rank == TeamRank.Solo);

        if (lastHiringTeam == null)
        {
            if (hasEnoughPointsToHireTeam)
            {
                Title.text = $"<b>Hire {Teams.GetFormattedTeamType(teamType)}";
                Hide(Padlock);
                Hide(HiringProgress);
            }
            else
            {
                Title.text = Visuals.Negative($"<b>Need {promotionCost} manager points");
                Hide(Padlock);
                Show(HiringProgress);
                HiringProgress.fillAmount = managerPoints * 1f / promotionCost;
            }

            GetComponent<Button>().enabled = hasEnoughPointsToHireTeam;
        }
        else
        {
            Show(Padlock);
            Show(HiringProgress);

            HiringProgress.fillAmount = (lastHiringTeam.Workers + lastHiringTeam.HiringProgress / 100f) / Teams.GetMaxTeamSize(lastHiringTeam);
            Title.text = $"<b>Hiring workers for {Teams.GetFormattedTeamType(teamType)}";

            GetComponent<Button>().enabled = false;
        }
    }
}
