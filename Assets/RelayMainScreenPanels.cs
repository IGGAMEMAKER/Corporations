using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelayMainScreenPanels : View
{
    public GameObject InvestmentButton;
    public GameObject InvestmentPanel;

    public GameObject AudienceButton;
    public GameObject AudiencePanel;

    public void ChooseInvestments()
    {
        //Draw(InvestmentButton, false);
        Draw(AudiencePanel, false);

        //Draw(AudienceButton, true);
        Draw(InvestmentPanel, true);
    }

    public void ChooseAudience()
    {
        //Draw(InvestmentButton, true);
        Draw(AudiencePanel, true);

        //Draw(AudienceButton, false);
        Draw(InvestmentPanel, false);
    }

    void OnEnable()
    {
        ChooseAudience();
    }
}
