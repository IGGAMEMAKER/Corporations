public enum TargetCompany
{
    Selected,
    Product,
    Group
}

public class SetTargetCompany : View
{
    public TargetCompany TargetCompany;
    public int companyId;

    void UpdateTargetCompany()
    {
        switch (TargetCompany)
        {
            case TargetCompany.Group:
                companyId = MyGroupEntity.company.Id;
                break;

            case TargetCompany.Product:
                companyId = MyProductEntity.company.Id;
                break;

            case TargetCompany.Selected:
                companyId = SelectedCompany.company.Id;
                break;
        }

        UpdateLinks();
    }

    void UpdateLinks()
    {
        var l = GetComponent<LinkToCompanyPreview>();

        if (l != null)
            l.CompanyId = companyId;

        var i = GetComponent<LinkToInvestmentTab>();

        if (i != null)
            i.CompanyId = companyId;

        //var n = GetComponent<LinkTo>();

        //if (n != null)
        //    n
    }

    void OnEnable()
    {
        UpdateTargetCompany();
    }

    void Update()
    {
        UpdateTargetCompany();
    }
}
