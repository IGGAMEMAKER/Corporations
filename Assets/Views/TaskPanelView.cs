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

    public GameObject RemoveFeatureButton;

    public void SetEntity(int teamId, TeamTask teamTask)
    {
        this.teamId = teamId;
        this.teamTask = teamTask;

        ChosenTaskLabel.SetTask(teamTask);

        Draw(ChosenTaskLabel, true);
        Draw(RemoveTaskButton, true);
        Draw(RemoveFeatureButton, teamTask.IsFeatureUpgrade);

        var team = Flagship.team.Teams[teamId];

        TaskBenefit.text = "";

        if (teamTask.IsPending)
        {
            TaskBenefit.text = Visuals.Negative("<size=30>This task is not active!</size>") + "\nCause our team is doing other tasks. Hire more teams, to make more tasks at the same time.\n\n";
        }

        if (teamTask.IsFeatureUpgrade)
        {
            var f = teamTask as TeamTaskFeatureUpgrade;


            var segments = Marketing.GetAudienceInfos();

            float sumLoyalty = 0;
            float sumLoyaltyMax = 0;
            float sumLoyaltyABS = 0;

            var featureDescription = "";

            var rating = Products.GetFeatureRating(Flagship, f.NewProductFeature.Name);
            var ratingColor = Visuals.GetGradientColor(0, 10f, rating);



            foreach (var s in segments)
            {
                var segmentId = s.ID;

                var maxBenefit = Marketing.GetLoyaltyChangeFromFeature(Flagship, f.NewProductFeature, segmentId, true);
                var benefit = Marketing.GetLoyaltyChangeFromFeature(Flagship, f.NewProductFeature, segmentId, false);

                sumLoyalty += benefit;
                sumLoyaltyMax += maxBenefit;
                sumLoyaltyABS += Mathf.Abs(benefit);

                var maxBenefitFormatted = Visuals.Colorize(maxBenefit.ToString("+0;-#"), maxBenefit >= 0);
                var benefitFormatted = Visuals.Colorize(benefit.ToString("+0.0;-#"), ratingColor);

                //if (benefit != 0)
                //    featureDescription += $"\n\n{maxBenefitFormatted} loyalty for {s.Name}\n\t<i>currently</i>: {benefitFormatted}";
                //else
                //    featureDescription += "\n\nNo effects";

            }

            if (f.NewProductFeature.FeatureBonus.isRetentionFeature)
            {

            }
            if (f.NewProductFeature.FeatureBonus.isMonetisationFeature)
            {
                var monetisationBenefit = Products.GetFeatureActualBenefit(Flagship, f.NewProductFeature); // f.NewProductFeature.FeatureBonus.Max;
                var monetisationBenefitMax = f.NewProductFeature.FeatureBonus.Max;

                featureDescription += Visuals.Positive("\n\nLoyalty will increase after upgrading this feature");

                featureDescription += $"\n\n{Visuals.Positive($"Increases income by +{monetisationBenefit.ToString("0.0")}% (max: +{monetisationBenefitMax}%)")}";
            }



            var sumLoyaltyFormatted = Visuals.Colorize(sumLoyalty.ToString("+0.0;-#"), sumLoyalty >= 0);
            var sumLoyaltyMaxFormatted = Visuals.Colorize(sumLoyaltyMax.ToString("+0.0;-#"), sumLoyaltyMax >= 0);

            var ratingFormatted = Visuals.Colorize(rating.ToString("0.0"), ratingColor);

            var ratingCap = Products.GetFeatureRatingCap(Flagship);

            TaskBenefit.text += $"<size=40>Feature Quality: {ratingFormatted}lvl</size>" +
                $"\n\nThis feature will be upgraded to level {ratingCap}" +
                $"\n\n<size=30>Gives you {sumLoyaltyFormatted} loyalty total</size>" +
                //$"\n\nOn quality=10 you will get:" +
                //$"\n\n<size=30>{sumLoyaltyMaxFormatted} loyalty total</size>" +
                $"\n{featureDescription}"
                ;



            TaskModifiers.text = $"Will be upgraded to level {ratingCap} due to assigned team";
        }

        if (teamTask.IsMarketingTask)
        {
            TaskBenefit.text += $"Marketing task";
            TaskModifiers.text = "";
        }

        if (teamTask.IsHighloadTask)
        {
            TaskBenefit.text += $"Server";
            TaskModifiers.text = "";
        }

        //TaskExecutor.text = $"<b>{Visuals.Link(team.Name)}</b>";
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

    public void ShowAssignedTeam()
    {
        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        if (teamId >= 0)
            relay.ChooseManagersTabs(teamId);
    }
}
