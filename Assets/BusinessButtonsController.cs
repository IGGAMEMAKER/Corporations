using Assets.Utils;
using UnityEngine;

public class BusinessButtonsController : View
{
    public GameObject PromoteToGroupButton;
    public GameObject PivotButton;
    [SerializeField] GameObject ManageCompanyButton;
    [SerializeField] GameObject CloseCompanyButton;
    [SerializeField] GameObject SellCompanyButton;

    private void OnEnable()
    {
        var c = SelectedCompany;

        bool isProductCompany = c.company.CompanyType == CompanyType.ProductCompany;
        bool isGroupCEOAlready = MyGroupEntity != null;

        // controlled company buttons

        // is independent company
        PromoteToGroupButton.SetActive(c.isControlledByPlayer && isProductCompany);

        PivotButton.SetActive(c.isControlledByPlayer && isProductCompany);

        var manageable = CompanyUtils.IsDaughterOfCompany(MyGroupEntity, SelectedCompany) || SelectedCompany == MyCompany;
        ManageCompanyButton.SetActive(manageable);
        SellCompanyButton.SetActive(manageable);
        CloseCompanyButton.SetActive(manageable);

        //LeaveCEOButton.SetActive(c.isControlledByPlayer);

        //// not controlled
        //NominateAsCEO.SetActive(false && !c.isControlledByPlayer && !isGroupCEOAlready);
    }
}
