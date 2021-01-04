using UnityEngine;

public class CompetitionPanelView : View
{
    public GameObject ShowPositioningPanelButton;
    public GameObject ShowCompaniesButton;

    public GameObject CompaniesPanel;
    public GameObject DaughterCompaniesPanel;
    public GameObject PositioningPanel;


    private void OnEnable()
    {
        ShowCompanies();
    }

    public void ShowCompanies()
    {
        ToggleMode(false);
    }

    public void ShowPositioningPanel()
    {
        ToggleMode(true);
    }

    void ToggleMode(bool tg)
    {
        Draw(ShowCompaniesButton, tg);
        Draw(ShowPositioningPanelButton, !tg);

        // Draw(PositioningPanel, tg);
        Draw(CompaniesPanel, !tg);
        Draw(DaughterCompaniesPanel, !tg);
    }
}
