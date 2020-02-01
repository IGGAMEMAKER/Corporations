using Assets.Core;
using UnityEngine.UI;

public class RenderCompanyLimit : View
{
    public Text CompanyLimitDescription;

    public override void ViewRender()
    {
        base.ViewRender();

        var daughters = Companies.GetDaughterCompaniesAmount(MyCompany, Q);
        var limit = Companies.GetCompanyLimit(MyCompany);

        bool isOverflow = daughters > limit;

        GetComponent<Text>().text = $"Company limit: {Visuals.Colorize(daughters.ToString(), !isOverflow)} / {limit}";

        var iterationTime = Companies.GetCompanyLimitPenalty(MyCompany, Q);
        CompanyLimitDescription.text = isOverflow ?
            $"Product upgrade time will increase by {Visuals.Negative(iterationTime.ToString())}%"
            :
            $"";
    }
}
