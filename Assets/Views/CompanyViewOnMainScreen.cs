using Assets.Core;
using UnityEngine.UI;

public class CompanyViewOnMainScreen : View
{
    GameEntity company;

    public Text Name;
    public Text Users;

    public Image EmblemBackground;
    public Text EmblemText;

    public Image CircularProgressbar;
    public LinkToProjectView Link;

    public void SetEntity(GameEntity company)
    {
        this.company = company;

        ViewRender();
    }

    public override void ViewRender()
    {
        Name.text = company.company.Name;

        if (company.hasProduct)
        {
            Users.text = Format.Minify(Marketing.GetUsers(company));
            Users.color = Visuals.GetColorPositiveOrNegative(Marketing.GetAudienceChange(company, Q) >= 0);
        }
        else
        {
            Users.text = Format.Money(Economy.CostOf(company, Q));
            //Users.color
        }

        Hide(CircularProgressbar);

        Link.CompanyId = company.company.Id;

        EmblemText.text = Companies.GetShortName(company);
        EmblemBackground.color = Companies.GetCompanyUniqueColor(company.company.Id);
    }

}