using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetInitialNiche : MonoBehaviour, IPointerClickHandler
{
    NicheType nicheType;
    GameObject TypeCorporationNameContainer, ChooseInitialNicheContainer;

    //public override void Execute()
    //{
    //    ScreenUtils.Navigate(GameContext, ScreenMode.NicheScreen, Constants.MENU_SELECTED_NICHE, nicheType);

    //}

    public void SetNiche(NicheType nicheType, GameObject TypeCorporationNameContainer, GameObject ChooseInitialNicheContainer)
    {
        this.nicheType = nicheType;
        this.TypeCorporationNameContainer = TypeCorporationNameContainer;
        this.ChooseInitialNicheContainer = ChooseInitialNicheContainer;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        TypeCorporationNameContainer.GetComponent<NewCampaignController>();
    }
}
