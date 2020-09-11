using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanelView : View
{
    public Text TaskBenefit;
    public Text TaskExecutor;
    public Text TaskModifiers;

    int teamId;
    TeamTask teamTask;

    public GameObject RemoveTaskButton;
    public ChosenTeamTaskView ChosenTaskLabel;

    public void SetEntity(int teamId, TeamTask teamTask)
    {
        this.teamId = teamId;
        this.teamTask = teamTask;

        ChosenTaskLabel.SetTask(teamTask);

        Draw(ChosenTaskLabel, true);
        Draw(RemoveTaskButton, true);

        var team = Flagship.team.Teams[teamId];

        if (teamTask.IsFeatureUpgrade)
        {
            var f = teamTask as TeamTaskFeatureUpgrade;


            var segments = Marketing.GetAudienceInfos();

            float sumLoyalty = 0;

            var featureDescription = "";

            foreach (var s in segments)
            {
                var segmentId = s.ID;

                var maxBenefit = Marketing.GetLoyaltyChangeFromFeature(Flagship, f.NewProductFeature, segmentId, true);
                var benefit = Marketing.GetLoyaltyChangeFromFeature(Flagship, f.NewProductFeature, segmentId, false);

                sumLoyalty += benefit;

                var maxBenefitFormatted = Visuals.Colorize(maxBenefit.ToString("0.0"), maxBenefit >= 0);
                var benefitFormatted = Visuals.Colorize(benefit.ToString("0.0"), benefit >= 0);

                featureDescription += $"\n{maxBenefitFormatted} loyalty for {s.Name} (currently: {benefitFormatted})";
            }

            var rating = Products.GetFeatureRating(Flagship, f.NewProductFeature.Name);

            TaskBenefit.text = $"<size=40>Quality: {rating.ToString("0.0")} / 10lvl</size>" +
                $"\n<size=30>Gives you {Visuals.Colorize(sumLoyalty.ToString("0.0"), sumLoyalty >= 0)} loyalty total</size>" +
                $"\nOn level 10 it will give you" +
                $"\n{featureDescription}"
                ;

            var ratingCap = Products.GetFeatureRatingCap(Flagship, team, Q);
            TaskModifiers.text = $"This feature will be upgraded till level {ratingCap}";
        }

        if (teamTask.IsMarketingTask)
        {
            TaskBenefit.text = $"marketing task";
        }

        TaskExecutor.text = $"<b>{team.Name}</b>";
    }

    public override void ViewRender()
    {
        base.ViewRender();


    }

    public void RemoveFeature()
    {
        if (teamTask.IsFeatureUpgrade)
        {
            var f = teamTask as TeamTaskFeatureUpgrade;

            Products.RemoveFeature(Flagship, f.NewProductFeature.Name, Q);
            RemoveTask();
        }
    }

    public void RemoveTask()
    {
        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();
        
        relay.RemoveTask();
        relay.ChooseMainScreen();
    }
}
