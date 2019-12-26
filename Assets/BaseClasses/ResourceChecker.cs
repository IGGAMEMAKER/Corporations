using Assets.Utils;
using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Hint))]
public abstract class ResourceChecker : View
{
    internal Button Button;
    internal Hint Hint;
    public TeamResource RequiredResources;

    public abstract TeamResource GetRequiredResources();

    public abstract string GetBaseHint();

    void Render()
    {
        Button = GetComponent<Button>();
        Hint = GetComponent<Hint>();

        if (!Button)
            Debug.Log("Resource Checker: <Button> not found in " + gameObject.name);
        else
            Button.interactable = false;

        SetHint();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    public virtual void SetHint()
    {
        //if (!Hint || !HasProductCompany)
            return;

        //TeamResource resources = MyProductEntity.companyResource.Resources;
        //var RequiredResources = GetRequiredResources();

        //string idea = RequiredResourceSpec(RequiredResources.ideaPoints, resources.ideaPoints, "ideas");
        //string money = RequiredResourceSpec((int)RequiredResources.money, (int)resources.money, "$");
        //string manager = RequiredResourceSpec(RequiredResources.managerPoints, resources.managerPoints, "manager points");
        //string programmer = RequiredResourceSpec(RequiredResources.programmingPoints, resources.programmingPoints, "programming points");
        //string sales = RequiredResourceSpec(RequiredResources.salesPoints, resources.salesPoints, "marketing points");

        //string hint = $"{GetBaseHint()}\n This task costs\n\n{money}{idea}{manager}{programmer}{sales}";

        //Hint.SetHint(hint);
    }

    string RequiredResourceSpec(int req, int res, string literal)
    {
        if (req > 0)
            return Visuals.Colorize($"{req} {literal}\n", req > res ? VisualConstants.COLOR_NEGATIVE : VisualConstants.COLOR_POSITIVE);

        return "";
    }
}
