using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("Set niche for SetInitialNiche");
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
        TypeCorporationNameContainer.GetComponent<NewCampaignController>().SetNiche(nicheType);
        ChooseInitialNicheContainer.SetActive(false);
        TypeCorporationNameContainer.SetActive(true);
    }
}
