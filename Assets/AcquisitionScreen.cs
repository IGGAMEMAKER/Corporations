using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcquisitionScreen : View
{
    public Text Title;
    public Text ProposalStatus;

    public override void ViewRender()
    {
        base.ViewRender();

        Title.text = $"Acquisition of company {SelectedCompany.company.Name}";

        ProposalStatus.text = "???";
    }
}
