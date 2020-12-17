using Assets.Core;
using UnityEngine.UI;

public class RenderWorkerRole : View
{
    public Text Description;
    public override void ViewRender()
    {
        base.ViewRender();

        var Link = GetComponent<LinkToProjectView>();

        var text = "";
        var human = SelectedHuman;

        var role = Humans.GetRole(human);

        var formattedRole = Humans.GetFormattedRole(role);
        var rating = Humans.GetRating(Q, human);

        text = $"{formattedRole} ({rating}LVL)";

        bool isUnemployed = !Humans.IsEmployed(human);

        if (isUnemployed)
        {
            text += " (Unemployed)";
            Link.enabled = false;

            Description.text = Teams.GetRoleDescription(role, Q, isUnemployed);
        }
        else
        {
            var companyId = human.worker.companyId;

            var c = Companies.Get(Q, companyId);

            text += " " + Visuals.Link($"in {c.company.Name}");

            Link.enabled = true;
            Link.CompanyId = companyId;

            Description.text = Teams.GetRoleDescription(role, Q, isUnemployed, c);
        }


        GetComponent<Text>().text = text;
    }
}
