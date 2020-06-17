using Assets.Core;
using UnityEngine;

public class FlagshipCompanyView : View
{
    [Header("Flagship Only")]
    public CanvasGroup competitorListView;

    public CanvasGroup Workers;
    public RenderCompanyWorkerListView workerListView;

    public GameObject FirmLogo;

    public CanvasGroup FlagshipStuff;

    bool expand = false;

    public void SetEntity()
    {
        workerListView.SetEntity(Flagship);

        expand = false;

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
        RenderWorkersAndCompetitors();
    }

    void RenderWorkersAndCompetitors()
    {
        //DrawCanvasGroup(Workers, expand);

        bool showCompetitors = !expand && Flagship.isRelease;

        if (competitorListView != null)
            Draw(competitorListView.gameObject, showCompetitors);
    }

    public void ToggleState()
    {
        expand = !expand;


        Render();
    }

    void ResizeFirmLogo()
    {
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