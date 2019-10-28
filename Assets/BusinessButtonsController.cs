using Assets.Utils;
using UnityEngine;

public class BusinessButtonsController : View
{
    public GameObject PivotButton;
    [SerializeField] GameObject ManageCompanyButton;
    [SerializeField] GameObject CloseCompanyButton;
    [SerializeField] GameObject SellCompanyButton;

    private void OnEnable()
    {
        var c = SelectedCompany;

        bool isProductCompany = c.company.CompanyType == CompanyType.ProductCompany;

        // controlled company buttons

        // is independent company

        PivotButton.SetActive(c.isControlledByPlayer && isProductCompany);

        var manageable = CompanyUtils.IsCompanyRelatedToPlayer(GameContext, SelectedCompany); // CompanyUtils.IsDaughterOfCompany(MyGroupEntity, SelectedCompany) || SelectedCompany == MyCompany;
        ManageCompanyButton.SetActive(manageable);
        SellCompanyButton.SetActive(manageable);
        CloseCompanyButton.SetActive(manageable);

        //LeaveCEOButton.SetActive(c.isControlledByPlayer);

        //// not controlled
        //NominateAsCEO.SetActive(false && !c.isControlledByPlayer && !isGroupCEOAlready);
    }
}
