using Assets.Classes;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Hint))]
public abstract class ResourceChecker : View
{
    internal Button Button;
    internal Hint Hint;
    public TeamResource RequiredResources;

    public void Start()
    {
        Button = GetComponent<Button>();
        Hint = GetComponent<Hint>();

        RequiredResources = new TeamResource(10, 0, 0, 0, 0);
    }

    void Render()
    {
        if (IsEnoughResources)
        {
            RemoveHint();
            Button.interactable = true;
        }
        else
        {
            SetHint();
            Button.interactable = false;
        }
    }

    public void SetRequiredResources(TeamResource teamResource)
    {
        RequiredResources = teamResource;

        Render();
    }

    bool IsEnoughResources
    {
        get
        {
            return TeamResource.IsEnoughResources(MyProductEntity.companyResource.Resources, RequiredResources);
        }
    }

    private void SetHint()
    {
        TeamResource resources = MyProductEntity.companyResource.Resources;

        string idea = RequiredResourceSpec(RequiredResources.ideaPoints, resources.ideaPoints, "ideas");
        string money = RequiredResourceSpec((int)RequiredResources.money, (int)resources.money, "$");
        string manager = RequiredResourceSpec(RequiredResources.managerPoints, resources.managerPoints, "manager points");
        string programmer = RequiredResourceSpec(RequiredResources.programmingPoints, resources.programmingPoints, "programming points");
        string sales = RequiredResourceSpec(RequiredResources.salesPoints, resources.salesPoints, "marketing points");

        string hint = $"This task costs\n\n{money}{idea}{manager}{programmer}{sales}";

        Hint.SetHint(hint);
    }

    string RequiredResourceSpec(int req, int res, string literal)
    {
        if (req > 0)
            return VisualFormattingUtils.Colorize($"{req} {literal}", req > res ? VisualConstants.COLOR_NEGATIVE : VisualConstants.COLOR_POSITIVE);

        return "";
    }

    private void RemoveHint()
    {
        Hint.SetHint("");
    }
}
