using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShareholdersOnMainScreenListView : ListView
{
    [Header("Buttons")]
    public GameObject SearchNewInvestors;
    public GameObject ChangeGoals;
    public GameObject GetExtraCash;
    public GameObject ShowOffers;
    public GameObject BuyBackFromSpecificInvestor;
    public GameObject BuyBack;

    List<GameObject> PlayerButtons => new List<GameObject> { SearchNewInvestors, GetExtraCash, ChangeGoals };
    List<GameObject> InvestorButtons => new List<GameObject> { BuyBackFromSpecificInvestor, ShowOffers };


    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<InvestorPreview>().SetEntity((int)(object)entity, MyCompany);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        SetItems(MyCompany.shareholders.Shareholders.Keys.ToArray());
    }

    private void OnEnable()
    {
        ShowOnly(gameObject, PlayerButtons);
        ShowOnly(gameObject, InvestorButtons);
    }

    public override void OnItemSelected(int chosenIndex)
    {
        base.OnItemSelected(chosenIndex);

        bool isPlayerSelected = chosenIndex == 0;
        bool isSpecificInvestorSelected = chosenIndex > 0;

        foreach (var b in PlayerButtons)
            Draw(b, isPlayerSelected);

        foreach (var b in InvestorButtons)
            Draw(b, isSpecificInvestorSelected);

        if (isSpecificInvestorSelected)
        {
            BuyBackFromSpecificInvestor.GetComponent<BuyBackFromShareholder>().ShareholderId = MyCompany.shareholders.Shareholders.Keys.ToArray()[chosenIndex];
        }
    }
}
