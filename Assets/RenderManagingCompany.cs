using Assets.Core;
using UnityEngine.UI;

public class RenderManagingCompany : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var managingCompany = Companies.GetManagingCompanyOf(SelectedCompany, GameContext);

        bool isIndependent = managingCompany.company.Id == SelectedCompany.company.Id;

        // Independent company
        GetComponent<Text>().text = isIndependent ? "" : "Daughter of " + Visuals.Link(managingCompany.company.Name);

        GetComponent<LinkToProjectView>().CompanyId = managingCompany.company.Id;
        GetComponent<LinkToProjectView>().enabled = !isIndependent;
    }
}
