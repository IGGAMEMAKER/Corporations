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

        var expansion = Companies.GetPolicyValue(MyCompany, CorporatePolicy.LeaderOrTeam);
        bool isFocused = expansion == 1;


        SetText(Responsibility,     CorporatePolicy.LeaderOrTeam, ResponsibilityPolicy, true);
        SetText(Mindset,            CorporatePolicy.InnovationOrStability, MindsetPolicy, !isFocused);
        SetText(Focusing,           CorporatePolicy.FocusingOrSpread, FocusingPolicy, !isFocused);
        SetText(Expansion,          CorporatePolicy.BuyOrCreate, ExpansionPolicy, !isFocused);
        SetText(Competition,        CorporatePolicy.CompetitionOrSupport, CompetitionPolicy, !isFocused);
        SetText(Salaries,           CorporatePolicy.SalariesLowOrHigh, SalariesPolicy, !isFocused);
    }

    void SetText(Text text, CorporatePolicy corporatePolicy, GameObject policyObject, bool render)
    {
        var value = Companies.GetPolicyValue(MyCompany, corporatePolicy);

        text.text = value.ToString();

        policyObject.SetActive(render);
    }
}
