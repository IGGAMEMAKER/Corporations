using Assets.Core;

public class RenderCompanyRating : View
{
    private void OnEnable()
    {
        var company = SelectedCompany;

        int amountOfStars = Economy.GetCompanyRating(Q, company.company.Id);

        GetComponent<SetAmountOfStars>().SetStars(amountOfStars);
    }
}
