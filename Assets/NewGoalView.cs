using Assets;
using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class NewGoalView : View
{
    public Text Title;
    public Text Requirements;
    public Text Number;
    public GameObject PickButton;

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

        Draw(PickButton, true);
        Draw(Requirements, true);

        Title.text = InvestmentGoal.GetFormattedName();
        Requirements.text = InvestmentGoal.GetFormattedRequirements(MyCompany, Q);
        Number.text = $"#{index + 1}";
    }

    public void PickGoal()
    {
        Investments.AddCompanyGoal(MyCompany, Q, InvestmentGoal);
        SoundManager.Play(Sound.Action);

        FindObjectOfType<PickGoalsListView>().ViewRender();
        Refresh();
    }
}
