public partial class AIProductSystems : OnDateChange
{
    void GrowCompanyCost(GameEntity company)
    {
        if (company.hasProduct)
        {
            ManageProductCompany(company);

            return;
        }
    }
}
