using UnityEngine;
using UnityEngine.EventSystems;

public class SetInitialNiche : MonoBehaviour, IPointerClickHandler
{
    NicheType nicheType;
    GameObject TypeCorporationNameContainer, ChooseInitialNicheContainer;

    public void SetNiche(NicheType nicheType, GameObject TypeCorporationNameContainer, GameObject ChooseInitialNicheContainer)
    {
        this.nicheType = nicheType;
        this.TypeCorporationNameContainer = TypeCorporationNameContainer;
        this.ChooseInitialNicheContainer = ChooseInitialNicheContainer;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        TypeCorporationNameContainer.SetActive(true);
        TypeCorporationNameContainer.GetComponent<NewCampaignController>().SetNiche(nicheType);
        ChooseInitialNicheContainer.SetActive(false);
    }
}
