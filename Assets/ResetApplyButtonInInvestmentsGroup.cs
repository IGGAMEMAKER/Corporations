using Assets.Utils;
using UnityEngine.UI;

public class ResetApplyButtonInInvestmentsGroup : View
{
    public RegroupMoneyBetweenBalanceAndInvestments ApplyButton;
    Slider slider;
    Hint Hint;

    void Start()
    {
        Hint = GetComponent<Hint>();

        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(ApplyButton.ResetValue);
        slider.onValueChanged.AddListener(SetHint);
    }

    void SetHint(float value)
    {
        int percent = (int)(value * 100);

        var c = CompanyUtils.GetCompanyById(GameContext, MyGroupEntity.company.Id);

        var balance = c.companyResource.Resources.money;
        var investments = c.shareholder.Money;

        var total = balance + investments;

        investments = total * percent / 100;
        balance = total - investments;

        Hint.SetHint($"Money will be redistributed\n\n Balance: " +
            $"${ValueFormatter.Shorten(balance)}" +
            $"\nInvestments: ${ValueFormatter.Shorten(investments)}");
    }
}
