using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine.UI;

public class MarketShareView : View
{
    public Text ShareSize;
    public Text ShareChange;
    public Text NicheName;

    public LinkToNiche LinkToNiche;

    internal void SetEntity(NicheType nicheType)
    {
        var niche = NicheUtils.GetNicheEntity(GameContext, nicheType);
        var rating = NicheUtils.GetMarketRating(niche);

        var share = CompanyUtils.GetGroupMarketControl(MyCompany, nicheType, GameContext);

        //AnimateIfValueChanged(ShareSize, share + "%");

        ShareSize.text = share + "%";
        //ShareChange.text = Visuals.Positive("+13%");
        ShareChange.text = niche.nicheState.Phase.ToString();
        ShareChange.color = Visuals.GetGradientColor(0, 5f, rating);

        NicheName.text = EnumUtils.GetFormattedNicheName(nicheType);

        LinkToNiche.SetNiche(nicheType);
    }
}
