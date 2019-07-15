using Assets.Utils;
using UnityEngine.UI;

public class AcquisitionButtonView : View
{
    public void SetAcquisitionBid(long offer, bool willSell)
    {
        bool enoughMoney = MyCompany.companyResource.Resources.money >= offer;
        GetComponent<Button>().interactable = willSell && enoughMoney;


        string text = "";

        if (willSell)
            text = Visuals.Positive("They will sell this company! Hurry up, they will not wait for ages!");
        else
            text = Visuals.Negative("They will not sell this company! We need 75% of votes to safely acquire company");
            //text = $"We only have {Format.Money(Balance)}";

        GetComponent<Hint>().SetHint(text);
    }
}
