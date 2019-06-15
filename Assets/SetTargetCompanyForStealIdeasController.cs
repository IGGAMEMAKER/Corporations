public class SetTargetCompanyForStealIdeasController : View
{
    public CooldownView CooldownView;
    public StealIdeasController StealIdeasController;

    public void SetTargetCompanyForStealing(GameEntity company)
    {
        bool isCanSteal = IsMyCompetitor(company) && company.product.ProductLevel > MyProductEntity.product.ProductLevel;

        CooldownView.gameObject.SetActive(isCanSteal);
        StealIdeasController.gameObject.SetActive(isCanSteal);

        CooldownView.SetTargetCompanyForStealing(company.company.Id);
        StealIdeasController.SetTargetCompanyForStealing(company.company.Id);
    }
}
