using Assets.Classes;
using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(ListenMyProductResourceChanges))]
[RequireComponent(typeof(Hint))]
public abstract class ResourceChecker : View
{
    internal Button Button;
    internal Hint Hint;
    public TeamResource RequiredResources;

    public abstract TeamResource GetRequiredResources();

    public void Start()
    {
        Button = GetComponent<Button>();
        Hint = GetComponent<Hint>();
    }

    void Render()
    {
        if (!Button)
        {
            Debug.Log("Resource Checker: <Button> not found in " + gameObject.name);
        }
        else
        {
            Button.interactable = IsEnoughResources;
        }

        if (IsEnoughResources)
            RemoveHint();
        else
            SetHint();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    bool IsEnoughResources
    {
        get
        {
            return CompanyUtils.IsEnoughResources(MyProductEntity, GetRequiredResources());
        }
    }

    private void SetHint()
    {
        if (!Hint)
            return;

        TeamResource resources = MyProductEntity.companyResource.Resources;
        var RequiredResources = GetRequiredResources();

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
            return Visuals.Colorize($"{req} {literal}\n", req > res ? VisualConstants.COLOR_NEGATIVE : VisualConstants.COLOR_POSITIVE);

        return "";
    }

    private void RemoveHint()
    {
        if (!Hint)
            return;

        Hint.SetHint("");
    }
}
