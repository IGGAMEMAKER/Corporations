using Assets.Utils;
using UnityEngine.UI;

public class RenderCorporateCultureView : View
{
    public Text Leadership;
    public Text Mindset;
    public Text Focusing;
    public Text Expansion;

    public override void ViewRender()
    {
        base.ViewRender();

        var expansion = Companies.GetPolicyValue(MyCompany, CorporatePolicy.Focusing);
        bool isFocused = expansion == 1;


        SetText(Leadership, CorporatePolicy.LeaderOrTeam, false);
        SetText(Mindset,    CorporatePolicy.WorkerMindset, !isFocused);
        SetText(Focusing,   CorporatePolicy.Focusing, true);
        SetText(Expansion,  CorporatePolicy.CreateOrBuy, !isFocused);
    }

    void SetText(Text text, CorporatePolicy corporatePolicy, bool render)
    {
        var value = Companies.GetPolicyValue(MyCompany, corporatePolicy);

        text.text = value.ToString();

        text.gameObject.SetActive(render);
    }
}
