public partial class AIProductSystems : OnDateChange
{
    void GrowCompanyCost(GameEntity company)
    {
        switch (company.company.CompanyType)
        {
            case CompanyType.ProductCompany:
                ManageProductCompany(company);
                break;
        }
    }
}
