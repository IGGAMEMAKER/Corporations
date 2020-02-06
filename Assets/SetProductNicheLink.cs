using Assets.Core;
using UnityEngine.UI;

public class SetProductNicheLink : View
{
    public LinkToNiche Link;

    public override void ViewRender()
    {
        base.ViewRender();

        if (SelectedCompany.hasProduct)
        {
            var niche = SelectedCompany.product.Niche;
            Link.SetNiche(niche);
            Link.GetComponent<Text>().text = Enums.GetSingleFormattedNicheName(niche);
            Link.gameObject.SetActive(true);
        }
        else
        {
            Link.gameObject.SetActive(false);
        }
    }
}
