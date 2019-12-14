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

        SetText(Leadership, CorporatePolicy.LeaderOrTeam);
        SetText(Mindset,    CorporatePolicy.WorkerMindset);
        SetText(Focusing,   CorporatePolicy.Focusing);
        SetText(Expansion,  CorporatePolicy.CreateOrBuy);
    }

    void SetText(Text text, CorporatePolicy corporatePolicy)
    {
        var value = Companies.GetPolicyValue(MyCompany, corporatePolicy);

        text.text = value.ToString();
    }
}
