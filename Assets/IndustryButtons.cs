using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndustryButtons : View
{
    public GameObject TypeCorporationNameContainer, ChooseInitialNicheContainer;
    public SetInitialIndustry useful;
    public SetInitialIndustry ecommerce;
    public SetInitialIndustry socialMedia;
    public SetInitialIndustry technology;

    // Start is called before the first frame update
    void Start()
    {
        useful.SetIndustry(IndustryType.Tourism, TypeCorporationNameContainer, ChooseInitialNicheContainer);
        ecommerce.SetIndustry(IndustryType.Finances, TypeCorporationNameContainer, ChooseInitialNicheContainer);
        socialMedia.SetIndustry(IndustryType.Communications, TypeCorporationNameContainer, ChooseInitialNicheContainer);
        technology.SetIndustry(IndustryType.Technology, TypeCorporationNameContainer, ChooseInitialNicheContainer);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F3))
        {
            ScreenUtils.StartNewCampaign(Q, NicheType.ECom_Exchanging, "QWE");

            //var p = Companies.CreateProduct(Q, MyCompany, NicheType.ECom_Exchanging);

            //ScreenUtils.Navigate(Q, ScreenMode.ProjectScreen, Balance.MENU_SELECTED_COMPANY, p.company.Id);
        }
    }
}
