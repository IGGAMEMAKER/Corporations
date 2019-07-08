using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine.UI;

public class MarketShareView : View
{
    public Text ShareSize;
    public Text ShareChange;
    public Text NicheName;

    internal void SetEntity(NicheType entity)
    {
        ShareSize.text = "34%";
        ShareChange.text = Visuals.Positive("+13%");

        NicheName.text = EnumUtils.GetFormattedNicheName(entity);
    }
}
