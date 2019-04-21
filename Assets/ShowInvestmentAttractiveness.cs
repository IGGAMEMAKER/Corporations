using Assets.Utils;
using UnityEngine.UI;

public class ShowInvestmentAttractiveness : View
{
    void Update()
    {
        Render();
    }

    private void OnEnable()
    {
        //Render();
    }

    void Render()
    {
        int offers = CompanyUtils.GetInvestmentAttractiveness(GameContext, SelectedCompany.company.Id);

        string text;
        string hint = CompanyUtils.GetInvestmentAttractivenessDescription(GameContext, SelectedCompany.company.Id);

        if (offers > 0)
        {
            text = $"We will receive {offers} proposals due to our progress";
        }
        else
        {
            text = "Noone wants to invest in this company(";
        }

        GetComponent<Text>().text = text;
        GetComponent<Hint>().SetHint(hint);
    }
}
