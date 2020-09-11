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

                if (f.NewProductFeature.FeatureBonus.isMonetisationFeature)
                {
                    var monetisationBenefit = Products.GetFeatureActualBenefit(Flagship, f.NewProductFeature); // f.NewProductFeature.FeatureBonus.Max;
                    var monetisationBenefitMax = f.NewProductFeature.FeatureBonus.Max;

                    featureDescription += $"\n\n{Visuals.Positive($"Increases income by +{monetisationBenefit.ToString("0.0")}% (max: +{monetisationBenefitMax}%)")}";
                }
            }


            var sumLoyaltyFormatted = Visuals.Colorize(sumLoyalty.ToString("+0.0;-#"), sumLoyalty >= 0);
            var sumLoyaltyMaxFormatted = Visuals.Colorize(sumLoyaltyMax.ToString("+0.0;-#"), sumLoyaltyMax >= 0);

            var ratingFormatted = Visuals.Colorize(rating.ToString("0.0"), ratingColor);

            var ratingCap = Products.GetFeatureRatingCap(Flagship, team, Q);

            TaskBenefit.text = $"<size=40>Quality: {ratingFormatted} / 10lvl</size>" +
                //$"This feature will be upgraded to level {ratingCap}" + 
                $"\n\n<size=30>Gives you {sumLoyaltyFormatted} loyalty total</size>" +
                $"\n\nOn quality=10 you will get:" +
                $"\n\n<size=30>{sumLoyaltyMaxFormatted} loyalty total</size>" +
                $"\n{featureDescription}"
                ;

            TaskModifiers.text = $"This feature will be upgraded to level {ratingCap}";
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
