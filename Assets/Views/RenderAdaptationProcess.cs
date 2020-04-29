using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderAdaptationProcess : View
{
    public ProgressBar ProgressBar;
    public Text AdaptationLabel;

    public override void ViewRender()
    {
        base.ViewRender();

        var human = SelectedHuman;

        bool hasRelationshipsWithCompany = human.hasHumanCompanyRelationship;
        bool isAdapted = hasRelationshipsWithCompany && human.humanCompanyRelationship.Adapted == 100;

        ProgressBar.gameObject.SetActive(hasRelationshipsWithCompany && !isAdapted);
        AdaptationLabel.gameObject.SetActive(hasRelationshipsWithCompany);

        if (hasRelationshipsWithCompany)
        {
            ProgressBar.SetValue(human.humanCompanyRelationship.Adapted);
            AdaptationLabel.text = isAdapted ? "Is fully adapted to company!" : "Adapting to company...";
        }
    }
}
