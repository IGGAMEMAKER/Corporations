using UnityEngine;

public class InvestmentScreen : View
{
    public GameObject InvestmentActions;

    void OnEnable()
    {
        ToggleVisibility(SelectedCompany == MyGroupEntity);
    }

    void ToggleVisibility(bool show)
    {
        InvestmentActions.SetActive(show);
    }
}
