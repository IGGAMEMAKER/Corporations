using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrganisationView : View
{
    public TextMeshProUGUI OrganisationValue;
    public TextMeshProUGUI OrganisationGrowth;

    public Transform loadingBar;
    public Transform textPercent;

    public override void ViewRender()
    {
        base.ViewRender();

        var growth = Random.Range(-5, 3);
        var value = Flagship.team.Organisation;

        var organisationChange = Format.Sign(growth) + " weekly";

        OrganisationValue.text = value + "";
        OrganisationGrowth.text = Visuals.DescribeValueWithText(growth, organisationChange, organisationChange, "---");

        loadingBar.GetComponent<Image>().fillAmount = value / 100f;
        //textPercent.GetComponent<TextMeshProUGUI>().text = ((int)value).ToString("F0");
    }
}
