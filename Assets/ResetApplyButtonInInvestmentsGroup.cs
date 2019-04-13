using UnityEngine;
using UnityEngine.UI;

public class ResetApplyButtonInInvestmentsGroup : MonoBehaviour
{
    Slider slider;
    public RegroupMoneyBetweenBalanceAndInvestments ApplyButton;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(ApplyButton.ResetValue);
    }
}
