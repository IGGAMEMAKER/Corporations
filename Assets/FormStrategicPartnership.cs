using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormStrategicPartnership : ToggleButtonController
{
    public override void Execute()
    {
        CompanyUtils.SendStrategicPartnershipRequest(MyCompany, SelectedCompany);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        bool isPartnersAlready = CompanyUtils.IsHaveStrategicPartnership(MyCompany, SelectedCompany);
        bool isCanBePartnersTheoretically = CompanyUtils.IsCanBePartnersTheoretically(MyCompany, SelectedCompany);

        GetComponent<Button>().interactable = !isPartnersAlready && isCanBePartnersTheoretically;
    }
}
