using Assets.Utils;
using UnityEngine.UI;

public class RenderCompanyLimit : View
{
    public Text CompanyLimitDescription;

    public override void ViewRender()
    {
        base.ViewRender();

        var daughters = Companies.GetDaughterCompaniesAmount(MyCompany, GameContext);
        var limit = Companies.GetCompanyLimit(MyCompany);

        bool isOverflow = daughters > limit;

        GetComponent<Text>().text = $"Company limit: {Visuals.Colorize(daughters.ToString(), !isOverflow)} / {limit}";

        var iterationTime = Companies.GetCompanyLimitPenalty(MyCompany, GameContext);
        CompanyLimitDescription.text = isOverflow ?
            $"Product upgrade time will increase by {Visuals.Negative(iterationTime.ToString())}%"
            :
            $"";
    }
}
