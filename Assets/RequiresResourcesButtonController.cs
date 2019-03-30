using Assets.Classes;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Hint))]
public class RequiresResourcesButtonController : View
{
    Button Button;
    Hint Hint;
    public TeamResource RequiredResources;

    void Start()
    {
        Button = GetComponent<Button>();
        Hint = GetComponent<Hint>();

        RequiredResources = new TeamResource(0, 0, 0, 0, 0);
    }

    void Update()
    {
        if (isEnoughResources)
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
    }

    bool IsEnoughResources(TeamResource resources, TeamResource required)
    {

        if (required.ideaPoints > resources.ideaPoints) return false;
        if (required.managerPoints > resources.managerPoints) return false;
        if (required.programmingPoints > resources.programmingPoints) return false;
        if (required.money > resources.money) return false;
        if (required.salesPoints > resources.salesPoints) return false;

        return true;
    }

    bool isEnoughResources
    {
        get
        {
            return IsEnoughResources(myProduct.Resources, RequiredResources);
        }
    }

    string RequiredResourceSpec(int req, int res, string literal)
    {
        string phrase = "";

        if (req > 0)
        {
            string lacks = req > res ? " (!)" : "";
            phrase = $"{req}{lacks} {literal} \n";
        }

        return phrase;
    }

    private void SetHint()
    {
        TeamResource resources = myProduct.Resources;

        string idea = RequiredResourceSpec(RequiredResources.ideaPoints, resources.ideaPoints, "ideas");
        string money = RequiredResourceSpec((int) RequiredResources.money, (int) resources.money, "$");
        string manager = RequiredResourceSpec(RequiredResources.managerPoints, resources.managerPoints, "manager points");
        string programmer = RequiredResourceSpec(RequiredResources.programmingPoints, resources.programmingPoints, "programming points");
        string sales = RequiredResourceSpec(RequiredResources.salesPoints, resources.salesPoints, "marketing points");

        string hint = "This task costs\n" + money + idea + manager + programmer + sales;

        Hint.SetHint(hint);
    }

    private void RemoveHint()
    {
        Hint.SetHint("");
    }
}
