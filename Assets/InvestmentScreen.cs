using UnityEngine;

public class InvestmentScreen : View
{
    public GameObject DirectorsBoard;
    public GameObject InvestmentActions;

    void OnEnable()
    {
        ToggleVisibility(SelectedCompany == MyGroupEntity || SelectedCompany == MyProductEntity);
    }

    void ToggleVisibility(bool show)
    {
        DirectorsBoard.SetActive(show);
        InvestmentActions.SetActive(show);
    }
}
