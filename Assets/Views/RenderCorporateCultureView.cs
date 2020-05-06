using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class RenderCorporateCultureView : View
{
    public Text Mindset;
    public GameObject MindsetPolicy;

    public Text Focusing;
    public GameObject FocusingPolicy;

    public Text Expansion;
    public GameObject ExpansionPolicy;

    public Text Responsibility;
    public GameObject ResponsibilityPolicy;

    public Text Competition;
    public GameObject CompetitionPolicy;

    public Text Salaries;
    public GameObject SalariesPolicy;


    public override void ViewRender()
    {
        base.ViewRender();

        SetText(Focusing,           CorporatePolicy.FocusingOrSpread, FocusingPolicy, false);
        SetText(Expansion,          CorporatePolicy.Make, ExpansionPolicy, true);
        SetText(Competition,        CorporatePolicy.CompetitionOrSupport, CompetitionPolicy, true);
        SetText(Salaries,           CorporatePolicy.SalariesLowOrHigh, SalariesPolicy, true);
    }

    void SetText(Text text, CorporatePolicy corporatePolicy, GameObject policyObject, bool render)
    {
        var value = Companies.GetPolicyValue(MyCompany, corporatePolicy);

        text.text = value.ToString();

        policyObject.SetActive(render);
    }
}
