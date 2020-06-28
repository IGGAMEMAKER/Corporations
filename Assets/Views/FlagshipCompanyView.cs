using Assets.Core;
using UnityEngine;

public class FlagshipCompanyView : View
{
    [Header("Flagship Only")]
    public CanvasGroup competitorListView;

    public GameObject FirmLogo;

    public CanvasGroup FlagshipStuff;
    public GameObject CompetitionPreview;

    bool expand = true;

    public void SetEntity()
    {
        expand = true;

        Render();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void Render()
    {
        DrawCanvasGroup(FlagshipStuff, expand);

        ResizeFirmLogo();
        RenderCompetitors();
    }

    void RenderCompetitors()
    {
        bool showCompetitors = !expand && Flagship.isRelease;

        Draw(CompetitionPreview, showCompetitors);
        //if (competitorListView != null)
        //{
        //    DrawCanvasGroup(competitorListView, showCompetitors);
        //    Draw(competitorListView.gameObject, showCompetitors);
        //}
    }

    public void ToggleState()
    {
        expand = !expand;

        Render();
    }

    void ResizeFirmLogo()
    {
        return;
        var scale = 1f;

        var company = Flagship;

        bool isGlobalMode = !expand;
        if (isGlobalMode)
        {
            var marketShare = Companies.GetMarketShareOfCompanyMultipliedByHundred(company, Q);

            // share = 0
            var minSize = 0.85f;

            // share = 100
            var maxSize = 2.5f;

            scale = minSize + (maxSize - minSize) * marketShare / 100;
        }

        transform.localScale = new Vector3(scale, scale, scale);
    }
}