using UnityEngine.UI;

public class ShowInestmentRound : View
{
    void OnEnable()
    {
        Render();
    }

    void Render()
    {
        GetComponent<Text>().text = "Round: " + SelectedCompany.investmentRounds.InvestmentRound;
    }
}
