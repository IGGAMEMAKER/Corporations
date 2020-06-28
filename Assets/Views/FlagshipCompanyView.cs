using Assets.Core;
using UnityEngine;

public class FlagshipCompanyView : View
{
    [Header("Flagship Only")]
    public CanvasGroup FlagshipStuff;
    public GameObject CompetitionPreview;

    bool expand = true;

    public void OnEnable()
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

        RenderCompetitors();
    }

    void RenderCompetitors()
    {
        bool showCompetitors = !expand && Flagship.isRelease;

        Draw(CompetitionPreview, showCompetitors);
    }

    public void ToggleState()
    {
        expand = !expand;

        Render();
    }
}