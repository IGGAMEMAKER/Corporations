using Assets.Utils;
using UnityEngine.UI;

public class RegroupMoneyBetweenBalanceAndInvestments : ButtonController
{
    public int Percent;

    public override void Execute()
    {
        CompanyEconomyUtils.RestructureFinances(GameContext, Percent, MyGroupEntity.company.Id);
    }

    public void ResetValue(float value)
    {
        Percent = (int)(value * 100);

        UpdateInteractibility();
    }

    void UpdateInteractibility()
    {
        GetComponent<Button>().interactable = Percent >= 0;
    }
}
