using UnityEngine.UI;

public class NewCampaignController : View
{
    NicheType NicheType;
    IndustryType Industry;
    public StartCampaignButton StartCampaignButton;
    public InputField Input;

    private void Start()
    {
        ActivateInputField();
    }

    //public void SetNiche(NicheType nicheType)
    //{
    //    NicheType = nicheType;

    //    StartCampaignButton.SetNiche(nicheType, Input);

    //    ActivateInputField();
    //}

    //public void SetIndustry(IndustryType industry)
    //{
    //    Industry = industry;

    //    StartCampaignButton.SetIndustry(industry, Input);

    //    ActivateInputField();
    //}

    void ActivateInputField()
    {
        Input.Select();
        Input.ActivateInputField();
    }

    public void OnCompanyNameChange()
    {
        StartCampaignButton.gameObject.SetActive(Input.text.Length > 0);
    }

    public void OnFormSubmit()
    {
        StartCampaignButton.Execute();
    }
}
