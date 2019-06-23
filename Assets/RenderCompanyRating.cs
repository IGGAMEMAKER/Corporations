using Assets.Utils;

public class RenderCompanyRating : View
{
    private void OnEnable()
    {
        var company = SelectedCompany;

        int amountOfStars = CompanyEconomyUtils.GetCompanyRating(GameContext, company.company.Id);

        GetComponent<SetAmountOfStars>().SetStars(amountOfStars);
    }
}
