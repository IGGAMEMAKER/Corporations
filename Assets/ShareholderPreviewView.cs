using Entitas;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShareholderPreviewView : View
{
    int ShareholderId;
    int Shares;
    int TotalShares;

    public Text NameLabel;
    public Text SharesLabel;
    public Hint SharesAmountHint;

    GameEntity Shareholder;

    public void SetEntity(int shareholderId, int shares, int totalShares)
    {
        ShareholderId = shareholderId;
        Shares = shares;
        TotalShares = totalShares;

        Shareholder = Array.Find(GameContext.GetEntities(GameMatcher.Shareholder), s => s.shareholder.Id == shareholderId);

        Render();
    }

    void Render()
    {
        NameLabel.text = Shareholder.shareholder.Name;
        SharesLabel.text = Shares * 100 / TotalShares + "%";
        SharesAmountHint.SetHint("");
    }
}
