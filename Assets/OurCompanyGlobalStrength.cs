using Assets.Core;
using UnityEngine.UI;

public class OurCompanyGlobalStrength : View
{
    void OnEnable()
    {
        var industries = MyCompany.companyFocus.Industries;

        var text = "";

        foreach (var ind in industries)
        {
            var strength = Companies.GetCompanyStrengthInIndustry(MyCompany, ind, Q);
            text += "on " + Enums.GetFormattedIndustryName(ind) + " industry\n" + strength + "\n\n";
        }

        GetComponent<Text>().text = text;
    }
}
