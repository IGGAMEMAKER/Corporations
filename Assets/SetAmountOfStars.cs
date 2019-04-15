using Assets.Utils;
using UnityEngine;

public class SetAmountOfStars : View
{
    public GameEntity company;

    public void SetEntity(GameEntity companyEntity)
    {
        company = companyEntity;

        Render();
    }

    void Render()
    {
        int amountOfStars = CompanyEconomyUtils.GetCompanyRating(company.company.Id);

        foreach (Transform child in transform)
            child.gameObject.SetActive(child.GetSiblingIndex() < amountOfStars);
    }

    private void OnEnable()
    {
        company = SelectedCompany;

        Render();
    }
}
