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
                companyId = MyGroupEntity != null ? MyGroupEntity.company.Id : 0;
                break;

            case TargetCompany.Product:
                if (HasProductCompany)
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
        var l = GetComponent<LinkToProjectView>();

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
}
