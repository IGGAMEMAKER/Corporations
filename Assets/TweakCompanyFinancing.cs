using Assets.Core;
using UnityEngine.UI;

public class TweakCompanyFinancing : View
{
    public InvestInControllableCompany ApplyButton;
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

        var c = Companies.Get(GameContext, MyGroupEntity.company.Id);

        var total = c.companyResource.Resources.money;

        var investments = total * percent / 100;

        Hint.SetHint($"We will invest {Format.Money(investments)}\nOur share size will increase");
    }
}
