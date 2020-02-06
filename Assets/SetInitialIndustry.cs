using Assets.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetInitialIndustry : MonoBehaviour, IPointerClickHandler
{
    public IndustryType IndustryType;
    GameObject TypeCorporationNameContainer, ChooseInitialNicheContainer;

    public void SetIndustry(IndustryType industry, GameObject TypeCorporationNameContainer, GameObject ChooseInitialNicheContainer)
    {
        this.IndustryType = industry;
        this.TypeCorporationNameContainer = TypeCorporationNameContainer;
        this.ChooseInitialNicheContainer = ChooseInitialNicheContainer;

        GetComponentInChildren<Text>().text = Enums.GetFormattedIndustryName(IndustryType);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        TypeCorporationNameContainer.SetActive(true);
        TypeCorporationNameContainer.GetComponent<NewCampaignController>().SetIndustry(IndustryType);
        ChooseInitialNicheContainer.SetActive(false);
    }
}
