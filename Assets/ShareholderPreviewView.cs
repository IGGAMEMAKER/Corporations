using Entitas;
using System;
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
        var investorGroup = GameContext.GetEntities(GameMatcher.Shareholder);

        ShareholderId = shareholderId;
        Shares = shares;
        TotalShares = totalShares;

        Shareholder = Array.Find(investorGroup, s => s.shareholder.Id == shareholderId);

        Render();
    }

    void Render()
    {
        NameLabel.text = Shareholder.shareholder.Name;
        SharesLabel.text = Shares * 100 / TotalShares + "%";
        SharesAmountHint.SetHint("");
    }
}
