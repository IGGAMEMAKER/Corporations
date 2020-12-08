using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewCampaignController : View
{
    NicheType NicheType;
    IndustryType Industry;
    public StartCampaignButton StartCampaignButton;
    public InputField Input;

    //public void SetNiche(NicheType nicheType)
    //{
    //    NicheType = nicheType;

    //    StartCampaignButton.SetNiche(nicheType, Input);

    //    Input.Select();
    //    Input.ActivateInputField();
    //}

    //public void SetIndustry(IndustryType industry)
    //{
    //    Industry = industry;

    //    StartCampaignButton.SetIndustry(industry, Input);

    //    Input.Select();
    //    Input.ActivateInputField();
    //}

    public void OnCompanyNameChange()
    {
        StartCampaignButton.gameObject.SetActive(Input.text.Length > 0);
    }

    public void OnFormSubmit()
    {
        StartCampaignButton.Execute();
    }
}
