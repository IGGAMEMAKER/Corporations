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

    public BuyBackFromShareholder BuyBackFromShareholder;

    public ProductUpgradeLinks MainInfo;

    List<GameObject> PlayerButtons => new List<GameObject> { SearchNewInvestors, GetExtraCash, MainInfo.gameObject };
    List<GameObject> InvestorButtons => new List<GameObject> { BuyBackFromSpecificInvestor, /*ShowOffers,*/ CurrentInvestments, MainInfo.gameObject };

    public GameObject InvestmentsContextMenu;

    bool isPlayerSelected = false;
    int shareholderId = 0;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<InvestorPreview>().SetEntity((int)(object)entity, MyCompany);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        SetItems(MyCompany.shareholders.Shareholders.Keys.ToArray());
    }

    public override void OnDeselect()
    {
        base.OnDeselect();

        HideButtons();

        var a = FindObjectOfType<MainPanelRelay>();
        if (a != null)
            a.ShowDefaultMode();
    }

    public void HideButtons()
    {
        HideAll(PlayerButtons);
        HideAll(InvestorButtons);

        Hide(InvestmentsContextMenu);
    }

    public override void OnItemSelected(int chosenIndex)
    {
        base.OnItemSelected(chosenIndex);

        Show(InvestmentsContextMenu);

        isPlayerSelected = chosenIndex == 0;

        if (isPlayerSelected)
        {
            HideAll(InvestorButtons);
            ShowAll(PlayerButtons);

            // there is noone to ask money for (there is only a player)
            if (MyCompany.shareholders.Shareholders.Keys.Count == 1)
            {
                Hide(GetExtraCash);
            }
        }
        else
        {
            HideAll(PlayerButtons);
            ShowAll(InvestorButtons);

            BuyBackFromShareholder.ViewRender();
        }


        shareholderId = MyCompany.shareholders.Shareholders.Keys.ToArray()[chosenIndex];

        RenderShareholderData();
    }

    public void RenderShareholderData()
    {
        var shares = Companies.GetShareSize(Q, MyCompany, shareholderId);
        var goal = "Goal: ???";

        string name = isPlayerSelected ? "YOU" : Companies.GetInvestorName(Q, shareholderId); // investor.shareholder.Name;

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


        MainInfo.Title.text = $"<b>{name}</b>\n{Visuals.Colorize(shares)}% shares\n{goal}";

        BuyBackFromSpecificInvestor.GetComponent<BuyBackFromShareholder>().ShareholderId = shareholderId;

        CurrentInvestments.GetComponentInChildren<TextMeshProUGUI>().text = !hasInvestments ? "Is not paying investments" : investmentInfo;
    }
}
