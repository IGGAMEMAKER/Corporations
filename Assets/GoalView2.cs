using Assets;
using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalView2 : View
{
    public Text Title;
    public Text Requirements;
    public Text Number;
    public GameObject CompleteButton;

    public Image ProgressImage;

    InvestmentGoal InvestmentGoal;
    int index;

    public void SetEntity(InvestmentGoal goal, int index)
    {
        InvestmentGoal = goal;
        this.index = index;

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var company = MyCompany;

        var executor = Companies.Get(Q, InvestmentGoal.ExecutorId);

        bool completed = Investments.CanCompleteGoal(executor, Q, InvestmentGoal);

        Draw(CompleteButton, completed);
        Draw(Requirements, !completed);
        Draw(ProgressImage, !completed);

        Title.text = InvestmentGoal.GetFormattedName();
        Requirements.text = InvestmentGoal.GetFormattedRequirements(executor, Q);
        Number.text = $"#{index + 1}";

        //var requirements = InvestmentGoal.GetGoalRequirements(executor, Q);

        //var progress = InvestmentGoal.GetGoalProgress(executor, Q) / 100f;

        //ProgressImage.fillAmount = progress;
    }

    public void CompleteGoal()
    {
        bool willComplete = Investments.CompleteGoal(MyCompany, Q, InvestmentGoal);

        if (InvestmentGoal.InvestorGoalType == InvestorGoalType.BuyBack)
        {
            if (willComplete)
            {
                CloseModal("Missions");
                NotificationUtils.AddSimplePopup(Q, Visuals.Positive("CONGRATULATIONS! YOU WON!"), Visuals.Positive("Hope you've enjoyed the game:)"));

                // SPECIAL SOUND AT THE GAME END?
                SoundManager.Play(Sound.GoalCompleted);
            }
        }
        else
        {
            if (willComplete)
            {
                //SoundManager.Play(Sound.Action);
                SoundManager.Play(Sound.GoalCompleted);

                FindObjectOfType<MissionRelay>().ShowActiveMissions();
            }
        }

        FindObjectOfType<GoalsListView>().ViewRender();
        Refresh();
    }
}
