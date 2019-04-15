using UnityEngine;

public class BusinessButtonsController : View
{
    public GameObject ManageCompanyButton;
    public GameObject PromoteToGroupButton;
    public GameObject PivotButton;
    public GameObject LeaveCEOButton;
    public GameObject NominateAsCEO;

    private void OnEnable()
    {
        var c = SelectedCompany;

        bool isProductCompany = c.company.CompanyType == CompanyType.ProductCompany;
        bool isGroupCEOAlready = MyGroupEntity != null;

        // controlled company buttons

        // is independent company
        PromoteToGroupButton.SetActive(c.isControlledByPlayer && isProductCompany);

        PivotButton.SetActive(c.isControlledByPlayer && isProductCompany);

        LeaveCEOButton.SetActive(c.isControlledByPlayer);

        // not controlled
        NominateAsCEO.SetActive(!c.isControlledByPlayer && !isGroupCEOAlready);
    }
}
