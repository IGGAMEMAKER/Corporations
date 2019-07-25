using Assets.Utils;
using UnityEngine;

public class BusinessButtonsController : View
{
    public GameObject PromoteToGroupButton;
    public GameObject PivotButton;
    [SerializeField] GameObject ManageCompanyButton;

    private void OnEnable()
    {
        var c = SelectedCompany;

        bool isProductCompany = c.company.CompanyType == CompanyType.ProductCompany;
        bool isGroupCEOAlready = MyGroupEntity != null;

        // controlled company buttons

        // is independent company
        PromoteToGroupButton.SetActive(c.isControlledByPlayer && isProductCompany);

        PivotButton.SetActive(c.isControlledByPlayer && isProductCompany);

        ManageCompanyButton.SetActive(CompanyUtils.IsDaughterOfCompany(MyGroupEntity, SelectedCompany) || SelectedCompany == MyCompany);

        //LeaveCEOButton.SetActive(c.isControlledByPlayer);

        //// not controlled
        //NominateAsCEO.SetActive(false && !c.isControlledByPlayer && !isGroupCEOAlready);
    }
}
