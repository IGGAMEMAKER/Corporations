using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine.UI;

public class SetLinkToNiche : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var niche = MyProductEntity.product.Niche;

        GetComponent<LinkToNiche>().SetNiche(niche);
        GetComponent<Text>().text = Visuals.Link(EnumUtils.GetFormattedNicheName(niche));
    }
}
