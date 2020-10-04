using Assets.Core;
using System.Collections;
using System.Collections.Generic;
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

        var lastHiringTeam = Flagship.team.Teams.FirstOrDefault(t => t.TeamType == teamType && t.Workers != 8);

        if (lastHiringTeam == null)
        {
            Hide(Padlock);
            Hide(HiringProgress);

            Title.text = $"<b>{Teams.GetFormattedTeamType(teamType)}";

            GetComponent<Button>().enabled = true;
        }
        else
        {
            Show(Padlock);
            Show(HiringProgress);

            HiringProgress.fillAmount = (lastHiringTeam.Workers + lastHiringTeam.HiringProgress / 100f) / 8;
            Title.text = $"<b>Hiring workers for {Teams.GetFormattedTeamType(teamType)}";

            GetComponent<Button>().enabled = false;
        }
    }
}
