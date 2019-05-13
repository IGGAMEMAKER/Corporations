using UnityEngine;

public class InvestmentScreen : View
{
    public GameObject DirectorsBoard;
    public GameObject InvestmentActions;

    void OnEnable()
    {
        ToggleVisibility(SelectedCompany == MyProductEntity || SelectedCompany == MyGroupEntity);
    }

    void ToggleVisibility(bool show)
    {
        DirectorsBoard.SetActive(show);
        InvestmentActions.SetActive(show);
    }
}
