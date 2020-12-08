using Assets.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetInitialIndustry : MonoBehaviour
{
    public IndustryType IndustryType;
    GameObject TypeCorporationNameContainer, ChooseInitialNicheContainer;

    public void SetIndustry(IndustryType industry, GameObject TypeCorporationNameContainer, GameObject ChooseInitialNicheContainer)
    {
        this.IndustryType = industry;
        this.TypeCorporationNameContainer = TypeCorporationNameContainer;
        this.ChooseInitialNicheContainer = ChooseInitialNicheContainer;

        GetComponentInChildren<TextMeshProUGUI>().text = Enums.GetFormattedIndustryName(IndustryType);
    }

    public void ChooseIndustry()
    {
        TypeCorporationNameContainer.SetActive(true);
        //TypeCorporationNameContainer.GetComponent<NewCampaignController>().SetIndustry(IndustryType);
        ChooseInitialNicheContainer.SetActive(false);
    }
}
