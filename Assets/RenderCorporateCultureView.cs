using Assets.Utils;
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

    public override void ViewRender()
    {
        base.ViewRender();

        var expansion = Companies.GetPolicyValue(MyCompany, CorporatePolicy.Focusing);
        bool isFocused = expansion == 1;


        SetText(Mindset,    CorporatePolicy.WorkerMindset, MindsetPolicy, !isFocused);
        SetText(Focusing,   CorporatePolicy.Focusing, FocusingPolicy, true);
        SetText(Expansion,  CorporatePolicy.CreateOrBuy, ExpansionPolicy, !isFocused);
    }

    void SetText(Text text, CorporatePolicy corporatePolicy, GameObject policyObject, bool render)
    {
        var value = Companies.GetPolicyValue(MyCompany, corporatePolicy);

        text.text = value.ToString();

        policyObject.SetActive(render);
    }
}
