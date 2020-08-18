using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    public GameObject CurrentInvestments;

    public ProductUpgradeLinks MainInfo;

    List<GameObject> PlayerButtons => new List<GameObject> { SearchNewInvestors, GetExtraCash, ChangeGoals, MainInfo.gameObject };
    List<GameObject> InvestorButtons => new List<GameObject> { BuyBackFromSpecificInvestor, /*ShowOffers,*/ CurrentInvestments, MainInfo.gameObject };

    public GameObject CompanyActions;

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

    public override void OnDeselect()
    {
        base.OnDeselect();

        HideAll(PlayerButtons);
        HideAll(InvestorButtons);
    }

    public override void OnItemSelected(int chosenIndex)
    {
        base.OnItemSelected(chosenIndex);

        bool isPlayerSelected = chosenIndex == 0;

        foreach (var b in PlayerButtons)
            Draw(b, isPlayerSelected);

        ShowAll(InvestorButtons);

        var shareholderId = MyCompany.shareholders.Shareholders.Keys.ToArray()[chosenIndex];
        BuyBackFromSpecificInvestor.GetComponent<BuyBackFromShareholder>().ShareholderId = shareholderId;

        // active investments
        var investments = MyCompany.shareholders.Shareholders[shareholderId].Investments;
        var activeInvestments = investments.Where(i => i.RemainingPeriods > 0);
        bool hasInvestments = activeInvestments.Count() > 0;

        var investmentInfo = string.Join(
            "\n",
            activeInvestments
            .Select(i => $"Will give {Format.MinifyMoney(i.Portion)} for {i.RemainingPeriods} weeks")
            .ToArray()
            );

        CurrentInvestments.GetComponentInChildren<TextMeshProUGUI>().text = !hasInvestments ? "Is not paying investments" : investmentInfo;


        var shares = Companies.GetShareSize(Q, MyCompany.company.Id, shareholderId);
        var goal = "Get most users";

        string name = isPlayerSelected ? "YOU" : Companies.GetInvestorName(Q, shareholderId); // investor.shareholder.Name;


        MainInfo.Title.text = $"<b>{name}</b>\n{shares}% shares\n{goal}";
    }
}
