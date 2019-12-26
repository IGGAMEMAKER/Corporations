using Assets.Core;
using Assets.Core.Formatting;
using UnityEngine;
using UnityEngine.UI;

public class RenderNicheInfoInProjectScreen : View
{
    public Text NicheName;
    public GameObject NicheRoot;
    public LinkToNiche LinkToNiche;

    public override void ViewRender()
    {
        base.ViewRender();

        //ToggleNicheObjects(CanRenderNiche);

        //if (CanRenderNiche)
        //    RenderLinkToNiche();
    }

    //bool CanRenderNiche
    //{
    //    get
    //    {
    //        return SelectedCompany.hasProduct;
    //    }
    //}

    ////void ToggleNicheObjects(bool show)
    ////{
    ////    NicheRoot.SetActive(show);
    ////}

    //private void RenderLinkToNiche()
    //{
    //    NicheType niche = SelectedCompany.product.Niche;

    //    string text = EnumUtils.GetFormattedNicheName(niche);

    //    NicheName.text = Visuals.Link(text);

    //    LinkToNiche.SetNiche(niche);
    //}
}
