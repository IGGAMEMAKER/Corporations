using Assets.Utils;
using UnityEngine.UI;

public class MonthlyIncomeView : View
    , IAnyDateListener
{
    Text Text;

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }

    void Start()
    {
        Text = GetComponent<Text>();

        ListenDateChanges(this);
    }

    void OnEnable()
    {
        Render();
    }

    void Render()
    {
        // TODO Update
        Text.text = ValueFormatter.ShortenValueMockup(CompanyEconomyUtils.GetCompanyIncome(MyProductEntity, GameContext)) + "$";
    }
}
