using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamPreview : View
{
    public TeamInfo TeamInfo;
    public int teamId;

    public Image HiringProgress;
    public Image HiringProgressBackground;
    public Text HiringProgressText;

    public void SetEntity(TeamInfo info, int teamId)
    {
        this.teamId = teamId;

        var company = Flagship;

        TeamInfo = info;
        var TeamType = info.TeamType;

        //TeamTypeImage.sprite = GetTeamTypeSprite();

        int maxWorkers = 8;
        int workers = info.Workers; // Random.Range(0, maxWorkers);
        bool hasFullTeam = workers >= maxWorkers;

        var hiringProgress = info.HiringProgress;

        HiringProgress.fillAmount = workers / maxWorkers;
        HiringProgressText.text = (workers * 100 / maxWorkers) + "%";

        Draw(HiringProgress, !hasFullTeam);
        Draw(HiringProgressText, !hasFullTeam);
        Draw(HiringProgressBackground, !hasFullTeam);
    }
}
